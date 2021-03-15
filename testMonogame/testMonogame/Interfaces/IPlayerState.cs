using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public interface IPlayerState
    {
        public void SetDamaged(int framesRemaining);
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
        public void spawnBomb(GameManager game);
        public void spawnArrow(GameManager game);
        public void spawnBoomerang(GameManager game);
        public void spawnSwordProjectile(GameManager game);
        public int getX();
        public int getY();
        public void setMoving(bool moving);
        public bool isMoving();
        public void damage();
        //makes untouchable and limits actions
        public void setStasis(bool stasisIn);
        public bool getStasis();
        public void Update(GameManager game);
    }
}
