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
        private Player player;

        public Enemy(int health, int speed, Texture2D picture, Collider collider )
           : base(picture, collider)
        {
            this.health = health;
            this.moveSpeed = speed;
        }

    }
}
