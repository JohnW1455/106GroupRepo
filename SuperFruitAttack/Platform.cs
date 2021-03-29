﻿using System;
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
        public override bool CheckCollision(GameObject gameObject)
        {
            // First checks if the player is intersecting an obstacle
            if (base.CheckCollision(gameObject))
            {
                Rectangle entityRect = gameObject.ColliderObject.Bounds;
                Rectangle platRect = gameObject.ColliderObject.Bounds;

                Rectangle resultant = Rectangle.Intersect(entityRect, platRect);

				if (resultant.Width < resultant.Height)
				{
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
					// if the player is intersecting from above 
					if ((platRect.Y - entityRect.Y) > 0)
					{
						// move player up
						entityRect.Y -= resultant.Height;
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



		public override void Draw(SpriteBatch sb)
		
		{
			//add if statements to give platforms a reason to have sepperate draw methods

			//I was thinking maybe some rectangles we can jump through the bottom off but that might be too hard
			base.Draw(sb);
		}
		




	}
}