using ConcentrationTest.Forms;
using System.Windows;
using System.Windows.Input;

namespace ConcentrationTest
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();           
        }
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            TestForm testForm = new TestForm(sender, e);
            testForm.Show();
            Close();
        }
        private void Stat_Click(object sender, RoutedEventArgs e)
        {
            StatsForm statForm = new StatsForm();
            statForm.Show();
            Close();
        }
        private void AccountInfo_Click(object sender, RoutedEventArgs e)
        {
            AccountInfo accInfo = new AccountInfo();
            accInfo.Show();
            Close();
        }
        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthForm authForm = new AuthForm();
            authForm.Show();
            Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
