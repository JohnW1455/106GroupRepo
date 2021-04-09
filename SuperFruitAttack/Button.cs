using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    /// <summary>
    /// Author: Elliot Gong
    /// This is a button class that will handle user mouse actions and interfaces.
    /// </summary>
    public class Button 
    {
        //These are the 2 fields for button, the texture and the rectangle.
        private Texture2D image;
        private Rectangle box;
        /// <summary>
        /// This is the constructor for the button class.
        /// </summary>
        /// <param name="image">This is the button's image.</param>
        /// <param name="x">This is the x coordinate of the top-left corner of the button.</param>
        /// <param name="y">This is the y coordinate of the top-left corner of the button.</param>
        /// <param name="width">This is the button's width.</param>
        /// <param name="height">This is the button's height.</param>
        public Button(Texture2D image, int x, int y, int width, int height)
        {
            //I initialize the texture and rectangle fields with the given parameters.
            this.image = image;
            box = new Rectangle(x, y, width, height);
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
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        /// <summary>
        /// This is the overriden draw method that draws the button.
        /// </summary>
        /// <param name="sb">This is the spritebatch object that'll draw the button.</param>
        public void Draw(SpriteBatch sb)
        {
            //Here, I draw the button differently depending if the mouse is hovered over it.
            MouseState mouse = Mouse.GetState();
            //If the mouse isn't over the button, it is drawn in ghost white by default.
            sb.Draw(image, box, Color.GhostWhite);
            //But if the mouse hovers over the button, it's drawn in full white.
            if(mouse.X > box.X && mouse.X < (box.X + box.Width) 
               && mouse.Y > box.Y && mouse.Y <(box.Y + box.Height))
            {
                sb.Draw(image, box, Color.White);
            }
        }
        /// <summary>
        /// This method will check if the button was clicked, which'll help trigger
        /// specific actions and transitions.
        /// </summary>
        /// <param name="mouse">This is the moustate used to check for previous 'clicks'.</param>
        /// <returns>A bool is returned depending if the button was clicked.</returns>
        public bool IsClicked(MouseState mouse)
        {
            //I get the current mouse state and previous mouse state, and check if the left mouse button
            //was clicked once while the mouse was over the button.
            MouseState cursor = Mouse.GetState();
            if(cursor.LeftButton == ButtonState.Pressed && mouse.LeftButton == ButtonState.Released 
                && cursor.X > box.X && cursor.X < (box.X + box.Width)
               && cursor.Y > box.Y && cursor.Y < (box.Y + box.Height))
            {
                //If the button was clicked, the method returns true.
                return true;
            }
            //Otherwise, it returns false.
            else
            {
                return false;
            }
        }
    }
}
