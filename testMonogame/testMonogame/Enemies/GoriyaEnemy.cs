using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    class GoriyaEnemy : ISprite, IEnemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        Random randomNumber = new Random();
        int directionCounter;
        int directionFrame;
        int direction;
        int health;
        public IGoriyaState state;
        Texture2D texture;
        Texture2D projTexture;
        bool throwing;
        int throwCounter;

        public GoriyaEnemy(Texture2D inTexture, Texture2D inProjTexture, Vector2 position)
        {
            texture = inTexture;
            projTexture = inProjTexture;
            state = new GoriyaWL(texture, projTexture, this);
            health = 3;
            X = (int)position.X;
            Y = (int)position.Y;
            directionCounter = 0;
            directionFrame = randomNumber.Next(200);
            direction = randomNumber.Next(1, 4);
        }
        public int getHealth() { return health; }
        public void Move(int xChange, int yChange)
        {
            X += xChange * (int)GameplayConstants.ENEMY_SPEED_MODIFIER;
            Y += yChange * (int)GameplayConstants.ENEMY_SPEED_MODIFIER;
            directionCounter += 1;
            if (directionCounter > directionFrame)
            {
                directionCounter = 0;
                directionFrame = randomNumber.Next(200);
                direction = randomNumber.Next(1, 4);
                changeState(direction);
            }
        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Attack(IPlayer player)
        {
            player.TakeDamage(2);
        }

        public void setThrow(bool isThrow)
        {
            throwing = isThrow;
        }

        public bool getThrow()
        {
            return throwing;
        }

        public void changeState(int direction)
        {
            /*
             * 1 = Down
             * 2 = Up
             * 4 = Right
             * 3 = Left
             * Default = Down
             */
            switch (direction)
            {
                case 1:
                    state = new GoriyaWD(texture, projTexture, this);
                    break;
                case 2:
                    state = new GoriyaWU(texture, projTexture, this);
                    break;
                case 3:
                    state = new GoriyaWL(texture, projTexture, this);
                    break;
                case 4:
                    state = new GoriyaWR(texture, projTexture, this);
                    break;
                default:
                    state = new GoriyaWD(texture, projTexture, this);
                    break;
            }
        }

        //public int getX()
        //{
        //    return x;
        //}

        //public int getY()
        //{
        //    return y;
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch);
        }

        public void Update(GameManager game)
        {
            throwCounter += 1;
            if (throwCounter > 600)
            {
                setThrow(true);
                state.spawnBoomerang(game);
                throwCounter = 0;
            }
            state.Update(game);
        }

        public void Move()
        {
            //Do Nothing, movement is handled by the other move method recieving values that are passed in by states
        }

        public Rectangle getDestRect()
        {
            return state.getDestRect();
        }
    }
}
