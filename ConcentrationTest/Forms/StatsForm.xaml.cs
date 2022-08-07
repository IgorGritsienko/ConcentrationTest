using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ConcentrationTest.Forms
{
    /// <summary>
    /// Логика взаимодействия для StatsForm.xaml
    /// </summary>
    public partial class StatsForm : Window
    {
        public StatsForm()
        {
            InitializeComponent();

            using (AppContext db = new AppContext())
            {
                List<Stat> userResults = User.GetUserResults(db, UserSaver.user.id);

                foreach (var stat in userResults)
                {
                    dataGrid.Items.Add(new UserResultsHistory(Math.Round(stat.attentionConcentration, 2), Math.Round(stat.attentionSpan, 2), stat.testDate));   // добавляем в датагрид данные о результатах тестирования этого пользователя
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            rb1.IsChecked = false;
            rb2.IsChecked = false;
            rb3.IsChecked = false;

            minAge.Text = "";
            maxAge.Text = "";
        }

        private void Compare_Click(object sender, RoutedEventArgs e)
        {
            string sex;
            int _minAge;
            int _maxAge;

            if (rb1.IsChecked == true) sex = "Мужской";
            else if (rb2.IsChecked == true) sex = "Женский";
            else sex = "None";
             
            // если не указаны границы возраста, задать абсолютный минимум и условный максимум
            if (string.IsNullOrEmpty(minAge.Text))
                _minAge = 0;
            else _minAge = Convert.ToInt32(minAge.Text);

            if (string.IsNullOrEmpty(maxAge.Text))
                _maxAge = 1000;
            else _maxAge = Convert.ToInt32(maxAge.Text);

            using (AppContext db = new AppContext())
            {
                var (K, A) = Stat.AgregateData(db, sex, _minAge, _maxAge);     // вычисляем средние данные с учетом фильтров

                if (double.IsNaN(K) || double.IsNaN(A))     // если данных нет
                {
                    K_result.Text = "По выбранным фильтрам нет еще данных";
                    A_result.Text = "";
                }
                else
                {
                    K_result.Text = string.Format("Средние показатели концентрации внимания для выбранных параметров: {0,2:f}", K);
                    A_result.Text = string.Format("Средние показатели устойчивости внимания для выбранных параметров: {0,2:f}", A);
                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }
    }
}