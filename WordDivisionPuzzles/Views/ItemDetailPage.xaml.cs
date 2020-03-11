using System;
using System.Collections;
using System.ComponentModel;
using WordDivisionPuzzles.Models;
using WordDivisionPuzzles.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordDivisionPuzzles.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]


    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public Item Item { get; set; }
        static int columnWidth = 20;
        public WDPItem wdpItem { get; set; }
        static ArrayList letters = new ArrayList();
        CommonTasks commonMethods = new CommonTasks();

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Quotient = "Item 1",
                Divisor = "This is an item description.",

            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            var strDivisor = viewModel.Item.Divisor; // Get the divisor from the model.
            var strQuotient = viewModel.Item.Quotient; // Get the quotient from the model.


            Random random = new Random();
            ToolbarItem toolBarItem = (ToolbarItem)Content.FindByName("NewToolBar");
            toolBarItem.CommandParameter = viewModel.Item.Id;

            int iDivisor = int.Parse(viewModel.Item.Divisor);
            int iQuotient = int.Parse(viewModel.Item.Quotient);

            int iDividend = iDivisor * iQuotient;
            string divisor = iDivisor.ToString();
            string quotient = iQuotient.ToString();
            string dividend = iDividend.ToString();
            int iDivisorLength = divisor.Length;
            int iQuotientLength = quotient.Length;
            int iDividendLength = dividend.Length;
            int iTotalLength = iDivisorLength + iDividendLength + 1;


            Grid grid = commonMethods.ShapeGrid(iTotalLength, iDivisorLength); // Create the grid size.
            BindingContext = this;
            var letters = viewModel.Item.Letters;

            var strings = letters.Cast<string>().ToArray();

            var lettersString = string.Join(" ", strings);
            var lettersString1 = string.Join("", strings);

            //LettersButton1.CommandParameter = lettersString;
            LettersButton1.CommandParameter = viewModel.Item.Id;
            if (viewModel.Item.Solved == 1)
            {
                LettersButton1.BackgroundColor = Color.Black;
                LettersButton1.Text = "Solved";
                LettersButton1.TextColor = Color.Beige;
                LettersButton1.IsEnabled = false;
                e0.Text = letters[0].ToString();
                e1.Text = letters[1].ToString();
                e2.Text = letters[2].ToString();
                e3.Text = letters[3].ToString();
                e4.Text = letters[4].ToString();
                e5.Text = letters[5].ToString();
                e6.Text = letters[6].ToString();
                e7.Text = letters[7].ToString();
                e8.Text = letters[8].ToString();
                e9.Text = letters[9].ToString();
                SolvedLabel.IsVisible = true;
            }

            grid = FirstThreeRows(iTotalLength, divisor, quotient, dividend, grid, letters);
            grid = LastLines(iTotalLength, divisor, quotient, dividend, grid, letters);

            Grid containerGrid = (Grid)Content.FindByName("NewGrid1");
            Grid containerGrid2 = (Grid)Content.FindByName("NewGrid2");


            Grid answerGrid = commonMethods.AnswerGrid();
            //containerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            containerGrid.Children.Add(grid, 0, 0);
            //containerGrid2.Children.Add(answerGrid, 0, 0);

            StackLayout01.Children.Add(containerGrid);
            //StackLayout02.Children.Add(containerGrid2);

            StackLayout0.Children.Add(StackLayout01);
            //StackLayout0.Children.Add(StackLayout02);

            this.Content = StackLayout0;//containerGrid; // set the content
            //this.Content = containerGrid; // set the content

        
  
        }
        // The first three lines, including the horizontal border, of the long division problem.
        public Grid FirstThreeRows(int iTotalLength, string divisor, string quotient, string dividend, Grid grid, ArrayList letters)
        {
            int iDivisorLength = divisor.Length;
            int iQuotientLength = quotient.Length;
            int iDividendLength = dividend.Length;


            // First & Second Row: The Quotient
            int j = 0;

            for (int i = 1; i < iTotalLength; i++) // loop through the total number of columns.
            {

                if (i < (iTotalLength - iQuotientLength))
                {
                    if (i == iDivisorLength) // if this, then print the vertical border
                    {
                        grid.Children.Add(commonMethods.BvBorderVertical(), i, 4); // column, row   
                    }
                    if (i > iDivisorLength) // if this, then print the vertical border
                    {
                        grid.Children.Add(commonMethods.BvBorderHorizontal(), i, 3); // column, row   
                    }

                    if (i < iDivisorLength) // else, print empty spaces
                    {
                        grid.Children.Add(new Label
                        {
                            Text = "",
                            FontSize = 24,
                            WidthRequest = columnWidth
                        }
                            , i, 2);
                    }

                }
                if (i >= (iTotalLength - iQuotientLength)) // if spaces are accounted for, print the quotient and the horizontal border.
                {

                    grid.Children.Add(new Label
                    {
                        Text = (string)letters[int.Parse(quotient.Substring(j, 1))],
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 2);

                    grid.Children.Add(commonMethods.BvBorderHorizontal(), i, 3); // column, row   -- horizontal border

                    j++;
                }
            } // End First & Second Row

            // Third Rows: The Divisor and Dividend
            j = 0;
            grid.Children.Add(commonMethods.BvBorderCorner(), iDivisorLength, 3);
            for (int i = 0; i < iTotalLength; i++)
            {
                if (i < iDivisorLength)
                {
                    grid.Children.Add(new Label
                    {
                        Text = (string)letters[int.Parse(divisor.Substring(i, 1))],// divisor.Substring(i, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 4);
                }

                if (i > iDivisorLength)
                {
                    grid.Children.Add(new Label
                    {
                        Text = (string)letters[int.Parse(dividend.Substring(j, 1))], // dividend.Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 4);
                    j++;
                }
            } // Third Rows

            return grid;
        }

        // The last lines of the long division problem.
        public Grid LastLines(int iTotalLength, string divisor, string quotient, string dividend, Grid grid, ArrayList letters)
        {
            int iCol = divisor.Length + 1;
            int iRow = 6;
            int iDivideInto = 0;
            int iDivisor = int.Parse(divisor);
            bool isFirstPass = true;
            int iProduct = 0;



            for (int i = divisor.Length; i < dividend.Length; i++)
            {

                // If this is the first pass, create iDivideInto from a substring of dividend.
                if (isFirstPass)
                {
                    if (int.Parse(dividend.Substring(0, divisor.Length)) >= int.Parse(divisor))
                    {
                        iDivideInto = int.Parse(dividend.Substring(0, divisor.Length));
                    }
                    else
                    {
                        iDivideInto = int.Parse(dividend.Substring(0, divisor.Length + 1));
                        //iCol++;
                        i++;
                    }
                    isFirstPass = false;
                }



                int iQuotient = iDivideInto / iDivisor;
                iProduct = iQuotient * iDivisor;

                // Print the Product, Subtraction Sign, and the Border
                if (iProduct.ToString().Length < iDivideInto.ToString().Length)
                {
                    iCol++;
                }

                bool isSubtractFirstPass = true;


                for (int j = 0; j < iProduct.ToString().Length; j++)
                {
                    //Product
                    grid.Children.Add(new Label
                    {
                        Text = (string)letters[int.Parse(iProduct.ToString().Substring(j, 1))],// iProduct.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                    , iCol + j, iRow);

                    //Subtraction sign
                    if (isSubtractFirstPass)
                    {
                        grid.Children.Add(new Label
                        {
                            Text = "-",
                            FontSize = 24,
                            HorizontalTextAlignment = TextAlignment.Center,
                            WidthRequest = 4,
                            TextColor = Color.White
                        }
                        , iCol + j - 1, iRow);

                        isSubtractFirstPass = false;
                    }

                    //Border
                    grid.Children.Add(commonMethods.BvBorderHorizontal(), iCol + j, iRow + 1); // column, row   -- horizontal border
                }
                iRow += 2;
                // End Print the Product, Subtraction Sign, and the Border

                // Get the difference between iDivideInto and iProduct
                int iRemainder = iDivideInto - iProduct;

                // Now, lets count how many columns to adjust
                int iColAdjust = iProduct.ToString().Length - iRemainder.ToString().Length;
                iCol += iColAdjust;



                // Next, determine how many characters of dividend to bring down

                int k = 0;
                while (iRemainder < iDivisor)
                {
                    k++;
                    iRemainder = int.Parse(iRemainder.ToString() + dividend.Substring(i, k));

                }
                iDivideInto = iRemainder;
                for (int j = 0; j < iDivideInto.ToString().Length; j++)
                {
                    grid.Children.Add(new Label
                    {
                        Text = (string)letters[int.Parse(iDivideInto.ToString().Substring(j, 1))], // iDivideInto.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                    , iCol + j, iRow);
                }
                iRow++;
            }


            iRow++;
            bool isSubtractFirstPass2 = true;
            int iLastZeroPosition = 0;
            for (int j = 0; j < iDivideInto.ToString().Length; j++)
            {
                //Product
                grid.Children.Add(new Label
                {
                    Text = (string)letters[int.Parse(iDivideInto.ToString().Substring(j, 1))],// iDivideInto.ToString().Substring(j, 1),
                    FontSize = 24,
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = columnWidth,
                    TextColor = Color.White
                }
                , iCol + j, iRow);

                //Subtraction sign
                if (isSubtractFirstPass2)
                {
                    grid.Children.Add(new Label
                    {
                        Text = "-",
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = 4,
                        TextColor = Color.White
                    }
                    , iCol + j - 1, iRow);

                    isSubtractFirstPass2 = false;
                }


                //Border
                grid.Children.Add(commonMethods.BvBorderHorizontal(), iCol + j, iRow + 1); // column, row   -- horizontal border


                iLastZeroPosition = j;
            }
            iRow += 2;
            // Last Zero
            grid.Children.Add(new Label
            {
                Text = (string)letters[0],// "0",
                FontSize = 24,
                HorizontalTextAlignment = TextAlignment.Center,
                WidthRequest = columnWidth,
                TextColor = Color.White
            }
            , iCol + iLastZeroPosition, iRow);

            return grid;
        }

        // Delete the current puzzle.
        async void DeleteItem_Clicked(object sender, EventArgs e)
        {

            var toolBarDelete = (ToolbarItem)sender; // The delete button

            //MessagingCenter.Send(this, "DeleteItem", Item);


            //await Navigation.PopModalAsync();



            string theID = toolBarDelete.CommandParameter.ToString(); // The id of the item.

            BaseViewModel baseViewModel = new BaseViewModel();
            await baseViewModel.DataStore.DeleteItemAsync(theID);
            var wpddb = new WDPDB();

            WDPItem wdpitem = new WDPItem();
            wdpitem.Id = theID;
            await wpddb.DeleteItemAsync(wdpitem);
            //await Navigation.PushAsync(new ItemsPage()); // This works to redirect to items list page.
            await Navigation.PopToRootAsync();
         
        }

        // Button click that submits user answer.
        private void Submit_Answer(object sender, EventArgs e)
        {
            StartAnimation(sender);
        }

        private async void StartAnimation(object sender)
        {

            ////////////////////////
           
            Button lettersButton1 = (Button)sender;
            string answerID = lettersButton1.CommandParameter.ToString();
            bool checkAnswer = true;

            WDPDB wdpdb = new WDPDB();
            WDPItem wdpitem = new WDPItem();
            wdpitem = await wdpdb.GetItemAsync(answerID);

            string t1 = wdpitem.Id;
            string t2 = wdpitem.Letters;
            string t3 = wdpitem.Solved.ToString();
            string t4 = wdpitem.Quotient.ToString();


            string answerKey = wdpitem.Letters;
            answerKey = Regex.Replace(answerKey, @"\s+", "");

            if (e0.Text != answerKey.Substring(0, 1)) { checkAnswer = false; }
            if (e1.Text != answerKey.Substring(1, 1)) { checkAnswer = false; }
            if (e2.Text != answerKey.Substring(2, 1)) { checkAnswer = false; }
            if (e3.Text != answerKey.Substring(3, 1)) { checkAnswer = false; }
            if (e4.Text != answerKey.Substring(4, 1)) { checkAnswer = false; }
            if (e5.Text != answerKey.Substring(5, 1)) { checkAnswer = false; }
            if (e6.Text != answerKey.Substring(6, 1)) { checkAnswer = false; }
            if (e7.Text != answerKey.Substring(7, 1)) { checkAnswer = false; }
            if (e8.Text != answerKey.Substring(8, 1)) { checkAnswer = false; }
            if (e9.Text != answerKey.Substring(9, 1)) { checkAnswer = false; }
            ///////////////////////////

            checkAnswer = true;
            if (checkAnswer == false)
            {

                lettersButton1.BackgroundColor = Color.DarkRed;
                lettersButton1.Text = "Incorrect, Try Again";
                lettersButton1.TextColor = Color.White;
                await Task.Delay(400);
                await lettersButton1.FadeTo(0, 400);
                await Task.Delay(400);
                await lettersButton1.FadeTo(1, 400);
                lettersButton1.BackgroundColor = Color.Silver;
                lettersButton1.Text = "Submit Solution";
                lettersButton1.TextColor = Color.Black;
            }

            if (checkAnswer == true)
            {

                lettersButton1.BackgroundColor = Color.DarkGreen;
                lettersButton1.Text = "Correct! Good work!";
                await Task.Delay(400);
                await lettersButton1.FadeTo(0, 400);
                await Task.Delay(400);
                await lettersButton1.FadeTo(1, 400);
                lettersButton1.BackgroundColor = Color.Black;
                lettersButton1.Text = "Solved";
                lettersButton1.TextColor = Color.Beige;
                lettersButton1.IsEnabled = false;
                SolvedLabel.IsVisible = true;


                wdpitem.Solved = 1;
                await wdpdb.SaveItemAsync(wdpitem);

            }
        }
    }
}