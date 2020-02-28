using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordDivisionPuzzles.Models;

namespace WordDivisionPuzzles.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        //readonly List<Item> items;
        public List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
              //new Item { Id = System.Guid.NewGuid().ToString(), Quotient = "49832", Divisor="231" },

            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {

            // The string is getting passed. At least, A string is getting passed. Is it the correct string?
            // Memory address? If the correct string, why isnt the below finding the associated item?
            //var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            Item oldItem = new Item();

            // the id strings arent matching. Why?
            foreach (var item in items)
            {
                string idFromItem = item.Id;
                string idFromPassed = id;
           

                if (item.Id == id)
                {
                    oldItem = item;
                }
            }

            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}