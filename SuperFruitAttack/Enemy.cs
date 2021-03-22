using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SuperFruitAttack
{
    abstract class Enemy : GameObject
    {
        private int health;
        private int moveSpeed;
        private Player player;

        public Enemy(Texture2D pic, int pHealth, int speed, Player p, Colliders.Collider collide) : base(pic, collide)
        {
            health = pHealth;
            moveSpeed = speed;
            player = p;
        }

        // called when the enemy takes damage
        public void TakeDamage(int dmg)
        {
            health -= dmg;
        }

        public abstract void Tick();

    }
}
