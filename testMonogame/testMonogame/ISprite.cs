using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    interface ISprite
    {
        public void Draw(SpriteBatch spriteBatch);
        public void Update(Game1 game);
    }
}
