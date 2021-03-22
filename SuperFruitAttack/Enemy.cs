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

        public Enemy(Texture2D picture, int x, int y, int width, int height, Collider collider )
           : base(picture, x, y, width, height, collider)
        {

        }

    }
}
