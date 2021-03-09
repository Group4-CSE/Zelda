using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace testMonogame
{
    public interface IEnemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        //Update enemy movements (Maybe use vectors, so will not be void)
        public void Move();
        //Attack the player
        public void Attack(IPlayer player);
        // Take damage from player
        public void takeDamage(int dmg);
        //returns the destination rectangle of the sprite associated with this entity
        public Rectangle getDestRect();
        //Draw
        public void Draw(SpriteBatch spriteBatch);
        //Update
        public void Update(Game1 game);

    }
}

