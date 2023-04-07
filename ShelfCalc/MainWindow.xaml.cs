using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShelfCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// обновляем поля после ввода параметров стеллажа
        /// </summary>
        /// <param name="Stell">экземпляр класса стеллажа</param>
        /// <param name="LowerShelf">TextBlock в котором отображается нижняя полка</param>
        /// <param name="StandHeight">TextBlock в котором отображается высота стойки</param>
        /// <param name="Amount">TextBlock в котором отображается количество полок</param>
        /// <param name="Distance">TextBlock в котором отображается расстояние между полками</param>
        /// <param name="UpperHeight">TextBlock в котором отображается высота верхней полки</param>
        /// <param name="ShelfHeight">TextBlock в котором отображается общая высота стеллажа</param>
        /// <param name="ShelfPosition">TextBlock в котором отображается положение полок</param>
        private void RefreshFilds(StellCalc Stell,
            TextBlock LowerShelf, TextBlock StandHeight, TextBlock Amount, TextBlock Distance, TextBlock UpperHeight, TextBlock ShelfHeight,
            StackPanel ShelfPosition)
        {
            // Проверяем на инициализацию 
            if (Stell == null) { return; }

            // Определяем положение нижней полки
            LowerShelf.Text = Math.Round(Stell.LowerShelf, 1).ToString();

            // Определяем расстояние между полками
            StandHeight.Text = Math.Round(Stell.StandHeight, 1).ToString();

            // Отображаем количество полок
            Amount.Text = Stell.Amount.ToString();

            // Отображаем вычисленную дистанцию
            Distance.Text = Math.Round(Stell.ShelfDistance, 1).ToString();

            // Отображаем высоту верхней полки
            UpperHeight.Text = Math.Round(Stell.UpperShelf, 1).ToString();

            // Отображаем полную выстоу стеллажа
            ShelfHeight.Text = Math.Round(Stell.TotalHeight, 1).ToString();

            // Отображаем положение полок
            ShelfPosition.Children.Clear();
            TextBlock Block1 = new TextBlock
            {
                Text = Math.Round(Stell.LowerShelf, 1).ToString(),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.AliceBlue)
            };
            ShelfPosition.Children.Add(Block1);
            foreach (double Item in Stell.ShelfPosArray)
            {
                TextBlock block = new TextBlock
                {
                    Text = Math.Round(Item, 1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.AliceBlue),
                    FontSize = 20,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                ShelfPosition.Children.Add(block);
            }
        }

        /// <summary>
        /// Пересчет серии Т
        /// </summary>
        private void SeriesTRecalc()
        {
            try
            {
                if (ShelfDistanceT is null || ShelfLowerT is null || ShelfAmountT is null)
                {
                    return;
                }
                SeriesT25 It25 = new SeriesT25(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text);
                SeriesT125 It125 = new SeriesT125(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text);
                RefreshFilds(It25, ShelfLowerT25, ShelfStandHeightT25, ShelfAmountT25, ShelfDistanceT25, ShelfUpperHeightT25, ShelfHeightT25, ShelfPositionT25);
                RefreshFilds(It125, ShelfLowerT125, ShelfStandHeightT125, ShelfAmountT125, ShelfDistanceT125, ShelfUpperHeightT125, ShelfHeightT125, ShelfPositionT125);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        /// <summary>
        /// Пересчет серии Л с фальшполом
        /// </summary>
        private void SeriesLFRecalc()
        {
            try
            {
                if (ShelfDistanceLF is null || ShelfLowerLF is null || ShelfAmountLF is null)
                {
                    return;
                }
                SeriesL25F It25 = new SeriesL25F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                SeriesL125F It125 = new SeriesL125F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                RefreshFilds(It25, ShelfLowerLF25, ShelfStandHeightLF25, ShelfAmountLF25, ShelfDistanceLF25, ShelfUpperHeightLF25, ShelfHeightLF25, ShelfPositionLF25);
                RefreshFilds(It125, ShelfLowerLF125, ShelfStandHeightLF125, ShelfAmountLF125, ShelfDistanceLF125, ShelfUpperHeightLF125, ShelfHeightLF125, ShelfPositionLF125);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        /// <summary>
        /// Пересчет серии Л
        /// </summary>
        private void SeriesLRecalc()
        {
            try
            {
                if (ShelfDistanceL is null || ShelfLowerL is null || ShelfAmountL is null)
                {
                    return;
                }
                SeriesL25 It25 = new SeriesL25(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                SeriesL125 It125 = new SeriesL125(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                RefreshFilds(It25, ShelfLowerL25, ShelfStandHeightL25, ShelfAmountL25, ShelfDistanceL25, ShelfUpperHeightL25, ShelfHeightL25, ShelfPositionL25);
                RefreshFilds(It125, ShelfLowerL125, ShelfStandHeightL125, ShelfAmountL125, ShelfDistanceL125, ShelfUpperHeightL125, ShelfHeightL125, ShelfPositionL125);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        /// <summary>
        /// Пересчет стационаров
        /// </summary>
        private void StatRecalc()
        {
            try
            {
                if (ShelfDistanceStat is null || ShelfLowerStat is null || ShelfAmountStat is null)
                {
                    return;
                }
                Stat25 It25 = new Stat25(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text);
                Stat125 It125 = new Stat125(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text);
                RefreshFilds(It25, ShelfLowerStat25, ShelfStandHeight25, ShelfAmountStat25, ShelfDistanceStat25, ShelfUpperHeight25, ShelfHeight25, ShelfPosition25);
                RefreshFilds(It125, ShelfLowerStat125, ShelfStandHeight125, ShelfAmountStat125, ShelfDistanceStat125, ShelfUpperHeight125, ShelfHeight125, ShelfPosition125);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void ShelfDistanceStat_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatRecalc();
        }

        private void ShelfAmountStat_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatRecalc();
        }
        private void ShelfLowerStat_TextChanged(object sender, TextChangedEventArgs e)
        {
            StatRecalc();
        }

        /// <summary>
        /// Начальные значения при загрузке окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShelfLowerStat.Text = "42";
            ShelfDistanceStat.Text = "345";
            ShelfAmountStat.Text = "5";

            ShelfLowerL.Text = "107";
            ShelfDistanceL.Text = "345";
            ShelfAmountL.Text = "5";

            ShelfLowerLF.Text = "127";
            ShelfDistanceLF.Text = "345";
            ShelfAmountLF.Text = "5";

            ShelfLowerT.Text = "210";
            ShelfDistanceT.Text = "345";
            ShelfAmountT.Text = "5";
        }

        private void ShelfLowerL_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLRecalc();
        }

        private void ShelfDistanceL_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLRecalc();
        }

        private void ShelfAmountL_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLRecalc();
        }

        private void ShelfLowerT_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesTRecalc();
        }

        private void ShelfDistanceT_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesTRecalc();
        }

        private void ShelfAmountT_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesTRecalc();
        }

        /// <summary>
        /// Рисуем стеллаж со стойкой с шагом 25 мм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double Glubina = 300;
            try
            {
                Glubina = double.Parse(ShelfWidth.Text);
            }
            catch (Exception)
            {
            }

            double Dlina = 1000;
            try
            {
                Dlina = double.Parse(ShelfLength.Text);
            }
            catch (Exception)
            {
            }

            if (Glubina < 200)
                Glubina = 200;

            if (Dlina < 200)
                Dlina = 200;

            if (Stati.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    DrawStell.Draw(new Stat25(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text), Glubina, Dlina, true, false);
                    this.Show();
                }
                else
                {
                    this.Hide();
                    DrawStell.Draw(new Stat25(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text), Glubina, Dlina, true, true);
                    this.Show();
                }
            }

            if (TabL.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    SeriesL25 toDraw = new SeriesL25(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, false);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, false);

                    this.Show();
                }
                else
                {
                    this.Hide();
                    SeriesL25 toDraw = new SeriesL25(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, true);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, true);
                    this.Show();
                }
                //MessageBox.Show("Выбана Серия Л");
            }

            if (TabLF.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    SeriesL25F toDraw = new SeriesL25F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, false);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, false);

                    this.Show();
                }
                else
                {
                    this.Hide();
                    SeriesL25F toDraw = new SeriesL25F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, true);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, true);
                    this.Show();
                }
            }

            if (TabT.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    DrawStell.Draw(new SeriesT25(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text), Glubina, Dlina, true, false);
                    this.Show();
                }
                else
                {
                    this.Hide();
                    DrawStell.Draw(new SeriesT25(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text), Glubina, Dlina, true, true);
                    this.Show();
                }
                //MessageBox.Show("Выбана Серия T");
            }

        }

        private void ShelfAmountLF_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLFRecalc();
        }

        private void ShelfDistanceLF_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLFRecalc();
        }

        private void ShelfLowerLF_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeriesLFRecalc();
        }

        /// <summary>
        /// Рисуем стеллаж со стойкой с шагом 12,5 мм
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double Glubina = 300;
            try
            {
                Glubina = double.Parse(ShelfWidth.Text);
            }
            catch (Exception)
            {
            }

            double Dlina = 1000;
            try
            {
                Dlina = double.Parse(ShelfLength.Text);
            }
            catch (Exception)
            {
            }

            if (Glubina < 200)
                Glubina = 200;

            if (Dlina < 200)
                Dlina = 200;

            if (Stati.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    DrawStell.Draw(new Stat125(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text), Glubina, Dlina, true, false);
                    this.Show();
                }
                else
                {
                    this.Hide();
                    DrawStell.Draw(new Stat125(ShelfDistanceStat.Text, ShelfLowerStat.Text, ShelfAmountStat.Text), Glubina, Dlina, true, true);
                    this.Show();
                }
            }

            if (TabL.IsSelected)
            {

                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    SeriesL125 toDraw = new SeriesL125(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, false);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, false);

                    this.Show();
                }
                else
                {
                    this.Hide();
                    SeriesL125 toDraw = new SeriesL125(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, true);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, true);
                    this.Show();
                }
            }

            if (TabLF.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    SeriesL125F toDraw = new SeriesL125F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, false);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, false);

                    this.Show();
                }
                else
                {
                    this.Hide();
                    SeriesL125F toDraw = new SeriesL125F(ShelfDistanceLF.Text, ShelfLowerLF.Text, ShelfAmountLF.Text);
                    if (toDraw.LowerShelf == toDraw.MinimalLowerShelf)
                        DrawStell.Draw(toDraw, Glubina, Dlina, false, true);
                    else
                        DrawStell.Draw(toDraw, Glubina, Dlina, true, true);
                    this.Show();
                }
            }

            if (TabT.IsSelected)
            {
                if (Side1.IsChecked == true)
                {
                    this.Hide();
                    DrawStell.Draw(new SeriesT125(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text), Glubina, Dlina, true, false);
                    this.Show();
                }
                else
                {
                    this.Hide();
                    DrawStell.Draw(new SeriesT125(ShelfDistanceT.Text, ShelfLowerT.Text, ShelfAmountT.Text), Glubina, Dlina, true, true);
                    this.Show();
                }
            }
        }
    }
}
