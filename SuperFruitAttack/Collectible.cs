using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    public class Collectible : GameObject
    {
        public Collectible(Texture2D image, Collider collide) : base(image, collide)
        {
        }
    }
}