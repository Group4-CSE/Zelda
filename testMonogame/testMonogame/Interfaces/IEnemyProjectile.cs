using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IEnemyProjectile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public void Move();
        public void doDamage(IPlayer player);
        public void delete(Game1 game);
        //returns the destination rectangle of the sprite associated with this entity
        public Rectangle getDestRect();
        //Draw
        public void Draw(SpriteBatch spriteBatch);
        //Update
        public void Update(Game1 game);
    }
}
