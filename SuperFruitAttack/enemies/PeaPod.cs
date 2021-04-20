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

        private double triggerTime;

        public PeaPod(Texture2D pic, int health, int speed, Collider collide) : base(pic, health, speed, collide)
        {

        }

        public override void TakeDamage(int dmg)
        {
            // peapod takes no damage
        }

        public override void Tick(GameTime time)
        {
            triggerTime += time.ElapsedGameTime.TotalSeconds;

            if (triggerTime >= 2)
            {
                GameObjectManager.AddObject(
                    Projectile.Create(X, Y, 
                        new Vector2(0, 1), false));
                triggerTime = 0;
            }
        }
    }
}
