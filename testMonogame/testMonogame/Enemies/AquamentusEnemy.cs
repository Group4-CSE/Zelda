using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class AquamentusEnemy : IEnemy, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int health;

        int maxHealth = 6;


        //drawing stuff

        const int width = 48;
        const int height = 64;

        Color color = Color.White;
        LinkedList<Rectangle> frames = new LinkedList<Rectangle>();
        LinkedList<Rectangle> hurtFrames = new LinkedList<Rectangle>();
        LinkedListNode<Rectangle> currentFrame;
        int frameDelay = 20;
        int frameWait;
        int hurtFlash;

        /*
         *Rectangle frame1 = new Rectangle(0, 0, 24, 32);
         * Rectangle frame2 = new Rectangle(25, 0, 24, 32);
         *Rectangle frame3 = new Rectangle(50, 0, 24, 32);
         *Rectangle frame4 = new Rectangle(75, 0, 24, 32);
         * 
         * 
         * 
         */ 


        //movement stuff
        int xVel; //doesnt have y velocity
        int directionWait;
        int directionStall = 10; //how many updates until we change direction
        int moveDelay = 6;
        int moveDelayCount;

        int fireDelay = 240;
        int fireDelayCount;
        public  AquamentusEnemy(Texture2D inTexture, Vector2 position)
        {
            // Stuff for drawing
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y; 
            frameWait = 0;
            frames.AddLast(new Rectangle(0, 0, 24, 32));
            frames.AddLast(new Rectangle(25, 0, 24, 32));
            frames.AddLast(new Rectangle(50, 0, 24, 32));
            frames.AddLast(new Rectangle(75, 0, 24, 32));
            hurtFrames.AddLast(new Rectangle(0, 33, 24, 32));
            hurtFrames.AddLast(new Rectangle(25, 33, 24, 32));
            hurtFrames.AddLast(new Rectangle(50, 33, 24, 32));
            currentFrame = frames.First;
            hurtFlash = 0;


            // Stuff for state
            health = maxHealth;


            xVel = 2;
            directionWait = 0;
            moveDelayCount = 0;

            
        }

        public void Attack(IPlayer player)
        {
            //Deal damage if you make contact
            player.TakeDamage(1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, width, height);
            frameWait++;
            if (frameWait >= frameDelay) {
                if (hurtFlash == 0)
                {
                    if (currentFrame.Next != null) currentFrame = currentFrame.Next;
                    else currentFrame = frames.First;
                }
                else
                {
                    if (currentFrame.Next != null) currentFrame = currentFrame.Next;
                    else
                    {
                        //loop through hurt frames, hurtflash number of times
                        currentFrame = hurtFrames.First;
                        hurtFlash--;
                    }
                }
                frameWait = 0;
            }
            //source rectangle is the frame that we are currently on in the list
            spriteBatch.Draw(texture, destRect, currentFrame.Value, color); 



        }

        public void Move()
        {
            directionWait++;
            if (directionWait > directionStall)
            {
                xVel *= -1;
                directionWait = 0;
            }
            X += xVel;
        }

        public void takeDamage(int dmg)
        {
            //if invulnerable during hurtflash then put all this inside if(hurtflash==0)
            health -= dmg;
            hurtFlash = 3;
            currentFrame = hurtFrames.First;
            //flash colors
        }
        void spawnFireBalls(Game1 game)
        {
            game.AddEnemyProjectile((ISprite)new FireBallEnemyProjectile(texture, new Vector2(X + width / 4, Y + height / 4), new Vector2(-1, 1)));
            game.AddEnemyProjectile((ISprite)new FireBallEnemyProjectile(texture, new Vector2(X + width / 4, Y + height / 4), new Vector2(-1, 0)));
            game.AddEnemyProjectile((ISprite)new FireBallEnemyProjectile(texture, new Vector2(X + width / 4, Y + height / 4), new Vector2(-1, -1)));

        }
        public void Update(Game1 game)
        {
            //slow movement since aquamentus rocks back and forth slowly
            moveDelayCount++;
            if (moveDelayCount > moveDelay)
            {
                Move();
                moveDelayCount = 0;
            }
            fireDelayCount++;
            if (fireDelayCount > fireDelay)
            {
                spawnFireBalls(game);
                fireDelayCount = 0;
            }

            //add collision and projectile stuff
        }
    }
}
