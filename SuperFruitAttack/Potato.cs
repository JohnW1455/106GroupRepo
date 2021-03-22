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
    /// <summary>
    /// Basic grunt enemy that walks a set distance, back and forth
    /// </summary>
    class Potato : Enemy
    {
        protected float homePosition;

        public Potato(Texture2D pic, int health, int speed, Player p, Colliders.Collider collide) : base(pic, health, speed, p, collide)
        {
            homePosition = collide.Position.X;
        }

        public override void Tick()
        {
            
        }
    }
}
