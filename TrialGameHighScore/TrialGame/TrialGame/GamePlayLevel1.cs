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
    public class GamePlayLevel1
    {
        private Game1 game;
        private float timer = 0;
        private float enemySpawnTimer = 0f;
        private int[] powerSpawn = new int[4];
        private Random random = new Random(System.DateTime.Now.Millisecond);
        private Camera camera;
        private Radar radar;

        public GamePlayLevel1(Game1 game)
        {
            this.game = game;
            GameEntity character, enemy;

            camera = new Camera(game.GraphicsDevice.Viewport);

            character = new Character();
            enemy = new Enemy();
            game.Entities.Add(character);
            game.Entities.Add(enemy);
            game.Enemies.Add(enemy);

            radar = new Radar();

            for (int i = 0; i < game.Entities.Count; i++)
                game.Entities[i].LoadContent();

            for (int i = 0; i < 4; i++)
                powerSpawn[i] = random.Next(30);

            Game1.Instance.PowerFlag = true;

            game.SpriteFont = game.Content.Load<SpriteFont>("Arial");
        }

        public void Update(GameTime gameTime, float timeLimit)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(Game1.Instance.PowerFlag)
                for (int i = 0; i < 4; i++)
                    if (powerSpawn[i] == (int)timer)
                    {
                        Game1.Instance.powerSpawner();
                        Game1.Instance.PowerFlag = false;
                    }

            if (game.EnemyCount == 0)
            {
                game.calculateScore(timer, game.Ammo, game.Lives);
                game.Congrats();
            }

            if (timer >= timeLimit)
                game.GameOver();

            if (game.Enemies.Count() == 0)
                enemySpawnTimer = Game1.Instance.enemySpawner();

            if (enemySpawnTimer > (float)5)
                if(game.Enemies.Count() < 4)
                    enemySpawnTimer = game.enemySpawner();

            for (int i = 0; i < game.Entities.Count(); i++)
            {
                game.Entities[i].Update(gameTime);
                if (game.Entities[i].Alive == false)
                    game.Entities.RemoveAt(i);
            }

            for (int i = 0; i < game.CharacterBullets.Count(); i++)
                if (game.CharacterBullets[i].Alive == false)
                    game.CharacterBullets.RemoveAt(i);

            for (int i = 0; i < game.EnemyBullet.Count(); i++)
                if (game.EnemyBullet[i].Alive == false)
                    game.EnemyBullet.RemoveAt(i);

            for (int i = 0; i < game.Enemies.Count(); i++)
                if (game.Enemies[i].Alive == false)
                    game.Enemies.RemoveAt(i);

            camera.Update(gameTime, game.Entities[0].Position,game.Entities[0].BoundingBox);
            radar.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                            BlendState.AlphaBlend,
                            null, null, null, null,
                            camera.transform);
            game.spriteBatch.Draw(game.Background, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0),1.0f, SpriteEffects.None, 1);
            for (int i = 0; i < game.Entities.Count; i++)
            {
                game.Entities[i].Draw(gameTime);
            }
            spriteBatch.DrawString(game.SpriteFont, "Enemy Left: " + game.EnemyCount, new Vector2(game.Entities[0].Position.X-350,game.Entities[0].Position.Y-255), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Lives Left: " + game.Lives, new Vector2(game.Entities[0].Position.X - 350, game.Entities[0].Position.Y - 235), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Ammos Left: " + game.Ammo, new Vector2(game.Entities[0].Position.X - 350, game.Entities[0].Position.Y - 215), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Time: " + timer.ToString("0.00"), new Vector2(game.Entities[0].Position.X, game.Entities[0].Position.Y - 255), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Time Limit: " + game.TimeLimit.ToString("0.00"), new Vector2(game.Entities[0].Position.X + 280, game.Entities[0].Position.Y - 255), Color.Red);

            radar.Draw();

            spriteBatch.End();
        }
    }
}
