using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TrialGame
{
    public class Enemy : GameEntity
    {
        Random random = new Random(System.DateTime.Now.Millisecond);
        int enemyWalkSpeed;

        private SoundEffect tankFire { get; set; }
        private SoundEffect tankExplosion { get; set; }

        float fireRate;
        float elapsedTime = 1.0f;

        public override void LoadContent()
        {
            Position = new Vector2(random.Next(Game1.Instance.Background.Width-50), random.Next(Game1.Instance.Height/2-300));
            Look = new Vector2(0, 1);
            Sprite = Game1.Instance.Content.Load<Texture2D>("GamePlay/enemyTank");
            rot = 0.0f;

            tankExplosion = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/tankexplosion");
            tankFire = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/tankfire");

        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.Instance.GameIndex == 1)
            {
                fireRate = 2f;
                enemyWalkSpeed = 110;
            }

            if (Game1.Instance.GameIndex == 2)
            {
                fireRate = 1.5f;
                enemyWalkSpeed = 140;
            }

            if (Game1.Instance.GameIndex == 3)
            {
                fireRate = 1f;
                enemyWalkSpeed = 170;
            }

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < Game1.Instance.CharacterBullets.Count(); i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.CharacterBullets[i].BoundingBox))
                {
                    Alive = false;
                    Game1.Instance.CharacterBullets[i].Alive = false;
                    tankExplosion.Play();
                }
            }

            if (elapsedTime >= fireRate)
            {
                EnemyProjectile enemyBullet = new EnemyProjectile(this, rot);
                Game1.Instance.EnemyBullet.Add(enemyBullet);
                float distFromEnemyToBullet = 28.0f;
                enemyBullet.Look = Look;
                enemyBullet.Position = Position + enemyBullet.Look * distFromEnemyToBullet;
                enemyBullet.LoadContent();

                Game1.Instance.Entities.Add(enemyBullet);
                elapsedTime = 0.0f;
                tankFire.Play();
            }
            elapsedTime += timeDelta;

            //if enemy reachers bottom of screen
            if (Position.Y > Game1.Instance.Background.Height - Sprite.Height / 2)
            {
                Alive = false;
                Game1.Instance.enemyHitPlayer();
            }

            Vector2 distance = Position - Game1.Instance.Entities[0].Position;
            float distanceBetweenEnemyPlayer = distance.Length();

            if (distanceBetweenEnemyPlayer <= 400)
            {
                rot = (float)Math.Atan2(distance.Y, distance.X);
            }
            else
            {
                Look = new Vector2(0, 1);
                rot = (float)MathHelper.PiOver2 * 3;
            }
            Position += timeDelta * enemyWalkSpeed * Look;
            Look.X = -(float)(Math.Cos(rot));
            Look.Y = -(float)(Math.Sin(rot));
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, rot + (float)MathHelper.PiOver2, center, 1.0f, SpriteEffects.None, 1);
        }

    }
}
