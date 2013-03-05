using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TrialGame
{

    enum GAMESTATE
    {
        START,
        SELECT,
        LEVEL1,
        LEVEL2,
        LEVEL3,
        END,
        FINISH,
        SCORE
    }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private static Game1 instance;
        public static Game1 Instance
        { get { return instance; } }

        private SpriteFont spriteFont;
        public SpriteFont SpriteFont { get { return spriteFont; } set { spriteFont = value; } }

        private List<GameEntity> entities = new List<GameEntity>();
        public List<GameEntity> Entities { get { return entities; } set { entities = value; } }

        private List<GameEntity> characterBullets = new List<GameEntity>();
        public List<GameEntity> CharacterBullets { get { return characterBullets; } set { characterBullets = value; } }

        private List<GameEntity> enemyBullet = new List<GameEntity>();
        public List<GameEntity> EnemyBullet { get { return enemyBullet; } set { enemyBullet = value; } }

        private List<GameEntity> enemies = new List<GameEntity>();
        public List<GameEntity> Enemies { get { return enemies; } set { enemies = value; } }

        public int Width
        { get { return Window.ClientBounds.Width; } }

        public int Height
        { get { return Window.ClientBounds.Height; } }

        public void playerHitEnemy() { EnemyCount--; }

        public void enemyHitPlayer() { Lives--; }

        public float TimeLimit { get; set; }
        public int EnemyCount { get; set; }
        public int Lives { get; set; }
        public int Ammo { get; set; }
        public int GameIndex { get; set; }
        public bool PowerFlag { get; set; }
        public int Score { get; set; }
        public Texture2D Background { get; set; }
        public Rectangle BackgroundRec { get; set; }

        StartScreen startScreen;
        GameOverScreen gameOverScreen;
        LevelSelectionScreen levelSelectionScreen;
        CongratsScreen congratsScreen;
        ScoreScreen scoreScreen;
        GamePlayLevel1 level1;
        GamePlayLevel2 level2;
        GamePlayLevel3 level3;

        public void ViewScore()
        {
            scoreScreen = new ScoreScreen(this);
            gameState = GAMESTATE.SCORE;

            congratsScreen = null;
        }

        public void Congrats()
        {
            congratsScreen = new CongratsScreen(this);
            gameState = GAMESTATE.FINISH;

            level1 = null;
            level2 = null;
            level3 = null;
        }

        public void levelSelect()
        {
            levelSelectionScreen = new LevelSelectionScreen(this);
            gameState = GAMESTATE.SELECT;

            startScreen = null;
        }

        public void LevelOne()
        {
            level1 = new GamePlayLevel1(this);
            gameState = GAMESTATE.LEVEL1;
            GameIndex = 1;

            levelSelectionScreen = null;

            Ammo = 20;
            TimeLimit = 30f;
            EnemyCount = 5;
            Lives = 3;
        }

        public void LevelTwo()
        {
            level2 = new GamePlayLevel2(this);
            gameState = GAMESTATE.LEVEL2;
            GameIndex = 2;

            levelSelectionScreen = null;

            Ammo = 40;
            TimeLimit = 40f;
            EnemyCount = 15;
            Lives = 3;
        }

        public void LevelThree()
        {
            level3 = new GamePlayLevel3(this);
            gameState = GAMESTATE.LEVEL3;
            GameIndex = 3;

            levelSelectionScreen = null;

            Ammo = 60;
            TimeLimit = 60f;
            EnemyCount = 25;
            Lives = 3;
        }

        public void calculateScore(float timer, int ammoLeft, int livesLeft)
        {
            int tempScore = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                if ((int)timer == i)
                {
                    tempScore += i*10;
                    break;
                }
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                if (ammoLeft == i)
                {
                    tempScore +=i * 20;
                    break;
                }
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                if (livesLeft == i)
                {
                    tempScore += i * 50;
                    break;
                }
            }

            Score = tempScore;
        }

        public void GameOver()
        {
            for (int i = 0; i < Entities.Count(); i++)
                Entities[i].Alive = false;

            for (int i = 0; i < Enemies.Count(); i++)
                Enemies[i].Alive = false;

            for (int i = 0; i < CharacterBullets.Count(); i++)
                CharacterBullets[i].Alive = false;

            for (int i = 0; i < EnemyBullet.Count(); i++)
                EnemyBullet[i].Alive = false;

            gameOverScreen = new GameOverScreen(this);
            gameState = GAMESTATE.END;

            level1 = null;
            level2 = null;
            level3 = null;
        }

        GAMESTATE gameState;

        public float enemySpawner()
        {
            Enemy enemy = new Enemy();
            enemy.LoadContent();
            Entities.Add(enemy);
            Enemies.Add(enemy);

            return 0f;
        }

        public void powerSpawner()
        {
            PowerUps power = new PowerUps();
            power.LoadContent();
            Entities.Add(power);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreen = new StartScreen(this);
            gameState = GAMESTATE.START;
            Background = Content.Load<Texture2D>("floormap");
            spriteFont = Content.Load<SpriteFont>("Arial");

            BackgroundRec = new Rectangle(-2, -2, (int)Background.Width + 4, (int)Background.Height + 4);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Escape))
                this.Exit();

            switch (gameState)
            {
                case GAMESTATE.START:
                    if (startScreen != null)
                        startScreen.Update();
                    break;
                case GAMESTATE.SELECT:
                    if (levelSelectionScreen != null)
                        levelSelectionScreen.Update();
                    break;
                case GAMESTATE.LEVEL1:
                    if (level1 != null)
                    {
                        level1.Update(gameTime, TimeLimit);
                    }
                    break;
                case GAMESTATE.LEVEL2:
                    if (level2 != null)
                    {
                        level2.Update(gameTime, TimeLimit);
                    }
                    break;
                case GAMESTATE.LEVEL3:
                    if (level3 != null)
                    {
                        level3.Update(gameTime, TimeLimit);
                    }
                    break;
                case GAMESTATE.END:
                    if (gameOverScreen != null)
                        gameOverScreen.Update();
                    break;
                case GAMESTATE.FINISH:
                    if (congratsScreen != null)
                        congratsScreen.Update();
                    break;
                case GAMESTATE.SCORE:
                    if (scoreScreen != null)
                        scoreScreen.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            switch (gameState)
            {
                case GAMESTATE.START:
                    if (startScreen != null)
                        startScreen.Draw(spriteBatch);
                    break;
                case GAMESTATE.SELECT:
                    if (levelSelectionScreen != null)
                        levelSelectionScreen.Draw(spriteBatch);
                    break;
                case GAMESTATE.LEVEL1:
                    if (level1 != null)
                        level1.Draw(spriteBatch, gameTime);
                    break;
                case GAMESTATE.LEVEL2:
                    if (level2 != null)
                    {
                        spriteBatch.Begin();
                        level2.Draw(spriteBatch, gameTime);
                        spriteBatch.End();
                    }
                    break;
                case GAMESTATE.LEVEL3:
                    if (level3 != null)
                    {
                        spriteBatch.Begin();
                        level3.Draw(spriteBatch, gameTime);
                        spriteBatch.End();
                    }
                    break;
                case GAMESTATE.END:
                    if (gameOverScreen != null)
                        gameOverScreen.Draw(spriteBatch);
                    break;
                case GAMESTATE.FINISH:
                    if (congratsScreen != null)
                        congratsScreen.Draw(spriteBatch);
                    break;
                case GAMESTATE.SCORE:
                    if (scoreScreen != null)
                        scoreScreen.Draw(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
