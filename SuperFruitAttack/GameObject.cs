using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    /* Author: Elliot Gong & Nathan Caron
     * Purpose: Create a class that simulates a universal game object.
     * Restriction: Must use the collider class to handle collisions between objects.
     * Date: 3/17/2021*/
    public abstract class GameObject
    {
        //These are the 2 fields for the gameObject class: the image and the 
        //collider object.
        protected Texture2D image;
        protected Collider colliderObject;

        public GameObject(Texture2D image, Collider colliderObject)
        {
            this.colliderObject = colliderObject;
            this.image = image;
        }

        public static GameObject Create(int x, int y, string name, Texture2D texture)
        {
            int width = texture.Width;
            int height = texture.Height;
            
            switch (name.ToLower())
            {
                case "potato":
                    return new Potato(texture, 1, 10, new BoxCollider(x, y, width, height));
                case "carrot":
                    return new Carrot(texture, 2, 0, new BoxCollider(x, y, width, height));
                case "peapod":
                    return new PeaPod(texture, 1000, 0, new BoxCollider(x, y, width, height));
                case "player":
                    return new Player(4, 10, texture, new BoxCollider(x, y, width, height));
                case "collectible":
                    return new Collectible(texture, new BoxCollider(x, y, width, height));
                case "platform":
                    return new Platform(texture, new BoxCollider(x, y, width, height));
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
            get { return (int)colliderObject.Position.X; }
            set { colliderObject.Position = new Vector2(value, Y); }
        }

        public int Y
        {
            get { return (int)colliderObject.Position.Y; }
            set { colliderObject.Position = new Vector2(X, value); }
        }

        public int Width
        {
            get { return colliderObject.Size.X; }
        }

        public int Height
        {
            get { return colliderObject.Size.Y; }
        }
        /// <summary>
        /// This method draws the game object in its specified location.
        /// </summary>
        /// <param name="sb">This is the spritebatch object that'll draw the game object.</param>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, 
                new Rectangle(colliderObject.Position.ToPoint(),
                    colliderObject.Size), 
                Color.White);
        }

        public Collider ColliderObject
        {
            get { return colliderObject; }
            set { colliderObject = value; }
        }
        /// <summary>
        /// This method checks if a game object is colliding with another game object.
        /// </summary>
        /// <param name="otherCollider"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(GameObject otherObject)
        {
            return colliderObject.CheckCollision(otherObject.ColliderObject);
        }

    }
}
