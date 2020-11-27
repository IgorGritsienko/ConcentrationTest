using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcentrationTest
{
    public class Stat       // связь с бд
    {
        public int id { get; set; }
        public int userId { get; set; }
        public double attentionConcentration { get; set; }   // концентрация внимания
        public double attentionSpan { get; set; }            // устойчивость внимания
        public string testDate { get; set; }

        public Stat() { }

        public Stat(int userId, double attentionConcentration, double attentionSpan, string testDate)
        {
            this.userId = userId;
            this.attentionConcentration = attentionConcentration;
            this.attentionSpan = attentionSpan;
            this.testDate = testDate;
        }

        public void SaveStat()
        {
            using (AppContext db = new AppContext())
            {
                if (double.IsNaN(attentionConcentration) || double.IsNaN(attentionSpan))    // если не число, то не сохраняем
                    return;
                else
                {
                    db.Stats.Add(this);
                    db.SaveChanges();
                }
            }
        }

        public List<Stat> GetUserResults(AppContext db, int id)
        {
            List<Stat> userStats = db.Stats.Where(b => b.userId == id).ToList();   // вычленить все прошлые результаты для этого пользователя
            return userStats;
        }

        public (double K, double A) AgregateData(AppContext db, string sex, int minAge, int maxAge)
        {
            List<User> usersFilteredSex;

            if (sex == "None")
            {
                usersFilteredSex = db.Users.ToList();                       // если пол не указан, берем весь список
            }
            else
            {
                usersFilteredSex = db.Users.Where(b => b.Gender == sex).ToList();   // иначе отбираем по полу
            }

            List<int> usersToCount = new List<int>();                       // список конечных пользователей, у которых нужно брать статистику

            foreach (User user in usersFilteredSex)
            {              
                DateTime dateNow = DateTime.Now;                            // текущее время
                DateTime birthdate = Convert.ToDateTime(user.Birthdate);    // дата рождения
                birthdate = birthdate.AddDays(1);

                DateTime zeroTime = new DateTime(1, 1, 1);

                TimeSpan span = dateNow - birthdate;                        // вычисляем разность в днях
                int year = (zeroTime + span).Year - 1;                      // с помощью этого получаем разность в годах (с учетом месяцев и дней)
            
                if (minAge <= year && year <= maxAge)                       // если подходит под условие, то добавляем в список для дальнейшей работы
                {
                    usersToCount.Add(user.id);
                }
            }

            List<Stat> usersStats = new List<Stat>();

            foreach (int id in usersToCount)                                // ищем по id из списка статистику пользователей и добавляем ее в другой список
            {
                List<Stat> tmp = db.Stats.Where(b => b.userId == id).ToList();
                usersStats.AddRange(tmp);                                   // добавить все подходящие записи от одного пользователя
            }

            double A = 0, K = 0;

            foreach (Stat stat in usersStats)                               // складываем статистику каждого пользователя
            {
                K += stat.attentionConcentration;                           // концентрация внимания
                A += stat.attentionSpan;                                    // устойчивость внимания
            }

            K /= usersStats.Count;                                          // делим на количество элементов, чтобы получить среднее
            A /= usersStats.Count;

            return (K, A);
        }   //  вычислить средние данные по указанной группе
    }
}