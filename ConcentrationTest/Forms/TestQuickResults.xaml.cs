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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConcentrationTest.Forms
{
    /// <summary>
    /// Логика взаимодействия для TestQuickResults.xaml
    /// </summary>
    public partial class TestQuickResults : Window
    {
        //List<TempStat> testTempStats;
        //int interval;


        public TestQuickResults()
        {
            InitializeComponent();
            //interval = 30;

            //bool empty = true;

            //List<Data> Line2 = new List<Data>();


            //Line2.Add(new Data() { Id = 1, Value = 250 });
            //Line2.Add(new Data() { Id = 2, Value = 160 });
            //Line2.Add(new Data() { Id = 3, Value = 123 });
            //Line2.Add(new Data() { Id = 4, Value = 204 });
            //Line2.Add(new Data() { Id = 5, Value = 130 });
            //Line2.Add(new Data() { Id = 6, Value = 124 });

            ////List<Data> Line1 = new List<Data>();
            //MyClass ms = new MyClass();


            //ms.Line1.Add(new Data() { Id = 1, Value = 200 });
            //ms.Line1.Add(new Data() { Id = 2, Value = 150 });
            //ms.Line1.Add(new Data() { Id = 3, Value = 0 });
            //ms.Line1.Add(new Data() { Id = 4, Value = 200 });
            //ms.Line1.Add(new Data() { Id = 5, Value = 150 });
            //ms.Line1.Add(new Data() { Id = 6, Value = 0 });


        }

        public TestQuickResults(List<TempStat> tempStats, int tempStatInterval)
        {
            //InitializeComponent();
            //testTempStats = tempStats;
            //interval = tempStatInterval;

            //bool empty = true;

            //List<Data> lineCharViewed = new List<Data>();
            //List<Data> lineCharRight = new List<Data>();
            //List<Data> lineCharWrong = new List<Data>();

            //foreach (TempStat tempStat in testTempStats)
            //{
            //    if (empty)                                               // если добавляем данные первый раз
            //    {
            //        lineCharViewed.Add(new Data() { value = tempStat.numCharViewed, time = interval });
            //        lineCharRight.Add(new Data() { value = tempStat.numCharRight, time = interval });
            //        lineCharWrong.Add(new Data() { value = tempStat.numCharWrong, time = interval });
            //        empty = false;
            //    }
            //    else
            //    {
            //        lineCharViewed.Add(new Data() { value = lineCharViewed.Last().value + tempStat.numCharViewed, time = lineCharViewed.Last().time + interval });
            //        lineCharRight.Add(new Data() { value = lineCharRight.Last().value + tempStat.numCharRight, time = lineCharRight.Last().time + interval });
            //        lineCharWrong.Add(new Data() { value = lineCharWrong.Last().value + tempStat.numCharWrong, time = lineCharWrong.Last().time + interval });
            //    }
            //}
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }

    //public class MyClass
    //{
    //    public List<Data> Line1 = new List<Data>();
    //}
    //{
    //    public MyClass()
    //{
    //    Line1.Add(new Data() { Id = 1, Value = 200 });
    //    Line1.Add(new Data() { Id = 2, Value = 150 });
    //    Line1.Add(new Data() { Id = 3, Value = 0 });
    //    Line1.Add(new Data() { Id = 4, Value = 200 });
    //    Line1.Add(new Data() { Id = 5, Value = 150 });
    //    Line1.Add(new Data() { Id = 6, Value = 0 });

    //}
    //public List<Data> Line1 { get; set; } = new List<Data>();
    //public class Data
    //    {
    //        public int Value { get; set; }
    //        public int Id { get; set; }
    //    }
   // }

    //public class Data
    //{
    //    public int value { get; set; }
    //    public int time { get; set; }
    //}
}
