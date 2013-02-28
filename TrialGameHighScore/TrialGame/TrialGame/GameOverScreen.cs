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
    public class GameOverScreen
    {
        private Texture2D Screen;
        private KeyboardState lastState;
        private Game1 game;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
            Screen = game.Content.Load<Texture2D>("endgame");
            lastState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                game.levelSelect();
            }
            else if (kState.IsKeyDown(Keys.Escape) && lastState.IsKeyUp(Keys.Escape))
            {
                game.Exit();
            }

            lastState = kState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Screen != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Screen, new Vector2(0f, 0f), Color.White);
                spriteBatch.End();
            }
        }
    }
}
