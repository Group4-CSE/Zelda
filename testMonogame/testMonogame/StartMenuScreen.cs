using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    class StartMenuScreen
    {
        Texture2D texture;
        SpriteFont header;
        SpriteFont font;

        GameManager game;

        //a bunch of rectangles for drawing. 
        Rectangle background = new Rectangle(0, 0, 256, 175);
        Rectangle destRect = new Rectangle(130, 100, 256 * 2, 175 * 2);
        Rectangle boxRectangle = new Rectangle (0, 181, 64, 20);
        Rectangle difficultyDestRect = new Rectangle(130 + (96 * 2), 100 + (24 * 2), 128, 40);
        Rectangle modeDestRect = new Rectangle(130 + (96 * 2), 100 + (56 * 2), 128, 40);
        Rectangle playDestRect = new Rectangle(130 + (96 * 2), 100 + (88 * 2), 128, 40);
        Rectangle selectionSourceRect = new Rectangle(72, 182, 68, 24);
        Rectangle[] selectionDestRect =
        {
            new Rectangle(130 + (94 * 2), 100 + (22 * 2), 128+8, 40+8),
            new Rectangle(130 + (94 * 2), 100 + (54 * 2), 128+8, 40+8),
            new Rectangle(130 + (94 * 2), 100 + (86 * 2), 128+8, 40+8)
    };
        int selectedBox;//0=diff,1=mode,2=play



        int buttonX = 365;
        int difficultyY = 160;
        Vector2 modeLocation = new Vector2(360, 224);
        Vector2 playLocation = new Vector2(365, 288);
        public StartMenuScreen(Texture2D intexture, SpriteFont smallFont, SpriteFont bigFont, GameManager inGame)
        {
            texture = intexture;
            font = smallFont;
            header = bigFont;

            selectedBox = 0;

            game = inGame;
        }

        public void nextOption()
        {
            selectedBox++;
            if (selectedBox > 2) selectedBox = 2;
        }
        public void previousOption()
        {
            selectedBox--;
            if (selectedBox <0) selectedBox = 0;
        }
        public int getSelectedBox()
        {
            return selectedBox;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, background, Color.White);
            spriteBatch.DrawString(header, "New Game", new Vector2(250, 0), Color.White);







            String difficulty = "";
            Color difficultyColor = Color.White;
            int diffX = buttonX;
            int diffValue = game.GetDifficulty();
            //figure out our difficuly and adjust accordingly
            if (diffValue == 0)
            {
                //easy
                difficulty = "Easy";
                difficultyColor = Color.Green;
            }
            else if (diffValue == 1)
            {
                //normal
                difficulty = "Normal";
                difficultyColor = Color.Blue;
                diffX -= 5;
            }
            else if (diffValue == 2)
            {
                //hard
                difficulty = "Hard";
                difficultyColor = Color.Red;
            }
            //mode box is blue if not horde, red if horde. its a toggle
            Color modeColor = Color.Blue;
            if (game.IsHorde())
            {
                modeColor = Color.Red;
            }
            spriteBatch.Draw(texture, difficultyDestRect, boxRectangle, difficultyColor);
            spriteBatch.Draw(texture, modeDestRect, boxRectangle, modeColor);
            spriteBatch.Draw(texture, playDestRect, boxRectangle, Color.Blue);

            spriteBatch.Draw(texture, selectionDestRect[selectedBox], selectionSourceRect, Color.White);

            spriteBatch.DrawString(font, difficulty, new Vector2(diffX, difficultyY), Color.Black);
            spriteBatch.DrawString(font, "Horde", modeLocation, Color.Black);
            spriteBatch.DrawString(font, "Play", playLocation, Color.Black);

        }
        public void Update(GameManager game)
        {
            //NA
        }
    }
}
