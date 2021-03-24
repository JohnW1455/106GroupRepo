using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using SuperFruitAttack.Colliders;

namespace SuperFruitAttack
{
    class GameObjectManager
    {
        private Player player;
        private List<GameObject> items;
        private List<Enemy> enemies;
        private List<Collectible> collectibles;
        private List<Projectile> projectiles;


        public GameObjectManager(Player player)
        {
            collectibles = new List<Collectible>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            items = new List<GameObject>();
            this.player = player;
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
       
        public void RemoveObject(GameObject thing)
        {
            if (thing is Enemy)
            {
                enemies.Remove((Enemy)thing);
            }
            else if(thing is Projectile)
            {
                projectiles.Remove((Projectile)thing);
            }
            else if(thing is Collectible)
            {
                collectibles.Remove((Collectible)thing);
            }
        }

        public void Tick(int gameTime)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.CheckCollision(player.Collider);
            }
            foreach(Collectible collectible in collectibles)
            {
                collectible.CheckCollision(player.Collider);
            }
        }

        public void CheckCollision(Collider collider)
        {

        }
    }
}
