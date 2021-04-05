using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using testMonogame.Interfaces;

namespace testMonogame
{
    class ItemSelectionScreen
    {

        const int menuX = 130;
        const int menuY = 0;
        Dictionary<String, Rectangle> mapSourceRects = new Dictionary<string, Rectangle>();
        Dictionary<String, Rectangle> mapDestRects = new Dictionary<string, Rectangle>();

        Rectangle menuSourceRect = new Rectangle(0, 0, 256, 176);
        Rectangle menuDestRect = new Rectangle(menuX, menuY, 256*2, 176*2);

        Rectangle hideSourceRectangle = new Rectangle(0, 0, 20, 20);
        Rectangle mapDestRect = new Rectangle(menuX+(47 * 2), menuY+(111 * 2), 40, 40);
        Rectangle compassDestRect = new Rectangle(menuX+(44 * 2), menuY+(152 * 2), 40, 40);


        //inventory
        Rectangle selectedItemDest = new Rectangle(menuX+67 * 2,menuY+ 47 * 2, 16, 32);
        Rectangle boomerangSource = new Rectangle(267, 67, 8, 16);
        Rectangle bombSource = new Rectangle(276, 67, 8, 16);
        Rectangle arrowSource = new Rectangle(285, 67, 8, 16);
        Rectangle bowSource = new Rectangle(294, 67, 8, 16);
        Rectangle selectedBox = new Rectangle(267, 84, 16, 16);
        List<String> inventory;
        String selectedItem;


        bool mapShown;
        bool compassShown;

