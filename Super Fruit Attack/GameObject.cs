using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Super_Fruit_Attack
{
    class GameObject
    {
        private Rectangle box;
        private Texture2D image;

        public GameObject(Texture2D image, int x, int y, int width, int height)
        {
            this.image = image;
            box = new Rectangle(x, y, width, height);
        }
    }
}
