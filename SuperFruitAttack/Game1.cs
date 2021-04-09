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
        private Texture2D playerAvatar;
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
            playerAvatar = Content.Load<Texture2D>("Images/Player/simple stickman");
            pause = new Button(pauseButton,
                               _graphics.PreferredBackBufferWidth / 2 - pauseButton.Width / 2,
                               _graphics.PreferredBackBufferHeight / 2,
                               pauseButton.Width,
                               pauseButton.Height);

            start = new Button( startButton,
                                _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2,
                                _graphics.PreferredBackBufferHeight / 2 ,
                                startButton.Width,
                                startButton.Height);
            instructions = new Button(instructionsButton,
                                      _graphics.PreferredBackBufferWidth / 2 - instructionsButton.Width / 2,
                                      _graphics.PreferredBackBufferHeight / 2 + 150,
                                      instructionsButton.Width,
                                      instructionsButton.Height);
            menu = new Button(menuBtton,
                              _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2,
                              _graphics.PreferredBackBufferHeight / 2 - menuBtton.Height,
                              menuBtton.Width,
                              menuBtton.Height);

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
                        LevelManager.CurrentLevel = 0;
                        if (start.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.transition;
                        }
                        else if(instructions.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.instructions;
                        }
                        break;
                    case GameStages.instructions:
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.menu;
                        }
                        if(start.IsClicked(previousMouse) == true)
                        {
                            status = GameStages.transition;
                        }
                        break;
                    case GameStages.gamePlay:
                        GameObjectManager.Tick(gameTime);
                        GameObjectManager.CheckCollision();
                        if (LevelManager.CurrentLevel < LevelManager.LevelCount && 
                            GameObjectManager.Flag.CheckCollision(GameObjectManager.Player))
                        {
                            status = GameStages.transition;
                        }
                        else if(LevelManager.CurrentLevel == LevelManager.LevelCount &&
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
                           
                            LevelManager.RestartLevel();
                            status = GameStages.menu;
                        }
                        break;
                    case GameStages.gameOver:
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            LevelManager.RestartLevel();
                            status = GameStages.menu;

                        }
                        break;  
                }
            }
            else
            {
                if(pause.IsClicked(previousMouse) == true)
                {
                    pause.Image = resumeButton;
                    status = GameStages.gamePlay;
                }
            }
            
            previousMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch(status)
            {
                case GameStages.menu:
                    _spriteBatch.DrawString(gameTitle, "Super Fruit Attack",
                        new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                    _graphics.PreferredBackBufferHeight / 2 - 200),
                                    Color.White);
                    start.Draw(_spriteBatch);
                    instructions.Draw(_spriteBatch);
                    break;
                case GameStages.instructions:
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
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 50,
                                _graphics.PreferredBackBufferHeight/2 - 200),
                                Color.White);
                    menu.Draw(_spriteBatch);
                    break;
                case GameStages.pause:
                    pause.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
