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
    public class EnemyProjectile:GameEntity
    {
        public override void LoadContent()
        {
            Sprite = Game1.Instance.Content.Load<Texture2D>("bullet");
        }

        public override void Update(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = 350.0f;

            Position -= Look * timeDelta * speed;

            int height = Game1.Instance.Height;

            //off screen
            if (Position.Y > height)
                Alive = !Alive;
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
