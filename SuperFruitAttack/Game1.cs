using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Level.LoadTextures(Content);

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

            base.Draw(gameTime);
        }
    }
}
