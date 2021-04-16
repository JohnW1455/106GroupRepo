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

        private static readonly Texture2D _Texture = Resources.GetTexture("simple ball");

        public static Projectile Create(int x, int y, Vector2 direction, bool playerBullet)
        {
            return new Projectile(_Texture, 
                new CircleCollider(x, y, 5), playerBullet, 
                direction * 8);
        }

        private Projectile(Texture2D pic, Collider collider, bool pBullet, Vector2 vel)
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
            if (collider.Position.X < GameObjectManager.Player.X - 690 || collider.Position.X > GameObjectManager.Player.X + 690)
            {
                GameObjectManager.RemoveObject(this);
            }
        }
    }
}
