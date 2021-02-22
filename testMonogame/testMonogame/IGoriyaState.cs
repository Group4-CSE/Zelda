using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    interface IGoriyaState
    {
        public void Attack(IPlayer player);
        public void Move();
        public void takeDamage(int dmg);
    }
}
