using System;
using System.Collections.Generic;
using System.Text;

namespace Video_Game_Health_System
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }


        public Player(string name, int health)
        {
            this.Name = name;
            this.Health = health;
        }

        public event Action OnDeath;

        public void TakeDamage(int damage)
        {
            Health = Health - damage;
            Console.WriteLine(Name + "took" + damage + "damage! Health is now" + Health);

            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
      




    }
}
