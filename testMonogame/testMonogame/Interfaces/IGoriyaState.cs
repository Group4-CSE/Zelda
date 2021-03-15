using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    interface IGoriyaState
    {
        public void Attack(IPlayer player);
        public void Move();
        public void takeDamage(int dmg);
        public void Draw(SpriteBatch spriteBatch);
        public void Update(GameManager game);
        public void spawnBoomerang(GameManager game);
        public Rectangle getDestRect();
    }
}
