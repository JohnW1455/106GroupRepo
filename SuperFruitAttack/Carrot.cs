using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    class Carrot : Enemy
    {
        public Carrot(Texture2D pic, int health, int speed, Player p, Colliders.Collider collide) : base(pic, health, speed, p, collide)
        {

        }

        public override void Tick()
        {

        }
    }
}
