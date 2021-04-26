using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    class Carrot : Enemy
    {

        private double triggerTime;

        public Carrot(Texture2D pic, int health, int speed, Collider collide) : base(pic, health, speed, collide)
        {
            triggerTime = 0;
        }

        public override void Tick(GameTime time)
        {
            int posCompare = GameObjectManager.Player.X - this.X;
            triggerTime += time.ElapsedGameTime.TotalSeconds;

            if (triggerTime >= 2 && posCompare != 0)
            {
                GameObjectManager.AddObject(
                    Projectile.Create(X + Width/2, Y + Height/2, 
                        new Vector2(Math.Sign(posCompare), 0), false));
                triggerTime = 0;
            }   
        }
    }
}
