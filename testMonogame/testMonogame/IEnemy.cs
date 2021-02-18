using System;
namespace testMonogame
{
    public interface IEnemy
    {
        //Update enemy movements (Maybe use vectors, so will not be void)
        public void Move();
        //Attack the player
        public void Attack(IPlayer player);
        // Take damage from player
        public void takeDamage(int dmg);
    }
}

