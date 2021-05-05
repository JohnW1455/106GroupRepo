using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuperFruitAttack
{
    /* Author: Nathan Caron
     *Purpose: create a class that'll use external data to create an in-game level environment.
     *Date: 5/1/2021*/
    public struct LevelData
    {
        public int Width;
        public int Height;
        public string[] Objects;
        public byte[] Indices;
    }

    public class Level
    {
        public static Level Parse(LevelData mapData)
        {
            var level = new Level(mapData.Width, mapData.Height);

            for (var gridY = 0; gridY < mapData.Height; gridY++)
            {
                for (var gridX = 0; gridX < mapData.Width; gridX++)
                {
                    var index = mapData.Indices[gridY * mapData.Width + gridX];
                    var imageName = mapData.Objects[index];
                    
                    if (imageName == "empty")
                        continue;
                    
                    var texture = Resources.GetTexture(imageName);
                    var x = gridX * Game1.RESOLUTION + (Game1.RESOLUTION - texture.Width) / 2;
                    var y = gridY * Game1.RESOLUTION + Game1.RESOLUTION - texture.Height;

                    var gameObject = GameObject.Create(texture.Name, x, y, texture);
                    level.Objects.Add(gameObject);
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