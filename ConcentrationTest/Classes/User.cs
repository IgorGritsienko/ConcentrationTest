using System.Linq;

namespace ConcentrationTest
{
    // имеется связь с бд
    public class User
    {
        public int id { get; set; }

        private string login;
        private string password;
        private string email;
        private string birthdate;
        private string gender;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }
        public string Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public User() { }

        public User(string login, string password, string email, string birthdate, string gender)
        {
            this.login = login;
            this.password = password;
            this.email = email;
            this.birthdate = birthdate;
            this.gender = gender;
        }

        public User GetCurrentUserInfo(AppContext db, int id)
        {
            User user = db.Users.FirstOrDefault(b => b.id == id);      // берем всю информацию о пользователе с таким логином
            return user;
        }
    }

    // нужен для сохранения данных о пользователе при авторизации и последующей работы с приложением
    class UserSaver
    {
        public static User user;
    }
}