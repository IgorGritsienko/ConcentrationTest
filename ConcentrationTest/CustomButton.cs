using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConcentrationTest
{
    public class CustomButton : Button
    {
        public enum State { pressed, unpressed };   // состояние кнопки

        public Button button;

        public State state;

        public CustomButton()
        {
            button = new Button();
            state = State.unpressed;
        }
    }
}