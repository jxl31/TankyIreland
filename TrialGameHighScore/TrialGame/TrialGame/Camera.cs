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

        public Camera(Viewport playerView)
        {
            view = playerView;
        }

        public void Update(GameTime gameTime, Vector2 playerPos, Rectangle playerRec)
        {
            center = new Vector2(playerPos.X + (playerRec.Width / 2) - 400,
                playerPos.Y + (playerRec.Height / 2) - 300);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
