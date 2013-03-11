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
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 center;
        Radar radar;
        Texture2D healthBar;
        Texture2D enemyBar;

        public Camera(Viewport playerView)
        {
            view = playerView;
            radar = new Radar();
            healthBar = Game1.Instance.Content.Load<Texture2D>("GamePlay/healthBar");
            enemyBar = Game1.Instance.Content.Load<Texture2D>("GamePlay/enemyBar");
        }

        public void Update(GameTime gameTime, Vector2 playerPos, Rectangle playerRec)
        {

            //movement in all direction
            if (playerPos.X >= 400 - playerRec.Width/2 && playerPos.X <= Game1.Instance.Background.Width + playerRec.Width/2 - 400
                && playerPos.Y >= 300 - playerRec.Height/2 && playerPos.Y <= Game1.Instance.Background.Height + playerRec.Height/2 - 300)
            {
                center = new Vector2(playerPos.X - 400 + (playerRec.Width/2),
                    playerPos.Y - 300 + (playerRec.Height/2));
            }

            //left -> bottom left corner
            if (playerPos.X <= 400 - playerRec.Width / 2 && playerPos.Y >= Game1.Instance.Background.Height - 300 - playerRec.Height/2)
                center = new Vector2(0, Game1.Instance.Background.Height - 600);
            else if (playerPos.X <= 400 - playerRec.Width / 2)
                center = new Vector2(0, playerPos.Y + playerRec.Height / 2 - 300);

            //top left corner -> top
            if (playerPos.Y <= 300 - playerRec.Height / 2 && playerPos.X <= 400 - playerRec.Width / 2)
                center = new Vector2(0, 0);
            else if (playerPos.Y <= 300 - playerRec.Height / 2)
                center = new Vector2(playerPos.X + playerRec.Width / 2 - 400, 0);

            //top right corner -> right
            if (playerPos.X >= Game1.Instance.Background.Width - 400 - playerRec.Width / 2 && playerPos.Y <= 300 - playerRec.Height / 2)
                center = new Vector2(Game1.Instance.Background.Width - 800, 0);
            else if (playerPos.X >= Game1.Instance.Background.Width - 400 - playerRec.Width / 2)
                center = new Vector2(Game1.Instance.Background.Width - 800, playerPos.Y + playerRec.Height / 2 - 300);

            //bottom left corner -> bottom
            if (playerPos.X >= Game1.Instance.Background.Width - 400 - playerRec.Width / 2 && playerPos.Y >= Game1.Instance.Background.Height - 300 - playerRec.Height / 2)
                center = new Vector2(Game1.Instance.Background.Width - 800, Game1.Instance.Background.Height - 600);
            else if (playerPos.X >= 400 - playerRec.Width / 2 && playerPos.X <= Game1.Instance.Background.Width - 400 - playerRec.Width / 2
                && playerPos.Y >= Game1.Instance.Background.Height - 300 - playerRec.Height / 2)
                center = new Vector2(playerPos.X + playerRec.Width / 2 - 400, Game1.Instance.Background.Height - 600);

            transform = (Matrix.CreateScale(new Vector3(1, 1, 0)))  *
                    Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));

            radar.Update(gameTime, center);
        }

        public void Draw(SpriteBatch spriteBatch, float timer)
        {

            //spriteBatch.DrawString(Game1.Instance.SpriteFont, "Health:", new Vector2(center.X, center.Y), Color.White);
            //spriteBatch.Draw(healthBar, new Rectangle((int)center.X + 2, (int)center.Y + 25, Game1.Instance.Health, 15), Color.White);
            //spriteBatch.DrawString(Game1.Instance.SpriteFont, "Enemy Left:", new Vector2(center.X, center.Y + 45), Color.White);
            //spriteBatch.Draw(enemyBar, new Rectangle((int)center.X + 2, (int)center.Y + 70, Game1.Instance.EnemyCount * 10, 15), Color.Black);
            //spriteBatch.DrawString(Game1.Instance.SpriteFont, "Ammos Left: " + Game1.Instance.Ammo, new Vector2(center.X, center.Y + 90), Color.White);
            //spriteBatch.DrawString(Game1.Instance.SpriteFont, "Time: " + timer.ToString("0.00"), new Vector2(center.X + 325, center.Y+10), Color.Black);
            //spriteBatch.DrawString(Game1.Instance.SpriteFont, "Time Limit: " + Game1.Instance.TimeLimit.ToString("0.00"), new Vector2(center.X + 600, center.Y + 10), Color.Red);

            radar.Draw(spriteBatch);
        }
    }
}
