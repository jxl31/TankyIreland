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
    public class Character:GameEntity
    {
        int walkspeed = 300;
        int rotationSpeed = 3;

        //tank body
        Vector2 center = new Vector2();

        public override void LoadContent()
        {
            Look = new Vector2(0, -1);
            
            //tank body
            Sprite = Game1.Instance.Content.Load<Texture2D>("GamePlay/tank");
            Position = new Vector2(Game1.Instance.Background.Width / 2, Game1.Instance.Background.Height / 2);
            rot = 0.0f;
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
        }

        float fireRate = 2.0f;
        float elapsedTime = 10.0f;

        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Look.X = (float)Math.Sin(rot);
            Look.Y = -(float)Math.Cos(rot);

            float hasToPass = 0.70f / fireRate;

            for (int i = 0; i < Game1.Instance.Enemies.Count(); i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.Enemies[i].BoundingBox))
                {
                    Position = new Vector2((Game1.Instance.Background.Width / 2), (Game1.Instance.Background.Height / 2));
                    rot = 0.0f;
                    Game1.Instance.Enemies[i].Alive = false;
                    Game1.Instance.enemyHitPlayer();
                    Game1.Instance.playerHitEnemy();
                }
            }

            for (int i = 0; i < Game1.Instance.EnemyBullet.Count(); i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.EnemyBullet[i].BoundingBox))
                {
                    Position = new Vector2((Game1.Instance.Background.Width / 2), (Game1.Instance.Background.Height / 2));
                    Game1.Instance.EnemyBullet[i].Alive = false;
                    rot = 0.0f;
                    Game1.Instance.enemyHitPlayer();
                    if (Game1.Instance.Lives <= 0)
                        Game1.Instance.GameOver();
                }
            }

            if (kState.IsKeyDown(Keys.Space) && elapsedTime > hasToPass)
            {
                CharacterProjectile bullet = new CharacterProjectile();
                Game1.Instance.CharacterBullets.Add(bullet);
                Game1.Instance.Entities.Add(bullet);
                float distFromCharacterToBullet = 25.0f;
                bullet.Position = Position + Look * distFromCharacterToBullet;
                bullet.Look = Look;
                bullet.LoadContent();
                if (Game1.Instance.Ammo != 0)
                    Game1.Instance.Ammo--;
                else
                    Game1.Instance.GameOver();
                
                elapsedTime = 0.0f;
            }

            elapsedTime += timeDelta;

            if (kState.IsKeyDown(Keys.Left)) rot -= timeDelta * rotationSpeed;
            if (kState.IsKeyDown(Keys.Right)) rot += timeDelta * rotationSpeed;
            if (kState.IsKeyDown(Keys.Up)) Position += timeDelta * walkspeed * Look;
            if (kState.IsKeyDown(Keys.Down)) Position -= timeDelta * walkspeed * Look;

            //brings character back to center of screen
            if(!Game1.Instance.BackgroundRec.Contains(BoundingBox))
                Position = new Vector2((Game1.Instance.Background.Width / 2), (Game1.Instance.Background.Height / 2));
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, rot, center, 1.0f, SpriteEffects.None, 1);
        }

    }
}
