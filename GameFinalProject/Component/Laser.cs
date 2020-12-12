using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimationExplosion
{
    public class Laser : DrawableGameComponent
    {
        private Texture2D laser;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 bulletDirection;
        private Vector2 mousePosition;
        private int delayCounter;
        private int delay;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Speed { get => speed; set => speed = value; }
        public Vector2 BulletDirection { get => bulletDirection; set => bulletDirection = value; }
        public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }

        public Laser(Game game, SpriteBatch spriteBatch, int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.laser = game.Content.Load<Texture2D>("Images/Laser");
            this.delay = delay;
            hide();
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
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(laser, Position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, laser.Width, laser.Height);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                Vector2 bulletdirection = MousePosition - Position;
                bulletdirection.Normalize();
                Speed = new Vector2(150, 150);
                Position += bulletdirection * Speed;
                //if (explosionRect.Intersects(getBound()))
                //{
                //    hide();
                //}
                delayCounter = 0;
            }
            base.Update(gameTime);
        }
    }
}

