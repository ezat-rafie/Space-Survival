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
using AnimationExplosion;

namespace GameFinalProject
{
    public class PlayScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Song song;
        private Texture2D background;
        private Vector2 backgroundPosition = new Vector2(0,-400);

        //Alien
        private Alien alien;
        //private Vector2 alienSpeed = new Vector2(1, 1);
        //private Vector2 alienPosition = new Vector2(2, 2);

        //Multiple aliens
        Alien[] aliens;
        Random random = new Random();
        private int maxAlien = 8;
        //private int minAlien = 1;
        Rectangle alienRect;

        private int minAlienPositionX = 0;
        private int maxAlienPositionX = 900;
        //private int minAlienPositionY = -1500;
        //private int maxAlienPositionY = 200;

        private int minAlienSpeed = 1;
        private int maxAlienSpeed = 3;
        
        //Shooting
        private Shooting shooting;
        private SoundEffect alienDying;

        //Astronaut
        private Astronaut astronaut;
        private Rectangle astronautRect;
        private Rectangle overlapDie;

        // add string components
        //StringComponent msInfo;
        StringComponent scoreInfo;
        int score = 0;
        string totalScore;
        SpriteFont font;

        // time gap between creating next alien
        private int delay = 100;

        // timer role (practice)
        //private int delayCounter;
        //private int timer;

        //Mouse click explosion
        private Explosion explosion;
        private MouseState oldState;
        private SoundEffect shootingSound;
        private Texture2D explosionSprite;
        Rectangle explosionRect;
        private Rectangle overlapKill;

        //Rocket
        private Rocket rocket;

        //Laser
        private Laser laser;

        public PlayScene(Game game, SpriteBatch spriteBatch, Song song, Texture2D background) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.song = song;   
            MediaPlayer.IsRepeating = true;
            this.background = background;

            // Rocket
            rocket = new Rocket(game, spriteBatch);
            this.Components.Add(rocket);

            //Alien
            alien = new Alien(game, spriteBatch, game.Content.Load<Texture2D>("Images/Alien"), 3);
            //this.Components.Add(alien);

            //aliens
            aliens = new Alien[maxAlien];
            for (int i = 0; i < maxAlien; i++)
            {
                aliens[i] = new Alien(game, spriteBatch, game.Content.Load<Texture2D>("Images/Alien"), 3);
                this.Components.Add(aliens[i]);
            }

            //Astronaut
            astronaut = new Astronaut(game, spriteBatch, game.Content.Load<Texture2D>("Images/Astronaut"), 3);
            this.Components.Add(astronaut);

            //Explosion
            explosionSprite = game.Content.Load<Texture2D>("Images/explosion");
            shootingSound = game.Content.Load<SoundEffect>("Sounds/Explosion");
            explosion = new Explosion(game, spriteBatch, explosionSprite, Vector2.Zero,shootingSound, 3);
            this.Components.Add(explosion);

            //Laser
            //laser = new Laser(game, spriteBatch, 3);
            //this.Components.Add(laser);

            //Shooting
            alienDying = game.Content.Load<SoundEffect>("Sounds/alienDying");
            shooting = new Shooting(game, alien, explosion);
            this.Components.Add(shooting);

