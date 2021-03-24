using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    class Projectile : GameObject
    {
        protected bool isPlayerBullet;
        protected Vector2 velocity;

        public Projectile(Texture2D pic, Colliders.Collider collide, bool pBullet, Vector2 vel) : base(pic, collide)
        {
            isPlayerBullet = pBullet;
            velocity = vel;
        }

        public void Tick()
        {
            collider.Position += velocity;
        }
    }
}
