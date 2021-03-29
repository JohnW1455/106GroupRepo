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

        public void Tick(GameTime time)
        {
            int posCompare = this.X - GameObjectManager.Player.X;

            triggerTime += time.ElapsedGameTime.TotalSeconds;

            if (triggerTime >= 2)
            {
                GameObjectManager.AddObject(new Projectile(Game1.GetTexture("simple ball"),
                        new CircleCollider(this.X, this.Y, 20), false, new Vector2(5 * Math.Sign(posCompare), 0)));
                triggerTime = 0;
            }   
        }
    }
}
