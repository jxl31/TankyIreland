using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TrialGame
{
    public class PowerUps:GameEntity
    {
        Random random = new Random(System.DateTime.Now.Millisecond);
        public int which;
        SoundEffect reload;
        SoundEffect heal;
        SoundEffect nuclear;
        

        public override void LoadContent()
        {
            which = random.Next(0, 2);
            Sprite = Game1.Instance.Content.Load<Texture2D>("PowerUps/power"+ which);
            Position = new Vector2(random.Next(Game1.Instance.Background.Width - Sprite.Width), random.Next(Game1.Instance.Background.Height-Sprite.Height));
            reload = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/reload");
            heal = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/heal");

            //not working
            nuclear = Game1.Instance.Content.Load<SoundEffect>("SoundEffect/nuclear");
        }

        public override void Update(GameTime gameTime)
        {
            if (BoundingBox.Intersects(Game1.Instance.Entities[0].BoundingBox))
            {
                if (which == 0) LifeUp();
                if (which == 1) AmmoUp();
                if (which == 2) Abomination();
            }
            else
            {
                for (int i = 0; i < Game1.Instance.CharacterBullets.Count(); i++)
                {
                    if (BoundingBox.Intersects(Game1.Instance.CharacterBullets[i].BoundingBox))
                    {
                        if (which == 0) LifeUp(i);
                        if (which == 1) AmmoUp(i);
                        if (which == 2) Abomination(i);
                    }
                }
            }
        }
        public void LifeUp()
        {
            Alive = false;
            Game1.Instance.PowerFlag = true;
            if (Game1.Instance.Health < 100)
            {
                Game1.Instance.Health += 20;
                heal.Play();
            }
            else
            {
                Game1.Instance.Health = 100;
                heal.Play();
            }

        }

        public void AmmoUp()
        {
            Alive = false;
            Game1.Instance.PowerFlag = true;
            Game1.Instance.Ammo += 10;
            reload.Play();
        }

        //not working
        public void Abomination()
        {
            Alive = false;
            Game1.Instance.PowerFlag = true;
            nuclear.Play();
            for (int i = 0; i < Game1.Instance.Enemies.Count(); i++)
            {
                Game1.Instance.Enemies[i].Alive = false;
                Game1.Instance.playerHitEnemy();
            }
        }

        public void LifeUp(int x)
        {
            Alive = false;
            Game1.Instance.CharacterBullets[x].Alive = false;
            Game1.Instance.PowerFlag = true;
            if (Game1.Instance.Health < 100)
            {
                Game1.Instance.Health += 20;
                heal.Play();
            }
            else
            {
                Game1.Instance.Health = 100;
                heal.Play();
            }
        }

        public void AmmoUp(int x)
        {
            Alive = false;
            reload.Play();
            Game1.Instance.CharacterBullets[x].Alive = false;
            Game1.Instance.PowerFlag = true;
            Game1.Instance.Ammo += 10;
        }

        //not working
        public void Abomination(int x)
        {
            Alive = false;
            nuclear.Play();
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
