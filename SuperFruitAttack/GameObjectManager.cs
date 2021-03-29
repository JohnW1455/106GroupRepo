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
        private List<GameObject> characters;
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
            characters = new List<GameObject>();
            collectibles = new List<Collectible>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            this.player = player;
        }

        public Player P1
        {
            get { return player; }
            set { player = value; }
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
                characters.Add(thing);
                enemies.Add((Enemy)thing);
            }
            else if(thing is Projectile)
            {
                projectiles.Add((Projectile)thing);
            }
            else if(thing is Collectible)
            {
                characters.Add(thing);
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
        /// <summary>
        /// This method 
        /// </summary>
        public void Tick()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.Health == 0)
                {
                    RemoveObject(enemy);
                }
            }
            foreach (Collectible item in collectibles)
            {
                if (item.IsActive == false)
                {
                    RemoveObject(item);
                }
            }
            foreach(Projectile bullet in projectiles)
            {
                if(bullet.Collided == true)
                {
                    RemoveObject(bullet);
                }
            }
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
                    player.TakeDamage(1);
                }
            }
            foreach(Collectible collectible in collectibles)
            {
                if(collectible.CheckCollision(player) == true);
                {
                    collectibles.Remove(collectible);
                }
                
            }
            foreach(Projectile bullet in projectiles)
            {
                if(bullet.CheckCollision(player) == true && bullet.IsPlayerBullet == false)
                {
                    player.TakeDamage(bullet.Damage);
                    bullet.Collided = true;
                }
            }
            foreach(Projectile bullet in projectiles)
            {
                foreach(Enemy enemy in enemies)
                {
                    if(bullet.CheckCollision(enemy) == true && bullet.IsPlayerBullet == true)
                    {
                        enemy.TakeDamage(bullet.Damage);
                        bullet.Collided = true;
                    }
                }
            }
        }
    }
}
