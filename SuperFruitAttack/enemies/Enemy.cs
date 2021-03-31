using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    abstract class Enemy : GameObject
    {
        private int health;
        private int moveSpeed;

        public int MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        public Enemy(Texture2D pic, int pHealth, int speed, Collider collide) : base(pic, collide)
        {
            health = pHealth;
            moveSpeed = speed;
        }

        // called when the enemy takes damage
        public void TakeDamage(int dmg)
        {
            health -= dmg;
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        
        public abstract void Tick(GameTime time);

        public override void Draw(SpriteBatch sb)
        {
            if (health > 0)
            {
                base.Draw(sb);
            }
        }

    }
}
