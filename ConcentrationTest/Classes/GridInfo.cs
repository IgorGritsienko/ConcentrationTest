using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcentrationTest
{
    public class UserResultsHistory                                       // класс для вывода информации в таблице в форме StatsForm
    {
        public double attentionConcentration { get; set; }          // концентрация внимания
        public double attentionSpan { get; set; }                   // устойчивость внимания
        public string date { get; set; }                            // дата прохождения

        public UserResultsHistory() { }

        public UserResultsHistory(double attentionConcentration, double attentionSpan, string date)
        {
            this.attentionConcentration = attentionConcentration;
            this.attentionSpan = attentionSpan;
            this.date = date;
        }
    }

    public class TestStatistics                     // класс для добавления записей в таблицу результатов после прохождения тестирования
    {
        public int viewed { get; set; }
        public int right { get; set; }
        public int wrong { get; set; }
        public int time { get; set; }

        public TestStatistics() { }

        public TestStatistics(int viewed, int right, int wrong, int time)
        {
            this.viewed = viewed;
            this.right = right;
            this.wrong = wrong;
            this.time = time;
        }
    }
}