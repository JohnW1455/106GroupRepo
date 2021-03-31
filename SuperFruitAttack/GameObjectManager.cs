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
    /// This class manages all the gameObjects in the game.
    /// </summary>
    static class GameObjectManager
    {
       
        private static Player player;
        //These are the lists that'll hold all the gameObjects that will be in the game.
        private static List<GameObject> items;
        private static List<Enemy> enemies;
        private static List<Collectible> collectibles;
        private static List<Projectile> projectiles;
        private static List<Platform> platforms;
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
        }
        
        public static Player Player => player;
        /// <summary>
        /// This method adds any type of game Object to their respective list.
        /// </summary>
        /// <param name="thing">This is the gameObject that'll be added to the
        /// object manager class.</param>
        public static void AddObject(GameObject thing)
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
            else if(thing is Player)
            {
                player = (Player)thing;
            }
            else if(thing is Platform)
            {
                platforms.Add((Platform)thing);
            }
        }
       /// <summary>
       /// This method removes any specific game Object from their respective list.
       /// </summary>
       /// <param name="thing">This is the specified object that'll be removed.</param>
        public static void RemoveObject(GameObject thing)
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
            else if(thing is Platform)
            {
                platforms.Remove((Platform)thing);
            }
            else if(thing is Player)
            {
                player = null;
            }
        }

        /// <summary>
        /// This method checks all the objects and performs specific actions for when specific objects
        /// collide.
        /// </summary>
        public static void CheckCollision()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.CheckCollision(player) == true)
                {
                    player.TakeDamage();
                }
            }
            foreach(Collectible collectible in collectibles)
            {
                if(collectible.CheckCollision(player) == true)
                {

                    RemoveObject(collectible);
                }
            }
            foreach(Projectile bullet in projectiles)
            {
                foreach(Enemy enemy in enemies)
                {
                    if(bullet.CheckCollision(enemy) == true && bullet.IsPlayerBullet == true)
                    {
                        enemy.TakeDamage(6);
                        if(enemy.Health <= 0)
                        {
                            enemies.Remove(enemy);
                        }
                    }   
                }
                if(bullet.CheckCollision(player) == true && bullet.IsPlayerBullet == false)
                {
                    player.TakeDamage();
                    RemoveObject(bullet);
                    if(player.Health == 0)
                    {
                        RemoveObject(player);
                    }
                } 
            }
            foreach(Projectile bullet in projectiles)
            {
                foreach(Platform platform in platforms)
                {
                    if(bullet.CheckCollision(platform) == true)
                    {
                        RemoveObject(bullet);
                    }
                }
            }

            foreach(Platform platform in platforms)
            {
                // Checks for player collisions with platforms
                if (platform.ColliderObject.Bounds.Intersects(player.ColliderObject.Bounds))
                {
                    // Gets the intersection of player and the platform
                    Rectangle overlap = Rectangle.Intersect(
                        player.ColliderObject.Bounds,
                        platform.ColliderObject.Bounds);

                    // If width of overlap is less than height, perform X adjustment
                    if (overlap.Width <= overlap.Height)
                    {
                        // If player X is less than platform X, move left
                        if (player.X < platform.X)
                        {
                            player.X -= overlap.Width;
                        }
                        // If player X is greater than platform X, move right
                        if (player.X > platform.X)
                        {
                            player.X += overlap.Width;
                        }
                    }
                    // If height of overlap is less than width, perform Y adjustment
                    if (overlap.Height < overlap.Width)
                    {
                        // If the player Y is less than the platform Y, move up
                        if (player.Y < platform.Y)
                        {
                            player.Y -= overlap.Height;
                            // Grounds the player
                            player.IsGrounded = true;
                        }
                        // If this collision doesn't occur, the player is not grounded
                        else
                        {
                            player.IsGrounded = false;
                        }
                        // If the player Y is greater than the platform Y, move down
                        if (player.Y > platform.Y)
                        {
                            player.Y += platform.Y;
                        }
                        // Resets the player's Y velocity
                        player.PlayerVelocity = new Vector2(0, 0);
                    }
                }
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
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
