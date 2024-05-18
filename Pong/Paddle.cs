using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    internal class Paddle
    {
        private Vector2 _position;
        private float _speed;
        private Texture2D _texture;

        public Vector2 Position { get { return _position; } }
        public float Speed { get { return _speed; } }
        public Texture2D Texture { get { return _texture; }
            set {
                _texture ??= value;
            }
        }
    

        public Paddle(Texture2D texture, Vector2 position) {
            _speed = 2f;
            _texture = texture;
            _position = position;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this._texture,
                this._position,
                null,
                Color.White,
                0f, // rotation
                new Vector2(_texture.Width/2, _texture.Height/2), // draw center
                new Vector2(1, 2), // scale factors (x,y)
                SpriteEffects.None,
                0f);

        }

        public void moveUp()
        {
            if(this._position.Y > (this._texture.Height))
                _position.Y -= _speed;
        }
        public void moveDown(GraphicsDeviceManager _graphics)
        {
            if (this._position.Y < (_graphics.PreferredBackBufferHeight - this._texture.Height))
            _position.Y += _speed;
        }

    }
}
