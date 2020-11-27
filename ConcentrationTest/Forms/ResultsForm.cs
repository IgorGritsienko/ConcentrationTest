using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ConcentrationTest.Forms
{
    /// <summary>
    /// Логика взаимодействия для ResultsForm.xaml
    /// </summary>
    public partial class ResultsForm : Window
    {
        public ResultsForm(List<TempStat> tempStats, double K, double A)
        {
            InitializeComponent();

            foreach (TempStat ts in tempStats)                      // заносим все записи ститастики в таблицу
            {
                dataGrid.Items.Add(new TestStatistics(ts.numCharViewed, ts.numCharRight, ts.numCharWrong, ts.time));
            }

            if (double.IsNaN(A))                                    // выводим результаты по двум критериям
            {
                result_A.Text = "Недостаточно данных";
            }
            else
            {
                result_A.Text += Math.Round(A, 2);
            }
            result_K.Text += Math.Round(K, 2);                      
        }

        private void ToTest_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}