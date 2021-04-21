using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using Microsoft.Xna.Framework.Media;


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
        private static bool ticking;
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
            if (ticking)
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
            if (ticking)
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
            foreach (Platform platform in platforms)
            {
                foreach (Projectile projectile in projectiles)
                {
                    if (projectile.CheckCollision(platform))
                        RemoveObject(projectile);
                }

                platform.CheckCollision(player);
            }

            foreach (Projectile projectile in projectiles)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (!projectile.IsPlayerBullet)
                        break;

                    if (projectile.CheckCollision(enemy))
                    {
                        enemy.TakeDamage(1);
                        RemoveObject(projectile);
                    }
                }
                
                if (projectile.IsPlayerBullet)
                    continue;
                
                if (projectile.CheckCollision(player))
                {
                    player.TakeDamage();
                    RemoveObject(projectile);
                }
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.CheckCollision(player))
                {
                    player.TakeDamage();
                }
            }
        }

        public static void Tick(GameTime gameTime)
        {
            ticking = true;
            
            player?.Tick(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Tick(gameTime);
            }

            foreach (Projectile projectile in projectiles)
            {
                projectile.Tick(gameTime);
            }
            
            CheckCollision();

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Health <= 0)
                    RemoveObject(enemy);
            }

            ticking = false;
            
            foreach (GameObject add in toAdd)
            {
                AddObject(add);
            }

            foreach (GameObject remove in toRemove)
            {
                RemoveObject(remove);
            }
            
            toAdd.Clear();
            toRemove.Clear();
        }

        public static Matrix CameraMatrix(int screen_X, int screen_Y, int max_X, int max_Y)
        {
            float scaleFactor = 1.15f;
            // Calculates the X translation
            float translate_X = (player.X + 16) - (screen_X / (2 * scaleFactor));
            float translate_Y = (player.Y + 16) - (screen_Y / (2 * scaleFactor));
            // Corrects the translations so that they don't go out of bounds
            if (translate_X < 0)
            {
                translate_X = 0;
            }
            else if (translate_X > max_X - (screen_X / scaleFactor))
            {
                translate_X = max_X - (screen_X / scaleFactor);
            }
            if (translate_Y < 0)
            {
                translate_Y = 0;
            }
            else if (translate_Y > max_Y - (screen_Y / scaleFactor))
            {
                translate_Y = max_Y - (screen_Y / scaleFactor);
            }
            Matrix translation = Matrix.CreateTranslation(-translate_X, -translate_Y, 0);
            Matrix scaling = Matrix.CreateScale(scaleFactor, scaleFactor, 1f);
            Matrix result = Matrix.Multiply(translation, scaling);
            return result;
        }
      
        public static void Draw(SpriteBatch sb)
        {
            if(player != null)
            {
                player.Draw(sb);
            }
            if(flag != null)
            {
                flag.Draw(sb);

            }
            
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
