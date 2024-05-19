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
        private Vector2 _startPos;

        private Area2D.Area2D _hitbox;

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
        public Area2D.Area2D Hitbox { get { return _hitbox; } }

        public Ball(Vector2 startPosition)
        {
            _speed = 8f;
            this._direction = new Vector2(-1, -1);
            this._startPos = startPosition;
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
            CalcHitbox();
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
        public void Reset()
        {
            this._position = this._startPos;
        }

        private void CalcHitbox()
        {
            _hitbox = new Area2D.Area2D(
                new Vector2(Position.X - this.Texture.Width / 2, Position.Y - this.Texture.Height / 2),
                new Vector2(Position.X + this.Texture.Width / 2, Position.Y + this.Texture.Height / 2)
            );
        }
    }
}
