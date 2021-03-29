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
        private int dmg;

        public int MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        public Enemy(Texture2D pic, int pHealth, int speed, int dmg, Collider collide) : base(pic, collide)
        {
            this.dmg = dmg;
            health = pHealth;
            moveSpeed = speed;
        }
        public int Dmg
        {
            get { return dmg; }
            set { dmg = value; }
        }
        // called when the enemy takes damage
        public void TakeDamage(int dmg)
        {
            health -= dmg;
        }

        public abstract void Tick();

        public override void Draw(SpriteBatch sb)
        {
            if (health <= 0)
            {
                base.Draw(sb);
            }
        }

    }
}
