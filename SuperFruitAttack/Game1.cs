﻿using Microsoft.Xna.Framework;
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
        private Texture2D peaEnemy;
        private Texture2D carrotEnemy;
        private Texture2D potatoEnemy;
        private Texture2D powerUp;
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
            powerUp = Content.Load<Texture2D>("powerUp");
            peaEnemy = Content.Load<Texture2D>("peapod");
            carrotEnemy = Content.Load<Texture2D>("carrot");
            potatoEnemy = Content.Load<Texture2D>("potato");
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
                            menu.X = _graphics.PreferredBackBufferWidth / 2 - 150;
                            menu.Y = 30;
                            start.X = _graphics.PreferredBackBufferHeight / 2 + 150;
                            start.Y = 30;
                            status = GameStages.instructions;
                        }
                        break;
                    case GameStages.instructions:
                        
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            start.X = _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2;
                            start.Y = _graphics.PreferredBackBufferHeight / 2;
                            status = GameStages.menu;
                        }
                        if(start.IsClicked(previousMouse) == true)
                        {
                            
                            menu.Y = _graphics.PreferredBackBufferHeight / 2 + 30;
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
                                menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                                menu.Y = _graphics.PreferredBackBufferHeight / 2;
                                status = GameStages.winGame;
                            }
                            if(GameObjectManager.Player.Health <= 0)
                            {
                                menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                                menu.Y = _graphics.PreferredBackBufferHeight / 2;
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
                            menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                            menu.Y = _graphics.PreferredBackBufferHeight / 2;
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
                            start.X = _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2;
                            start.Y = _graphics.PreferredBackBufferHeight / 2;
                            status = GameStages.menu;
                        }
                        break;
                    case GameStages.gameOver:
                       
                        if(menu.IsClicked(previousMouse) == true)
                        {
                            start.X = _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2;
                            start.Y = _graphics.PreferredBackBufferHeight / 2;
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
                    pause.Image = pauseButton;
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
                    _spriteBatch.Draw(peaEnemy, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 115,
                                                                        _graphics.PreferredBackBufferHeight / 2 + 18,
                                                                        peaEnemy.Width + 10,
                                                                        peaEnemy.Height + 10),
                                                                        Color.White);
                    _spriteBatch.Draw(carrotEnemy,new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 50,
                                                                       _graphics.PreferredBackBufferHeight / 2 + 60,
                                                                       peaEnemy.Width + 10,
                                                                       peaEnemy.Height + 10),
                                                                       Color.White);
                    _spriteBatch.Draw(potatoEnemy, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 70,
                                                                       _graphics.PreferredBackBufferHeight / 2 + 110,
                                                                       peaEnemy.Width + 10,
                                                                       peaEnemy.Height + 10),
                                                                       Color.White);
                    _spriteBatch.Draw(powerUp, new Rectangle(_graphics.PreferredBackBufferWidth / 2 + 125,
                                                                       _graphics.PreferredBackBufferHeight / 2 + 50,
                                                                       65,
                                                                       65),
                                                                       Color.White);
                    _spriteBatch.DrawString(arial16bold, "Collect powerups to gain \n" +
                                                         "      a stat boost.",
                                                         new Vector2(_graphics.PreferredBackBufferWidth / 2 + 40,
                                                         _graphics.PreferredBackBufferHeight / 2 - 30),
                                                         Color.Black);
                    _spriteBatch.DrawString(arial16bold, "There are 3 enemy types:\n\n" +
                                                         "peapod(shoots downward)\n\n" +
                                                         "carrot(shoots in player's direction)\n\n" +
                                                         "potato(wanders back and forth)",
                                                         new Vector2(15,
                                                         _graphics.PreferredBackBufferHeight / 2 - 30),
                                                         Color.Black);
                    _spriteBatch.DrawString(arial16bold, "A - move left, " +
                                                         "D - move right, " +
                                                         "W - Jump, " +
                                                         "J - Shoot, " +
                                                         "G - Activate God-Mode ",
                                            new Vector2(50,
                                                        160),
                                                        Color.Black);
                    _spriteBatch.DrawString(arial16bold, "*When God-Mode is active, your character"
                                                          + "appear dimmer.*\n" +
                                                          "To Wallclimb, jump onto a wall, hold the " +
                                                          "movement key facing in the direction of\n" +
                                                          "wall, and jump off and move in the " +
                                                          "opposite direction.",
                                           new Vector2(15,
                                                       _graphics.PreferredBackBufferHeight - 80),
                                                       Color.Black); ; ; 
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

                        GameObjectManager.Player.DrawPowerUps(_spriteBatch);
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
                                _graphics.PreferredBackBufferHeight / 2 - 90),
                                Color.Gold);
                    menu.Draw(_spriteBatch);
                    break;
                case GameStages.gameOver:
                    menu.Draw(_spriteBatch);
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 100,
                                _graphics.PreferredBackBufferHeight/2 - 90),
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
