using System;
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
            string strDivisor = iDivisor.ToString();
            string strQuotient = iQuotient.ToString();
            string strDividend = iDividend.ToString();
            int iDivisorLength = strDivisor.Length;
            int iQuotientLength = strQuotient.Length;
            int iDividendLength = strDividend.Length;

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