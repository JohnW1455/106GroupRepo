using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SuperFruitAttack
{
    /// <summary>
    /// This class manages all the gameObjects in the game.
    /// </summary>
    class GameObjectManager
    {
        private Player player;
        //These are the lists that'll hold all the gameObjects that will be in the game.
        private List<GameObject> items;
        private List<Enemy> enemies;
        private List<Collectible> collectibles;
        private List<Projectile> projectiles;

        /// <summary>
        /// This constructor instantiates all the gameObjects in the game, including the player.
        /// </summary>
        /// <param name="player">This is the player object that'll be managed in this class.</param>
        public GameObjectManager(Player player)
        {
            //Here I instantiate all the fields.
            collectibles = new List<Collectible>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            items = new List<GameObject>();
            this.player = player;
        }
        /// <summary>
        /// This method adds any type of game Object to their respective list.
        /// </summary>
        /// <param name="thing">This is the gameObject that'll be added to the
        /// object manager class.</param>
        public void AddObject(GameObject thing)
        {
            //Here, we categorize the gameObject into its respective subclass object.
            //For each type of object, we add it to its respective list.
            if (thing is Enemy)
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
       /// <summary>
       /// This method removes any specific game Object from their respective list.
       /// </summary>
       /// <param name="thing">This is the specified object that'll be removed.</param>
        public void RemoveObject(GameObject thing)
        {
            //Here, we categorize the gameObject into its respective subclass object.
            //For each type of object, we remove it from its respective list.
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

        public void Tick()
        {
            
        }
        /// <summary>
        /// This method checks all the objects and performs specific actions for when specific objects
        /// collide.
        /// </summary>
        public void CheckCollision()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.CheckCollision(player) == true)
                {
                    player.TakeDamage(enemy.Dmg);
                }
            }
            foreach(Collectible collectible in collectibles)
            {
                collectible.CheckCollision(player);
            }
            foreach(Projectile bullet in projectiles)
            {
                if(bullet.CheckCollision(player) == true && bullet.IsPlayerBullet == false)
                {
                    player.TakeDamage(bullet.Damage);
                }

            }
        }
    }
}
