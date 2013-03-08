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
    public class StartScreen
    {
        private KeyboardState lastState;
        private Game1 game;
        private SpriteFont Title { get; set; }
        private SpriteFont normalWriting { get; set; }
        private SpriteFont massiveWriting { get; set; }
        private Texture2D cursor;
        private Texture2D tankModel;
        private Texture2D enemyModel;
        private int hover = -1;

        public StartScreen(Game1 game)
        {
            this.game = game;
            lastState = Keyboard.GetState();
            Title = game.Content.Load<SpriteFont>("ScreenFont");
            normalWriting = game.Content.Load<SpriteFont>("Arial");
            cursor = game.Content.Load<Texture2D>("cursor");
            tankModel = game.Content.Load<Texture2D>("tank");
            enemyModel = game.Content.Load<Texture2D>("enemyTank");
            massiveWriting = game.Content.Load<SpriteFont>("Massive");
        }

        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();
            Vector2 mLook = new Vector2(mState.X, mState.Y);
            bool selected = false;
            hover =-1;

            if(mState.LeftButton == ButtonState.Pressed) selected = true;

            Vector2 textSize = Title.MeasureString("Play Game");
            Vector2 position = new Vector2(Game1.Instance.Width/2-(textSize.X/2),200.0f);
            Rectangle textBound = new Rectangle((int)position.X, (int)position.Y, (int)textSize.X, (int)textSize.Y);
            if ((mLook.X > textBound.X) && (mLook.X < textBound.X + textBound.Width)
                            && (mLook.Y > textBound.Y) && (mLook.Y < textBound.Y + textBound.Height))
                hover = 0;

            if (selected)
                if (hover == 0) game.levelSelect();

            lastState = kState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            MouseState mState = Mouse.GetState();
            Game1.Instance.GraphicsDevice.Clear(Color.SteelBlue);
            spriteBatch.Begin();
            CenterText("Tanky Ireland", 50.0f, Color.Red);
            if (hover == 0) CenterText("Play Game", 200.0f, Color.Black);
            else CenterText("Play Game", 200.0f, Color.Red);

            Game1.Instance.spriteBatch.Draw(tankModel, new Vector2(150, 350), null, Color.White, 0.0f, new Vector2(0,0), 2.0f, SpriteEffects.None, 1);
            spriteBatch.DrawString(normalWriting, "Player", new Vector2(175, 320), Color.Black);

            spriteBatch.DrawString(massiveWriting, "V.S.", new Vector2(350, 400), Color.Black);

            Game1.Instance.spriteBatch.Draw(enemyModel, new Vector2(600, 480),null, Color.White, (float)MathHelper.Pi, new Vector2(0, 0), 2.0f, SpriteEffects.None, 1);
            spriteBatch.DrawString(normalWriting, "Enemy", new Vector2(500, 320), Color.Black);

            game.spriteBatch.Draw(cursor, new Vector2(mState.X,mState.Y), Color.White);
            spriteBatch.End();
        }

        public void CenterText(string text, float y, Color color)
        {
            Vector2 textSize = Title.MeasureString(text);
            int midX = Game1.Instance.Width / 2;
            Game1.Instance.spriteBatch.DrawString(Title, text, new Vector2(midX - (textSize.X / 2), y), color);
        }
    }
}
