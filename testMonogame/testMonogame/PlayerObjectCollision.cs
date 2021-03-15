using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using testMonogame.Interfaces;
using testMonogame.Rooms;

namespace testMonogame
{
    public class PlayerObjectCollision
    {

        Rectangle playerRect;
        Rectangle objectRect;
        Rectangle collisionRect;

        const int height = 16;
        const int width = 16;

        public void detectCollision(IPlayer player, List<IObject> items, List<IObject> blocks,IRoom r)
        {


            playerRect = player.getDestRect();
            bool isIgnored;
            foreach (IObject block in blocks)
            {
                isIgnored = false;
                objectRect = block.getDestRect();
                //playerRect = player.getDestRect();
                collisionRect = Rectangle.Intersect(playerRect, objectRect);
                if (block is BlueSandBlock || block is DragonBlock || block is FishBlock) isIgnored = true;
                //if they've collided
                if (!collisionRect.IsEmpty & !isIgnored)
                {
                    //generate collision rectangle
                    blockCollisionHandler(collisionRect, player, block);
                }
            }
            IObject[] itemArr = items.ToArray();
            foreach (IObject item in itemArr)
            {
                isIgnored = false;
                objectRect = item.getDestRect();
                //playerRect = player.getDestRect();
                collisionRect = Rectangle.Intersect(playerRect, objectRect);
                //if they've collided
                if (!collisionRect.IsEmpty & !isIgnored)
                {
                    //generate collision rectangle
                    itemCollisionHandler(collisionRect, player, item,r);
                }
            }
        }
        public void itemCollisionHandler(Rectangle collisionRect, IPlayer player, IObject collided,IRoom room)
        {
            collided.Interact(player);
            room.RemoveItem(collided);

        }
        public void blockCollisionHandler(Rectangle collisionRect, IPlayer player, IObject collided)
        {
            collided.Interact(player);
            int x = collisionRect.Width;
            int y = collisionRect.Height;
            if (x > y)
            {
                //if player is above the collided thing
                if (player.Y < collided.Y)
                {
                    player.Y -= y;
                }
                //otherwise player is below object
                else
                {
                    player.Y += y;
                }
            }
            else
            {
                if (player.X < collided.X)
                {
                    player.X -= x;
                }
                //otherwise player is below object
                else
                {
                    player.X += x;
                }
            }
           
        }
    }
}
