using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace ConcentrationTest
{
    /// <summary>
    /// Логика взаимодействия для RegForm.xaml
    /// </summary>
    public partial class RegForm : Window
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void ButtonRegClick(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();
            string email = emailBox.Text.Trim().ToLower();
            string birthdate = datePicker.SelectedDate?.ToShortDateString() ?? "";
            string gender = GenderComboBox.Text.Trim();

            bool error = false;

            using (AppContext db = new AppContext())
            {
                if (login.Length < 3)
                {
                    textBoxLogin.ToolTip = "Логин должен быть не меньше 3 символов";
                    textBoxLogin.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                    error = true;
                }
                else
                {
                    if (db.Users.FirstOrDefault(p => p.Login == login) != null)     // если логин в бд уже есть
                    {
                        textBoxLogin.ToolTip = "Данный логин уже существует";
                        textBoxLogin.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                        error = true;
                    }
                    else
                    {
                        textBoxLogin.ToolTip = null;
                        textBoxLogin.Background = System.Windows.Media.Brushes.Transparent;
                    }
                }

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
                }

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

                if (String.IsNullOrEmpty(birthdate))
                {
                    datePicker.ToolTip = "Выберите дату рождения";
                    datePicker.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                    error = true;
                }
                else
                {
                    datePicker.ToolTip = null;
                    datePicker.Background = System.Windows.Media.Brushes.Transparent;
                }

                if (String.IsNullOrEmpty(gender))
                {
                    GenderComboBox.ToolTip = "Выберите свой пол";
                    GenderComboBox.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFB6C1");
                    error = true;
                }
                else
                {
                    GenderComboBox.ToolTip = null;
                    GenderComboBox.Background = System.Windows.Media.Brushes.Transparent;
                }
                if (!error)                                                                 // если нет ошибок ввода, то добавить пользователя в бд
                {
                    User user = new User(login, pass, email, birthdate, gender);
                    db.Users.Add(user);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно.", "Выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void RedirectToAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthForm authForm = new AuthForm();
            Close();
            authForm.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
