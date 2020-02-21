﻿using System;
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

        public NewItemPage()
        {
            InitializeComponent();

            int iDivisor = GetRandom(500, 1000);
            int iQuotient = GetRandom(10000, 99999);
            int iDividend = iDivisor * iQuotient;
            string divisor = iDivisor.ToString();
            string quotient = iQuotient.ToString();
            string dividend = iDividend.ToString();
            int iDivisorLength = divisor.Length;
            int iQuotientLength = quotient.Length;
            int iDividendLength = dividend.Length;
            int iTotalLength = iDivisorLength + iDividendLength + 1;
            Grid grid = ShapeGrid(iTotalLength, iDivisorLength); // Create the grid size.

            grid = FirstThreeRows(iTotalLength, divisor, quotient, dividend, grid);
            grid = LastLines(iTotalLength, divisor, quotient, dividend, grid);

            Grid containerGrid = (Grid)Content.FindByName("NewGrid");
            containerGrid.Children.Add(grid, 0, 0);
            this.Content = containerGrid; // set the content
            StoreItem(iDivisor, iQuotient);

        }

        // The first three lines, including the horizontal border, of the long division problem.
        public Grid FirstThreeRows(int iTotalLength, string divisor, string quotient, string dividend, Grid grid)
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
                        Text = quotient.Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                        , i, 2);

                    grid.Children.Add(BvBorderHorizontal(), i, 3); // column, row   -- horizontal border

                    j++;
                }
            } // End First & Second Row

            // Third Rows: The Divisor and Dividend
            j = 0;
            grid.Children.Add(BvBorderCorner(), iDivisorLength, 3);
            for (int i = 0; i < iTotalLength; i++)
            {
                if (i < iDivisorLength)
                {
                    grid.Children.Add(new Label
                    {
                        Text = divisor.Substring(i, 1),
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
                        Text = dividend.Substring(j, 1),
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
        public Grid LastLines(int iTotalLength, string divisor, string quotient, string dividend, Grid grid)
        {
            int iCol = divisor.Length + 1;
            int iRow = 6;
            int iDivideInto = 0;
            int iDivisor = int.Parse(divisor);
            bool isFirstPass = true;

 


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
                int iProduct = iQuotient * iDivisor;

                // Print the Product
                if (iProduct.ToString().Length < iDivideInto.ToString().Length)
                {
                    iCol++;
                    //i++;
                }

                for (int j = 0; j < iProduct.ToString().Length; j++)
                {
                    grid.Children.Add(new Label
                    {
                        Text = iProduct.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                    , iCol + j, iRow);
                }
                iRow++;
                // End Print the Product

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
                        Text = iDivideInto.ToString().Substring(j, 1),
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        WidthRequest = columnWidth,
                        TextColor = Color.White
                    }
                    , iCol + j, iRow);
                }


          
                iRow++;
            }
            

            return grid;
        }



        // Store the quotient and divisor for later.
        // Later make integer quotient and divisor hidden, while displaying
        // .. the alphabetical form.
        public void StoreItem(int iDivisor, int iQuotient)
        {
            Item = new Item
            {
                Quotient = iQuotient.ToString(),
                Divisor = iDivisor.ToString()
            };
            BindingContext = this;
        }


        // Get a random number for both the iDivisor and iQuotient, 
        // and make sure they do not contain zero's
        public int GetRandom(int iRandomStart, int iRandomEnd)
        {
            Random random = new Random();

            int iRandom = random.Next(iRandomStart, iRandomEnd);

            while (iRandom.ToString().Contains("0"))
            {
                iRandom = random.Next(iRandomStart, iRandomEnd);
            }
            return iRandom;
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