using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IEnemyProjectile
    {
        public void Move();
        public void doDamage(IPlayer player);
        public void delete(Game1 game);
    }
}
