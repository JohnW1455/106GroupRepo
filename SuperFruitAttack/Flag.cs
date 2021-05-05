using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    //This class simulates a flag that'll trigger a level's win condition.
    public class Flag : GameObject
    {
        public Flag(Texture2D image, Collider colliderObject) : base(image, colliderObject)
        {
        }
    }
}