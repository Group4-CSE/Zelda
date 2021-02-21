using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace testMonogame.Commands
{
    public class CycleEnemyCommand : ICommand
    {
        Game1 game;
        public CycleEnemyCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.cycleEnemy();
        }
    }
}
