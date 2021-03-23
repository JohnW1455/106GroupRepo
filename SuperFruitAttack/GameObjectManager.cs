using System;
using System.Collections.Generic;
using System.Text;

namespace SuperFruitAttack
{
    class GameObjectManager
    {
        private Player player;
        private List<GameObject> items;
        private List<Enemy> enemies;
        private List<Collectible> collectibles;
        private List<Projectile> projectiles;


        public GameObjectManager()
        {
            collectibles = new List<Collectible>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            items = new List<GameObject>();
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void AddCollectible(Collectible collectible)
        {
            collectibles.Add(collectible);
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }


    }
}
