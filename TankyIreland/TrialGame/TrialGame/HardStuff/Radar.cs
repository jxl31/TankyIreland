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
            playerDot = Game1.Instance.Content.Load<Texture2D>("Radar/playermini");
            enemyDot = Game1.Instance.Content.Load<Texture2D>("Radar/enemymini");
            powerUpDot = Game1.Instance.Content.Load<Texture2D>("Radar/powerupmini");
            radarImage = Game1.Instance.Content.Load<Texture2D>("Radar/radarImage");
            
            //center of the radar also the player
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
                float distanceBetweenEnemyAndPlayer = scale.Length(); //distance between player position and enemy. the length will determine
                                                                      //if the enemy will be in the radar. radar view is 1000 radius
                if (distanceBetweenEnemyAndPlayer < radarView)
                {
                    scale *= radarRadius / radarView; //how far the radar can see
                    scale += radarOffset; //scale of the radar in the bottom of the screen

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
    }
}