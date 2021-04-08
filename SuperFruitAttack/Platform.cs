using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    class Platform : GameObject
    {
		//fields



		//Properties




		//Constructoor

        public Platform(Texture2D pic, Collider collide) : base(pic, collide)
        {

        }



		//Methods
        public bool CheckCollision(Player gameObject)
        {
            // First checks if the player is intersecting an obstacle
            if (base.CheckCollision(gameObject))
            {
                Rectangle entityRect = gameObject.ColliderObject.Bounds;
                Rectangle platRect = this.ColliderObject.Bounds;

                Rectangle resultant = Rectangle.Intersect(entityRect, platRect);

				if (resultant.Width < resultant.Height)
				{
					// Sets the player's Wall Climb to true
					gameObject.WallClimb = true;
					// if player is intersecting from right
					if ((platRect.X - entityRect.X) > 0)
					{
						// move player left
						entityRect.X -= resultant.Width;
					}
					// if the player is intersecting from the left
					else
					{
						// move player right
						entityRect.X += resultant.Width;
					}
				}
				else
				{
					gameObject.PlayerVelocity = new Vector2(gameObject.PlayerVelocity.X, 0);
					// if the player is intersecting from above 
					if ((platRect.Y - entityRect.Y) > 0)
					{
						// move player up
						entityRect.Y -= resultant.Height;
						gameObject.IsGrounded = true;
					}
					// if the player is intersecting from below
					else
					{
						// move player down
						entityRect.Y += resultant.Height;
					}
				}
				Vector2 newPos = entityRect.Location.ToVector2();
				gameObject.ColliderObject.Position = newPos;

				return true;
			}
			return false;
		}
    }
}
