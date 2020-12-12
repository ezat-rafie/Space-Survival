﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinalProject
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
        private bool isAlive;

        //private const int WIDTH = 128;
        //private const int HEIGHT = 128;

        private const int ROW = 5;
        private const int COL = 5;

        public Vector2 Position { get => position; set => position = value; }
        public Vector2 Speed { get => speed; set => speed = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        public Alien(Game game,
            SpriteBatch spriteBatch,
            Texture2D alien,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.alien = alien;
            this.delay = delay;
            IsAlive = true;
            dimension = new Vector2(alien.Width / COL, alien.Height / ROW);

            hide();
            createFrames();
        }

        public void hide()
        {
            //this.Enabled = false;
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
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = -1;
                }
                delayCounter = 0;
            }
            position += speed;

            if (position.X < 0)
            {
                speed.X = -speed.X;
            }
            if (position.X + alien.Width / COL > Shared.stage.X)
            {
                speed.X = -speed.X;
            }

            base.Update(gameTime);

        }
        public Rectangle GetBound()
        {
            return new Rectangle((int)position.X + 10, (int)position.Y + 10, alien.Width / COL - 20, alien.Height / ROW - 20);
        }
    }
}
