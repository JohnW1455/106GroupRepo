using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SuperFruitAttack.Colliders;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using Microsoft.Xna.Framework.Media;

namespace SuperFruitAttack
{
    public enum GameStages { menu, instructions, gamePlay, gameOver, winGame, transition, pause};
    public enum PlayerState { faceLeft, faceRight, walkLeft, walkRight, jumpLeft, jumpRight};
    public class Game1 : Game
    {
        public const int RESOLUTION = 32;
        public static Song ShootSound;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState previousMouse;

        private GameStages status;

        private Texture2D startButton;
        private Texture2D instructionsButton;
        private Texture2D menuBtton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;

        private SpriteFont gameTitle;
        private Button start;
        private Button menu;
        private Button instructions;
        private Button pause;
        private SpriteFont arial16bold;
        private double transitionTime;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = Resources.ROOT_DIRECTORY;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            MediaPlayer.Volume = .1f;
            status = GameStages.menu;
            transitionTime = 2;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.Init(Content);
            LevelManager.LoadLevels();
            
            gameTitle = Content.Load<SpriteFont>("Text/Titles/Roboto36");
            pauseButton = Content.Load<Texture2D>("Images/buttons/pause button");
            resumeButton = Content.Load<Texture2D>("Images/buttons/Resume");
            startButton = Content.Load<Texture2D>("start button");
            instructionsButton = Content.Load<Texture2D>("Images/instructions");
            menuBtton = Content.Load<Texture2D>("Images/buttons/menu");

            pause = new Button(pauseButton,
                               _graphics.PreferredBackBufferWidth / 2 - pauseButton.Width / 4,
                               _graphics.PreferredBackBufferHeight / 2,
                               pauseButton.Width/2,
                               pauseButton.Height/2);

            start = new Button( startButton,
                                _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 4,
                                _graphics.PreferredBackBufferHeight / 2 ,
                                startButton.Width/2,
                                startButton.Height/2);
            instructions = new Button(instructionsButton,
                                      _graphics.PreferredBackBufferWidth / 2 - instructionsButton.Width / 4,
                                      _graphics.PreferredBackBufferHeight / 2 + 100,
                                      instructionsButton.Width/2,
                                      instructionsButton.Height/2);
            menu = new Button(menuBtton,
                              _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 4,
                              _graphics.PreferredBackBufferHeight / 2 - menuBtton.Height,
                              menuBtton.Width/2,
                              menuBtton.Height/2);

            // Used to print out variables during gameplay for debugging
            arial16bold = Content.Load<SpriteFont>("arial16bold");

            ShootSound = Content.Load<Song>("boomph");

            // TODO: use this.Content to load your game content here
            // REMOVE AFTER TESTING
            // REMOVE AFTER TESTING
            // REMOVE AFTER TESTING
            // REMOVE AFTER TESTING
            // REMOVE AFTER TESTING
            //LevelManager.NextLevel(); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here 
            if(status != GameStages.pause)
            {
                switch(status)
                {
                    case GameStages.menu:
                        LevelManager.CurrentLevelNumber = 0;
                        if (start.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.transition;
                        }
                        else if(instructions.IsClicked(previousMouse) == true)
                        { 
                            menu.Y = _graphics.PreferredBackBufferHeight / 2 - 50;
                            status = GameStages.instructions;
                        }
                        break;
                    case GameStages.instructions:
                        menu.Y = 30;
                        start.Y = 120;
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            start.Y = _graphics.PreferredBackBufferHeight / 2;
                            status = GameStages.menu;
                        }
                        if(start.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.transition;
                        }
                        break;
                    case GameStages.gamePlay:
                        pause.X = _graphics.PreferredBackBufferWidth - pause.Width - 10;
                        pause.Y = 10;
                        GameObjectManager.Tick(gameTime);
                        if (LevelManager.CurrentLevelNumber < LevelManager.LevelCount && 

                            GameObjectManager.Flag.CheckCollision(GameObjectManager.Player))
                        {
                            status = GameStages.transition;
                        }
                        if(LevelManager.CurrentLevelNumber == LevelManager.LevelCount &&
                            GameObjectManager.Flag.CheckCollision(GameObjectManager.Player))
                        {
                            status = GameStages.winGame;
                        }
                        if(GameObjectManager.Player.Health <= 0)
                        {
                            status = GameStages.gameOver;
                        }
                        if(pause.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.pause;
                            pause.Image = resumeButton;
                        }
                        break;
                    case GameStages.transition:
                        transitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                        if(transitionTime <= 0)
                        {
                            LevelManager.NextLevel();
                            status = GameStages.gamePlay;
                            transitionTime = 2;
                        }
                        break;
                    case GameStages.winGame:
                        if(menu.IsClicked(previousMouse) == true )
                        {
                            status = GameStages.menu;
                        }
                        break;
                    case GameStages.gameOver:
                       
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.menu;
                        }
                        break;  
                }
            }
            else
            {
                if(pause.IsClicked(previousMouse) == true)
                {
                    pause.Image = pauseButton;
                    status = GameStages.gamePlay;
                }
            }
            previousMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (status == GameStages.gamePlay)
            {
                _spriteBatch.Begin(transformMatrix: GameObjectManager.CameraMatrix(
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight,
                    LevelManager.CurrentLevel.PixelWidth, LevelManager.CurrentLevel.PixelHeight));
            }
            else
            {
                _spriteBatch.Begin();
            }
            // TODO: Add your drawing code here
            switch(status)
            {
                case GameStages.menu:
                    _spriteBatch.DrawString(gameTitle, "Super Fruit Attack",
                        new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                    _graphics.PreferredBackBufferHeight / 2 - 150),
                                    Color.White);
                    start.Draw(_spriteBatch);
                    instructions.Draw(_spriteBatch);
                    break;
                case GameStages.instructions:
                    _spriteBatch.DrawString(arial16bold, "A - move left \n" +
                                                         "D - move right \n" +
                                                         "Space - Jump \n" +
                                                         "Left Mouse Button - Shoot",
                                            new Vector2(_graphics.PreferredBackBufferWidth / 2 - 60,
                                                        _graphics.PreferredBackBufferHeight / 2 ),
                                                        Color.White);
                    menu.Draw(_spriteBatch);
                    start.Draw(_spriteBatch);
                    break;
                case GameStages.gamePlay:
                    GameObjectManager.Draw(_spriteBatch);
                    pause.Draw(_spriteBatch);
                    break;
                case GameStages.transition:
                    _spriteBatch.DrawString(arial16bold, "Loading", 
                                            new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                            _graphics.PreferredBackBufferHeight / 2 - 50),
                                            Color.White);
                    break;
                case GameStages.winGame:
                    _spriteBatch.DrawString(gameTitle, "You Win",
                                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                _graphics.PreferredBackBufferHeight / 2 - 200),
                                Color.White);
                    menu.Draw(_spriteBatch);
                    break;
                case GameStages.gameOver:
                    menu.Draw(_spriteBatch);
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 50,
                                _graphics.PreferredBackBufferHeight/2 - 150),
                                Color.White);
                    break;
                case GameStages.pause:
                    pause.Draw(_spriteBatch);
                    _spriteBatch.DrawString(arial16bold, "PAUSED",
                                    new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                _graphics.PreferredBackBufferHeight / 2 - 200),
                                Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
