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
    public class StringComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        public Vector2 Position { get; set; }
        public string Message { get; set; }
        public Color color;

        public StringComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            string message,
            Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            Position = position;
            Message = message;
            this.color = color;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, Message, Position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
