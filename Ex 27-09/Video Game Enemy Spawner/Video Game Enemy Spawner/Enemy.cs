using System;
using System.Collections.Generic;
using System.Text;

namespace Video_Game_Enemy_Spawner
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Damage { get; set; }

        public Enemy(string name, int damage)
        {
            this.Name = name;
            this.Damage = damage;
        }

        public virtual void Attack()
        {
            Console.WriteLine("The generic enemy attacks the player!"); 
        }
    }
}
