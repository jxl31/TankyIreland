using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TrialGame
{
    public class ScoreScreen
    {
        private Texture2D background;
        private StreamReader sr;
        private string line;
        private Game1 game;
        private Texture2D cursor;

        public ScoreScreen(Game1 game)
        {
            this.game = game;
            background = game.Content.Load<Texture2D>("screenBackground");
            cursor = game.Content.Load<Texture2D>("cursor");
        }

        public void Update() { }

        public void Draw(SpriteBatch spriteBatch)
        {
            int i = 0;
            sr = new StreamReader("scores.txt");
            spriteBatch.Begin();
            Vector2 textSize = Game1.Instance.SpriteFont.MeasureString("Hello");
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            int startAt = 100;
            int border = 5;

            CentreText("Scores",10,Color.Blue);

            while ((line = sr.ReadLine()) != null)
            {
                Vector2 position = new Vector2(50, startAt + ((textSize.Y + border) * i));
                spriteBatch.DrawString(game.SpriteFont,(i+1)+". "+line, position, Color.Orange);
                i++;
            }
            sr.Close();
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
