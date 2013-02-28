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
        int border = 20;
        int enemyWalkSpeed;

        bool statusPos = true; //flag to run one if statement in update just once
        bool movingLeft = false;

        float fireRate;
        float elapsedTime = 1.0f;

        public override void LoadContent()
        {
            Position = new Vector2(random.Next(Game1.Instance.Width - 40), random.Next(Game1.Instance.Height / 2));
            Look = new Vector2(0, -1);
            Sprite = Game1.Instance.Content.Load<Texture2D>("smalltank");
        }

        public override void Update(GameTime gameTime)
        {
            if (Game1.Instance.GameIndex == 1)
            {
                fireRate = 1f;
                enemyWalkSpeed = 80;
            }

            if (Game1.Instance.GameIndex == 2)
            {
                fireRate = .8f;
                enemyWalkSpeed = 100;
            }

            if (Game1.Instance.GameIndex == 3)
            {
                fireRate = .7f;
                enemyWalkSpeed = 120;
            }

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= fireRate)
            {
                EnemyProjectile enemyBullet = new EnemyProjectile();
                Game1.Instance.EnemyBullet.Add(enemyBullet);
                float distFromEnemyToBullet = -25.0f;
                enemyBullet.Position = this.Position + Look * distFromEnemyToBullet;
                enemyBullet.Look = this.Look;
                enemyBullet.LoadContent();

                Game1.Instance.Entities.Add(enemyBullet);
                elapsedTime = 0.0f;
            }
            elapsedTime += timeDelta;

            int screenWidth = Game1.Instance.Width;
            if (Position.X <= screenWidth / 2 && statusPos)
            {
                movingLeft = true;
                statusPos = !statusPos;
            }

            if (movingLeft) moveLeft(timeDelta);
            else moveRight(timeDelta);
        }

        public void moveLeft(float timeDelta)
        {
            if (Position.X >= border)
                Position.X -= timeDelta * enemyWalkSpeed;

            else
            {
                movingLeft = false;
                moveRight(timeDelta);
            }
        }

        public void moveRight(float timeDelta)
        {
            if (Position.X <= Game1.Instance.Width - border - Sprite.Width)
                Position.X += timeDelta * enemyWalkSpeed;
            else
            {
                movingLeft = true;
                moveLeft(timeDelta);
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
