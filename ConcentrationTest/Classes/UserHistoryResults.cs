using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcentrationTest
{
    public class UserResultsHistory                                 // класс для вывода информации в таблице в форме StatsForm
    {
        public double attentionConcentration { get; set; }          // концентрация внимания
        public double attentionSpan { get; set; }                   // устойчивость внимания
        public string date { get; set; }                            // дата прохождения

        public UserResultsHistory(double attentionConcentration, double attentionSpan, string date)
        {
            this.attentionConcentration = attentionConcentration;
            this.attentionSpan = attentionSpan;
            this.date = date;
        }
    }
}
