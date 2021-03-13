using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class PlayerWallCollision
    {
        Rectangle playerRect;
        Rectangle wallRect;
        
        
        int xCollisionSize;
        int yCollisionSize;
        public void detectCollision(IPlayer player, Rectangle walls, Rectangle floor)
        {
            
            playerRect = player.getDestRect();
            wallRect = walls;
            Rectangle wallCollision = Rectangle.Intersect(playerRect, walls);
            Rectangle floorCollision = Rectangle.Intersect(playerRect, floor);
            //for true collision x and y are arbitrary, width and height matter
            Rectangle trueCollision = new Rectangle(wallCollision.X, wallCollision.Y, floorCollision.Width - wallCollision.Width, floorCollision.Height - wallCollision.Height);
            if (!wallCollision.Equals(floorCollision)) handleCollision(trueCollision,player) ;
        }

        public void handleCollision(Rectangle collisionRect, IPlayer player) 
        {
            xCollisionSize = collisionRect.Width;
            yCollisionSize = collisionRect.Height;
            // We are in a Top / Bottom style collision
            if (xCollisionSize > yCollisionSize)
            {
                // IF the bottom left corner of our enemy is less than the the top left corner of the block
                // AND the bottom left corner of our enemy is greater than the half way point of the height of the block
                // We are on the top
                if (playerRect.Y < wallRect.Y+(wallRect.Height/2))
                {
                    player.Y -= yCollisionSize;
                }
                else // We are on the bottom
                {
                    player.Y += yCollisionSize;
                }
            }
            else // We are in a Left / Right style collision
            {
                // IF the top left corner of our enemy is less than the top right corner of the block
                // AND the top left corner of our enemy is greater than the half way point of the width of the block
                // We are on the left
                if (playerRect.X < wallRect.X + (wallRect.Width / 2))
                {
                    player.X -= xCollisionSize;
                }
                else // We are on the right
                {
                    player.X += xCollisionSize;
                }
            }
        }
    }
}
