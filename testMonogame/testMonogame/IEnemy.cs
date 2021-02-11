using System;
namespace testMonogame
{
    public interface IEnemy
    {
        //Update the Enemy Sprite
        public void Update();
        //Update enemy movements (Maybe use vectors, so will not be void)
        public void Move();
        //Get the HP to see how many more attacks left of the player
        public void getHP();
        //Attack the player
        public void Attack();
    }
}

