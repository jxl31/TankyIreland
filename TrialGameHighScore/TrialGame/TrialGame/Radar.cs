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
    public class Radar
    {
        private Texture2D playerDot;
        private Texture2D enemyDot;
        private Texture2D powerUpDot;
        private Texture2D radarImage;

        private Vector2 radarCenter;
        private const float radarView = 1000.0f;
        private const float radarRadius = 100.0f;
        private Vector2 radarOffset;

        public Radar()
        {
            playerDot = Game1.Instance.Content.Load<Texture2D>("playermini");
            enemyDot = Game1.Instance.Content.Load<Texture2D>("enemymini");
            powerUpDot = Game1.Instance.Content.Load<Texture2D>("powerupmini");
            radarImage = Game1.Instance.Content.Load<Texture2D>("radarImage");
            

            radarCenter = new Vector2(radarImage.Width / 2.0f, radarImage.Height / 2.0f);
        }

        public void Update(GameTime gameTime, Vector2 cameraView)
        {
            radarOffset = new Vector2(cameraView.X + 100, cameraView.Y + 500);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(radarImage, radarOffset, null, new Color(100, 100, 100, 150), 0.0f, radarCenter, 2 * (radarRadius / radarImage.Height), SpriteEffects.None, 0.0f);

            foreach (Enemy enemy in Game1.Instance.Enemies)
            {
                Vector2 scale = enemy.Position - Game1.Instance.Entities[0].Position;
                float distanceBetweenEnemyAndPlayer = scale.Length();

                if (distanceBetweenEnemyAndPlayer < radarView)
                {
                    scale *= radarRadius / radarView;
                    scale += radarOffset;

                    spriteBatch.Draw(enemyDot, scale, Color.White);
                }
            }

            foreach (PowerUps powerUp in Game1.Instance.Entities.OfType<PowerUps>())
            {
                Vector2 scale1 = powerUp.Position - Game1.Instance.Entities[0].Position;
                float distanceBetweenEnemyAndPowerUp = scale1.Length();

                if (distanceBetweenEnemyAndPowerUp < radarView)
                {
                    scale1 *= radarRadius / radarView;
                    scale1 += radarOffset;

                    spriteBatch.Draw(powerUpDot, scale1, Color.White);
                }
            }

            spriteBatch.Draw(playerDot, radarOffset, Color.White);
        }

        //public Radar(Vector2 centerPosition)
        //{
        //    radar = Game1.Instance.Content.Load<Texture2D>("floormap");
        //    playerMini = Game1.Instance.Content.Load<Texture2D>("playermini");
        //    enemyMini = Game1.Instance.Content.Load<Texture2D>("enemymini");
        //    radarRec = new Rectangle((int)centerPosition.X + 10, (int)centerPosition.Y + 250, (int)(Game1.Instance.Background.Width * scale), (int)(Game1.Instance.Height * scale));
        //}

        //public void Update(GameTime gameTime)
        //{
        //    playerMiniPos.X = Game1.Instance.Entities[0].Position.X * scale + 10;
        //    playerMiniPos.Y = Game1.Instance.Entities[0].Position.Y * scale + 270;

        //    for (int i = 0; i < Game1.Instance.Enemies.Count(); i++)
        //    {
        //        Game1.Instance.Enemies
        //    }
        //}

        //public Radar()
        //{
        //    radar = Game1.Instance.Content.Load<Texture2D>("floormap");
        //    playerMini = Game1.Instance.Content.Load<Texture2D>("playermini");
        //    enemyMini = Game1.Instance.Content.Load<Texture2D>("enemymini");
        //    scale = .12f;
        //}

        //public void Update(GameTime gameTime)
        //{
        //    radarPos = new Vector2(Game1.Instance.Entities[0].Position.X - Game1.Instance.Width / 2 + Game1.Instance.Entities[0].Sprite.Width / 2,
        //                           Game1.Instance.Entities[0].Position.Y + (scale * Game1.Instance.Background.Height) - Game1.Instance.Entities[0].Sprite.Height / 2);

        //    playerMiniPos = new Vector2(radarPos.X, radarPos.Y);
        //}

        //public void Draw()
        //{
        //    Game1.Instance.spriteBatch.Draw(radar, radarPos, null, Color.YellowGreen, 0, Vector2.Zero, new Vector2(scale, scale), SpriteEffects.None, 0);

        //    //Game1.Instance.spriteBatch.Draw(playerMini,playerMiniPos,
        //}
    }
}
