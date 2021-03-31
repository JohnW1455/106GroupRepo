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

        public static void Init(ContentManager contentManager)
        {
            var paths = Directory.EnumerateFiles(
                    $"{ROOT_DIRECTORY}/",
                    "*.png",
                    SearchOption.TopDirectoryOnly)
                .Select(x => x.Split('/')
                    .Last()
                    .Replace(".png", ""))
                .ToArray();

            foreach (var path in paths)
            {
                _Textures[path] = contentManager.Load<Texture2D>(path);
            }
        }

        public static Texture2D GetTexture(string name)
        {
            return _Textures[name];
        }
    }
}