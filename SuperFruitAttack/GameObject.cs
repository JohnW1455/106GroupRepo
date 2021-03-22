using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    public abstract class GameObject
    {
        private Texture2D image;
        private Collider collideObject;

        public GameObject(Texture2D image, Collider collide)
        {
            collideObject = collide;
            this.image = image;
        }

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public int X
        {
            get { return (int)collideObject.Position.X; }
            set { collideObject.Position = new Vector2(value, Y); }
        }

        public int Y
        {
            get { return (int)collideObject.Position.Y; }
            set { collideObject.Position = new Vector2(X, value); }
        }

        public int Width
        {
            get { return collideObject.Size.X; }
        }

        public int Height
        {
            get { return collideObject.Size.Y; }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, 
                new Rectangle(collideObject.Position.ToPoint(),
                    collideObject.Size), 
                Color.White);
        }

        public virtual bool CheckCollision(Collider objectCollide)
        {
            return collideObject.CheckCollision(objectCollide);
        }
    }
}
