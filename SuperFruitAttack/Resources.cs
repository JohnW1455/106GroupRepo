using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFruitAttack
{
    public static class Resources
    {
        public const string ROOT_DIRECTORY = "Content";
        
        private static readonly Dictionary<string, Texture2D> _Textures = new Dictionary<string, Texture2D>();
        private static ContentManager _contentManager;
        
        public static void Init(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public static Texture2D GetTexture(string name)
        {
            if (!_Textures.TryGetValue(name, out Texture2D texture))
            {
                texture = _contentManager.Load<Texture2D>(name);
                _Textures[name] = texture;
            }
            return texture;
        }

        public static SpriteFont GetSpriteFont(string name)
        {
            return _contentManager.Load<SpriteFont>(name);
        }
    }
}