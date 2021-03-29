using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    class PeaPod : Enemy
    {
        public PeaPod(Texture2D pic, int health, int speed, Collider collide) : base(pic, health, speed, collide)
        {

        }

        public override void Tick()
        {

        }
    }
}
