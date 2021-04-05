using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace testMonogame
{   
    public class HUD
    {
        public int hudX { get; set; }
        public int hudY { get; set; }


        Texture2D spriteSheet;
        SpriteFont font;

 

        const int hudWidth = 256;
        const int mapWidth = 94;
        const int hudHeight = 45;


        Rectangle mapCoverSource = new Rectangle(0, 79, mapWidth-1, hudHeight);
        Rectangle mainHudSource = new Rectangle(0, 0, hudWidth, hudHeight);
        Rectangle triforceSource = new Rectangle(0, 80, 16, 12);
        Rectangle triforceDest ;
        Rectangle mainHudDest;

        bool mapIsShown;
        bool triforceShown;

        string rupeeAmount;
        string keyAmount;
        string bombAmount;


        //health stuff
        int heartOffsetX;
        int heartOffsetY;
        Rectangle[] heartSource=
        {
            new Rectangle(0,46,7,8),
            new Rectangle(24,46,7,8),
            new Rectangle(16,46,7,8),
            new Rectangle(8,46,7,8),
        };

        int hearts;
        int heartFraction;

        //Item stuff
        Rectangle boomerangSource = new Rectangle(32, 46, 5, 8);
        Rectangle empty = new Rectangle(135, 14, 5, 8);
        Rectangle bombSource = new Rectangle(38, 46, 8, 14);
        Rectangle bowSource = new Rectangle(47, 46, 8, 16);
        Rectangle selectedSourceRect;

        public HUD(Texture2D inSpriteSheet, SpriteFont inFont)
        { 
            hudX = 130;
            hudY = 0;

            triforceDest = new Rectangle(hudX + (5 * 32) + (32 / 4), hudY + (1 * 16), (32 - 5) / 2, 16 - 4);

            mapIsShown = false;
            triforceShown = false;

             heartOffsetX =  183 * 2;
             heartOffsetY =  28 * 2;

            spriteSheet = inSpriteSheet;
            font = inFont;

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mainHudDest= new Rectangle(hudX, hudY, hudWidth*2, hudHeight*2);
            spriteBatch.Draw(spriteSheet, mainHudDest, mainHudSource, Color.White);
             if (!mapIsShown) spriteBatch.Draw(spriteSheet, new Rectangle(hudX, hudY, 94*2, 46*2), mapCoverSource, Color.Black);
            if (triforceShown) spriteBatch.Draw(spriteSheet, triforceDest, triforceSource, Color.Red);

            int heartX = hudX+heartOffsetX;
            int heartY = hudY+heartOffsetY;
            for(int i=1; i <= hearts; i++)
            {
                
                
                //second row
                if (i == 9) {
                    heartX = heartOffsetX;
                    heartY = heartOffsetY - 16;
                }
                //all but the last heart are definitely full
                if (i != hearts) spriteBatch.Draw(spriteSheet, new Rectangle(heartX, heartY, 14, 16), heartSource[0], Color.White);
                //health%4 returns 0 if the final heart is full, 1 if the final heart is 1/4, 2 if final heart is 1/2, and 3 if final heart is 3/4
                else spriteBatch.Draw(spriteSheet, new Rectangle(heartX, heartY, 14, 16), heartSource[heartFraction], Color.White);
                heartX += 16;
            }
            
            if(rupeeAmount!=null)spriteBatch.DrawString(font, rupeeAmount, new Vector2(hudX + 105*2, hudY + 5*2), Color.White);
            if (keyAmount != null) spriteBatch.DrawString(font, keyAmount, new Vector2(hudX + 105*2, hudY + 21*2), Color.White);
            if (bombAmount != null) spriteBatch.DrawString(font, bombAmount, new Vector2(hudX + 105*2, hudY + 29*2), Color.White);

            //draw selected item
            int itemX = hudX+134*2 + (14-selectedSourceRect.Width);
            int itemY = hudY+14*2 + (18 - selectedSourceRect.Height);
            //int itemX = 0;
            //int itemY = 0;
            spriteBatch.Draw(spriteSheet, new Rectangle(itemX, itemY, selectedSourceRect.Width*2, selectedSourceRect.Height*2),selectedSourceRect,Color.White);



        }
        public void Update(GameManager game)
        {
            IPlayer p = game.getPlayer();
            if (!mapIsShown && p.Map) mapIsShown = true;
            if (!triforceShown && p.Compass) triforceShown = true;

            int healthTotal = p.GetHP();
            hearts = (int)Math.Ceiling(healthTotal / 4.0);
            heartFraction=healthTotal % 4;
           // Debug.WriteLine(healthTotal);


            rupeeAmount = "X" + p.Rupees.ToString();
            keyAmount = "X" + p.Keys.ToString();
            bombAmount = "X" + p.Bombs.ToString();

            String selected = p.GetSelectedItem();
            //Debug.WriteLine(selected);
            switch (selected)
            {
                case "Bomb":
                    selectedSourceRect = bombSource;
                    break;
                case "Boomerang":
                    selectedSourceRect = boomerangSource;
                    break;
                case "Bow":
                    selectedSourceRect = bowSource;
                    break;
                default:
                    selectedSourceRect = empty;
                    break;
            }
        }





    }
}
