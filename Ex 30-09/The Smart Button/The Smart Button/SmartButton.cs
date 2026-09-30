using System;
using System.Collections.Generic;
using System.Text;

namespace The_Smart_Button
{
    public class SmartButton
    {
        public event Action OnClick;

        public void Press()
        {
            Console.WriteLine("Button was physically pressed down.");
            OnClick?.Invoke();
        }
    }
}
