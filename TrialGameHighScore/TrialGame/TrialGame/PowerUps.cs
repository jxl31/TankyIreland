﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TrialGame
{
    public class PowerUps:GameEntity
    {
        Random random = new Random(System.DateTime.Now.Millisecond);
        public int which;

        public override void LoadContent()
        {
            which = (int)(new Random().NextDouble() * 2);
            Sprite = Game1.Instance.Content.Load<Texture2D>("power"+ which);
            Position = new Vector2(random.Next(Game1.Instance.Width - 40), random.Next(Game1.Instance.Height / 2));
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Game1.Instance.CharacterBullets.Count(); i++)
                if (BoundingBox.Intersects(Game1.Instance.CharacterBullets[i].BoundingBox))
                {
                    if (which == 0) LifeUp(i);
                    if (which == 1) AmmoUp(i);
                    if (which == 2) Abomination(i);
                }      
        }

        public void LifeUp(int x)
        {
            Alive = false;
            Game1.Instance.CharacterBullets[x].Alive = false;
            Game1.Instance.PowerFlag = true;
            if (Game1.Instance.Lives < 3)
                Game1.Instance.Lives++;
        }

        public void AmmoUp(int x)
        {
            Alive = false;
            Game1.Instance.CharacterBullets[x].Alive = false;
            Game1.Instance.PowerFlag = true;
            Game1.Instance.Ammo += 5;
        }

        public void Abomination(int x)
        {
            Alive = false;
            Game1.Instance.CharacterBullets[x].Alive = false;
            Game1.Instance.PowerFlag = true;
            for (int i = 0; i < Game1.Instance.Enemies.Count(); i++)
            {
                Game1.Instance.Enemies[i].Alive = false;
                Game1.Instance.playerHitEnemy();
            }
        }



        public override void Draw(GameTime gameTime)
        {
            Vector2 center = new Vector2();
            center.X = Sprite.Width / 2;
            center.Y = Sprite.Height / 2;
            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White, 0.0f, center, 1.0f, SpriteEffects.None, 1);
        }
    }
}