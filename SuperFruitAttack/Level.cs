using System.Collections.Generic;
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
        public string[] Objects;
        public int[] Indices;
    }

    public class Level
    {
        private static Dictionary<string, Texture2D> _textures;

        public static void LoadTextures(ContentManager contentManager)
        {
            string[] paths = Directory.EnumerateFiles(
                "Content/", 
                "*.png", 
                SearchOption.TopDirectoryOnly).ToArray();

            _textures = new Dictionary<string, Texture2D>();
            for (int i = 0; i < paths.Length; i++)
            {
                _textures[paths[i]] = contentManager.Load<Texture2D>(paths[i]);
            }
        }
        
        public static Level Parse(LevelData data)
        {
            Level level = new Level(data.Width, data.Height);

            for (int gridY = 0; gridY < data.Height; gridY++)
            {
                for (int gridX = 0; gridX < data.Width; gridX++)
                {
                    string objectString = data.Objects[data.Indices[gridY * data.Width + gridX]];

                    if (string.IsNullOrEmpty(objectString))
                    {
                        continue;
                    }

                    Texture2D texture = _textures[objectString];
                    int x = gridX * Game1.RESOLUTION + (Game1.RESOLUTION - texture.Width) / 2;
                    int y = gridY * Game1.RESOLUTION + Game1.RESOLUTION - texture.Height;

                    GameObject obj = GameObject.Create(x, y, objectString, texture);
                    level.Objects.Add(obj);
                }
            }

            return level;
        }

        public int Width { get; }
        public int Height { get; }
        public int PixelWidth { get; }
        public int PixelHeight { get; }
        
        public readonly List<GameObject> Objects;

        private Level(int width, int height)
        {
            Width = width;
            Height = height;
            PixelWidth = width * Game1.RESOLUTION;
            PixelHeight = height * Game1.RESOLUTION;
            
            Objects = new List<GameObject>();
        }
    }
}