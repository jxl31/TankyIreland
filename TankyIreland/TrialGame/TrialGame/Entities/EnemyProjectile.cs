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
    public class EnemyProjectile:GameEntity
    {
        Enemy enemy;
        float maxDistance = 270.0f;
        SoundEffect tankExplosion;

        public EnemyProjectile(Enemy enemy, float rot)
        {
            this.enemy = enemy;
            Look = new Vector2(0, 1);
            this.rot = rot;
        }

        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("GamePlay/enemyBullet");
            tankExplosion = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/tankexplosion");

        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 350.0f;

            Position += Look * timeDelta * speed;

            Vector2 distance = Position - enemy.Position;
            float distanceFromEnemy = distance.Length();

            if (distanceFromEnemy > maxDistance)
            {
                Alive = false;
            }

            //off screen
            if (Position.Y > Game1.Instance.Background.Height)
            {
                Alive = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, rot + (float) MathHelper.PiOver2 * 3, center, 1.0f, SpriteEffects.None, 1);
        }
    }
}
