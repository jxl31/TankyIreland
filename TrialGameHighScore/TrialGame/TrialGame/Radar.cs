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
    public class Radar
    {
        Texture2D radar;
        Vector2 radarPos;
        Texture2D playerMini;
        Vector2 playerMiniPos;
        Texture2D enemyMini;
        float scale;

        public Radar()
        {
            radar = Game1.Instance.Content.Load<Texture2D>("floormap");
            playerMini = Game1.Instance.Content.Load<Texture2D>("playermini");
            enemyMini = Game1.Instance.Content.Load<Texture2D>("enemymini");
            scale = .12f;
        }

        public void Update(GameTime gameTime)
        {
            radarPos = new Vector2(Game1.Instance.Entities[0].Position.X - Game1.Instance.Width / 2 + Game1.Instance.Entities[0].Sprite.Width / 2,
                                   Game1.Instance.Entities[0].Position.Y + (scale * Game1.Instance.Background.Height) - Game1.Instance.Entities[0].Sprite.Height / 2);

            playerMiniPos = new Vector2(radarPos.X, radarPos.Y);
        }

        public void Draw()
        {
            Game1.Instance.spriteBatch.Draw(radar, radarPos, null, Color.YellowGreen, 0, Vector2.Zero, new Vector2(scale, scale), SpriteEffects.None, 0);

            Game1.Instance.spriteBatch.Draw(playerMini,playerMiniPos,
        }
    }
}
