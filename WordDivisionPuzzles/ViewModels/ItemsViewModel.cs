using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WordDivisionPuzzles.Models;
using WordDivisionPuzzles.Views;

using Xamarin.Forms;

namespace WordDivisionPuzzles.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Puzzles";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();


                // var items = await DataStore.GetItemsAsync(true);

                var getItems = new WDPDB();
                var items = await getItems.GetItemsAsync();


                //WDPDB wdpdb = new WDPDB(); //new
                //var items = await wdpdb.GetItemsAsync(); //new
                foreach (var item in items)
                {

                    // need to transform WPDDB item to Model item


                    var newItem = new Item();
                    newItem.Id = item.Id;
                    newItem.AlphaDivisor = item.AlphaDivisor;
                    newItem.AlphaQuotient = item.AlphaQuotient;
                    newItem.Quotient = item.Quotient;
                    newItem.Divisor = item.Divisor;
                    newItem.Letters = new ArrayList(item.Letters.Split(' '));

                    Items.Add(newItem);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}