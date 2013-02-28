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
        int border = 20;
        int topBorder = 460;
        int walkspeed = 260;

        public override void LoadContent()
        {
            Look = new Vector2(0, -1);
            
            //tank body
            Sprite = Game1.Instance.Content.Load<Texture2D>("tankbody");
            Position = new Vector2((Game1.Instance.Background.Width / 2), (Game1.Instance.Background.Height - border - Sprite.Height/2));

            //tank turret
            Sprite2 = Game1.Instance.Content.Load<Texture2D>("tankturret");
            turretPosition = new Vector2((Game1.Instance.Background.Width/2 + (Sprite.Width-60)),Game1.Instance.Background.Height-border - (Sprite.Height-20));
        }

        float fireRate = 2.0f;
        float elapsedTime = 10.0f;

        public override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float hasToPass = 0.70f / fireRate;

            for (int i = 0; i < Game1.Instance.EnemyBullet.Count(); i++)
            {
                if (BoundingBox.Intersects(Game1.Instance.EnemyBullet[i].BoundingBox))
                {
                    Position = new Vector2((Game1.Instance.Background.Width / 2), (Game1.Instance.Background.Height/2));
                    turretPosition = new Vector2((Game1.Instance.Width / 2 + (Sprite.Width - 60)), Game1.Instance.Height - border - (Sprite.Height - 20));
                    Game1.Instance.EnemyBullet[i].Alive = false;
                    Game1.Instance.enemyHitPlayer();
                    if (Game1.Instance.Lives == 0)
                        Game1.Instance.GameOver();
                    if (Game1.Instance.Ammo == 0)
                        Game1.Instance.GameOver();
                }
            }

            if (kState.IsKeyDown(Keys.Space) && elapsedTime > hasToPass)
            {
                CharacterProjectile bullet = new CharacterProjectile();
                Game1.Instance.CharacterBullets.Add(bullet);
                Game1.Instance.Entities.Add(bullet);
                float distFromCharacterToBullet = 25.0f;
                bullet.Position = this.Position + Look * distFromCharacterToBullet;
                bullet.Look = this.Look;
                bullet.LoadContent();
                if (Game1.Instance.Ammo != 0)
                    Game1.Instance.Ammo--;
                else
                    Game1.Instance.GameOver();
                
                elapsedTime = 0.0f;
            }

            elapsedTime += timeDelta;

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left))
            {
                //if (Position.X > border + Sprite.Width/2)
                //{
                    Position.X -= timeDelta * walkspeed;
                    turretPosition.X -= timeDelta * walkspeed;
                //}
            }

            if ((kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right)))
            {
                //if (Position.X < Game1.Instance.Width - border - Sprite.Width / 2)
                //{
                    Position.X += timeDelta * walkspeed;
                    turretPosition.X += timeDelta * walkspeed;
                //}
            }

            if ((kState.IsKeyDown(Keys.W) || kState.IsKeyDown(Keys.Up)))
            {
                //if (Position.Y > topBorder)
                //{
                    Position.Y -= timeDelta * walkspeed;
                    turretPosition.Y -= timeDelta * walkspeed;
                //}
            }

            if ((kState.IsKeyDown(Keys.S) || kState.IsKeyDown(Keys.Down)))
            {
                //if (Position.Y < Game1.Instance.Height - border - Sprite.Height / 2)
                //{  
                    Position.Y += timeDelta * walkspeed;
                    turretPosition.Y += timeDelta * walkspeed;
                //}
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //tank body
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;

            //tank turret
            Vector2 center1 = new Vector2();
            center1.X = Sprite2.Width / 2;
            center1.Y = Sprite2.Height / 2;

            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, 0.0f, center, 1.0f, SpriteEffects.None, 1);
            Game1.Instance.spriteBatch.Draw(Sprite2, turretPosition, null, Color.White, 0.0f, center1, 1.0f, SpriteEffects.None, 1);

        }

    }
}
