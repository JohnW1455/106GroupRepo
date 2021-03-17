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
        private Collider collider;


        public GameObject(Texture2D image, int x, int y, int width, int height, Collider collider)
        {
            this.collider = collider;
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

        public virtual void IsDead()
        {

        }

        public virtual void CheckCollision(Collider objectCollide)
        {
            if (collider is BoxCollider)
            {
                BoxCollider boxCollision = (BoxCollider)collider;
                boxCollision.CheckCollision(objectCollide);
                boxCollision.CheckCollision((BoxCollider)objectCollide);
                boxCollision.CheckCollision((CircleCollider)objectCollide);
            }
            else if (collider is CircleCollider)
            {
                CircleCollider circleCollision = (CircleCollider)collider;
                circleCollision.CheckCollision(objectCollide);
                circleCollision.CheckCollision((BoxCollider)objectCollide);
                circleCollision.CheckCollision((CircleCollider)objectCollide);
            }
        }

    }
}
