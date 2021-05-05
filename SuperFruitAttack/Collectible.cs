using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperFruitAttack.Colliders;
using System;
using System.Collections.Generic;


namespace SuperFruitAttack
{
    /* Author: Nathan Caron
     * Purpose: Create a power up class
     * Date: 5/1/2021*/
    public enum PowerUp
    {
        HealthUp,
        SpeedBoost,
        DoubleDamage,
        JumpBoost
    }
    
    public class Collectible : GameObject
    {
        private static readonly Random _random = new Random();
        private static readonly List<Tuple<PowerUp, int>> _possiblePowerUps =
            new List<Tuple<PowerUp, int>>
            {
                Tuple.Create(PowerUp.HealthUp, 0),
                Tuple.Create(PowerUp.SpeedBoost, 10000),
                Tuple.Create(PowerUp.DoubleDamage, 10000),
                Tuple.Create(PowerUp.JumpBoost, 10000)
            };
        
        public Collectible(Texture2D image, Collider colliderObject) : base(image, colliderObject)
        {
        }

        public void OnCollect(Player player)
        {
            var (powerUp, time) = _possiblePowerUps[_random.Next(_possiblePowerUps.Count)];
            player.ApplyPowerUp(powerUp, time);
        }
    }
}