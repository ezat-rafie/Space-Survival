using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinalProject.Component
{
    public class Alien : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D alien;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int WIDTH = 128;
        private const int HEIGHT = 128;

        private const int ROW = 5;
        private const int COL = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Speed { get => speed; set => speed = value; }

        public Alien(Game game,
            SpriteBatch spriteBatch,
            Texture2D alien,
            Vector2 position,
            Vector2 speed,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.alien = alien;
            this.position = position;
            this.speed = speed;
            this.delay = delay;

            dimension = new Vector2(alien.Width / COL, alien.Height / ROW);

            //hide();
            createFrames();
        }

        public void hide()
        {
            this.Enabled = false;
            this.Visible = false;

        }
        public void start()
        {
            this.Enabled = true;
            this.Visible = true;
            frameIndex = -1;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (frameIndex >= 0)
            {
                spriteBatch.Draw(alien, Position, frames[frameIndex], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > alien.Height / HEIGHT * alien.Width / WIDTH - 1)
                {
                    frameIndex = -1;
                }
                delayCounter = 0;
            }
            position += speed;
            base.Update(gameTime);
        }
        public Rectangle GetBound()
        {
            return new Rectangle((int)position.Y, (int)position.Y, alien.Width, alien.Height);
        }
    }
}
