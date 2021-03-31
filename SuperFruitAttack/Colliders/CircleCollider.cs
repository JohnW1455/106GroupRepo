using Microsoft.Xna.Framework;

namespace SuperFruitAttack.Colliders
{
    //Author: Nathan Caron
    //Date: 2/26/21
    //Purpose: Represents a circle collider
    public class CircleCollider : Collider
    {
        public int Radius { get; }

        public Point Center => Position.ToPoint() + new Point(Radius);

        /// <summary>
        /// Creates a new CircleCollider
        /// </summary>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="radius">Radius of the circle</param>
        public CircleCollider(int x, int y, int radius) :
            base(x, y, radius * 2, radius * 2)
        {
            Radius = radius;
        }

        protected override bool CheckCollision(BoxCollider other)
        {
            return BoxCircleCollision(other, this);
        }

        protected override bool CheckCollision(CircleCollider other)
        {
            return CircleCircleCollision(this, other);
        }
    }
}