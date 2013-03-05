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

        public Camera(Viewport playerView)
        {
            view = playerView;
            radar = new Radar();
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
            spriteBatch.DrawString(Game1.Instance.SpriteFont, "Enemy Left: " + Game1.Instance.EnemyCount,new Vector2(center.X,center.Y), Color.Black);
            spriteBatch.DrawString(Game1.Instance.SpriteFont, "Lives Left: " + Game1.Instance.Lives, new Vector2(center.X, center.Y + 25), Color.Black);
            spriteBatch.DrawString(Game1.Instance.SpriteFont, "Ammos Left: " + Game1.Instance.Ammo, new Vector2(center.X,center.Y+50), Color.Black);
            spriteBatch.DrawString(Game1.Instance.SpriteFont, "Time: " + timer.ToString("0.00"), new Vector2(center.X+ 300,center.Y), Color.Black);
            spriteBatch.DrawString(Game1.Instance.SpriteFont, "Time Limit: " + Game1.Instance.TimeLimit.ToString("0.00"), new Vector2(center.X+600,center.Y), Color.Red);
            radar.Draw(spriteBatch);
        }
    }
}
