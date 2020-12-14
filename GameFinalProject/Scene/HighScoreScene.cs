using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GameFinalProject
{
    /// <summary>
    /// To display TOP 5 high Score record
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        StringComponent[] highScore = new StringComponent[5];
        SpriteFont fontHigh;
        string temp = "Shared.player";

        Score scoreTest = new Score();

        public HighScoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            fontHigh = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            for (int i =0 ; i < 5 ; i++)
            {
                temp += (i + 1).ToString();
                highScore[i] = new StringComponent(game, spriteBatch, fontHigh, new Vector2(100, 200 + 100 * i), "", Color.Yellow);
                //highScore[i].Message = $"{i+1} - {Shared.playerArr[i]} : {Shared.scoreArr[i]} \n";
                this.Components.Add(highScore[i]);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
