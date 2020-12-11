using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFinalProject
{
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private AboutComponent aboutComponent;
        private Texture2D menuBackground;
        private Vector2 position = new Vector2(0, 0);
        public AboutScene(Game game, 
            SpriteBatch spriteBatch,
            Texture2D menuBackground) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.menuBackground = menuBackground;
            aboutComponent = new AboutComponent(game, spriteBatch,
                                game.Content.Load<SpriteFont>("Fonts/RegularFont"),
                                game.Content.Load<SpriteFont>("Fonts/SpecialFont"));
            this.Components.Add(aboutComponent);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBackground, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