            /*
            // getting mouse position practice
            font = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            string msg = "TEST";
            msInfo = new StringComponent(game, spriteBatch, font, Vector2.Zero, msg, Color.AliceBlue);
            this.Components.Add(msInfo);

            */
            // Point
            font = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            string totalScore = " ";
            scoreInfo = new StringComponent(game, spriteBatch, font, Vector2.Zero, totalScore, Color.AliceBlue);
            this.Components.Add(scoreInfo);
            
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundPosition, Color.White);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        int i = 0;
        int j = 0;


        public override void Update(GameTime gameTime)
        {
            
            if ((i > delay && j < aliens.Length) || (i == 0))
            {
                Vector2 randAlienPosition = new Vector2(random.Next(minAlienPositionX, maxAlienPositionX), 0);
                Vector2 randAlienSpeed = new Vector2(random.Next(minAlienSpeed, maxAlienSpeed), random.Next(minAlienSpeed, maxAlienSpeed));
                aliens[j].Position = randAlienPosition;
                aliens[j].Speed = randAlienSpeed;
                aliens[j].IsAlive = true;

                aliens[j].start();
                Vector2 direction = astronaut.Position - aliens[j].Position;
                direction.Normalize();
                aliens[j].Position += direction * aliens[j].Speed;
                j++;
                i = 0;
                if (j == aliens.Length)
                {
                    j = 0;
                }
            }
            i++;

            // Aliens' movement
            //foreach (Alien alien in aliens)
            //{
            //    Vector2 direction = astronaut.Position - alien.Position;
            //    direction.Normalize();
            //    alien.Position += direction * alien.Speed;
            //}


            //mouseClick explosion
            MouseState ms = Mouse.GetState();

            if (ms.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                explosion.Position = new Vector2(ms.X-55, ms.Y-55);
                explosion.start();

                //laser.MousePosition = new Vector2(ms.X, ms.Y);
                //laser.Position = new Vector2(astronaut.Position.X + 64, astronaut.Position.Y + 5);
                //laser.start();

                shootingSound.Play();
                explosionRect = explosion.getBound();
            }
            oldState = ms;

            astronautRect = astronaut.GetBound();
            foreach (Alien alien in aliens)
            {
                alienRect = alien.GetBound();
                overlapDie = new Rectangle();
                overlapKill = new Rectangle();
                overlapDie = Rectangle.Intersect(alienRect, astronautRect);
                overlapKill = Rectangle.Intersect(alienRect, explosionRect);
                int overlapDieDim = overlapDie.Width * overlapDie.Height;
                int overlapKillDim = overlapKill.Width * overlapKill.Height;

                if (overlapKillDim > 1000 && alien.IsAlive)
                {
                    alien.Speed += alien.Speed * 10;
                    alien.hide();
                    alienDying.Play();
                    alienRect = Rectangle.Empty;
                    explosionRect = Rectangle.Empty;
                    alien.IsAlive = false;
                    score += 100;
                    break;
                }
                //if (explosionRect.Intersects(alienRect))
                //{
                //    alien.Speed += alien.Speed * 10;
                //    alien.hide();
                //    alienDying.Play();
                //    alienRect = Rectangle.Empty;
                //    explosionRect = Rectangle.Empty;
                //    alien.IsAlive = false;
                //    score += 100;
                //    break;
                //}
                if (overlapDieDim > 1500 && alien.IsAlive)
                {
                    MessageBox.Show("Game Over", $"You died! Score : {score}", new[] { "New Game", "Main Page", "Exit" });
                    this.Enabled = false;
                }
                //if (astronautRect.Intersects(alienRect) && alien.IsAlive)
                //{
                //    MessageBox.Show("Game Over", "You died", new[] { "New Game", "Main Page", "Exit" });
                //    this.Enabled = false;
                //}

                
            }



            /*
            delayCounter++;
            if (delayCounter > delay)
            {
                timer = timer + 1;
                delayCounter = 0;
            }

            // Update mouse info
            ms = Mouse.GetState();
            string msg = $"MOUSE POSITION : {ms.X} , {ms.Y}";
            Vector2 dimension = font.MeasureString(msg);
            Vector2 strPos = new Vector2(Shared.stage.X - dimension.X,
                Shared.stage.Y - dimension.Y);
            msInfo.Position = strPos;
            msInfo.Message = msg;
            */

            // Update score info
            totalScore = $"SCORE : {score}";
            scoreInfo.Message = totalScore;

            // Make it faster~
            if (score > 2000)
            {
                maxAlien = 20;
                delay = 10;
                minAlienSpeed = 10;
                maxAlienSpeed = 12;
            }
            else if (score > 800)
            {
                minAlienSpeed = 7;
                maxAlienSpeed = 10;
            }
            else if (score > 500)
            {
                delay = 50;
                minAlienSpeed = 5;
                maxAlienSpeed = 8;
            }
            else if (score > 300)
            {
                minAlienSpeed = 3;
                maxAlienSpeed = 6;
            }


            base.Update(gameTime);
        }
    }
}
