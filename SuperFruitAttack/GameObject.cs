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
        protected Texture2D image;
        protected Collider collideObject;

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

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, 
                new Rectangle(collideObject.Position.ToPoint(),
                    collideObject.Size), 
                Color.White);
        }

        public virtual void IsDead()
        {

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
