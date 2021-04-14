using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;
using System;

namespace SuperFruitAttack {
    public class PowerUp : Collectible
    
	
	{
		//fields
		private string powerName;


        //properties


        public string PowerName
        {
            get { return powerName; }
            set { powerName = value; }
        }


        //Constructor

        public PowerUp(Texture2D image, Collider collide) : base(image, collide)
        {
            //by default the collectible will be active
            IsActive = true;

            Random rand = new Random();
            int whichPower = rand.Next(4); //only either 0, or 1 for now for proof of concept

            switch (whichPower)
            {
                case 0:
                    PowerName = "speedBoost";
                    break;

                case 1:
                    PowerName = "slowDown";

                    break;

                case 2://case to be used later
                    PowerName = "highJump";
                    break;

                case 3://case to be used later
                    PowerName = "healthIncrease";
                    break;
            }


        }

        //Methods

        public string TextMessage()
        {
            return "The mystery effect is " + PowerName;
        }



        public override void OnCollect(Player player)
        {
            int oldVal;
            switch (powerName)
            {
                case "speedBoost":

                    oldVal = (int)player.MoveSpeed.X;
                    player.MoveSpeed = new Vector2(oldVal + 5, 0);
                    break;

                case "slowDown":
                    oldVal = (int)player.MoveSpeed.X;
                    if (oldVal > 5)
                    {
                        player.MoveSpeed = new Vector2(oldVal - 5, 0);
                    }
                    else if (oldVal > 1)
                    {
                        player.MoveSpeed = new Vector2(2, 0);
                    }

                    break;

                case "highJump"://to be used later
                    oldVal = (int)player.JumpVelocity.Y;
                    player.JumpVelocity = new Vector2(0, oldVal + 5);
                    break;

                case "healthIncrease"://to be used later
                    oldVal = player.Health;
                    player.Health = oldVal + 2;

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