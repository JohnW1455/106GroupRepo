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
    /* Authors: Nathan Caron, Elliot Gong
     * Purpose: Handle game transition and stages.
     * Date: 4/2/2021*/
    public enum GameStages { menu, instructions, gamePlay, gameOver, winGame, transition, pause};
    public enum PlayerState { faceLeft, faceRight, walkLeft, walkRight, jumpLeft, jumpRight};
    public class Game1 : Game
    {
        public const int RESOLUTION = 32;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState previousMouse;
        private KeyboardState previousKey;
        private GameStages status;

        private Texture2D startButton;
        private Texture2D instructionsButton;
        private Texture2D menuBtton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;
        private Texture2D title;

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

            pauseButton = Content.Load<Texture2D>("Images/buttons/pause");
            resumeButton = Content.Load<Texture2D>("Images/resume");
            startButton = Content.Load<Texture2D>("Images/buttons/play");
            instructionsButton = Content.Load<Texture2D>("Images/buttons/instructions");
            menuBtton = Content.Load<Texture2D>("Images/menu");
            title = Content.Load<Texture2D>("Images/title");

         

            pause = new Button(pauseButton,
                               _graphics.PreferredBackBufferWidth / 2 - pauseButton.Width / 2,
                               _graphics.PreferredBackBufferHeight / 2,
                               pauseButton.Width/2,
                               pauseButton.Height/2);

            start = new Button( startButton,
                                _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2,
                                _graphics.PreferredBackBufferHeight / 2 ,
                                startButton.Width,
                                startButton.Height);
            instructions = new Button(instructionsButton,
                                      _graphics.PreferredBackBufferWidth / 2 - instructionsButton.Width / 2,
                                      _graphics.PreferredBackBufferHeight / 2 + 100,
                                      instructionsButton.Width,
                                      instructionsButton.Height);
            menu = new Button(menuBtton,
                              _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2,
                              _graphics.PreferredBackBufferHeight / 2 - menuBtton.Height,
                              menuBtton.Width,
                              menuBtton.Height);

            // Used to print out variables during gameplay for debugging
            arial16bold = Content.Load<SpriteFont>("arial16bold");
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
                        if(GameObjectManager.Player != null)
                        {
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
                            if(GameObjectManager.Player.ColliderObject.Bounds.Y >= 
                               LevelManager.CurrentLevel.PixelHeight)
                            {
                                // Drops player health to 0 to prevent possible bugs
                                    GameObjectManager.Player.Health = 0;
                                    status = GameStages.gameOver;
                            }
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
                if(menu.IsClicked(previousMouse) == true)
                {
                    status = GameStages.menu;
                }

            }
            previousKey = Keyboard.GetState();
            previousMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                Resources.GetTexture("background"), 
                new Rectangle(0, 0, 
                    _graphics.PreferredBackBufferWidth, 
                    _graphics.PreferredBackBufferHeight),
                Color.White);
            if (status == GameStages.gamePlay && GameObjectManager.Player != null)
            {
                _spriteBatch.End();
                 _spriteBatch.Begin(transformMatrix: GameObjectManager.CameraMatrix(
                 _graphics.PreferredBackBufferWidth,
                 _graphics.PreferredBackBufferHeight,
                 LevelManager.CurrentLevel.PixelWidth, LevelManager.CurrentLevel.PixelHeight));
            }
            switch(status)
            {
                case GameStages.menu:
                    _spriteBatch.Draw(title, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - title.Width / 4,
                                                           _graphics.PreferredBackBufferHeight / 2 - title.Height / 2 - 40,
                                                           title.Width / 2,
                                                           title.Height / 2),
                                                           Color.White);
                    start.Draw(_spriteBatch);
                    instructions.Draw(_spriteBatch);
                    break;
                case GameStages.instructions:
                    _spriteBatch.DrawString(arial16bold, "A - move left \n" +
                                                         "D - move right \n" +
                                                         "W - Jump \n" +
                                                         "Space - Shoot \n" +
                                                         "Activate GodMode - G",
                                            new Vector2(_graphics.PreferredBackBufferWidth / 2 - 60,
                                                        _graphics.PreferredBackBufferHeight / 2  - 30),
                                                        Color.Black);
                    menu.Draw(_spriteBatch);
                    start.Draw(_spriteBatch);
                    break;
                case GameStages.gamePlay:
                    GameObjectManager.Draw(_spriteBatch);
                    _spriteBatch.End();
                    _spriteBatch.Begin();
                    if(GameObjectManager.Player != null)
                    {
                        switch (GameObjectManager.Player.Health)
                        {
                            case 4:
                                _spriteBatch.Draw(Resources.GetTexture("hpFull"), new Vector2(15, 10), Color.White);
                                break;
                            case 3:
                                _spriteBatch.Draw(Resources.GetTexture("hpThird"), new Vector2(15, 10), Color.White); 
                                break;
                            case 2:
                                _spriteBatch.Draw(Resources.GetTexture("hpHalf"), new Vector2(15, 10), Color.White);
                                break;
                            case 1:
                                _spriteBatch.Draw(Resources.GetTexture("hpQuart"), new Vector2(15, 10), Color.White);
                                break;
                        }

                    }
                    
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
                                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100,
                                _graphics.PreferredBackBufferHeight / 2 - 200),
                                Color.Gold);
                    menu.Draw(_spriteBatch);
                    break;
                case GameStages.gameOver:
                    menu.Draw(_spriteBatch);
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 100,
                                _graphics.PreferredBackBufferHeight/2 - 200),
                                Color.DarkRed);
                    break;
                case GameStages.pause:
                    menu.Draw(_spriteBatch);
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
