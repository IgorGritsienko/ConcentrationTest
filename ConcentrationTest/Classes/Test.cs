using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ConcentrationTest
{
    public class Test
    {
        public int maxViewedRow = 0;              // запоминаем номер строки и столбца - ячейки, на которую пользователь нажал в последний раз;
        public int maxViewedColumn = 0;           // используется, чтобы запомнить место, до которого дошел пользователь, в случаях для подсчета временной статистики
                                                  // или истечения времени теста

        private int tempStatInterval = 10;                // интервал сбора статистики
        public int countdown;                            // обратный отсчет времени теста
        public int initialTime = 31;                     // начальное время

        public TestData testData = new TestData();
        public int numberToFind;

        public DispatcherTimer tempStatTimer = new System.Windows.Threading.DispatcherTimer(DispatcherPriority.Normal);      // создание таймера для временной статистики

        public List<TempStat> tempStats = new List<TempStat>();        // список для хранения временной статистики

        public Test()
        {
            numberToFind = testData.NumberToFind();
        }

        public void TempStatTimer_Tick(object sender, EventArgs e)             // каждый тик происходит вычисление статистики
        {
            Test tempTest = new Test();

            var results = tempTest.ComputeTempResults(testData.testGrid, numberToFind, maxViewedRow, maxViewedColumn);

            TempStat tempStat = new TempStat();
            tempStat.numCharViewed = results.C;
            tempStat.numCharRight = results.PC;
            tempStat.numCharWrong = results.OC;
            tempStat.time = initialTime - countdown + 1;
            tempStats.Add(tempStat);
        }

        public void InitTimer(Grid grid, int numberToFind, object sender, EventArgs e)
        {
            tempStatTimer.Tick += new EventHandler(TempStatTimer_Tick); 
            tempStatTimer.Interval = new TimeSpan(0, 0, 0, tempStatInterval);                 // единицы измерение (сек)
        }

        #region

        private bool isMissedOrMarkedWrong(Button button, int numberToFind)
        {
            //if (button.state == CustomButton.State.pressed && (int)button.button.Content != numberToFind ||
            //    button.state == CustomButton.State.unpressed && (int)button.button.Content == numberToFind)

            if (button.Background == Brushes.Orange && (int)button.Content != numberToFind ||
            button.Background == Brushes.Transparent && (int)button.Content == numberToFind)

            {
                return true;
            }
            else
                return false;
        }

        public (double K, double A) ComputeFinishedResults(Grid grid, int numberToFind, int initialTime, int countdown, int cellCount)                              // если пользователь закончил тест и нажал на кнопку готово
        {
            int OC = 0;     // количество ошибочно отмеченных и пропущенных символов
            int PC = 0;     // количество правильно просмотренных символов
            double K;           // концентрация внимания

            int C = cellCount;      // общее количество просмотренных символов
            int t;      // время тестирования
            double A;           // устойчивость внимания

            foreach (Button btn in grid.Children)
            {
                if (isMissedOrMarkedWrong(btn, numberToFind))           // если пропущено нужное число или отмечено ненужное
                {
                    OC++;
                }
                else
                {
                    PC++;
                }
            }

            t = initialTime - countdown;
            A = C / (double)t;
            K = (PC - OC) * 100.0 / (PC + OC);

            return (K, A);
        }

        public (double K, double A) ComputeUnfinishedResults(Grid grid, int numberToFind, int maxViewedRow, int maxViewedColumn, int initialTime)          // если пользователь не успел закончить тест за предоставленное время
        {
            int OC = 0;     // количество ошибочно отмеченных и пропущенных символов
            int PC = 0;     // количество правильно просмотренных символов
            double K;           // концентрация внимания


            int C;      // общее количество просмотренных символов
            int t = initialTime;      // время тестирования
            double A;           // устойчивость внимания

            foreach (Button btn in grid.Children)       // перебираем все кнопки до последней отмеченной, проверяя правильность и неправильность отмеченных
            {
                if ((int)btn.GetValue(Grid.RowProperty) < maxViewedRow)                          // если не дошли до последней строчки
                {                                                                                       // второй вариант: Grid.GetRow(btn) != ...
                    if (isMissedOrMarkedWrong(btn, numberToFind))                               // если пропущено нужное число или отмечено ненужное
                    {
                        OC++;
                    }
                    else
                    {
                        PC++;
                    }
                }
                else if ((int)btn.GetValue(Grid.RowProperty) == maxViewedRow)                   // если дошли до последней строчки, то проверяем, до какого столбца дошли
                {
                    if ((int)btn.GetValue(Grid.ColumnProperty) + 1 <= maxViewedColumn)
                    {
                        if (isMissedOrMarkedWrong(btn, numberToFind))
                        {
                            OC++;
                        }
                        else
                        {
                            PC++;
                        }
                    }
                }
            }

            C = (maxViewedRow) * grid.ColumnDefinitions.Count + maxViewedColumn;

            A = C / (double)t;
            K = (PC - OC) * 100.0 / (PC + OC);
            return (K, A);
        }

        public (int C, int PC, int OC) ComputeTempResults(Grid grid, int numberToFind, int maxViewedRow, int maxViewedColumn)
        {
            int OC = 0;     // количество ошибочно отмеченных и пропущенных символов
            int PC = 0;     // количество правильно просмотренных символов

            int C = (maxViewedRow) * grid.ColumnDefinitions.Count + maxViewedColumn + 1;      // общее количество просмотренных символов

            foreach (Button btn in grid.Children)       // перебираем все кнопки до последней отмеченной, проверяя правильность и неправильность отмеченных
            {
                if ((int)btn.GetValue(Grid.RowProperty) < maxViewedRow)
                {
                    if (isMissedOrMarkedWrong(btn, numberToFind))
                    {
                        OC++;
                    }
                    else
                    {
                        PC++;
                    }
                }
                else if ((int)btn.GetValue(Grid.RowProperty) == maxViewedRow)                   // если дошли до последней строчки, то проверяем, до какого столбца дошли
                {
                    if ((int)btn.GetValue(Grid.ColumnProperty) <= maxViewedColumn)
                    {
                        if (isMissedOrMarkedWrong(btn, numberToFind))
                        {
                            OC++;
                        }
                        else
                        {
                            PC++;
                        }
                    }
                }
                else
                    break;
            }
            return (C, PC, OC);
        }

        #endregion

        public class TestData
        {
            public Grid testGrid;
            public Random rand = new Random();

            public int numCols = 35;
            public int numRows = 35;
            public int cellCount;

            public TestData()
            {
                cellCount = numCols * numRows;
            }

            public int NumberToFind()                // запоминаем число, которое пользователю необходимо искать
            {
                return rand.Next(0, 10);
            }
        }
    }

    public class TempStat                       // используется для хранения временной статистики
    {
        public int numCharViewed;
        public int numCharRight;
        public int numCharWrong;
        public int time;

        public TempStat() { }

        public TempStat(int numCharViewed, int numCharRight, int numCharWrong, int time)
        {
            this.numCharViewed = numCharViewed;
            this.numCharRight = numCharRight;
            this.numCharWrong = numCharWrong;
            this.time = time;
        }
    }
}