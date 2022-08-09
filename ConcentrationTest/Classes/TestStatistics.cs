using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcentrationTest
{
    public class TestStatistics                     // класс для добавления записей в таблицу результатов после прохождения тестирования
    {
        public int viewed { get; set; }
        public int right { get; set; }
        public int wrong { get; set; }
        public int time { get; set; }

        public TestStatistics(int viewed, int right, int wrong, int time)
        {
            this.viewed = viewed;
            this.right = right;
            this.wrong = wrong;
            this.time = time;
        }
    }
}
