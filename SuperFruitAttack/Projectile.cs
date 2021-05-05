using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    /* Author: Nathan Caron
     * Purpose: Create a class that simulates a bullet projectile.
     * Date: 4/10/2021*/
    class Projectile : GameObject
    {
        private const int radius = 6;
        protected bool isPlayerBullet;
        protected Vector2 velocity;
        protected Collider collider;

        private static readonly Texture2D _Texture = Resources.GetTexture("simple ball");

        public static Projectile Create(int x, int y, Vector2 direction, bool playerBullet)
        {
            return new Projectile(_Texture, 
                new CircleCollider(x - radius, y - radius, radius), playerBullet, 
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

        /// <param name="gameTime"></param>
        public void Tick(GameTime gameTime)
        {
            collider.Position += velocity;
            if (collider.Position.X < GameObjectManager.Player.X - 690 || collider.Position.X > GameObjectManager.Player.X + 690)
            {
                GameObjectManager.RemoveObject(this);
            }
        }
    }
}
