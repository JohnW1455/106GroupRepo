using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SuperFruitAttack
{
    /// <summary>
    /// Author: Elliot Gong
    /// This class manages all the gameObjects in the game.
    /// </summary>
    static class GameObjectManager
    {
       
        private static Player player;
        private static Flag flag;
        //These are the lists that'll hold all the gameObjects that will be in the game.
        private static List<GameObject> items;
        private static List<Enemy> enemies;
        private static List<Collectible> collectibles;
        private static List<Projectile> projectiles;
        private static List<Platform> platforms;
        private static List<GameObject> toRemove;
        private static List<GameObject> toAdd;
        private static bool checkingCollisions;
        /// <summary>
        /// This constructor instantiates all the gameObjects in the game, including the player.
        /// </summary>
        /// <param name="player">This is the player object that'll be managed in this class.</param>
        static GameObjectManager()
        {
            //Here I instantiate all the fields.
            player = null;
            collectibles = new List<Collectible>();
            projectiles = new List<Projectile>();
            enemies = new List<Enemy>();
            items = new List<GameObject>();
            platforms = new List<Platform>();
            toAdd = new List<GameObject>();
            toRemove = new List<GameObject>();
        }
        
        public static Player Player => player;
        public static Flag Flag => flag;
        /// <summary>
        /// This method adds any type of game Object to their respective list.
        /// </summary>
        /// <param name="thing">This is the gameObject that'll be added to the
        /// object manager class.</param>
        public static void AddObject(GameObject thing)
        {
            if (checkingCollisions)
            {
                toAdd.Add(thing);
                return;
            }
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
            else if(thing is Player)
            {
                player = (Player)thing;
            }
            else if(thing is Platform)
            {
                platforms.Add((Platform)thing);
            }
            else if (thing is Flag f)
            {
                flag = f;
            }
        }
       /// <summary>
       /// This method removes any specific game Object from their respective list.
       /// </summary>
       /// <param name="thing">This is the specified object that'll be removed.</param>
        public static void RemoveObject(GameObject thing)
        {
            if (checkingCollisions)
            {
                toRemove.Add(thing);
                return;
            }
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
            else if(thing is Platform)
            {
                platforms.Remove((Platform)thing);
            }
            else if(thing is Player)
            {
                player = null;
            }
            else if (thing is Flag)
            {
                flag = null;
            }
        }
       
       public static void Reset()
       {
           enemies.Clear();
           projectiles.Clear();
           collectibles.Clear();
           platforms.Clear();
           toAdd.Clear();
           toRemove.Clear();
           player = null;
           flag = null;
       }

        /// <summary>
        /// This method checks all the objects and performs specific actions for when specific objects
        /// collide.
        /// </summary>
        public static void CheckCollision()
        {
            checkingCollisions = true;
            //I loop through the enemy objects and check if they collide with the player.
            for (var i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = enemies[i];
                //If the player collides with an enemy, the player takes damage.
                if (enemy.CheckCollision(player) == true)
                {
                    player.TakeDamage();
                }
            }

            for (var i = platforms.Count - 1; i >= 0; i--)
            {
                platforms[i].CheckCollision(player);
            }
            //I loop through the collectible objects to check if they collide with the player.
            for (var i = collectibles.Count - 1; i >= 0; i--)
            {
                //If the player collides with a collectible, they get some bonus, and the collectible
                //is deleted and removed from GameObjectManager.
                if(collectibles[i].CheckCollision(player) == true)
                {
                    RemoveObject(collectibles[i]);
                }
            }
            for (var i = projectiles.Count - 1; i >= 0; i--)
            {
                for (var j = enemies.Count - 1; j >= 0; j--)
                {
                    if(projectiles[i].CheckCollision(enemies[j]) == true && projectiles[i].IsPlayerBullet == true)
                    {
                        enemies[j].TakeDamage(6);
                        if(enemies[j].Health <= 0)
                        {
                            RemoveObject(enemies[j]);
                        }
                        RemoveObject(projectiles[i]);
                    }   
                }
                if(projectiles[i].CheckCollision(player) == true && projectiles[i].IsPlayerBullet == false)
                {
                    player.TakeDamage();
                    RemoveObject(projectiles[i]);
                    
                } 
            }
            foreach(Platform platform in platforms)
            {
                for (var i = projectiles.Count - 1; i >= 0; i--)
                {
                    if(projectiles[i].CheckCollision(platform) == true)
                    {
                        RemoveObject(projectiles[i]);
                    }
                }
            }

            checkingCollisions = false;
            foreach (var add in toAdd)
            {
                AddObject(add);
            }

            foreach (var remove in toRemove)
            {
                RemoveObject(remove);
            }
        }

        public static void Tick(GameTime gameTime)
        {
            player.Tick(gameTime);
            foreach (Enemy enemy in enemies)
            {
                enemy.Tick(gameTime);
            }

            foreach (Projectile projectile in projectiles)
            {
                projectile.Tick();
            }
        }

      
        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
            flag.Draw(sb);
            foreach(Platform platform in platforms)
            {
                platform.Draw(sb);
            }
            foreach(Enemy enemy in enemies)
            {
                enemy.Draw(sb);
            }
            foreach(Projectile bullet in projectiles)
            {
                bullet.Draw(sb);
            }
            foreach(Collectible item in collectibles)
            {
                item.Draw(sb);
            }
        }
    }
}
