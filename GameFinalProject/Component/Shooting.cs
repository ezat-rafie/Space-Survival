using AnimationExplosion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinalProject.Component
{
    class Shooting : GameComponent
    {
        private Alien alien;
        private Explosion explosion;
        private SoundEffect shootingSound;
        public Shooting(Game game,
            Alien alien,
            Explosion explosion,
            SoundEffect dyingSound) : base(game)
        {
            this.alien = alien;
            this.explosion = explosion;
            this.shootingSound = dyingSound;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle alienRect = alien.GetBound();
            Rectangle explosionRect = explosion.getBound();

            if (explosionRect.Intersects(alienRect))
            {
                alien.hide();
                shootingSound.Play();
            }
            base.Update(gameTime);
        }
    }
}
