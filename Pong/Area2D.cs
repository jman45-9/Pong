using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Area2D
{
    public class Area2D
    {
        private Vector2 _topLeft;
        private Vector2 _bottomRight;

        public Vector2 TopLeft { get { return _topLeft; } set { _topLeft = value; } }
        public Vector2 BottomRight { get { return _bottomRight; } set { _bottomRight = value; } }

        public Area2D(Vector2 topLeft, Vector2 bottomRight)
        {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }
        public bool IntersectingArea2D(Area2D other)
        {
            if (other == null) return false;
            if (InRange(this.TopLeft.X, this.BottomRight.X, other.TopLeft.X) && InRange(this.TopLeft.Y, this.BottomRight.Y, other.TopLeft.Y))
                return true;
            if (InRange(this.TopLeft.X, this.BottomRight.X, other.BottomRight.X) && InRange(this.TopLeft.Y, this.BottomRight.Y, other.BottomRight.Y))
                return true;
            var botLeft = new Vector2(other.TopLeft.X, other.BottomRight.Y);
            var topRight = new Vector2(other.BottomRight.X, other.TopLeft.Y);

            if (InRange(this.TopLeft.X, this.BottomRight.X, botLeft.X) && InRange(this.TopLeft.Y, this.BottomRight.Y, botLeft.Y))
                return true;
            if (InRange(this.TopLeft.X, this.BottomRight.X, topRight.X) && InRange(this.TopLeft.Y, this.BottomRight.Y, topRight.Y))
                return true;

            // reverse check
            if (InRange(other.TopLeft.X, other.BottomRight.X, this.TopLeft.X) && InRange(other.TopLeft.Y, other.BottomRight.Y, this.TopLeft.Y))
                return true;
            if (InRange(other.TopLeft.X, other.BottomRight.X, this.BottomRight.X) && InRange(other.TopLeft.Y, other.BottomRight.Y, this.BottomRight.Y))
                return true;
            botLeft = new Vector2(this.TopLeft.X, this.BottomRight.Y);
            topRight = new Vector2(this.BottomRight.X, this.TopLeft.Y);

            if (InRange(other.TopLeft.X, other.BottomRight.X, botLeft.X) && InRange(other.TopLeft.Y, other.BottomRight.Y, botLeft.Y))
                return true;
            if (InRange(other.TopLeft.X, other.BottomRight.X, topRight.X) && InRange(other.TopLeft.Y, other.BottomRight.Y, topRight.Y))
                return true;

            return false;
        }
        private bool InRange(float rangeMin, float rangeMax, float test)
        {
            return (rangeMin < test && test < rangeMax);
        }

    }


}
