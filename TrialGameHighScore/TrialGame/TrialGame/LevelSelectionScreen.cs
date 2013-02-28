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
    public class LevelSelectionScreen
    {
        private Game1 game;
        private Texture2D Screen;
        private Texture2D cursor;
        private MouseState mState = Mouse.GetState();

        public LevelSelectionScreen(Game1 game)
        {
            this.game = game;
            Screen = game.Content.Load<Texture2D>("levelselectionscreen");
            cursor = game.Content.Load<Texture2D>("cursor");
        }

        public void Update()
        {
            MouseState mState = Mouse.GetState();
            Rectangle levelOneBound = new Rectangle(195, 165, 90, 20);
            Rectangle levelTwoBound = new Rectangle(195, 220, 90, 20);
            Rectangle levelThreeBound = new Rectangle(195, 270, 90, 20);
            Rectangle cursorBound = new Rectangle(mState.X,mState.Y,5,5);
            bool pressed = false;
            if (mState.LeftButton == ButtonState.Pressed) pressed = true;

            if (pressed && cursorBound.Intersects(levelOneBound)) game.LevelOne();
            if (pressed && cursorBound.Intersects(levelTwoBound)) game.LevelTwo();
            if (pressed && cursorBound.Intersects(levelThreeBound)) game.LevelThree();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mState = Mouse.GetState();
            spriteBatch.Begin();
            if (Screen != null)
            {
                spriteBatch.Draw(Screen, new Vector2(0f, 0f), Color.White);
            }
            spriteBatch.Draw(cursor, new Vector2(mState.X, mState.Y), Color.White);
            spriteBatch.End();
        }
    }
}
