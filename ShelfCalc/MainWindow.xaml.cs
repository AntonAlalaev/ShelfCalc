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
            if (!(LowerShelf is null))
                LowerShelf.Text = Math.Round(Stell.LowerShelf, 1).ToString();

            // Определяем расстояние между полками
            if (!(StandHeight is null))
                StandHeight.Text = Math.Round(Stell.StandHeight, 1).ToString();

            // Отображаем количество полок
            if (!(Amount is null))
                Amount.Text = Stell.Amount.ToString();

            // Отображаем вычисленную дистанцию
            if (!(Distance is null))
                Distance.Text = Math.Round(Stell.ShelfDistance, 1).ToString();

            // Отображаем высоту верхней полки
            if (!(UpperHeight is null))
                UpperHeight.Text = Math.Round(Stell.UpperShelf, 1).ToString();


            // Отображаем полную выстоу стеллажа
            if (!(ShelfHeight is null))
                ShelfHeight.Text = Math.Round(Stell.TotalHeight, 1).ToString();

            // Отображаем положение полок
            if (!(ShelfPosition is null))
            {
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

        private void Slide1400ReCalc()
        {
            try
            {
                if (ShelfDistanceSlide is null || ShelfLowerSlide is null || ShelfAmountSlide is null)
                {
                    return;
                }
                Slide1400 It25 = new Slide1400(ShelfDistanceSlide.Text, ShelfLowerSlide.Text, ShelfAmountSlide.Text);
                //SeriesL125 It125 = new SeriesL125(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                RefreshFilds(It25, ShelfLowerSlide1000, ShelfStandHeightSlide1000, ShelfAmountSlide1000, ShelfDistanceSlide1000, ShelfUpperHeightSlide1000, null, ShelfPositionSlide1000);
                //RefreshFilds(It125, ShelfLowerL125, ShelfStandHeightL125, ShelfAmountL125, ShelfDistanceL125, ShelfUpperHeightL125, ShelfHeightL125, ShelfPositionL125);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        /// <summary>
        /// перессчет СГС
        /// </summary>
        private void SGSReCalc()
        {
            try
            {
                if (ShelfLowerSGS is null || ShelfDistanceSGS is null || ShelfAmountSGS is null)
                {
                    return;
                }
                SGS sgs50 = new SGS(ShelfDistanceSGS.Text, ShelfLowerSGS.Text, ShelfAmountSGS.Text);
                RefreshFilds(sgs50, ShelfLowerSGS_, ShelfStandHeightSGS_, ShelfAmountSGS_, ShelfDistanceSGS_, ShelfUpperHeightSGS_, null, ShelfPositionSGS_);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        /// <summary>
        /// перессчет СГС
        /// </summary>
        private void SBSGSReCalc()
        {
            try
            {
                if (ShelfLowerSGSSB is null || ShelfDistanceSGSSB is null || ShelfAmountSGSSB is null)
                {
                    return;
                }
                SBSGS sgs50 = new SBSGS(ShelfDistanceSGSSB.Text, ShelfLowerSGSSB.Text, ShelfAmountSGSSB.Text);
                RefreshFilds(sgs50, ShelfLowerSGSSB_, ShelfStandHeightSGSSB_, ShelfAmountSGSSB_, ShelfDistanceSGSSB_, ShelfUpperHeightSGSSB_, null, ShelfPositionSGSSB_);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        private void Slide1000ReCalc()
        {
            try
            {
                if (ShelfDistanceSlide10000 is null || ShelfLowerSlide10000 is null || ShelfAmountSlide10000 is null)
                {
                    return;
                }
                //ShelfLowerSlide100000
                Slide1000 It25 = new Slide1000(ShelfDistanceSlide10000.Text, ShelfLowerSlide10000.Text, ShelfAmountSlide10000.Text);
                //SeriesL125 It125 = new SeriesL125(ShelfDistanceL.Text, ShelfLowerL.Text, ShelfAmountL.Text);
                RefreshFilds(It25, ShelfLowerSlide100000, ShelfStandHeightSlide10000, ShelfAmountSlide100000, ShelfDistanceSlide100000, ShelfUpperHeightSlide10000, null, ShelfPositionSlide10000);
                //RefreshFilds(It125, ShelfLowerL125, ShelfStandHeightL125, ShelfAmountL125, ShelfDistanceL125, ShelfUpperHeightL125, ShelfHeightL125, ShelfPositionL125);
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
            ShelfLowerStat.Text = "67";
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

            ShelfLowerSlide.Text = "223";
            ShelfDistanceSlide.Text = "430";
            ShelfAmountSlide.Text = "3";

            ShelfLowerSlide10000.Text = "243";
            ShelfDistanceSlide10000.Text = "430";
            ShelfAmountSlide10000.Text = "3";

            ShelfLowerSGS.Text = "100";
            ShelfDistanceSGS.Text = "400";
            ShelfAmountSGS.Text = "4";

            ShelfLowerSGS.Text = "102";
            ShelfDistanceSGS.Text = "400";
            ShelfAmountSGS.Text = "4";

            ShelfLowerSGSSB.Text = "232";
            ShelfDistanceSGSSB.Text = "450";
            ShelfAmountSGSSB.Text = "4";
            Slide1400Tall.IsChecked = false;



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

        /// <summary>
        /// Прячем или показываем вставку для архивных стеллажей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (TabSlide.IsSelected || TabSlide14.IsSelected)
            {
                MainGrid.ColumnDefinitions[1].Width = new GridLength(0);
            }
            else
            {
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
            }
            */
            if (TabSlide.IsSelected || TabSlide14.IsSelected || TabSGS.IsSelected || TabSGSSB.IsSelected)
            {
                VisualGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                VisualGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);                
                if (TabSlide.IsSelected || TabSlide14.IsSelected)
                    ShelfLengthSlide.Text = "1200";
                else
                    ShelfLengthSlide.Text = "1100";
            }
            else
            {
                VisualGrid.ColumnDefinitions[1].Width = new GridLength(0, GridUnitType.Star);
                VisualGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                ShelfLength.Text = "1000";
            }

           


        }

        private void ShelfLowerSlide_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1400ReCalc();
        }

        private void ShelfDistanceSlide_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1400ReCalc();
        }

        private void ShelfAmountSlide_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1400ReCalc();
        }

        /// <summary>
        /// Отрисовка слайдов и СГС
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double Glubina = 800;
            try
            {
                Glubina = double.Parse(ShelfWidthSlide.Text);
            }
            catch (Exception)
            {
            }

            double Dlina = 1200;
            if (TabSGS.IsSelected || TabSGSSB.IsSelected)
                Dlina = 1100;

            try
            {
                Dlina = double.Parse(ShelfLengthSlide.Text);
            }
            catch (Exception)
            {
            }

            if (TabSlide14.IsSelected)
            {
                if (Glubina < 800)
                    Glubina = 800;
                if (Dlina < 800)
                    Dlina = 800;
            }

            if (TabSGS.IsSelected)
            {
                if (Glubina < 400)
                    Glubina = 400;
                if (Dlina < 400)
                    Dlina = 400;
            }

            if (TabSGSSB.IsSelected)
            {
                if (Glubina < 600)
                    Glubina = 600;
                if (Dlina < 600)
                    Dlina = 600;
            }




            if (TabSlide.IsSelected)
            {
                this.Hide();
                DrawStell.Draw(new Slide1400(ShelfDistanceSlide.Text, ShelfLowerSlide.Text, ShelfAmountSlide.Text), Glubina, Dlina, true, false);
                this.Show();
            }

            if (TabSlide14.IsSelected)
            {
                this.Hide();
                //ShelfDistanceSlide10000 is null || ShelfLowerSlide10000  is null || ShelfAmountSlide10000
                DrawStell.Draw(new Slide1000(ShelfDistanceSlide10000.Text, ShelfLowerSlide10000.Text, ShelfAmountSlide10000.Text), Glubina, Dlina, true, false);
                this.Show();
            }
            // если вкладка СГС выбрана
            if (TabSGS.IsSelected)
            {
                this.Hide();
                //ShelfDistanceSlide10000 is null || ShelfLowerSlide10000  is null || ShelfAmountSlide10000
                DrawStell.Draw(new SGS(ShelfDistanceSGS.Text, ShelfLowerSGS.Text, ShelfAmountSGS.Text), Glubina, Dlina, true, false);
                this.Show();

            }

            if (TabSGSSB.IsSelected)
            {
                this.Hide();
                //ShelfDistanceSlide10000 is null || ShelfLowerSlide10000  is null || ShelfAmountSlide10000
                DrawStell.Draw(new SBSGS(ShelfDistanceSGSSB.Text, ShelfLowerSGSSB.Text, ShelfAmountSGSSB.Text), Glubina, Dlina, true, false);
                this.Show();
            }
        }

        // 
        private void ShelfLowerSlide10000_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1000ReCalc();
        }

        private void ShelfDistanceSlide10000_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1000ReCalc();
        }

        private void ShelfAmountSlide10000_TextChanged(object sender, TextChangedEventArgs e)
        {
            Slide1000ReCalc();
        }

        // меняется высота нижней полки
        private void ShelfLowerSGS_TextChanged(object sender, TextChangedEventArgs e)
        {

            SGSReCalc();
        }

        // меняетмя расстояние между полками
        private void ShelfDistanceSGS_TextChanged(object sender, TextChangedEventArgs e)
        {
            SGSReCalc();
        }

        //меняется количество полок
        private void ShelfAmountSGS_TextChanged(object sender, TextChangedEventArgs e)
        {
            SGSReCalc();
        }

        private void ShelfLowerSGSSB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SBSGSReCalc();
        }

        private void ShelfDistanceSGSSB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SBSGSReCalc();
        }

        private void ShelfAmountSGSSB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SBSGSReCalc();
        }

    }
}
