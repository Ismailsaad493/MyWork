using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Delivery_App
{
    public class Restaurant
    {
        public event Action OnOrderReady;
        public void CookFood()
        {
            Console.WriteLine("Kitchen: Cooking the food...Done!");
            OnOrderReady?.Invoke();
        }
    }
}
