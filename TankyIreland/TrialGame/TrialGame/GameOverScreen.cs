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
        private MouseState mState, lmState;
        private Game1 game;
        private SpriteFont title;
        private Texture2D enemy;
        private Texture2D cursor;
        private SpriteFont gameoverWriting;
        private string[] choice = { "Retry", "Quit" };
        private int hover = -1;

        public GameOverScreen(Game1 game)
        {
            this.game = game;

            title = game.Content.Load<SpriteFont>("Font/Massive");
            enemy = game.Content.Load<Texture2D>("GamePlay/enemyTank");
            cursor = game.Content.Load<Texture2D>("cursor");
            gameoverWriting = game.Content.Load<SpriteFont>("Font/level");

            lmState = mState = Mouse.GetState();
        }

        public void Update()
        {

            mState = Mouse.GetState();
            Vector2 mLook = new Vector2(mState.X, mState.Y);
            bool selected = false;
            int startAt = 200;
            int border = 20;
            hover = -1;

            if (lmState.LeftButton == ButtonState.Released && mState.LeftButton == ButtonState.Pressed) selected = true;

            for (int i = 0; i < choice.Count(); i++)
            {
                Vector2 textSize = gameoverWriting.MeasureString(choice[0]);
                Vector2 position = new Vector2(game.Width / 2 - (textSize.X / 2), startAt + ((textSize.Y + border) * i));
                Rectangle textBound = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);
                if ((mLook.X > textBound.X) && (mLook.X < textBound.X + textBound.Width)
                            && (mLook.Y > textBound.Y) && (mLook.Y < textBound.Y + textBound.Height))
                    hover = i;
            }

            if (selected)
            {
                if (hover == 0) game.levelSelect();
                if (hover == 1) game.Exit();
            }

            lmState = mState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mState = Mouse.GetState();
            Game1.Instance.GraphicsDevice.Clear(Color.SteelBlue);
            spriteBatch.Begin();

            CentreText("GameOver", 50, Color.Red, title);

            int startAt = 200;
            int border = 20;
            Vector2 textSize = gameoverWriting.MeasureString("Hello");

            for (int i = 0; i < choice.Count(); i++)
            {
                Vector2 position = new Vector2(game.Width / 2, startAt + ((textSize.Y + border) * i));
                if (i == hover)
                    CentreText(choice[i], position.Y, Color.Red,gameoverWriting);

                else
                    CentreText(choice[i], position.Y, Color.Black,gameoverWriting);
            }

            spriteBatch.Draw(enemy, new Vector2(game.Width / 2 + 50, game.Height - enemy.Height), null, Color.White,(float)MathHelper.Pi,new Vector2(0,0), 2.0f, SpriteEffects.None, 1);

            spriteBatch.Draw(cursor, new Vector2(mState.X, mState.Y), Color.White);
            spriteBatch.End();
        }

        public void CentreText(string text, float y, Color color, SpriteFont style)
        {
            Vector2 textSize = game.SpriteFont.MeasureString(text);
            int midX = game.Width / 2;
            game.spriteBatch.DrawString(game.SpriteFont, text, new Vector2(midX - (textSize.X / 2), y), color);
        }
    }
}
