using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Paddle _p1Paddle;
        private Paddle _p2Paddle;

        private Ball _ball;

        private SpriteFont _gameFont;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible =false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int wallDis = 30;
            _p1Paddle = new Paddle(null, new Vector2(wallDis, _graphics.PreferredBackBufferWidth / 2));
            _p2Paddle = new Paddle(null, new Vector2(_graphics.PreferredBackBufferWidth - wallDis, _graphics.PreferredBackBufferWidth / 2));

            _ball = new Ball(new Vector2(_graphics.PreferredBackBufferWidth / 2,
                    _graphics.PreferredBackBufferHeight / 2));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var paddleTex = Content.Load<Texture2D>("paddleSprite");
            _p1Paddle.Texture = paddleTex;
            _p2Paddle.Texture = paddleTex;

            _ball.Texture = Content.Load<Texture2D>("ballSprite");
            _gameFont = Content.Load<SpriteFont>("uiFont");
            _p1Paddle.PointFont = _gameFont;
            _p2Paddle.PointFont = _gameFont;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W)) {
                _p1Paddle.MoveUp();
            } else if (kstate.IsKeyDown (Keys.S)) {
                _p1Paddle.MoveDown(_graphics);
            }

            if (kstate.IsKeyDown(Keys.Up)) {
                _p2Paddle.MoveUp();
            } else if (kstate.IsKeyDown(Keys.Down)) {
                _p2Paddle.MoveDown(_graphics);
            }

            _ball.Move();
            EdgeCode ballState = _ball.CheckEdge(_graphics);
            _ball.Bounce(ballState);
            if (ballState == EdgeCode.SideWall)
            {
                if (_ball.Position.X < _graphics.PreferredBackBufferWidth / 2)
                    _p2Paddle.IncScore();
                else
                    _p1Paddle.IncScore();

                _ball.Reset();
            }

            if (_ball.Hitbox.IntersectingArea2D(_p1Paddle.Hitbox) || _ball.Hitbox.IntersectingArea2D(_p2Paddle.Hitbox))
                _ball.Bounce(EdgeCode.SideWall);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _p1Paddle.Draw(_spriteBatch);
            _p2Paddle.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);
            _p1Paddle.PrintScore(_graphics, _spriteBatch, 0);
            _p2Paddle.PrintScore(_graphics, _spriteBatch, 1);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
