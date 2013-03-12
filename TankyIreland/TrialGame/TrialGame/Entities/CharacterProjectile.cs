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
    public class CharacterProjectile:GameEntity
    {
        float maxDistance = 330.0f;
        SoundEffect tankExplosion;
        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("GamePlay/PlayerBullet");
            rot = Game1.Instance.Entities[0].rot;
            tankExplosion = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/tankexplosion");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 450.0f;

            Position += Look * timeDelta * speed;

            //bullets colliding with enemyBullets. NULLIFY each other
            for (int i = 0; i < Game1.Instance.EnemyBullet.Count; i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.EnemyBullet[i].BoundingBox))
                {
                    Alive = false;
                    Game1.Instance.EnemyBullet[i].Alive = false;
                }
            }

            for (int i = 0; i < Game1.Instance.Enemies.Count(); i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.Enemies[i].BoundingBox))
                {
                    Alive = !Alive;
                    Game1.Instance.Enemies[i].Alive = false;
                    Game1.Instance.playerHitEnemy();
                    tankExplosion.Play();
                }   
            }

            //max distance
            Vector2 distance = Position - Game1.Instance.Entities[0].Position;
            float distanceFromPlayer = distance.Length();
            if (distanceFromPlayer > maxDistance)
                Alive = !Alive;


            //off screen
            if (Position.Y < 0 && Position.Y > Game1.Instance.Background.Height
                && Position.X < 0 && Position.X > Game1.Instance.Background.Width)
                Alive = !Alive;
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 1);
        }
    }
}
