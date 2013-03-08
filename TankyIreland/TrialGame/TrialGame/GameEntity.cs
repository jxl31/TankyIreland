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
    public abstract class GameEntity
    {
        public Vector2 Position;
        public Vector2 Look;
        public float rot;

        private Texture2D sprite;
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        private bool alive;
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public GameEntity()
        {
            alive = true;
        }

        public Rectangle BoundingBox { get { return new Rectangle((int)Position.X-Sprite.Width/2, (int)Position.Y-Sprite.Height/2, Sprite.Width, Sprite.Height); } }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
