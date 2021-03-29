using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    public class Collectible : GameObject
    {
        //fields

        protected string textMessage;

        protected bool isActive;
        


        //Properties

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        

        //Constructor

        public Collectible(Texture2D image, Collider collide) : base(image, collide)
        {
            //by default the collectible will be active
            IsActive = true;

        }


        //Methods


        public string TextMessage()
        {
            return "";
        }

        
        public void OnCollect(Player player)
        {

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