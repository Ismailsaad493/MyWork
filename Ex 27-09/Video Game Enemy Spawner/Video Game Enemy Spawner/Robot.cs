using System;
using System.Collections.Generic;
using System.Text;

namespace Video_Game_Enemy_Spawner
{
    public class Robot : Enemy
    {
        public Robot(string name, int damage)
            : base(name, damage)
        {}
    public override void Attack()
        {
            Console.WriteLine("Fires a laser beam at the player!");
        }
    }
}
