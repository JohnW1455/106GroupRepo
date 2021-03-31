using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace SuperFruitAttack.Colliders
{
    //Author: Nathan Caron
    //Date: 2/26/21
    //Purpose: Represent a basic collider for collision
    public abstract class Collider
    {
        public Vector2 Position { get; set; }
        public Point Size { get; }
        public Rectangle Bounds => new Rectangle(Position.ToPoint(), Size);

        /// <summary>
        /// Creates a new collider
        /// </summary>
        /// <param name="x">X location</param>
        /// <param name="y">Y location</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public Collider(int x, int y, int width, int height)
        {
            Position = new Vector2(x, y);
            Size = new Point(width, height);
        }

        /// <summary>
        /// Checks to see if this and another collider are colliding
        /// </summary>
        /// <param name="other">Collider to check against</param>
        /// <returns>If the colliders are colliding</returns>
        /// <exception cref="NotSupportedException">If collision between colliders is not support</exception>
        protected abstract bool CheckCollision(BoxCollider other);

        protected abstract bool CheckCollision(CircleCollider other);

        public bool CheckCollision(Collider other)
        {
            if (other is BoxCollider box)
            {
                return CheckCollision(box);
            }

            if (other is CircleCollider circleCollider)
            {
                return CheckCollision(circleCollider);
            }

            throw new NotSupportedException($"{other.GetType()} not supported");
        }

        /// <summary>
        /// Checks to see if two Boxes are colliding
        /// </summary>
        /// <param name="box1">Box 1</param>
        /// <param name="box2">Box 2</param>
        /// <returns>If the two colliders are colliding</returns>
        protected bool BoxBoxCollision(BoxCollider box1, BoxCollider box2)
        {
            return box1.Bounds.Intersects(box2.Bounds);
        }
        
        /// <summary>
        /// Checks to see if a box and circle are colliding
        /// </summary>
        /// <param name="box">Box collider</param>
        /// <param name="circle">Circle collider</param>
        /// <returns>If the two colliders are colliding</returns>
        protected bool BoxCircleCollision(BoxCollider box, CircleCollider circle)
        {
            // https://stackoverflow.com/questions/401847/circle-rectangle-collision-detection-intersection
            // Find the closest point to the circle within the rectangle
            float closestX = MathHelper.Clamp(circle.Center.X, box.Bounds.Left, box.Bounds.Right);
            float closestY = MathHelper.Clamp(circle.Center.Y, box.Bounds.Top, box.Bounds.Bottom);

            // Calculate the distance between the circle's center and this closest point
            Vector2 distance = new Vector2(
                circle.Center.X - closestX, 
                circle.Center.Y - closestY);

            // If the distance is less than the circle's radius, an intersection occurs
            float distanceSquared = distance.LengthSquared();
            return distanceSquared < (circle.Radius * circle.Radius);
        }

        /// <summary>
        /// Checks to see if two Circles are colliding
        /// </summary>
        /// <param name="circle1">Circle 1</param>
        /// <param name="circle2">Circle 2</param>
        /// <returns>If the two colliders are colliding</returns>
        protected bool CircleCircleCollision(CircleCollider circle1, CircleCollider circle2)
        {
            float xDist = circle1.Center.X - circle2.Center.X;
            float yDist = circle1.Center.Y - circle2.Center.Y;
            float distanceSquared = (xDist * xDist) + (yDist * yDist); // no sqrt is faster
            float radiusDistance = circle1.Radius + circle2.Radius;
            return distanceSquared < radiusDistance * radiusDistance;
        }
    }
}