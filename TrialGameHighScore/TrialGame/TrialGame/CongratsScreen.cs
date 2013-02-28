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
using System.IO;

namespace TrialGame
{
    public class CongratsScreen
    {
        private Texture2D cursor;
        private MouseState mState;
        private int hover=-1;
        private string[] choice = { "Retry", "View Score","Quit" };
        private Texture2D background;
        private Game1 game;
        private StreamWriter sw;

        public CongratsScreen(Game1 game)
        {
            this.game = game;
            background = game.Content.Load<Texture2D>("screenBackground");
            cursor = game.Content.Load<Texture2D>("cursor");
            sw = new StreamWriter("scores.txt", true);
            sw.WriteLine(game.Score);
            sw.Close();
        }

        public void Update()
        {
            mState = Mouse.GetState();
            Vector2 mLook = new Vector2(mState.X,mState.Y);
            bool selected = false;
            int startAt = 300;
            int border = 20;
            hover = -1;

            if (mState.LeftButton == ButtonState.Pressed) selected = true ;

            for (int i = 0; i < choice.Count(); i++)
            {
                Vector2 textSize = game.SpriteFont.MeasureString(choice[i]);
                Vector2 position = new Vector2(game.Width/2-(textSize.X/2), startAt + ((textSize.Y + border) * i));
                Rectangle textBound = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);
                if ((mLook.X > textBound.X) && (mLook.X < textBound.X + textBound.Width)
                            && (mLook.Y > textBound.Y) && (mLook.Y < textBound.Y + textBound.Height))
                    hover = i;
            }

            if (selected)
            {
                if (hover == 0) game.levelSelect();
                if (hover == 1) game.ViewScore();
                if (hover == 2) game.Exit();
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textSize = Game1.Instance.SpriteFont.MeasureString("Hello");
            int startAt = 300;
            int border = 20;

            spriteBatch.Begin();
            spriteBatch.Draw(background,Vector2.Zero,Color.White);
            CentreText("Congratulations", 150, Color.Blue);
            CentreText("Your score is: "+game.Score, 200, Color.Orange);

            for (int i = 0; i < choice.Count(); i++)
            {
                Vector2 position = new Vector2(game.Width/2, startAt + ((textSize.Y + border) * i));
                if (i == hover)
                    CentreText(choice[i], position.Y,Color.Green);

                else
                    CentreText(choice[i], position.Y, Color.Red);
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
