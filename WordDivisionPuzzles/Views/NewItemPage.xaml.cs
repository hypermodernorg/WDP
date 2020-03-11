using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WordDivisionPuzzles.Models;
using Xamarin.Forms;

namespace WordDivisionPuzzles.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class NewItemPage : ContentPage
    {

        public Item Item { get; set; }
        public WDPItem wdpItem { get; set; }
        static ArrayList letters = new ArrayList();
        CommonTasks commonMethods = new CommonTasks();

        public NewItemPage()
        {
            InitializeComponent();

            int iDivisor = commonMethods.GetRandom(500, 1000); // Class from the CommonTasks.cs file.
            int iQuotient = commonMethods.GetRandom(1000, 99999); // Class from the CommonTasks.cs file.
            int iDividend = iDivisor * iQuotient;
            string divisor = iDivisor.ToString();
            string quotient = iQuotient.ToString();
            string dividend = iDividend.ToString();
            int iDivisorLength = divisor.Length;
            int iQuotientLength = quotient.Length;
            int iDividendLength = dividend.Length;
            int iTotalLength = iDivisorLength + iDividendLength + 1;

           
            letters = MakeLetters();
            StoreItem(iDivisor, iQuotient);
        }

        public ArrayList MakeLetters()
        {
            Random random = new Random();
            ArrayList letters = new ArrayList();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < 10; i++)
            {
                int iRandom = random.Next(0, 25 - i);
                letters.Add(alphabet.Substring(iRandom, 1));
                alphabet = alphabet.Remove(iRandom, 1);
            }

            return letters;
        }

        public async void StoreItem(int iDivisor, int iQuotient)
        {
            string alphaDivisor = "";
            string alphaQuotient = "";
            for (int i = 0; i < iDivisor.ToString().Length; i++)
            {
                // (string) letters[int.Parse(quotient.Substring(j, 1))]
                alphaDivisor += (string)letters[int.Parse(iDivisor.ToString().Substring(i, 1))];
            }

            for (int i = 0; i < iQuotient.ToString().Length; i++)
            {
                alphaQuotient += (string)letters[int.Parse(iQuotient.ToString().Substring(i, 1))];
            }

            Item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Quotient = iQuotient.ToString(),
                Divisor = iDivisor.ToString(),
                Letters = letters,
                AlphaQuotient = alphaQuotient,
                AlphaDivisor = alphaDivisor
            };
            BindingContext = this;

            var strings = letters.Cast<string>().ToArray();

            var lettersString = string.Join(" ", strings);

            wdpItem = new WDPItem
            {
                Id = Guid.NewGuid().ToString(),
                Quotient = iQuotient.ToString(),
                Divisor = iDivisor.ToString(),
                Letters = lettersString,
                AlphaQuotient = alphaQuotient,
                AlphaDivisor = alphaDivisor
            };
            WDPDB wdpdb = new WDPDB();
            await wdpdb.SaveItemAsync(wdpItem);
            await Navigation.PopToRootAsync();
        }
    }
}