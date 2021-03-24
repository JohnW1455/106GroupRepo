using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFruitAttack
{
    // Class: Player
    // Author: Jack Doyle
    // Purpose: To represent the player and store info on the player
    //          in game.
    public class Player : GameObject
    {
        // Fields
        private int health;
        private int moveSpeed;
        // private Projectile bullet; (I don't know how this is relevant)

        // Properties
        public int Health
        {
            get { return health; }
        }

        public int MoveSpeed
        {
            get { return moveSpeed; }
        }

        // Constructor
        public Player(int playerHealth, int playerMS, Texture2D image,
            Collider collide) : base(image, collide)
        {
            health = playerHealth;
            moveSpeed = playerMS;
        }

        public void TakeDamage(int damage)
        {

        }

        public void Tick(GameTime time)
        {

        }
    }
}
