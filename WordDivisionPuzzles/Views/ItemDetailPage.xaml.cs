using System;
using System.ComponentModel;
using WordDivisionPuzzles.Models;
using WordDivisionPuzzles.ViewModels;
using System.Collections;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WordDivisionPuzzles.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]


    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        static int columnWidth = 20;
        CommonTasks task = new CommonTasks();

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
   
            var strDivisor = viewModel.Item.Divisor; // Get the divisor from the model.
            var strQuotient = viewModel.Item.Quotient; // Get the quotient from the model.
    

            Random random = new Random();


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


            Grid grid = task.ShapeGrid(iTotalLength, iDivisorLength); // Create the grid size.
            BindingContext = this;
            var letters = viewModel.Item.Letters;


            grid = FirstThreeRows(iTotalLength, divisor, quotient, dividend, grid, letters);
            grid = LastLines(iTotalLength, divisor, quotient, dividend, grid, letters);

            Grid containerGrid = (Grid)Content.FindByName("NewGrid");
            containerGrid.Children.Add(grid, 0, 0);
            this.Content = containerGrid; // set the content
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
                        grid.Children.Add(task.BvBorderVertical(), i, 4); // column, row   
                    }
                    if (i > iDivisorLength) // if this, then print the vertical border
                    {
                        grid.Children.Add(task.BvBorderHorizontal(), i, 3); // column, row   
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

                    grid.Children.Add(task.BvBorderHorizontal(), i, 3); // column, row   -- horizontal border

                    j++;
                }
            } // End First & Second Row

            // Third Rows: The Divisor and Dividend
            j = 0;
            grid.Children.Add(task.BvBorderCorner(), iDivisorLength, 3);
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
                    grid.Children.Add(task.BvBorderHorizontal(), iCol + j, iRow + 1); // column, row   -- horizontal border
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
                grid.Children.Add(task.BvBorderHorizontal(), iCol + j, iRow + 1); // column, row   -- horizontal border


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

  
        async void DeleteItem_Clicked(object sender, EventArgs e)
        {

            var toolBarDelete = (ToolbarItem)sender; // The delete button

            //MessagingCenter.Send(this, "DeleteItem", Item);
            await Navigation.PopModalAsync();


            //var toolBarDelete = (ToolbarItem)sender; // The delete button
            //string theID = toolBarDelete.CommandParameter.ToString(); // The id of the item.
            
            //BaseViewModel baseViewModel = new BaseViewModel();
            //baseViewModel.DataStore.DeleteItemAsync(theID);
       
            //Navigation.PushAsync(new ItemsPage()); // This works to redirect to items list page.
        }
    }
}