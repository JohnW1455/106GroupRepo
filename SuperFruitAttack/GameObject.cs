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
        /// <summary>
        /// This is the gameObject constructor that takes in a Texture2D and a collider
        /// object that'll check for collisions.
        /// </summary>
        /// <param name="image">This is the object's image.</param>
        /// <param name="colliderObject">This is the object's collider object that'll
        /// handle collisions with other gameObjects.</param>
        public GameObject(Texture2D image, Collider colliderObject)
        {
            this.colliderObject = colliderObject;
            this.image = image;
        }
        /// <summary>
        /// This method will create a specific game object/enemy based on user input.
        /// </summary>
        /// <param name="x">This is the x of the game object.</param>
        /// <param name="y">This is the y of the game object.</param>
        /// <param name="name">This is the object's designated name.</param>
        /// <param name="texture">This will be the object's image.</param>
        /// <returns></returns>
        public static GameObject Create(string name, int x, int y, Texture2D texture)
        {
            int width = texture.Width;
            int height = texture.Height;
            BoxCollider box = new BoxCollider(x, y, width, height);
            switch (name.ToLower())
            {
                case "potato":
                    return new Potato(texture, 1, 5, box);
                case "carrot":
                    return new Carrot(texture, 4, 0, box);
                case "peapod":
                    return new PeaPod(texture, 1000, 0, box);
                case "player":
                    return new Player(4, 5, texture, box);
                case "collectible":
                    return new Collectible(texture, box);
                case "platform":
                case "ground":
                    return new Platform(texture, box);
                case "levelflag":
                    return new Flag(texture, box);
                default:
                    throw new ArgumentException($"{name.ToLower()} is not a valid GameObject type");
            }

        }
        /// <summary>
        /// This is the property for an object's image.
        /// </summary>
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        /// <summary>
        /// This is the property for an object's top left x coordinate.
        /// </summary>
        public int X
        {
            get { return (int)colliderObject.Position.X; }
            set { colliderObject.Position = new Vector2(value, Y); }
        }
        /// <summary>
        /// This is the property for an object's top left y coordinate.
        /// </summary>
        public int Y
        {
            get { return (int)colliderObject.Position.Y; }
            set { colliderObject.Position = new Vector2(X, value); }
        }
        /// <summary>
        /// This is the getter property for an object's width
        /// </summary>
        public int Width
        {
            get { return colliderObject.Size.X; }
        }
        /// <summary>
        /// This is the getter property for an object's height.
        /// </summary>
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
            sb.Draw(image, colliderObject.Bounds, Color.White);
        }
        /// <summary>
        /// This is the property for an object's collider object.
        /// </summary>
        public Collider ColliderObject
        {
            get { return colliderObject; }
            set { colliderObject = value; }
        }
        /// <summary>
        /// This method checks if a game object is colliding with another game object.
        /// </summary>
        /// <param name="otherCollider">This is the other gameObject that'll be checked to
        /// determine if there are any collisions.</param>
        /// <returns>This method returns a bool detailing if this or the other object 
        /// collided.</returns>
        public virtual bool CheckCollision(GameObject otherObject)
        {
            return colliderObject.CheckCollision(otherObject.ColliderObject);
        }

    }
}
