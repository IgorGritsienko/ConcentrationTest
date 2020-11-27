using System.Windows;
using System.Windows.Input;

namespace ConcentrationTest.Forms
{
    /// <summary>
    /// Логика взаимодействия для RulesForm.xaml
    /// </summary>
    public partial class RulesForm : Window
    {
        public RulesForm()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
