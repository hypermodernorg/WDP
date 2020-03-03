using System;
using System.ComponentModel;
using System.Collections;
using WordDivisionPuzzles.Models;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;


namespace WordDivisionPuzzles.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]



    public partial class NewItemPage : ContentPage
    {

        //Todo: Need to correct the horizontal border when the product length is less than iDivideInto length.
        public Item Item { get; set; }
        static int columnWidth = 20;
        static ArrayList letters = new ArrayList();
        CommonTasks task = new CommonTasks();

        public NewItemPage()
        {
            InitializeComponent();

            // Class from the CommonTasks.cs file.
    
            int iDivisor = task.GetRandom(500, 1000);
            int iQuotient = task.GetRandom(1000, 99999);
       
            int iDividend = iDivisor * iQuotient;
            string divisor = iDivisor.ToString();
            string quotient = iQuotient.ToString();
            string dividend = iDividend.ToString();
            int iDivisorLength = divisor.Length;
            int iQuotientLength = quotient.Length;
            int iDividendLength = dividend.Length;
            int iTotalLength = iDivisorLength + iDividendLength + 1;


            Grid grid = task.ShapeGrid(iTotalLength, iDivisorLength); // Create the grid size.
            letters = MakeLetters();
           

            string zero = (string)letters[0];
            string one = (string)letters[1];
            string two = (string)letters[2];
            string three = (string)letters[3];
            string four = (string)letters[4];
            string five = (string)letters[5];
            string six = (string)letters[6];
            string seven = (string)letters[7];
            string eight = (string)letters[8];
            string nine = (string)letters[9];
          

            grid = FirstThreeRows(iTotalLength, divisor, quotient, dividend, grid, letters);
            grid = LastLines(iTotalLength, divisor, quotient, dividend, grid, letters);

            Grid containerGrid = (Grid)Content.FindByName("NewGrid");
            containerGrid.Children.Add(grid, 0, 0);
            this.Content = containerGrid; // set the content
            StoreItem(iDivisor, iQuotient);

        }

        public ArrayList MakeLetters()
        {
            Random random = new Random();
            ArrayList letters = new ArrayList();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i<10; i++)
            {
                int iRandom = random.Next(0, 25-i);
                letters.Add(alphabet.Substring(iRandom, 1));
                alphabet = alphabet.Remove(iRandom, 1);
            }

            return letters;
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
                        Text =  (string) letters[int.Parse(quotient.Substring(j, 1))],
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
                        , iCol + j-1, iRow);

                        isSubtractFirstPass = false;
                    }

                    //Border
                    grid.Children.Add(task.BvBorderHorizontal(), iCol + j, iRow+1); // column, row   -- horizontal border
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
            iRow+=2;
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

        // Store the quotient and divisor for later.
        // Later make integer quotient and divisor hidden, while displaying
        // .. the alphabetical form.
        public void StoreItem(int iDivisor, int iQuotient)
        {
            string alphaDivisor = "";
            string alphaQuotient = "";
            for (int i = 0;i<iDivisor.ToString().Length;i++)
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
                Quotient        = iQuotient.ToString(),
                Divisor         = iDivisor.ToString(),
                Letters         = letters,
                AlphaQuotient   = alphaQuotient,
                AlphaDivisor    = alphaDivisor
            };
            BindingContext = this;

            //var strings = new ArrayList().Cast<string>().ToArray();

            var theLetters = string.Join(" ", Item.Letters);

            Preferences.Set(Item.Id, Item.Divisor + "|" + Item.Quotient + "|" + theLetters + "|" + Item.AlphaDivisor + "|"+ Item.AlphaQuotient);
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}