using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IPlayerState
    {
        
        //call sprite.draw
        public void Draw(SpriteBatch spriteBatch);
        public Rectangle getDestRect();

        //move according to the state
        public void Move();
        //use Item
        public void PlaceItem();
        //Attack
        public void Attack();
        //items
        public void spawnBomb(Game1 game);
        public void spawnArrow(Game1 game);
        public void spawnBoomerang(Game1 game);
        public void spawnSwordProjectile(Game1 game);
        public int getX();
        public int getY();
        public void setMoving(bool moving);
        public bool isMoving();
        public void damage();
        //makes untouchable and limits actions
        public void setStasis(bool stasisIn);
        public bool getStasis();
        public void Update(Game1 game);
    }
}
