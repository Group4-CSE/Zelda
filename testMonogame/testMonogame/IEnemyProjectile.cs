using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    interface IEnemyProjectile
    {
        public void Move();
        public void doDamage(IPlayer player);
        public void delete();
    }
}
