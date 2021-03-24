using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
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
        public void AddObject(GameObject thing)
        {
            if(thing is Enemy)
            {
                enemies.Add((Enemy)thing);
            }
            else if(thing is Projectile)
            {
                projectiles.Add((Projectile)thing);
            }
            else if(thing is Collectible)
            {
                collectibles.Add((Collectible)thing);
            }
        }
       
    }
}
