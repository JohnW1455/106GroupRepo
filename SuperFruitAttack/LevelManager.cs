using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace SuperFruitAttack
{
    public static class LevelManager
    {
        private static readonly List<Level> _Levels = new List<Level>();

        private static int _currentLevel;

        public static void LoadLevels()
        {
            var paths = Directory.EnumerateFiles(
                $"{Resources.ROOT_DIRECTORY}/Levels/", 
                "*.level", 
                SearchOption.TopDirectoryOnly).ToArray();

            foreach (var path in paths)
            {
                var levelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path));
                var level = Level.Parse(levelData);
                _Levels.Add(level);
            }
        }

        public static void NextLevel()
        {
            GameObjectManager.Reset();
            _currentLevel++;

            foreach (var obj in _Levels[_currentLevel - 1].Objects)
            {
                GameObjectManager.AddObject(obj);
            }
        }
    }
}