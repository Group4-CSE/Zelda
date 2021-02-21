using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IPlayerProjectile
    {
        public void Move();
        public void doDamage(IEnemy target);
        public void delete(Game1 game);
    }
}
