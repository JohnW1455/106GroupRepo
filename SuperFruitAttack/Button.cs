using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    public class Button : GameObject
    {

        public Button(Texture2D image, Collider collide)
            :base(image, collide)
        {
        }

        public override void Draw(SpriteBatch sb)
        {
            MouseState mouse = Mouse.GetState();
            if()
            base.Draw(sb);
            
        }
    }
}
