using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    enum EdgeCode : ushort
    {
        NoWall = 0,
        FloorCeiling = 1,
        SideWall = 2
    }

    internal class Ball
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _direction;
        private float _speed;

        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture ??= value;
            }
        }
        public Vector2 Position { get { return _position; } }
        public Vector2 Direction { get { return _direction; } }
        public float Speed { get { return _speed; } }

        public Ball(Vector2 startPosition)
        {
            _speed = 2f;
            this._direction = new Vector2(-1, -1);
            this._position = startPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this._texture,
                this._position,
                null,
                Color.White,
                0f, // rotation
                new Vector2(_texture.Width / 2, _texture.Height / 2), // Draw center
                Vector2.One, // scale factors (x,y)
                SpriteEffects.None,
                0f);
        }

        public void Move()
        {
            _position += _direction;
        }

        public EdgeCode CheckEdge(GraphicsDeviceManager _graphics)
        {
            if (Position.Y <= (this._texture.Height / 2) || (this._position.Y >= (_graphics.PreferredBackBufferHeight - this._texture.Height)))
                return EdgeCode.FloorCeiling;
            if (Position.X <= (this._texture.Width / 2) || (this._position.X >= (_graphics.PreferredBackBufferWidth - this._texture.Width)))
                return EdgeCode.SideWall;

            return EdgeCode.NoWall;
        }

        public void Bounce(EdgeCode edgeCode)
        {
            if (edgeCode == EdgeCode.NoWall) return;
            if (edgeCode == EdgeCode.FloorCeiling)
            {
                _direction.Y *= -1;
                return;
            }
            if (edgeCode == EdgeCode.SideWall)
                _direction.X *= -1;
        }
    }
}
