using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ConcentrationTest
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();

            using (AppContext db = new AppContext())
            {
                User user = db.Users.Where(b => b.Login == login && b.Password == pass).FirstOrDefault();

                if (user != null)
                {
                    MessageBox.Show("Вы авторизовались", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserSaver.user = user;                       // запоминаем логин для последующих манипуляций с данными
                    MainMenu mainMenu = new MainMenu();
                    Close();
                    mainMenu.Show();
                }
                else
                    MessageBox.Show("Логин или пароль введены неккоректно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RedirectToReg_Click(object sender, RoutedEventArgs e)
        {
            RegForm regForm = new RegForm();
            Close();
            regForm.Show();
        }
    }
}