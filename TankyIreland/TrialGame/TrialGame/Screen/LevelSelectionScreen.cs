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
        private Texture2D cursor;
        private MouseState mState;
        private MouseState lmState;
        private SpriteFont levelWriting { get; set; }
        private SpriteFont title { get; set; }

        int hover = -1;
        string[] levelChoice = { "Level One", "Level Two", "Level Three", "Quit" };

        public LevelSelectionScreen(Game1 game)
        {
            this.game = game;
            lmState = mState = Mouse.GetState();
            cursor = game.Content.Load<Texture2D>("cursor");
            levelWriting = game.Content.Load<SpriteFont>("Font/level");
            title = game.Content.Load<SpriteFont>("Font/ScreenFont");
        }

        public void Update()
        {
            mState = Mouse.GetState();
            Vector2 mLook = new Vector2(mState.X, mState.Y);
            bool selected = false;
            int startAt = 150;
            int border = 20;
            hover = -1;

            if (lmState.LeftButton == ButtonState.Released && mState.LeftButton == ButtonState.Pressed) selected = true;

            for (int i = 0; i < levelChoice.Count(); i++)
            {
                Vector2 textSize = levelWriting.MeasureString(levelChoice[0]);
                Vector2 position = new Vector2(game.Width / 2 - (textSize.X / 2), startAt + ((textSize.Y + border) * i));
                Rectangle textBound = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);
                    if ((mLook.X > textBound.X) && (mLook.X < textBound.X + textBound.Width)
                                && (mLook.Y > textBound.Y) && (mLook.Y < textBound.Y + textBound.Height))
                        hover = i;
            }

            if (selected)
            {
                if (hover == 0) game.LevelOne();
                if (hover == 1) game.LevelTwo();
                if (hover == 2) game.LevelThree();
                if (hover == 3) game.Exit();
            }
            lmState = mState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mState = Mouse.GetState();
            game.GraphicsDevice.Clear(Color.SteelBlue);
            spriteBatch.Begin();
            int startAt = 150;
            int border = 20;
            Vector2 textSize = levelWriting.MeasureString("Hello");

            Vector2 textSizeTitle = levelWriting.MeasureString("Choose Level");
            spriteBatch.DrawString(title, "Choose Level", new Vector2(Game1.Instance.Width / 2 - 150, 50), Color.Red);

            for (int i = 0; i < levelChoice.Count(); i++)
            {
                Vector2 position = new Vector2(game.Width / 2, startAt + ((textSize.Y + border) * i));
                if (i == hover)
                    CentreText(levelChoice[i], position.Y, Color.Red);

                else
                    CentreText(levelChoice[i], position.Y, Color.Black);
            }


            spriteBatch.Draw(cursor, new Vector2(mState.X, mState.Y), Color.White);
            spriteBatch.End();
        }

        public void CentreText(string text, float y, Color color)
        {
            Vector2 textSize = game.SpriteFont.MeasureString(text);
            int midX = game.Width / 2;
            game.spriteBatch.DrawString(levelWriting, text, new Vector2(midX - (textSize.X / 2), y), color);
        }
    }
}
