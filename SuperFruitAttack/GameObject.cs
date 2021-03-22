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
        private Rectangle box;
        private Texture2D image;
        private Collider collideObject;


        public GameObject(Texture2D image, int x, int y, int width, int height, Collider collide)
        {
            if(collide is BoxCollider)
            {
                collideObject = new BoxCollider(x, y, width, height);
            }
            else if(collide is CircleCollider)
            {
                collideObject = new CircleCollider(X, Y, width / 2);
            }
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

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, box, Color.White);
        }
        public virtual void CheckCollision(Collider objectCollide)
        {
            if (objectCollide is BoxCollider)
            {
                BoxCollider boxCollision = (BoxCollider)objectCollide;

                boxCollision.CheckCollision(collideObject);
                boxCollision.CheckCollision((BoxCollider)collideObject);
                boxCollision.CheckCollision((CircleCollider)collideObject);
            }
            else if (objectCollide is CircleCollider)
            {
                CircleCollider circleCollision = (CircleCollider)objectCollide;
                circleCollision.CheckCollision(collideObject);
                circleCollision.CheckCollision((BoxCollider)collideObject);
                circleCollision.CheckCollision((CircleCollider)collideObject);
            }
        }

    }
}
