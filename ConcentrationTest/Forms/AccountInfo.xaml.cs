using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace ConcentrationTest
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AccountInfo : Window
    {
        AppContext db;
        User user = new User();
        public AccountInfo()
        {
            InitializeComponent();
            db = new AppContext();
            user = user.GetCurrentUserInfo(db, UserSaver.user.id);              // берем всю информацию о пользователе с таким логином по его id

            title.Text = "Личный кабинет пользователя " + UserSaver.user.Login;

            oldPass.Content = "Текущий пароль: " + user.Password;
            oldEmail.Content = "Текущий email: " + user.Email;
            oldDate.Content = "Текущая дата рождения: " + user.Birthdate;
            oldGender.Content = "Текущий пол: " + user.Gender;          
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();  // конвертер для перевода из hex в brush
            string pass = user.Password;
            string email = user.Email;

            string birthdate = user.Birthdate;
            string gender = user.Gender;
            bool error = false;

            if (!String.IsNullOrEmpty(textBoxPass.Password.Trim()))
            {
                if (pass.Length < 5)
                {
                    textBoxPass.ToolTip = "Пароль должен быть не меньше 5 символов";
                    textBoxPass.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                    error = true;
                }
                else
                {
                    textBoxPass.ToolTip = null; ;
                    textBoxPass.Background = System.Windows.Media.Brushes.Transparent;
                    pass = textBoxPass.Password.Trim();
                }
            }

            if (!String.IsNullOrEmpty(emailBox.Text.Trim()))
            {
                if (email.Length < 5)
                {
                    emailBox.ToolTip = "Email должен быть не меньше 5 символов";
                    emailBox.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                    error = true;
                }
                else
                {
                    if (!email.Contains('@'))
                    {
                        emailBox.ToolTip = "Email должен содержать символ '@'";
                        emailBox.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                        error = true;
                    }
                    else
                    {
                        if (!email.Contains('.'))
                        {
                            emailBox.ToolTip = "Email должен содержать символ '.'";
                            emailBox.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                            error = true;
                        }
                        else
                        {
                            emailBox.ToolTip = null;
                            emailBox.Background = System.Windows.Media.Brushes.Transparent;
                        }
                    }
                }
            }
                       
            if(!error)
            {
                User user = db.Users.Single(b => b.id == UserSaver.user.id);      // найти текущего пользователя по его id и изменить данные
                user.Password = pass;
                user.Email = email;

                if (!String.IsNullOrEmpty(datePicker.SelectedDate?.ToShortDateString()))
                {
                    user.Birthdate = datePicker.SelectedDate?.ToShortDateString();
                }

                if (!String.IsNullOrEmpty(GenderComboBox.Text.Trim()))
                {
                    user.Gender = GenderComboBox.Text.Trim();
                }
                db.SaveChanges();
                MessageBox.Show("Данные обновлены \nВозврат в главное меню", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Exit_Click(sender, e);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Close();
            mainMenu.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}