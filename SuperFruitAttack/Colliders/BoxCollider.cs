using Microsoft.Xna.Framework;

namespace SuperFruitAttack.Colliders
{
    //Author: Nathan Caron
    //Date: 2/26/21
    //Purpose: Represent a box collider
    public class BoxCollider : Collider
    {
        /// <summary>
        /// Creates a new BoxCollider
        /// </summary>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public BoxCollider(int x, int y, int width, int height) :
            base(x, y, width, height)
        {
        }

        protected override bool CheckCollision(BoxCollider other)
        {
            return BoxBoxCollision(this, other);
        }

        protected override bool CheckCollision(CircleCollider other)
        {
            return BoxCircleCollision(this, other);
        }
    }
}