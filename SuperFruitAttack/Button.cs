using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    public class Button 
    {
        private Texture2D image;
        private Rectangle box;
        public Button(Texture2D image, int x, int y, int width, int height)
        {
            this.image = image;
            box = new Rectangle(x, y, width, height);
        }
        
        public void Draw(SpriteBatch sb)
        {
            MouseState mouse = Mouse.GetState();
            sb.Draw(image, box, Color.GhostWhite);
            if(mouse.X > box.X && mouse.X < (box.X + box.Width) 
               && mouse.Y > box.Y && mouse.Y <(box.Y + box.Height))
            {
                sb.Draw(image, box, Color.White);
            }
        }
    }
}
