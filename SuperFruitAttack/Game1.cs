﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Threading.Tasks;
using System.Diagnostics;

namespace SuperFruitAttack
{
    public enum GameStages { menu, instructions, gameplay, gameOver, winGame, transition};
    public class Game1 : Game
    {
        public const int RESOLUTION = 32;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState previousMouse;
        private GameStages status;
        private GameObjectManager objectManager;
        private Dictionary<string, Texture2D> images;
        private Texture2D startButton;
        private Texture2D instructionsButton;
        private Button start;
        
        private Button instructions;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            status = GameStages.menu;
            objectManager = new GameObjectManager();
            images = new Dictionary<string, Texture2D>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Level.LoadTextures(Content);
            startButton = Content.Load<Texture2D>("start button");
            instructionsButton = Content.Load<Texture2D>("Images/instructions");
            start = new Button( startButton,
                                _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2,
                                _graphics.PreferredBackBufferHeight / 2 - 3 * startButton.Height,
                                startButton.Width,
                                startButton.Height);
            instructions = new Button(instructionsButton,
                                      _graphics.PreferredBackBufferWidth / 2 - instructionsButton.Width / 2,
                                      _graphics.PreferredBackBufferHeight / 2 - 2 * instructionsButton.Height,
                                      instructionsButton.Width,
                                      instructionsButton.Height);
            // TODO: use this.Content to load your game content here
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch(status)
            {
                case GameStages.menu:
                    if(start.IsClicked(previousMouse) == true)
                    {
                        status = GameStages.gameplay;
                    }
                    
                    break;
                case GameStages.instructions:
                    break;
                case GameStages.gameplay:
                    break;
                case GameStages.transition:
                    break;
                case GameStages.winGame:
                    break;
                case GameStages.gameOver:
                    break;
            }
            previousMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch(status)
            {
                case GameStages.menu:
                    break;
                case GameStages.instructions:
                    break;
                case GameStages.gameplay:
                    break;
                case GameStages.transition:
                    break;
                case GameStages.winGame:
                    break;
                case GameStages.gameOver:
                    break;
            }
            base.Draw(gameTime);
        }

        public void NextLevel()
        {

        }
    }
}
