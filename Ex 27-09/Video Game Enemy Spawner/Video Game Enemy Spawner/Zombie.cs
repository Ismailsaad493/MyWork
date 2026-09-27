using System;
using System.Collections.Generic;
using System.Text;

namespace Video_Game_Enemy_Spawner
{
    public class Zombie : Enemy
    {
        public Zombie(string name, int damage)
            :base(name, damage)
        {
        }
        public override void Attack()
        {
            Console.WriteLine("Groans and bites the player!");
        }
    }
}
