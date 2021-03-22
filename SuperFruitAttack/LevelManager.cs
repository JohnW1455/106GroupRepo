using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SuperFruitAttack
{
    public class LevelManager
    {
        private List<Level> _levels;

        private int _currentLevel;

        public LevelManager()
        {
            _levels = new List<Level>();
            _currentLevel = 0;
            LoadLevels();
        }

        private void LoadLevels()
        {
            string[] paths = Directory.EnumerateFiles(
                "Content/Levels/", 
                "*.level", 
                SearchOption.TopDirectoryOnly).ToArray();

            for (int i = 0; i < paths.Length; i++)
            {
                LevelData levelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(paths[i]));
                Level level = Level.Parse(levelData);
                _levels.Add(level);
            }
        }
    }
}