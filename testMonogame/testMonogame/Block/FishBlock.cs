﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class FishBlock : IObject, ISprite
    {

        Texture2D texture;
        Rectangle destRect;
        int x;
        int y;

        Rectangle sourceRect = new Rectangle(34, 0, 16, 16);
        Color color = Color.White;

        public FishBlock(Texture2D incTexture, Vector2 pos)
        {
            texture = incTexture;
            x = (int)pos.X;
            y = (int)pos.Y;
            destRect = new Rectangle(x, y, 32, 32);
        }

        public void Update(Game1 game)
        {
            // COLLISION WILL GO HERE
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Interact(IPlayer player)
        {
            // NULL
        }
    }
}