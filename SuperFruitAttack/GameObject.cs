using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    /* Author: Elliot Gong
     * Purpose: Create a class that simulates a universal game object.
     * Restriction: Must use the inherent collider class.
     * Date: 3/17/2021*/
    public abstract class GameObject
    {
        //These are the 2 fields for the gameObject class: the image and the 
        //collider object.
        protected Texture2D image;
        protected Collider collideObject;

        public GameObject(Texture2D image, Collider collide)
        {
            collideObject = collide;
            this.image = image;
        }

        public static GameObject Create(string imageName, Texture2D texture)
        {
            switch (name.ToLower())
            {
                case "potato":
                    //temp
                    return new Potato(texture, 1, 10, new BoxCollider(0, 0, 0, 0));
            }

            return null;
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
        /// <summary>
        /// This method draws the game object in its specified location.
        /// </summary>
        /// <param name="sb">This is the spritebatch object that'll draw the game object.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, 
                new Rectangle(collideObject.Position.ToPoint(),
                    collideObject.Size), 
                Color.White);
        }
        /// <summary>
        /// This method checks if a game object is colliding with another game object.
        /// </summary>
        /// <param name="objectCollide"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(Collider objectCollide)
        {
            return collideObject.CheckCollision(objectCollide);
        }

    }
}
