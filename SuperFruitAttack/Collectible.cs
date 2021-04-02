using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;
using System;


namespace SuperFruitAttack
{
    public class Collectible : GameObject
    {
        //fields

        protected string textMessage;

        protected bool isActive;

        protected string colEffect;

        

        //Properties

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public string ColEffect
        {
            get { return colEffect; }
            set { colEffect = value; }
        }
        

        //Constructor

        public Collectible(Texture2D image, Collider collide) : base(image, collide)
        {
            //by default the collectible will be active
            IsActive = true;

            Random rand = new Random();
            int whichPower = rand.Next(2); //only either 0, or 1 for now for proof of concept

            switch(whichPower)
            {
                case 0:
                    ColEffect = "speedBoost";
                    break;

                case 1:
                    ColEffect = "slowDown";
                    
                    break;

                case 2://case to be used later
                    ColEffect = "rappidFire";
                    break;

                case 3://case to be used later
                    ColEffect = "lowGravity";
                    break;
            }


        }


        //Methods


        public string TextMessage()
        {
            return "The mystery effect is " + ColEffect;
        }

        
        public void OnCollect(Player player)
        {
            int oldSpeed;
            switch (colEffect)
            {
                case "speedBoost":

                    oldSpeed = (int)player.MoveSpeed.X;
                    player.MoveSpeed = new Vector2(oldSpeed+5, 0);
                    break;

                case "slowDown":
                    oldSpeed = (int)player.MoveSpeed.X;
                    if (oldSpeed > 5)
                    {
                        player.MoveSpeed = new Vector2(oldSpeed - 5, 0);
                    }
                    else if (oldSpeed > 1)
                    {
                        player.MoveSpeed = new Vector2(2, 0);
                    }

                    break;

                case "lowGravity"://to be used later

                    break;

                case "rappidFire"://to be used later

                    break;

            }
        }

        public override void Draw(SpriteBatch sb)
        //Method used to draw Collectibles
        {
            //only draw the collectible if it is still active
            if (isActive)
            {
                base.Draw(sb);
            }
        }




    }
}