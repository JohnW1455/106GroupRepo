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

namespace SuperFruitAttack
{
    public enum GameStages { menu, instructions, gameplay, gameOver, winGame, transition};
    public enum PlayerState { faceLeft, faceRight, walkLeft, walkRight, jumpLeft, jumpRight, dead};
    public class Game1 : Game
    {
        public const int RESOLUTION = 32;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseState previousMouse;
        private GameStages status;
        private Texture2D startButton;
        private Texture2D instructionsButton;
        private Texture2D menuBtton;
        private Texture2D playerAvatar;
        private SpriteFont gameTitle;
        private Button start;
        private Button menu;
        private Button instructions;
        private Player p1;
        
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Resources.Init(Content);
            LevelManager.LoadLevels();
            gameTitle = Content.Load<SpriteFont>("Text/Titles/Roboto36");
            startButton = Content.Load<Texture2D>("start button");
            instructionsButton = Content.Load<Texture2D>("Images/instructions");
            menuBtton = Content.Load<Texture2D>("Images/buttons/menu");
            playerAvatar = Content.Load<Texture2D>("Images/Player/simple stickman");
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
            menu = new Button(menuBtton,
                              _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2,
                              _graphics.PreferredBackBufferHeight / 2 - 3 * menuBtton.Height,
                              menuBtton.Width,
                              menuBtton.Height);

            p1 = new Player(100, 10, playerAvatar, new BoxCollider(30,30,playerAvatar.Width, playerAvatar.Height));
            GameObjectManager.AddObject(p1);
            
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
                    break;
                case GameStages.gameplay:
                    GameObjectManager.Player.FireGun(gameTime);
                    GameObjectManager.Player.Tick(gameTime);
                    GameObjectManager.CheckCollision();
                    if(GameObjectManager.Player.Health == 0 || GameObjectManager.Player == null)
                    {
                        status = GameStages.gameOver;
                    }
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

            _spriteBatch.Begin();
            LevelManager.DrawLevel(_spriteBatch);
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

                    break;
                case GameStages.gameplay:
                    GameObjectManager.Draw(_spriteBatch);
                    GameObjectManager.Player.Draw(_spriteBatch);
                    break;
                case GameStages.transition:
                    break;
                case GameStages.winGame:
                    break;
                case GameStages.gameOver:
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 50,
                                _graphics.PreferredBackBufferHeight/2 - 200),
                                Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void NextLevel()
        {

        }
    }
}
