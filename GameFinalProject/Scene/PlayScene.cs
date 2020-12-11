using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using GameFinalProject.Component;

namespace GameFinalProject
{
    public class PlayScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Song song;
        private Texture2D background;
        private Vector2 backgroundPosition = new Vector2(0,0);

        private Alien alien;
        private Vector2 alienSpeed = new Vector2(2, 2);
        private Vector2 alienPosition = new Vector2(2, 2);

        private Astronaut astronaut;

        public PlayScene(Game game, SpriteBatch spriteBatch, Song song, Texture2D background) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.song = song;   
            MediaPlayer.IsRepeating = true;
            this.background = background;

            alien = new Alien(game, spriteBatch, game.Content.Load<Texture2D>("Images/Alien"), alienPosition, alienSpeed, 3);
            this.Components.Add(alien);

            astronaut = new Astronaut(game, spriteBatch, game.Content.Load<Texture2D>("Images/Astronaut"), 3);
            this.Components.Add(astronaut);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
