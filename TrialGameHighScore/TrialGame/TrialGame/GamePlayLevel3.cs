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
    public class GamePlayLevel3
    {
        private Game1 game;
        private float timer = 0f;
        private float enemySpawnTimer = 0f;
        private int[] powerSpawn = new int[9];
        private Random random = new Random(System.DateTime.Now.Millisecond);

        public GamePlayLevel3(Game1 game)
        {
            this.game = game;
            GameEntity character, enemy;

            character = new Character();
            enemy = new Enemy();
            game.Entities.Add(character);
            game.Entities.Add(enemy);
            game.Enemies.Add(enemy);

            for (int i = 0; i < 9; i++)
                powerSpawn[i] = random.Next(60);

            for (int i = 0; i < game.Entities.Count(); i++)
                game.Entities[i].LoadContent();

            Game1.Instance.PowerFlag = true;

            game.SpriteFont = Game1.Instance.Content.Load<SpriteFont>("Arial");
        }

        public void Update(GameTime gameTime,float timeLimit)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemySpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Game1.Instance.PowerFlag)
                for (int i = 0; i < 9; i++)
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

            if (enemySpawnTimer > (float)3)
                if (game.Enemies.Count() < 4)
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
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            game.spriteBatch.Draw(game.Background, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), 1.0f, SpriteEffects.None, 1);
            for (int i = 0; i < game.Entities.Count; i++)
            {
                game.Entities[i].Draw(gameTime);
            }
            spriteBatch.DrawString(game.SpriteFont, "Enemy Left: " + game.EnemyCount, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Lives Left: " + game.Lives, new Vector2(10, 30), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Ammos Left: " + game.Ammo, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Time: " + timer.ToString("0.00"), new Vector2(250, 60), Color.Black);
            spriteBatch.DrawString(game.SpriteFont, "Time Limit: " + game.TimeLimit.ToString("0.00"), new Vector2(250, 30), Color.Red);
        }
    }
}
