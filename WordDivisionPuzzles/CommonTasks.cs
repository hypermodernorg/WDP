using System;
using Xamarin.Forms;

namespace WordDivisionPuzzles
{

    public class CommonTasks
    {
        static int columnWidth = 20;
        static int answerColumnWidth = 25;

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


        public Grid AnswerGrid()
        {
            Grid answerGrid = new Grid();
            answerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            answerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            for (int i = 1; i<11;i++)
            {
                answerGrid.Children.Add(new Label
                {
                    Text = (i-1).ToString(),
                    FontSize = 22,
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = answerColumnWidth,
                    TextColor = Color.White
                }  , i, 0);

                answerGrid.Children.Add(new Entry
                {
                    FontSize = 22,
                    HorizontalTextAlignment = TextAlignment.Center,
                    WidthRequest = answerColumnWidth,
                    BackgroundColor = Color.Silver,
                    TextColor = Color.DarkSlateGray
                }, i, 1);
            }

            return answerGrid;
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

        public BoxView BvBorderHorizontal()
        {
            BoxView boxViewBorder = new BoxView
            {
                HeightRequest = 4,
                BackgroundColor = Color.White,
                WidthRequest = columnWidth
            };
            return boxViewBorder;
        }

        public BoxView BvBorderVertical()
        {
            BoxView boxViewBorder = new BoxView
            {
                WidthRequest = 4,
                BackgroundColor = Color.White,
                HeightRequest = columnWidth

            };
            return boxViewBorder;
        }

        public BoxView BvBorderCorner()
        {
            BoxView boxViewBorder = new BoxView
            {
                WidthRequest = 4,
                BackgroundColor = Color.White,
                HeightRequest = 4

            };
            return boxViewBorder;
        }
    }
}
