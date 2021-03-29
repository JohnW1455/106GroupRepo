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
        protected int damage;
        protected Collider collider;

        public Projectile(Texture2D pic, Collider collider, bool pBullet, Vector2 vel, int damage)
            : base(pic, collider)
        {
            this.collider = collider;
            this.damage = damage;
            isPlayerBullet = pBullet;
            velocity = vel;
        }
        
        public bool IsPlayerBullet
        {
            get { return isPlayerBullet; }
            set { isPlayerBullet = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public void Tick()
        {
            collider.Position += velocity;
        }
    }
}
