using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace SuperFruitAttack
{
    /* Author: Nathan Caron
     * Purpose: Create a manager for level objects.
     * Restrictions: Must account for level addition and deletion as well as reading the proper files.
     * Date: 4/10/2021*/
    public static class LevelManager
    {
        private static readonly List<Level> _Levels = new List<Level>();

        private static int _currentLevelNumber;

        public static Level CurrentLevel => _Levels[_currentLevelNumber - 1];

        public static void LoadLevels()
        {
            var paths = Directory.EnumerateFiles(
                "Levels/", 
                "*.level", 
                SearchOption.TopDirectoryOnly).ToArray();

            foreach (var path in paths)
            {
                var levelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(path));
                var level = Level.Parse(levelData);
                _Levels.Add(level);
            }
        }

        public static int CurrentLevelNumber
        {
            get { return _currentLevelNumber; }
            set { _currentLevelNumber = value; }
        }
        public static int LevelCount
        {
            get { return _Levels.Count; }

        }
        public static void NextLevel()
        {
            GameObjectManager.Reset();
            _currentLevelNumber++;
            foreach (var obj in CurrentLevel.Objects)
            {
                GameObjectManager.AddObject(obj is Platform ? obj : obj.Copy());
            }
        }


    }
}