using System;
using System.Collections.Generic;
using System.Text;

namespace Bug_Smasher_Leaderboard
{
    public class Developer
    {
        public string Name { get; set; }
        public int BugsFixed { get; set; }
        public bool IsActiveEmployee { get; set; }
        public Developer(string name, int bugsfixed, bool isactivemployee)
        {
            this.Name = name;
            this.BugsFixed = bugsfixed;
            this.IsActiveEmployee = isactivemployee;
       }
    }
}
