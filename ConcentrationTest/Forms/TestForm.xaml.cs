using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace ConcentrationTest.Forms
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class TestForm : Window
    {
        bool isTestActive = false;

        bool timeLeft = false;

        DispatcherTimer testTimeLeft = new System.Windows.Threading.DispatcherTimer(DispatcherPriority.Normal);      // создание таймера для UI

        Test test = new Test();

        public TestForm(object sender, EventArgs e)
        {
            InitializeComponent();
            test.testData.testGrid = testGridUI;
            numberToFind.Text += test.numberToFind.ToString();

            CreateGrid(test.testData);

            test.countdown = test.initialTime;
            test.InitTimer(test.testData.testGrid, test.numberToFind, sender, e);

            testTimeLeft.Tick += new EventHandler(TestTimeLeft_Tick);     // функция, которая будет выполняться
            testTimeLeft.Interval = new TimeSpan(0, 0, 0, 1);                // единицы измерение (сек)

            Complete.IsEnabled = false;
      }

        public void CreateGrid(Test.TestData testData)
        {
            // скрыть сетку с тестом и кнопку "готово" до начала теста
            testData.testGrid.Visibility = Visibility.Hidden;

            for (int i = 0; i < testData.numCols; i++)
                testData.testGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < testData.numRows; i++)
                testData.testGrid.RowDefinitions.Add(new RowDefinition());

            // определяем длину и ширину ячеек
            foreach (var g in testData.testGrid.RowDefinitions)
            {
                g.Height = new GridLength(22);
            }

            foreach (var g in testData.testGrid.ColumnDefinitions)
            {
                g.Width = new GridLength(22);
            }

            for (int i = 0; i < testData.cellCount; i++)
            {
                Button button = new Button();

                int idx = testData.testGrid.Children.Add(button);
                button = testData.testGrid.Children[idx] as Button;

                button.FontSize = 20;
                button.Content = testData.NumberToFind();     // надпись на кнопке будет от 0 до 9
                button.Foreground = Brushes.Black;

                button.FontWeight = FontWeights.Light;

                button.Margin = new Thickness(0);
                button.Padding = new Thickness(0);
                button.BorderThickness = new Thickness(0);

                button.Click += Button_Click;

                button.SetValue(Grid.RowProperty, i / testData.numCols);
                button.SetValue(Grid.ColumnProperty, i % testData.numCols);
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Center;

                button.Background = Brushes.Transparent;
            }
        }

        public void RecreateGrid(Test.TestData testData) // перегенерировать сетку
        {
            foreach (Button btn in testData.testGrid.Children)
            {
                btn.Content = testData.NumberToFind();
                btn.Background = Brushes.Transparent;
            }
        }

        private void Complete_Click(object sender, EventArgs e)                 // при завершении теста пользователем
        {
            testTimeLeft.Stop();
            test.tempStatTimer.Stop();

            double K, A;
            
            if (!timeLeft)      // если время не закончилось, а пользователь нажал на кнопку "готово" (время осталось, все символы просмотрены)
            {
                (K, A) = test.ComputeFinishedResults(test.testData.testGrid, test.numberToFind, test.initialTime, test.countdown, test.testData.testGrid.Children.Count);
            }
            else
            {
                (K, A) = test.ComputeUnfinishedResults(test.testData.testGrid, test.numberToFind, test.maxViewedRow, test.maxViewedColumn, test.initialTime);
            }

            Stat stat = new Stat(UserSaver.user.id, K, A, DateTime.Now.ToShortDateString());
            stat.SaveStat();

            MessageBox.Show("Ваши результаты обработаны. Переход к их просмотру.", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);

            ResultsForm form = new ResultsForm(test.tempStats, K, A);
            form.ShowDialog();

            Complete.IsEnabled = false;
            test.testData.testGrid.IsEnabled = false;
            Start.Content = "Сбросить";
        }

        private void Start_Click(object sender, RoutedEventArgs e)              // начало/сброс теста
        {
            if (!isTestActive)
            {
                // показать сетку и кнопку готово
                test.testData.testGrid.Visibility = Visibility.Visible;
                if (!Complete.IsVisible)
                    Complete.Visibility = Visibility.Visible;
                Start.Content = "Сбросить";
                test.testData.testGrid.IsEnabled = true;
                Complete.IsEnabled = true;
                Rules.IsEnabled = false;

                isTestActive = true;

                // запуск таймера
                test.tempStatTimer.Start();
                testTimeLeft.Start();
            }
            else
            {
                Start.Content = "Начать";

                test.testData.testGrid.Visibility = Visibility.Hidden;
                Complete.IsEnabled = false;
                isTestActive = false;
                Rules.IsEnabled = true;
              
                test.numberToFind = test.testData.rand.Next(0, 10);            // запоминаем число, которое пользователю необходимо искать
                numberToFind.Text = numberToFind.Text.Substring(0, numberToFind.Text.Length - 1) + test.numberToFind.ToString();       // изменить число для поиска

                test.tempStats.Clear();                                     // убрать старую статистику

                RecreateGrid(test.testData);         // сгенерировать числа заново
                
                // остановка таймера и возврат в начальное положение
                testTimeLeft.Stop();
                test.tempStatTimer.Stop();

                test.countdown = test.initialTime;
                timeLeftText.Text = "";
            }
        }

        private void TestTimeLeft_Tick(object sender, EventArgs e)              // тик для таймера, который отображает оставшееся время
        {
            test.countdown--;
            timeLeftText.Text = "Осталось времени: " + test.countdown.ToString() + "с";

            if (test.countdown == 0)     // если закончилось время, перенаправить на конец тестирования
            {
                timeLeft = true;
                Complete_Click(sender, e);
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)             // вызывается при нажатии на цифру из теста
                                                                                // функция нужна для изменения цвета фона кнопки с цифрой
        {
            Button btn = sender as Button;

            if (btn.Background == Brushes.Transparent)
            //if (btn.state == CustomButton.State.unpressed)     // индикатор для обозначения отмеченной кнопки
            {
                btn.Background = Brushes.Orange;
                //btn.state = CustomButton.State.pressed;
            }
            else
            {
                btn.Background = Brushes.Transparent;
              //  btn.state = CustomButton.State.unpressed;
            }

            if ((int)btn.GetValue(Grid.RowProperty) >= test.maxViewedRow)
            {
                test.maxViewedRow = (int)btn.GetValue(Grid.RowProperty);                         // запоминаем номер строки и столбца - ячейки, на которую пользователь нажал в последний раз;
                                                                                            // используется, чтобы запомнить место, до которого дошел пользователь, в случаях для подсчета временной статистики                                                                                             
                if ((int)btn.GetValue(Grid.ColumnProperty) > test.maxViewedColumn)               // или истечения времени теста
                    test.maxViewedColumn = (int)btn.GetValue(Grid.ColumnProperty);
            }
            //MessageBox.Show(string.Format("Нажатие - ряд {0}, строка {1}", (int)btn.GetValue(Grid.ColumnProperty) + 1, (int)btn.GetValue(Grid.RowProperty) + 1));
        }

        private void Rules_Click(object sender, RoutedEventArgs e)
        {
            RulesForm rulesForm = new RulesForm();
            rulesForm.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)    // для возможности перемещения формы с помощью курсора
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Close();
            mainMenu.Show();
        }
    }
}