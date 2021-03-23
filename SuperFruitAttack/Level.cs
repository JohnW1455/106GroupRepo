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
        public TileData[] Tiles;
    }

    public struct TileData
    {
        public string Ground;
        public string Object;
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

            foreach (TileData tile in data.Tiles)
            {
                if (!string.IsNullOrEmpty(tile.Ground))
                {
                    level.Ground.Add(_textures[tile.Ground]);
                }

                if (!string.IsNullOrEmpty(tile.Object))
                {
                    GameObject obj = GameObject.Create(tile.Object, _textures[tile.Object]);
                }
            }
            
            return level;
        }

        public int Width { get; }
        public int Height { get; }
        public int PixelWidth { get; }
        public int PixelHeight { get; }

        public readonly List<Texture2D> Ground;
        public readonly List<GameObject> Objects;

        private Level(int width, int height)
        {
            Width = width;
            Height = height;
            PixelWidth = width * Game1.RESOLUTION;
            PixelHeight = height * Game1.RESOLUTION;

            Ground = new List<Texture2D>();
            Objects = new List<GameObject>();
        }
    }
}