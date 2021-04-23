using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testMonogame
{
    class KeeseEnemy : IEnemy, ISprite
    {
        Texture2D texture;
        Rectangle destRect;
        public int X { get; set; }
        public int Y { get; set; }
        int health;
        int xRand;
        int yRand;
        int modifier;
        int directionFrame;
        int directionCounter;
        IPlayer player;
        Rectangle playerRect;

        // TODO: GameManager.GetDifficulty();
        int difficulty = 2;

        int playerOffset = 16;
        int trackingCooldown = 0;

        int enemyVel = 3;
        Random randomNumber = new Random();
        int frame = 1;
        const int width = 32;
        const int height = 32;
        Rectangle sourceRect = new Rectangle(17, 0, 16, 16);
        Color color = Color.White;

        Rectangle frame1 = new Rectangle(17, 0, 16, 16);
        Rectangle frame2 = new Rectangle(34, 0, 16, 16);

        public KeeseEnemy(Texture2D inTexture, Vector2 position)
        {
            // Stuff for drawing
            texture = inTexture;
            X = (int)position.X;
            Y = (int)position.Y;
            destRect = new Rectangle(X, Y, width, height);

            // Stuff for state
            health = 1;

            // Stuff for movement
            directionFrame = randomNumber.Next(200);
            directionCounter = 0;
            xRand = randomNumber.Next(enemyVel);
            yRand = randomNumber.Next(enemyVel);
            modifier = randomNumber.Next(enemyVel);
            xRand -= modifier;
            yRand -= modifier;
        }
        public int getHealth() { return health; }
        public Rectangle getDestRect()
        {
            return destRect;
        }
        public void Move()
        {
            // Code for making sure the stalfos is on a solid block and can move will be here
            // TEMP: No walk off screen
            if (directionCounter > directionFrame)
            {
                xRand = randomNumber.Next(enemyVel);
                yRand = randomNumber.Next(enemyVel);
                modifier = randomNumber.Next(enemyVel);
                xRand -= modifier;
                yRand -= modifier;
                directionFrame = randomNumber.Next(200);
                directionCounter = 0;
            }
            else
            {
                directionCounter += 1;
            }

            if (X + xRand < 0 || X + width + xRand > 800) xRand *= -1;
            if (Y + yRand < 0 || Y + height + yRand > 480) yRand *= -1;
            X += xRand;
            Y += yRand;
        }

        public void smartMove()
        {
            // Left and Right
            // Need to go Right
            if (X < playerRect.X + playerOffset)
            {
                X += enemyVel;
            }
            // Need to go Left
            else if (X > playerRect.X + playerOffset)
            {
                X -= enemyVel;
            }

            // Up and Down
            // Need to go Up
            if (Y < playerRect.Y + playerOffset)
            {
                Y += enemyVel;
            }
            // Need to go Down
            else if (Y > playerRect.Y + playerOffset)
            {
                Y -= enemyVel;
            }
        }

        public void Attack(IPlayer player)
        {
            player.TakeDamage(1);
            trackingCooldown = 240;
        }

        public void takeDamage(int dmg)
        {
            health -= dmg;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRect = new Rectangle(X, Y, width, height);
            frame += 1;
            if (frame > 60) frame = 0;
            if (frame < 30)
            {
                sourceRect = frame1;
            }
            else if (frame > 30 && frame < 60)
            {
                sourceRect = frame2;
            }

            spriteBatch.Draw(texture, destRect, sourceRect, color);
        }

        public void Update(GameManager game)
        {
            if (difficulty == 2)
            {
                player = game.getPlayer();
                playerRect = player.getDestRect();
                if (trackingCooldown == 0)
                {
                    smartMove();
                }
                else
                {
                    Move();
                    trackingCooldown -= 1;
                }
            }
            else
            {
                Move();
            }
            // If (enemy collides with player) attack(player)
            // If (playerAttack || playerProjectile collides with self) takeDamage(dmg)
        }

    }
}
