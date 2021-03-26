using System;
using System.Collections.Generic;
using System.Text;

namespace Super_Fruit_Attack
{
    class Collectible : GameObject
    {
        //fields
        private bool isActive;


        //properties

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        //Constructor
        public Collectible(Rectangle rec, Texture2D pic)
            :base(rec,pic)
        {
            isActive = true;
        }


        //Methods



    }
}
