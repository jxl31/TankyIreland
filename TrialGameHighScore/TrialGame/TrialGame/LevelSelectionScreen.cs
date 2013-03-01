﻿using System;
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

        int hover = -1;
        string[] levelChoice = { "Level One", "Level Two", "Level Three", "Quit" };

        public LevelSelectionScreen(Game1 game)
        {
            this.game = game;
            Screen = game.Content.Load<Texture2D>("screenBackground");
            cursor = game.Content.Load<Texture2D>("cursor");
        }

        public void Update()
        {
            MouseState mState = Mouse.GetState();
            Vector2 mLook = new Vector2(mState.X, mState.Y);
            bool selected = false;
            int startAt = 150;
            int border = 20;
            hover = -1;

            if (mState.LeftButton == ButtonState.Pressed) selected = true;

            for (int i = 0; i < levelChoice.Count(); i++)
            {
                Vector2 textSize = game.SpriteFont.MeasureString(levelChoice[0]);
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mState = Mouse.GetState();
            spriteBatch.Begin();
            int startAt = 150;
            int border = 20;
            Vector2 textSize = game.SpriteFont.MeasureString("Hello");
            spriteBatch.Draw(Screen, Vector2.Zero, Color.White);

            CentreText("Choose Level", 50, Color.Red);

            for (int i = 0; i < levelChoice.Count(); i++)
            {
                Vector2 position = new Vector2(game.Width / 2, startAt + ((textSize.Y + border) * i));
                if (i == hover)
                    CentreText(levelChoice[i], position.Y, Color.Green);

                else
                    CentreText(levelChoice[i], position.Y, Color.Red);
            }


            spriteBatch.Draw(cursor, new Vector2(mState.X, mState.Y), Color.White);
            spriteBatch.End();
        }

        public void CentreText(string text, float y, Color color)
        {
            Vector2 textSize = game.SpriteFont.MeasureString(text);
            int midX = game.Width / 2;
            game.spriteBatch.DrawString(game.SpriteFont, text, new Vector2(midX - (textSize.X / 2), y), color);
        }
    }
}
