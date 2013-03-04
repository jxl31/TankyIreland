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
        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("PlayerBullet");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 550.0f;

            Position += Look * timeDelta * speed;

            int width = Game1.Instance.Width;
            int height = Game1.Instance.Height;

            //off screen
            if (Position.Y < 20)
                Alive = false;

            //bullets colliding with enemyBullets. NULLIFY each other
            for (int i = 0; i < Game1.Instance.EnemyBullet.Count; i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.EnemyBullet[i].BoundingBox))
                {
                    Alive = !Alive;
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
                }
                   
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, 0.0f, center, 1.0f, SpriteEffects.None, 1);
        }
    }
}
