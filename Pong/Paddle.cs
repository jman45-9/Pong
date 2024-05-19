﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    internal class Paddle
    {
        private Vector2 _position;
        private float _speed;
        private Texture2D _texture;

        private Area2D.Area2D _hitbox;
        private const float yScale = 2;
        private int _score;

        public Vector2 Position { get { return _position; } }
        public float Speed { get { return _speed; } }
        public Texture2D Texture { get { return _texture; }
            set {
                _texture ??= value;
            }
        }
        public Area2D.Area2D Hitbox { get { return _hitbox; } }
        public int Score { get { return _score; } }
    

        public Paddle(Texture2D texture, Vector2 position) {
            _speed = 2f;
            _texture = texture;
            _position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this._texture,
                this._position,
                null,
                Color.White,
                0f, // rotation
                new Vector2(_texture.Width/2, _texture.Height/2), // Draw center
                new Vector2(1, yScale), // scale factors (x,y)
                SpriteEffects.None,
                0f);

        }

        public void MoveUp()
        {
            if(this._position.Y > (this._texture.Height))
                _position.Y -= _speed;
            calcHitbox();
        }
        public void MoveDown(GraphicsDeviceManager _graphics)
        {
            if (this._position.Y < (_graphics.PreferredBackBufferHeight - this._texture.Height))
            _position.Y += _speed;
            calcHitbox();
        }
        private void calcHitbox()
        {
            _hitbox = new Area2D.Area2D(
                new Vector2(Position.X - this.Texture.Width / 2, Position.Y - this.Texture.Height),
                new Vector2(Position.X + this.Texture.Width / 2, Position.Y + this.Texture.Height)
            );
        }

        private void incScore()
        {
            this._score++;
        }

    }
}
