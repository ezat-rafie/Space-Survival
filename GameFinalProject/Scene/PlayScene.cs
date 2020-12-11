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
        private Vector2 backgroundPosition = new Vector2(0,-400);

        private Alien alien;
        private Vector2 alienSpeed = new Vector2(2, 2);

        private Astronaut astronaut;
        private Vector2 alienPosition = new Vector2(2, 2);

        // add string components
        StringComponent msInfo;
        StringComponent scoreInfo;
        SpriteFont font;

        // timer role
        private int delay = 60;
        private int delayCounter;
        private int timer;


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

            // getting mouse position practice
            font = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            string msg = "TEST";
            msInfo = new StringComponent(game, spriteBatch, font, Vector2.Zero, msg, Color.AliceBlue);
            this.Components.Add(msInfo);

            // Point
            string score = "SCORE : ";
            scoreInfo = new StringComponent(game, spriteBatch, font, Vector2.Zero, score, Color.AliceBlue);
            this.Components.Add(scoreInfo);
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
            delayCounter++;
            if (delayCounter > delay)
            {
                timer = timer + 1;
                delayCounter = 0;
            }

            // Update mouse info
            MouseState ms = Mouse.GetState();
            string msg = $"MOUSE POSITION : {ms.X} , {ms.Y}";
            Vector2 dimension = font.MeasureString(msg);
            Vector2 strPos = new Vector2(Shared.stage.X - dimension.X,
                Shared.stage.Y - dimension.Y);
            msInfo.Position = strPos;
            msInfo.Message = msg;

            // Update score info
            string score = $"SCORE : {timer}";
            scoreInfo.Message = score;
            base.Update(gameTime);
        }
    }
}
