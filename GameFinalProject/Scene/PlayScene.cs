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
        private int level;

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

        //Fireball
        private Fireball fireball;
        Rectangle fireballRect;

        private int minPositionX = 0;
        private int maxPositionX = 900;
        //private int minAlienPositionY = -1500;
        //private int maxAlienPositionY = 200;

        private int minSpeed = 1;
        private int maxSpeed = 3;
        
        //Shooting
        private Shooting shooting;
        private SoundEffect alienDying;
        Rectangle mouseRect;

        //Astronaut
        private Astronaut astronaut;
        private Rectangle astronautRect;
        private Rectangle overlapDie;

        // add string components
        //StringComponent msInfo;
        StringComponent scoreInfo;
        public int score = 0;
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
        //private Rectangle overlapKill;

        //Rocket
        private Rocket rocket;

        //Laser
        private Laser laser;

        public PlayScene(Game game, SpriteBatch spriteBatch, Song song, Texture2D background, int level) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.song = song;   
            MediaPlayer.IsRepeating = true;
            this.background = background;
            this.level = level;

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

            //fireball
            if (level ==2)
            {
                fireball = new Fireball(game, spriteBatch, game.Content.Load<Texture2D>("Images/Fireball2"), 3);
                this.Components.Add(fireball);
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
            score = 0;
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
        int k = 0;

        public override void Update(GameTime gameTime)
        {
            
            if ((i > delay && j < aliens.Length) || (i == 0))
            {
                Vector2 randAlienPosition = new Vector2(random.Next(minPositionX, maxPositionX), 0);
                Vector2 randAlienSpeed = new Vector2(random.Next(minSpeed, maxSpeed), random.Next(minSpeed, maxSpeed));
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

            if (k > delay * 5 && level == 2)
            {
                Vector2 randPosition = new Vector2(random.Next(minPositionX, maxPositionX), 0);
                Vector2 randSpeed = new Vector2(random.Next(minSpeed, maxSpeed), random.Next(minSpeed, maxSpeed));
                fireball.Position = randPosition;
                fireball.Speed = randSpeed;
                fireball.start();
                k = 0;
            }
            k++;

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
                mouseRect = new Rectangle(ms.X - 5, ms.Y - 5, 10, 10);

                if (level == 2)
                {
                    fireballRect = fireball.GetBound();
                    if (mouseRect.Intersects(fireballRect))
                    {
                        MessageBox.Show("Game Over", $"You died! Score : {score}", new[] { "New Game", "Main Page", "Exit" });
                        this.Enabled = false;
                    } 
                }
            }
            oldState = ms;

            astronautRect = astronaut.GetBound();
            foreach (Alien alien in aliens)
            {
                alienRect = alien.GetBound();
                overlapDie = new Rectangle();
                //overlapKill = new Rectangle();
                overlapDie = Rectangle.Intersect(alienRect, astronautRect);
                //overlapKill = Rectangle.Intersect(alienRect, explosionRect);
                int overlapDieDim = overlapDie.Width * overlapDie.Height;
                //int overlapKillDim = overlapKill.Width * overlapKill.Height;

                //if (overlapKillDim > 800 && alien.IsAlive)
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

                // used mouseRect instead of explosionRect
                // for better targeting
                if (mouseRect.Intersects(alienRect))
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

            //Initialize it
            mouseRect = new Rectangle(0, 0, 0, 0);

            // Make it faster~
            if (score > 2000)
            {
                maxAlien = 20;
                delay = 10;
                minSpeed = 10;
                maxSpeed = 12;
            }
            else if (score > 800)
            {
                minSpeed = 7;
                maxSpeed = 10;
            }
            else if (score > 500)
            {
                //delay = 50;
                minSpeed = 5;
                maxSpeed = 8;
            }
            else if (score > 300)
            {
                minSpeed = 3;
                maxSpeed = 6;
            }


            base.Update(gameTime);
        }
    }
}
