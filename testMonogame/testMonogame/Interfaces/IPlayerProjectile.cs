using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IPlayerProjectile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public void Move();
        public void doDamage(IEnemy target);
        public void delete(GameManager game);
        public void collide(GameManager game);
        //returns the destination rectangle of the sprite associated with this entity
        public Rectangle getDestRect();
        //Draw
        public void Draw(SpriteBatch spriteBatch);
        //Update
        public void Update(GameManager game);

    }
}
