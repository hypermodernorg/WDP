using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordDivisionPuzzles
{
    public static class Constants
    {
        public const string DatabaseFilename = "wpddbv0-2.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }

    public class WDPDB
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public WDPDB()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(WDPItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(WDPItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<WDPItem>> GetItemsAsync()
        {
            return Database.Table<WDPItem>().ToListAsync();
        }

        public Task<List<WDPItem>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<WDPItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<WDPItem> GetItemAsync(string id)
        {
            return Database.Table<WDPItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(WDPItem item)
        {
            //if (item.Id != "")
            //{
            //    return Database.UpdateAsync(item);
            //}
            //else

            // UpdateAsync may be needed later to mark puzzles as solved.
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(WDPItem item)
        {
            return Database.DeleteAsync(item);
        }
    }

    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }

    public class WDPItem
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Quotient { get; set; }
        public string Divisor { get; set; }
        public string AlphaDivisor { get; set; }
        public string AlphaQuotient { get; set; }
        public string Letters { get; set; }
        public bool Solved { get; set; }
    }
}
