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
        protected Collider collider;

        public Projectile(Texture2D pic, Collider collider, bool pBullet, Vector2 vel)
            : base(pic, collider)
        {
            this.collider = collider;
            isPlayerBullet = pBullet;
            velocity = vel;
        }
        
        public bool IsPlayerBullet
        {
            get { return isPlayerBullet; }
            set { isPlayerBullet = value; }
        }

        public void Tick()
        {
            collider.Position += velocity;
        }
    }
}