        Texture2D texture;
        public ItemSelectionScreen(Texture2D sprite)
        {
            inventory = new List<String>();

            texture = sprite;

            mapShown = false;
            compassShown = true;

            setUpDestRects();
            setUpSourceRects(sprite);
        }
        void setUpSourceRects(Texture2D sprite)
        {
            Rectangle r1 = new Rectangle(283, 8, 8, 8);
            Rectangle r2 = new Rectangle(291, 8, 8, 8);
            Rectangle r4 = new Rectangle(291, 16, 8, 8);
            Rectangle r5 = new Rectangle(307, 16, 8, 8);
            Rectangle r6 = new Rectangle(315, 16, 8, 8);
            Rectangle r7 = new Rectangle(275, 24, 8, 8);
            Rectangle r8 = new Rectangle(283, 24, 8, 8);
            Rectangle r9 = new Rectangle(291, 24, 8, 8);
            Rectangle r10 = new Rectangle(299, 24, 8, 8);
            Rectangle r11 = new Rectangle(307, 24, 8, 8);
            Rectangle r12 = new Rectangle(283, 32, 8, 8);
            Rectangle r13 = new Rectangle(291, 32, 8, 8);
            Rectangle r14 = new Rectangle(299, 32, 8, 8);
            Rectangle r15 = new Rectangle(291, 40, 8, 8);
            Rectangle r16 = new Rectangle(283, 48, 8, 8);
            Rectangle r17 = new Rectangle(291, 48, 8, 8);
            Rectangle r18 = new Rectangle(299, 48, 8, 8);
            mapSourceRects.Add("Room1", r1);
            mapSourceRects.Add("Room2", r2);
            mapSourceRects.Add("Room4", r4);
            mapSourceRects.Add("Room5", r5);
            mapSourceRects.Add("Room6", r6);
            mapSourceRects.Add("Room7", r7);
            mapSourceRects.Add("Room8", r8);
            mapSourceRects.Add("Room9", r9);
            mapSourceRects.Add("Room10", r10);
            mapSourceRects.Add("Room11", r11);
            mapSourceRects.Add("Room12", r12);
            mapSourceRects.Add("Room13", r13);
            mapSourceRects.Add("Room14", r14);
            mapSourceRects.Add("Room15", r15);
            mapSourceRects.Add("Room16", r16);
            mapSourceRects.Add("Room17", r17);
            mapSourceRects.Add("Room18", r18);
        }
        void setUpDestRects()
        {
            int gridwidth = 16;
            int gridX = 128 * 2;
            int gridY = 96 * 2;
            Rectangle r1 = new Rectangle(menuX + gridX + (gridwidth * 2),
                menuY + gridY + (gridwidth * 1), 16, 16);
            Rectangle r2 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 1), 16, 16);
            Rectangle r4 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 2), 16, 16);
            Rectangle r5 = new Rectangle(menuX + gridX + (gridwidth * 5),
                menuY + gridY + (gridwidth * 2), 16, 16);
            Rectangle r6 = new Rectangle(menuX + gridX + (gridwidth * 6),
                menuY + gridY + (gridwidth * 2), 16, 16);
            Rectangle r7 = new Rectangle(menuX + gridX + (gridwidth * 1),
                menuY + gridY + (gridwidth * 3), 16, 16);
            Rectangle r8 = new Rectangle(menuX + gridX + (gridwidth * 2),
                menuY + gridY + (gridwidth * 3), 16, 16);
            Rectangle r9 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 3), 16, 16);
            Rectangle r10 = new Rectangle(menuX + gridX + (gridwidth * 4),
                menuY + gridY + (gridwidth * 3), 16, 16);
            Rectangle r11 = new Rectangle(menuX + gridX + (gridwidth * 5),
                menuY + gridY + (gridwidth * 3), 16, 16);
            Rectangle r12 = new Rectangle(menuX + gridX + (gridwidth * 2),
                menuY + gridY + (gridwidth * 4), 16, 16);
            Rectangle r13 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 4), 16, 16);
            Rectangle r14 = new Rectangle(menuX + gridX + (gridwidth * 4),
                menuY + gridY + (gridwidth * 4), 16, 16);
            Rectangle r15 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 5), 16, 16);
            Rectangle r16 = new Rectangle(menuX + gridX + (gridwidth * 2),
                menuY + gridY + (gridwidth * 6), 16, 16);
            Rectangle r17 = new Rectangle(menuX + gridX + (gridwidth * 3),
                menuY + gridY + (gridwidth * 6), 16, 16);
            Rectangle r18 = new Rectangle(menuX + gridX + (gridwidth * 4),
                menuY + gridY + (gridwidth * 6), 16, 16);
            mapDestRects.Add("Room1", r1);
            mapDestRects.Add("Room2", r2);
            mapDestRects.Add("Room4", r4);
            mapDestRects.Add("Room5", r5);
            mapDestRects.Add("Room6", r6);
            mapDestRects.Add("Room7", r7);
            mapDestRects.Add("Room8", r8);
            mapDestRects.Add("Room9", r9);
            mapDestRects.Add("Room10", r10);
            mapDestRects.Add("Room11", r11);
            mapDestRects.Add("Room12", r12);
            mapDestRects.Add("Room13", r13);
            mapDestRects.Add("Room14", r14);
            mapDestRects.Add("Room15", r15);
            mapDestRects.Add("Room16", r16);
            mapDestRects.Add("Room17", r17);
            mapDestRects.Add("Room18", r18);
        }
        public void Draw(SpriteBatch spriteBatch, Dictionary<String,IRoom>.KeyCollection roomKeys)
        {
            spriteBatch.Draw(texture, menuDestRect, menuSourceRect, Color.White);

            

            if (!mapShown) spriteBatch.Draw(texture, mapDestRect, hideSourceRectangle, Color.Black);
            if (!compassShown) spriteBatch.Draw(texture, compassDestRect, hideSourceRectangle, Color.Black);

            //String[] TestRooms = { "Room1", "Room2", "Room4", "Room5", "Room6",
            //"Room7","Room8","Room9","Room10","Room11","Room12","Room13","Room14","Room15","Room16","Room17","Room18"};
            foreach (String key in roomKeys)
            {
                spriteBatch.Draw(texture, mapDestRects[key], mapSourceRects[key], Color.White);
            }

            spriteBatch.Draw(texture, selectedItemDest, getItemSourceRect(selectedItem), Color.White);


            int currentDestX=menuX+131*2;
            int currentDestY=menuY+47*2;
            foreach(String item in inventory)
            {
                //Debug.WriteLine(item);
                spriteBatch.Draw(texture, new Rectangle(currentDestX, currentDestY, 16, 32), getItemSourceRect(item), Color.White);
                if (item.Equals(selectedItem)) spriteBatch.Draw(texture, new Rectangle(currentDestX - 8, currentDestY, 32, 32), selectedBox, Color.White);
                currentDestX += 48;
                if (currentDestX>menuX+204*2)
                {
                    currentDestX= menuX + 131 * 2;
                    currentDestY += 32;
                }
            }

            

        }
        Rectangle getItemSourceRect(String s)
        {
            Rectangle r;
            switch (s)
            {
                case "Bomb":
                    r = bombSource;
                    break;
                case "Arrow":
                    r = arrowSource;
                    break;
                case "Bow":
                    r = bowSource;
                    break;
                case "Boomerang":
                    r = boomerangSource;
                    break;
                default:
                    r = hideSourceRectangle;
                    break;
            }
            return r;
                
                
        }
        public void Update(GameManager game)
        {
            IPlayer p = game.getPlayer();

            inventory = p.GetInventory();
            selectedItem = p.GetSelectedItem();

            mapShown = p.Map;
            compassShown = p.Compass;
            
        }
    }
}
