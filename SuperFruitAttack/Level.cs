using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFruitAttack
{
    public struct LevelData
    {
        public int Width;
        public int Height;
        public string[] Tiles;
        public string[] Objects;
        public int[] Indices;
    }
    
    public class Level
    {
        private static Texture2D[] _textures;

        public static void LoadTextures(ContentManager contentManager)
        {
            string[] paths = Directory.EnumerateFiles(
                "Content/", 
                "*.png", 
                SearchOption.TopDirectoryOnly).ToArray();

            _textures = new Texture2D[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                _textures[i] = contentManager.Load<Texture2D>(paths[i]);
            }
        }
        
        public static Level Parse(LevelData data)
        {
            Level level = new Level(data.Width, data.Height);

            return level;
        }

        public int Width { get; }
        public int Height { get; }
        public int PixelWidth { get; }
        public int PixelHeight { get; }

        private Level(int width, int height)
        {
            Width = width;
            Height = height;
            PixelWidth = width * Game1.RESOLUTION;
            PixelHeight = height * Game1.RESOLUTION;
        }
    }
}