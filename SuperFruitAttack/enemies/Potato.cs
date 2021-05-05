using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFruitAttack.Colliders;
/// <summary>
/// 
/// </summary>
namespace SuperFruitAttack
{
    /* Author: Jack Doyle
  * Purpose: Create a potato enemy that wanders back and forth.
  * Date: 4/28/2021*/
    class Potato : Enemy
    {
        protected float homePosition;

        public Potato(Texture2D pic, int health, int speed,  Collider collide) :
            base(pic, health, speed, collide)
        {
            homePosition = collide.Position.X;
        }

        /// <summary>
        /// Makes the potato walk back and forth
        /// </summary>
        public override void Tick(GameTime time)
        {
            this.X -= MoveSpeed;
            if (this.X <= homePosition - 80 || this.X >= homePosition + 80)
            {
                MoveSpeed *= -1;
            }
        }
    }
}
