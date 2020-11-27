using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConcentrationTest
{
    public class CustomButton : Button
    {
        public enum State { pressed, unpressed };   // состояние кнопки
               
        public State state = State.unpressed;

        public CustomButton() { }
    }
}