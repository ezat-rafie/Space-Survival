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
    public class AboutComponent : DrawableGameComponent
    {
        // Declare global variables
        private SpriteBatch spriteBatch;

        private SpriteFont regularFont, specialFont;
        private Color regularColor = Color.DarkSlateGray;
        private Color specialColor = Color.White;

        private Vector2 posTitle = new Vector2(Shared.stage.X / 5, Shared.stage.Y / 4);
        private Vector2 posContent = new Vector2(Shared.stage.X / 5, Shared.stage.Y * 3 / 5);

        private string title = "PROG 2370\nGame Programming with Data Structure\n";
        private string creator = "\nCreated by\n - Ezatullah Rafie R \n - Keum Ji Kim";

        public AboutComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont specialFont) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.specialFont = specialFont;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(specialFont, title, posTitle, specialColor);
            spriteBatch.DrawString(specialFont, creator, posContent, specialColor);

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
