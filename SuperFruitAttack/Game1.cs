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
     * Date: 4/28/2021*/

    //We have several enums to handle game transitions and player states.
    public enum GameStages { menu, instructions, gamePlay, gameOver, winGame, transition, pause};
    public enum PlayerState { faceLeft, faceRight, walkLeft, walkRight, jumpLeft, jumpRight};
    public class Game1 : Game
    {
        public const int RESOLUTION = 32;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //These are the mouse and key state fields.
        private MouseState previousMouse;
        private KeyboardState previousKey;
        private GameStages status;
        //Here are all the textures for our game.
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
        //These are the main buttons for our game.
        private Button start;
        private Button menu;
        private Button instructions;
        private Button pause;
        //Here is the spritefont that'll be used for text.
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
            //Here we load in all our content from the content folder.
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

         
            //With the loaded content, we instantiate all our button objects.
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
                    //In the menu, the player can either start the game or check the instructions page.
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
                    //When the player is in the instructions page, they can either return back to the menu,
                    //or start the game from there.
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
                    //Here, we run all the code associated with live gameplay.
                    case GameStages.gamePlay:
                        
                        pause.X = _graphics.PreferredBackBufferWidth - pause.Width - 10;
                        pause.Y = 10;
                        //We update all the game objects in the game object manager.
                        GameObjectManager.Tick(gameTime);
                        //If the player hasn't died, we check for transtion conditions.
                        if(GameObjectManager.Player != null)
                        {
                            //If the player reaches the end of the level before the end of the game, we
                            //head to the transition cutscene phase.
                            if (LevelManager.CurrentLevelNumber < LevelManager.LevelCount && 
                                GameObjectManager.Flag.CheckCollision(GameObjectManager.Player))
                            {
                                status = GameStages.transition;
                            }
                            //if the player beats the last level, the win screen appears.
                            if(LevelManager.CurrentLevelNumber == LevelManager.LevelCount &&
                               GameObjectManager.Flag.CheckCollision(GameObjectManager.Player))
                            {
                                menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                                menu.Y = _graphics.PreferredBackBufferHeight / 2;
                                status = GameStages.winGame;
                            }
                            //But if the player dies, the lose screen appears.
                            if(GameObjectManager.Player.Health <= 0)
                            {
                                menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                                menu.Y = _graphics.PreferredBackBufferHeight / 2;
                                status = GameStages.gameOver;
                            }
                            //If the player exceeds the sky box, the player also dies.
                            if(GameObjectManager.Player.ColliderObject.Bounds.Y >= 
                               LevelManager.CurrentLevel.PixelHeight)
                            {
                                // Drops player health to 0 to prevent possible bugs
                                    GameObjectManager.Player.Health = 0;
                                    status = GameStages.gameOver;
                            }
                        }
                        //If the pause button is clicked, everything game object is "paused" in the 
                        //game object manager and the pause screen appears.
                        if(pause.IsClicked(previousMouse) == true)
                        {
                            menu.X = _graphics.PreferredBackBufferWidth / 2 - menuBtton.Width / 2;
                            menu.Y = _graphics.PreferredBackBufferHeight / 2;
                            status = GameStages.pause;
                            pause.Image = resumeButton;
                        }
                        break;
                    //This stage serves as a "loading screen" for our game.
                    case GameStages.transition:
                        transitionTime -= gameTime.ElapsedGameTime.TotalSeconds;
                        //After 2 seconds, we transition to the next level.
                        if(transitionTime <= 0)
                        {
                            LevelManager.NextLevel();
                            status = GameStages.gamePlay;
                            transitionTime = 2;
                        }
                        break;
                        //When the win screen appears, the player can return to the menu if they 
                        //click the menu button.
                    case GameStages.winGame:
                        if(menu.IsClicked(previousMouse) == true )
                        {
                            start.X = _graphics.PreferredBackBufferWidth / 2 - startButton.Width / 2;
                            start.Y = _graphics.PreferredBackBufferHeight / 2;
                            status = GameStages.menu;
                        }
                        break;
                    //In the game over screen, the player can return to the menu if they click 
                    //the menu button.
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
            //Here, we handle pause transitions changes when the player pauses the game.
            else
            {
                //When the game is paused, the pause button's text becomes "resume".
                //If the player clicks the resume button, they'll go back to gameplay.
                if(pause.IsClicked(previousMouse) == true)
                {
                    pause.Image = pauseButton;
                    status = GameStages.gamePlay;
                }
                //But if they click the menu button, they'll go back to the menu.
                if(menu.IsClicked(previousMouse) == true)
                {
                    pause.Image = pauseButton;
                    status = GameStages.menu;
                }

            }
            //We reinstantiate the key and mouse state fields.
            previousKey = Keyboard.GetState();
            previousMouse = Mouse.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            //Here we draw the main background of the game.
            _spriteBatch.Draw(
                Resources.GetTexture("background"), 
                new Rectangle(0, 0, 
                    _graphics.PreferredBackBufferWidth, 
                    _graphics.PreferredBackBufferHeight),
                Color.White);
            //This handles the scrolling matrix for our game, done by Jack Doyle.
            if (status == GameStages.gamePlay && GameObjectManager.Player != null)
            {
                _spriteBatch.End();
                 _spriteBatch.Begin(transformMatrix: GameObjectManager.CameraMatrix(
                 _graphics.PreferredBackBufferWidth,
                 _graphics.PreferredBackBufferHeight,
                 LevelManager.CurrentLevel.PixelWidth, LevelManager.CurrentLevel.PixelHeight));
            }
            //Here we draw different things in our game depending on the game stage.
            switch(status)
            {
                //We draw the game title, menu, start, and instruction buttons.
                case GameStages.menu:
                    _spriteBatch.Draw(title, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - title.Width / 4,
                                                           _graphics.PreferredBackBufferHeight / 2 - title.Height / 2 - 40,
                                                           title.Width / 2,
                                                           title.Height / 2),
                                                           Color.White);
                    start.Draw(_spriteBatch);
                    instructions.Draw(_spriteBatch);
                    break;
                //In the instructions page, we draw all the instructions text and visuals.
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
                                                       Color.Black);
                    //We draw the menu and start buttons.
                    menu.Draw(_spriteBatch);
                    start.Draw(_spriteBatch);
                    break;
                    //In gameplay, we draw all the game objects in teh game object manager.
                case GameStages.gamePlay:
                    GameObjectManager.Draw(_spriteBatch);
                    _spriteBatch.End();
                    _spriteBatch.Begin();
                    if(GameObjectManager.Player != null)
                    {
                        //Here, we draw the player's health bar, which changes appearance based on the player's health.
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
                    //We draw the pause button.
                    pause.Draw(_spriteBatch);
                    break;
                    //In the transition phase, we draw a little "loading" statement to simulate a loading screen.
                case GameStages.transition:
                    _spriteBatch.DrawString(arial16bold, "Loading", 
                                            new Vector2(_graphics.PreferredBackBufferWidth / 2 - 50,
                                            _graphics.PreferredBackBufferHeight / 2 - 50),
                                            Color.White);
                    break;
                //In the win screen, we draw the "You Win" statement and the menu button.
                case GameStages.winGame:
                    _spriteBatch.DrawString(gameTitle, "You Win",
                                new Vector2(_graphics.PreferredBackBufferWidth / 2 - 100,
                                _graphics.PreferredBackBufferHeight / 2 - 90),
                                Color.Gold);
                    menu.Draw(_spriteBatch);
                    break;
                //In the win screen, we draw the "You died" statement and the menu button.
                case GameStages.gameOver:
                    menu.Draw(_spriteBatch);
                    _spriteBatch.DrawString(gameTitle, "You Died",
                                new Vector2(_graphics.PreferredBackBufferWidth/2 - 100,
                                _graphics.PreferredBackBufferHeight/2 - 90),
                                Color.DarkRed);
                    break;
                //In the pause screen, we draw the "PAUSED" statement and draw the menu and resume button.
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
