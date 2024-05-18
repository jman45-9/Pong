using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Paddle _p1Paddle;
        private Paddle _p2Paddle;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int wallDis = 30;
            _p1Paddle = new Paddle(null, new Vector2(wallDis, _graphics.PreferredBackBufferWidth / 2));
            _p2Paddle = new Paddle(null, new Vector2(_graphics.PreferredBackBufferWidth - wallDis, _graphics.PreferredBackBufferWidth / 2));
            
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var paddleTex = Content.Load<Texture2D>("paddleSprite");
            _p1Paddle.Texture = paddleTex;
            _p2Paddle.Texture = paddleTex;


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W)) {
                _p1Paddle.moveUp();
            } else if (kstate.IsKeyDown (Keys.S)) {
                _p1Paddle.moveDown(_graphics);
            }

            if (kstate.IsKeyDown(Keys.O)) {
                _p2Paddle.moveUp();
            } else if (kstate.IsKeyDown(Keys.K)) {
                _p2Paddle.moveDown(_graphics);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _p1Paddle.draw(_spriteBatch);
            _p2Paddle.draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
