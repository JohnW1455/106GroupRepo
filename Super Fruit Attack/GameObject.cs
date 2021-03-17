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

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public int X
        {
            get { return box.X; }
            set { box.X = value; }
        }

        public int Y
        {
            get { return box.Y; }
            set { box.Y = value; }
        }

        public int Width
        {
            get { return box.Width; }
            set { box.Width = value; }
        }

        public int Height
        {
            get { return box.Height; }
            set { box.Height = value; }
        }
    }
}
