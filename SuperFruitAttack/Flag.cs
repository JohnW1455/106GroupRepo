using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    public class Flag : GameObject
    {
        public Flag(Texture2D image, Collider colliderObject) : base(image, colliderObject)
        {
        }
    }
}