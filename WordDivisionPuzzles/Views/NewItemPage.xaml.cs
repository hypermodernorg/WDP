using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        static int columnWidth = 20;
        static int iAA = 0;
        public NewItemPage()
        {
            InitializeComponent();
            iAA = 0;
            //////////////////////////

            

            Random random = new Random();


            int iDivisor = random.Next(500, 1000); //321;

            int iQuotient = random.Next(10000, 99999); //45678;

            //int iDividend       = iQuotient * iDivisor;

            // iDivisor = 721;

            //iQuotient = 92611;

            int iDividend = iQuotient * iDivisor;

            // Eliminate some of the issues with zeros
            while (iDividend.ToString().Contains("0") || iQuotient.ToString().Contains("0"))
            {
                iDivisor = random.Next(500, 1000);
                iQuotient = random.Next(10000, 99999);
                iDividend = iQuotient * iDivisor;
            }

            Item = new Item
            {
                Quotient = iQuotient.ToString(),
                Divisor = iDivisor.ToString()
            };
            BindingContext = this;


            int iDivisorLength = iDivisor.ToString().Length;
            int iQuotientLength = iQuotient.ToString().Length;
            int iDividendLength = iDividend.ToString().Length;

            int iTotalLength = iDividendLength + iDivisorLength + 1; // +1 for the vertical border

            Grid grid = ShapeGrid(iTotalLength, iDivisorLength); // Create the grid size.

            // the first row... the quotient, spacing, the underline, and the vertical border.
            int j = 0;
            for (int i = 1; i < iTotalLength; i++) // loop through the total number of columns.
            {

                if (i < (iTotalLength - iQuotientLength))
                {
                    if (i == iDivisorLength) // if this, then print the vertical border
                    {
                        grid.Children.Add(BvBorderVertical(), i, 4); // column, row   
                    }
                    if (i > iDivisorLength) // if this, then print the vertical border
                    {
                        grid.Children.Add(BvBorderHorizontal(), i, 3); // column, row   
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
                        Text = iQuotient.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 2);

                    grid.Children.Add(BvBorderHorizontal(), i, 3); // column, row   -- horizontal border

                    j++;
                }
            }
            // end the first row

            // the second row
            j = 0;
            int k = 0;
            for (int i = 0; i < iTotalLength; i++)
            {
                if (i < iDivisorLength)
                {
                    grid.Children.Add(new Label
                    {
                        Text = iDivisor.ToString().Substring(i, 1),
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
                        Text = iDividend.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 4);
                    j++;
                }
            }
            // end the second row

            // THE REST PART 1

            int iPosition;
            if (iDivisor <= int.Parse(iDividend.ToString().Substring(0, iDivisorLength)))
            {
                iPosition = iDivisorLength;
            }
            else
            {
                iPosition = iDivisorLength + 1;
            }

            int iDivideInto = int.Parse(iDividend.ToString().Substring(0, iPosition)); //1466
            iQuotient = iDivideInto / iDivisor; //4
            int iProduct = iQuotient * iDivisor;
            int iRemainder = iDivideInto - iProduct;  //182


            // END THE REST PART 1

            // THE REST PART 2
            int iCol = iDivisorLength + 1;
            int iRow = 6;
            
            for (int i = iPosition; i <= iDividendLength; i++)
            {
                int iAbsolutePosition = 0;
                int iAnotherAdjstment = 0;
                int m = 0;


                // ANDREW MESSED THIS UP
                if (i != iDividendLength)
                {
                    
                    j = 1;
                    while (iDivisor >= iRemainder)
                    {
                        if ((i + j) > iDividendLength)
                        {
                           // DisplayAlert("break", iRemainder.ToString(), "NEXT");
                            break;
                        } // break if dividend length is exceeded
                        if (iRemainder == 0) { DisplayAlert("test", iRemainder.ToString(), "NEXT"); }
                        iRemainder = int.Parse(iRemainder.ToString() + iDividend.ToString().Substring(i, j)); //dividend 26996150 and divisor 905 produce error here when last numbers are zero
                        j++;
                    }

                    if (iRemainder != 50000)
                    {

                        // what happens when length 3 - Length 4 = -1?

                        iDivideInto = iRemainder;
                        int iL = iDivideInto.ToString().Length - (iQuotient * iDivisor).ToString().Length; //Ex. 1466(4)-1284(4) = 0
                        if (iL <= -1)
                        {
                            iAnotherAdjstment = (iQuotient * iDivisor).ToString().Length - iDivideInto.ToString().Length;
                            iAA = iAnotherAdjstment;
                            iCol += iAnotherAdjstment;
                       
                        }
                        
                        //Lets print the iProduct and iDivideInto
                        bool bCheck = false;


                        int iPCorrect = 0;

                        if (iProduct.ToString().Length < iDivideInto.ToString().Length)
                        {
                            iPCorrect = iDivideInto.ToString().Length - iProduct.ToString().Length;
                        }



                        for (m = 0; m < iDivideInto.ToString().Length + iAnotherAdjstment; m++)
                        {
                   
                            iAbsolutePosition = iCol + iL + m;
                            // iCol: The divisor length + one. Plus one accounts for the vertivle border.
                            // iL: The length of divideinto - length of (iquotient * divisor)
                            //      -- Sometimes iL can be negative, in which case we subtract in reverse the above and assign
                            //      -- it to iAnotherAdjustment

                            //Todo: The issue for 721 * 92611 is that iAnotherAdjustment is not carrying over to the next iProduct.
                            //Todo: The fix involves making that happen.
                            //      Possible resolution candidate: add iAnotherAdjustment to iCol.

                            if (m - iPCorrect >= 0)
                            {
                                grid.Children.Add(new Label
                                {
                                    Text = iProduct.ToString().Substring(m - iPCorrect, 1),
                                    FontSize = 24,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    WidthRequest = columnWidth,
                                    TextColor = Color.White
                                }
                                , iAbsolutePosition - iPCorrect, iRow);

                            }

                            //iDivideInto

                            if (m - iAnotherAdjstment >= 0)
                            {
                                grid.Children.Add(new Label
                                {
                                    Text = iDivideInto.ToString().Substring(m - iAnotherAdjstment, 1),
                                    FontSize = 24,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    WidthRequest = columnWidth,
                                    TextColor = Color.White
                                }
                                , iAbsolutePosition - iPCorrect  + j - 1, iRow + 2);

                            }


                            // Horizonal row and subtraction signt
                            grid.Children.Add(BvBorderHorizontal(), iAbsolutePosition - iPCorrect , iRow + 1); // column, row   
                            if (bCheck == false)
                            {
                                grid.Children.Add(new Label
                                {
                                    Text = "-",
                                    FontSize = 24,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    WidthRequest = 4,
                                    TextColor = Color.White
                                }
                                    , iAbsolutePosition - iPCorrect  - 1, iRow);
                                bCheck = true;
                            }
                            

                        }
                    }
                }

                if (i == iDividendLength || (i-iAnotherAdjstment) == iDividendLength)
                {

                    j = 1;
                    int iTest = iRemainder + iProduct + iQuotient;

                    iRemainder = iProduct;


                    if (iRemainder != 0)
                    {
                        int iL = iDivideInto.ToString().Length - (iQuotient * iDivisor).ToString().Length; //Ex. 1466(4)-1284(4) = 0
                        iDivideInto = iRemainder;
                        //Lets print the iProduct and iDivideInto
                        bool bCheck = false;



                        for (m = 0; m < iDivideInto.ToString().Length; m++)
                        {

                            iAbsolutePosition = iCol + iL + m;

                            //iProduct
                            grid.Children.Add(new Label
                            {
                                Text = iProduct.ToString().Substring(m, 1),
                                FontSize = 24,
                                HorizontalTextAlignment = TextAlignment.Center,
                                WidthRequest = columnWidth,
                                TextColor = Color.White
                            }
                                , iAbsolutePosition, iRow);


                            grid.Children.Add(BvBorderHorizontal(), iAbsolutePosition, iRow + 1); // column, row   
                            if (bCheck == false)
                            {
                                grid.Children.Add(new Label
                                {
                                    Text = "-",
                                    FontSize = 24,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    WidthRequest = 4,
                                    TextColor = Color.White
                                }
                                    , iAbsolutePosition- 1, iRow);
                                bCheck = true;
                            }

                            if (m == iDivideInto.ToString().Length - 1)
                            {
                                grid.Children.Add(new Label
                                {
                                    Text = "0",
                                    FontSize = 24,
                                    HorizontalTextAlignment = TextAlignment.Center,
                                    WidthRequest = columnWidth,
                                    TextColor = Color.White
                                }
                                , iAbsolutePosition+ j -1, iRow + 2);
                            }
                        }
                    }
                }
                // END ANDREW MESSED THIS UP

                iCol++;
                iRow += 3;

                iQuotient = iDivideInto / iDivisor;
                iProduct = iQuotient * iDivisor;
                iRemainder = iDivideInto - iProduct;

                //End Lets print
            }
            // END THE REST PART 2

            grid.Children.Add(BvBorderCorner(), iDivisorLength, 3);
            //grid.Children.Add(BorderBoxViewVertical(), divisorLength, 3);
            //Grid containerGrid = ContainerGrid();
            Grid containerGrid = (Grid)Content.FindByName("NewGrid");
            // Number to Letter Conversion
            int iNumberColumns = grid.ColumnDefinitions.Count; // Get number of columns
            int iNumberRows = grid.RowDefinitions.Count; // Get number of rows.

            string sAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //List<string> ls = sAlphabet.Split(' ').ToList();

            // End Number to Letter Conversion

            containerGrid.Children.Add(grid, 0, 0);
            this.Content = containerGrid; // set the content
        }

        public Grid ContainerGrid()
        {
            Grid grid = new Grid
            {
                //VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                BackgroundColor = Color.Black,


            };

            grid.RowDefinitions.Add(new RowDefinition { Height = 30 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = 30 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 30 });
            return grid;
        }


        public Grid ShapeGrid(int iTotalLength, int iDivisorLength)
        {

            Grid grid = new Grid
            {
                //VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,


            };


            for (int i = 0; i < iTotalLength; i++)
            {
                if (i == iDivisorLength)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                }
                else
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                }


            }
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Star});

            return grid;

        }


        // Hack to get a border.
        public BoxView BvBorderHorizontal()
        {
            BoxView boxViewBorder = new BoxView
            {
                HeightRequest = 5,
                BackgroundColor = Color.White,
                WidthRequest = columnWidth
            };
            return boxViewBorder;
        }

        public BoxView BvBorderVertical()
        {
            BoxView boxViewBorder = new BoxView
            {
                WidthRequest = 5,
                BackgroundColor = Color.White,
                HeightRequest = columnWidth

            };
            return boxViewBorder;
        }

        public BoxView BvBorderCorner()
        {
            BoxView boxViewBorder = new BoxView
            {
                WidthRequest = 5,
                BackgroundColor = Color.White,
                HeightRequest = 5

            };
            return boxViewBorder;
        }

        //////////////////////////





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