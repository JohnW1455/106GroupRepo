using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SuperFruitAttack
{
    // Class: Player
    // Author: Jack Doyle
    // Purpose: To represent the player and store info on the player
    //          in game.
    public class Player : GameObject
    {
        private const int _TOTAL_POWER_UPS = 4;
        
        // Fields 
        private int health;
        private Vector2 moveSpeed;
        private Vector2 playerVelocity; 
        private Vector2 jumpVelocity;
        private Vector2 gravity;
        private PlayerState pState;
        private bool isGrounded;
        private MouseState prevMouse;
        private KeyboardState prevKey;
        private bool godMode;
        private bool wallClimb;
        private int invincible;
        
        // Textures needed
        private Texture2D playerRun;
        private Texture2D playerWall;

        private int[] powerUps;

        // Properties
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public Vector2 MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public PlayerState PState
        {
            get { return pState; }
        }
        public bool IsGrounded
        {
            get { return isGrounded; }
            set { isGrounded = value; }
        }
        public Vector2 PlayerVelocity
        {
            get { return playerVelocity; }
            set { playerVelocity = value; }
        }
        public bool GodMode
        {
            get { return godMode; }
            set { godMode = value; }
        }
        public bool WallClimb
        {
            get { return wallClimb; }
            set { wallClimb = value; }
        }
        public Vector2 JumpVelocity
        {
            get { return jumpVelocity; }
            set { jumpVelocity = value; }
        }

        // Constructor
        public Player(int playerHealth, int playerMS, Texture2D image,
            Collider collide) : base(image, collide)
        {
            // Initializes fields
            health = playerHealth;
            moveSpeed = new Vector2(playerMS, 0);
            gravity = new Vector2(0, .9f);
            playerVelocity = new Vector2(0, 0);
            pState = PlayerState.faceRight;
            jumpVelocity = new Vector2(0, -15.0f);
            isGrounded = true;
            invincible = 0;
            prevMouse = Mouse.GetState();
            prevKey = Keyboard.GetState();
            // Loads textures
            playerWall = Resources.GetTexture("playerWall");
            playerRun = Resources.GetTexture("playerRun");
            powerUps = new int[_TOTAL_POWER_UPS];
        }

        public void TakeDamage()
        {
            // Only damages the player when they are not in god mode and not invincible
            if (!godMode && invincible == 0)
            {
                health--;
                // Activates invincibilty for 30 ticks
                invincible = 30;
            }
        }

        public void Tick(GameTime time)
        {
            // Gets the current Keyboard State
            KeyboardState kb = Keyboard.GetState();

            float jumpMultiplier = HasPowerUp(PowerUp.JumpBoost) ? 1.1f : 1;
            // Runs the PlayerState FSM
            // NOTE: Jumps happen on state switch so the game doesn't need to check for
            //       single key presses or count jumps
            switch (pState)
            {
                case PlayerState.faceRight:
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && kb.IsKeyDown(Keys.W))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.W))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    break;
                case PlayerState.faceLeft:
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && kb.IsKeyDown(Keys.W))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.W))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    break;
                case PlayerState.walkRight:
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && kb.IsKeyDown(Keys.W) && (godMode || !wallClimb))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) &&
                        !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to facing right
                        pState = PlayerState.faceRight;
                    }
                    else if (kb.IsKeyDown(Keys.W) && (godMode || !wallClimb))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    break;
                case PlayerState.walkLeft:
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && kb.IsKeyDown(Keys.W) && (godMode || !wallClimb))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) &&
                        !kb.IsKeyDown(Keys.W))
                    {
                        // Switches to facing left
                        pState = PlayerState.faceLeft;
                    }
                    else if (kb.IsKeyDown(Keys.W) && (godMode || !wallClimb))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y * jumpMultiplier;
                        // Sets IsGrounded to false
                        isGrounded = false;
                    }
                    break;
                case PlayerState.jumpRight:
                    // If the player is still airborne, can only switch to jump left
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D) && !isGrounded)
                    {
                        // Switches to jump left
                        pState = PlayerState.jumpLeft;
                    }
                    // If the player is no longer airborne, can switch to other states
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D) && isGrounded)
                    {
                        // Switches to walk left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A) && isGrounded)
                    {
                        // Switches to walk right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) && isGrounded)
                    {
                        // Switches to face right
                        pState = PlayerState.faceRight;
                    }
                    break;
                case PlayerState.jumpLeft:
                    // If the player is still airborne, can only switch to jump left
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A) && !isGrounded)
                    {
                        // Switches to jump right
                        pState = PlayerState.jumpRight;
                    }
                    // If the player is no longer airborne, can switch to other states
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D) && isGrounded)
                    {
                        // Switches to walk left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A) && isGrounded)
                    {
                        // Switches to walk right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) && isGrounded)
                    {
                        // Switches to face left
                        pState = PlayerState.faceLeft;
                    }
                    break;
            }

            // Checks input for movement
            float speedMultiplier = HasPowerUp(PowerUp.SpeedBoost) ? 1.5f : 1;
            if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
            {
                // Moves the player left
                this.X -= (int)(moveSpeed.X * speedMultiplier);
            }
            else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A))
            {
                // Moves the player right
                this.X += (int)(moveSpeed.X * speedMultiplier);
            }

            if (SingleKeyPress(Keys.G))
            {
                godMode = !godMode;
            }

            // Fires bullets
            // Player cannot fire bullets while wall climbing, or if they were
            // wall climbing last update cycle
            if (!wallClimb)
            {
                FireGun();
            }
            // Applies gravity
            // If the player is in god mode and applying gravity would kill them, don't
            if (!(godMode && playerVelocity.Y + this.Y + 32 > LevelManager.CurrentLevel.PixelHeight))
            {
                ApplyGravity();
            }
            else
            {
                // Forcefully sets isGrounded to true so that you can't get softlocked
                // in pits
                isGrounded = true;
            }

            // Sets wall climb to false. If the player is wall climbing the collisions
            // will set it back to true
            wallClimb = false;

            // Decrements invincibility frames if the int is greater than 0
            if (invincible != 0)
            {
                invincible--;
            }
            
            TickPowerUps(time);

            // Collisions are handled in the object manager
        }

        /// <summary>
		/// Applies gravity to the player
		/// </summary>
		public void ApplyGravity()
        {
            // Adds the gravity to the player velocity
            playerVelocity += gravity;
            // Adds the velocity to the player position
            this.colliderObject.Position = this.colliderObject.Position + playerVelocity;
        }

        /// <summary>
        /// Fires the players gun in the direction they
        /// are facing. Requires the reload time to have elapsed
        /// </summary>
        private void FireGun()
        {
            // Checks to see if the user wanted to fire the gun and if it is a single press
            if (SingleKeyPress(Keys.J) == true)
            {
                // Checks the state of the character
                int direction = 1;
                if (pState == PlayerState.faceLeft || pState == PlayerState.jumpLeft ||
                pState == PlayerState.walkLeft)
                {
                    // Fires the bullet from the left
                    direction = -1;
                }
                if (pState == PlayerState.faceRight || pState == PlayerState.jumpRight ||
                pState == PlayerState.walkRight)
                {
                    direction = 1;
                }
                
                GameObjectManager.AddObject(
                    Projectile.Create(X + Width/2, Y + Height/2, 
                        new Vector2(direction, 0), true));
            }
            // Sets the prevMouse state to the current mouse
            
        }

        /// <summary>
        /// Draws the player object facing in the right direction
        /// </summary>              
        /// <param name="sb">SpriteBatch used for drawing the object</param>
        public override void Draw(SpriteBatch sb)
        {
            // If the player is un-damageable this frame
            if (godMode || invincible > 0)
            {
                // If the player is wall climbing, do the following
                if (wallClimb)
                {
                    // Checks the direction of the player and draws the sprite accordingly
                    if (pState == PlayerState.faceRight || pState == PlayerState.jumpRight
                        || pState == PlayerState.walkRight)
                    {
                        // Draws the sprite facing right
                        sb.Draw(
                            playerWall,
                            this.ColliderObject.Bounds,
                            Color.Maroon);
                    }
                    else
                    {
                        // Draws the sprite facing left
                        sb.Draw(
                            playerWall,
                            this.ColliderObject.Bounds,
                            null,
                            Color.Maroon,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.FlipHorizontally,
                            0.0f);
                    }
                }
                // Otherwise, check the player's state and draw accordingly
                else if (pState == PlayerState.faceLeft || pState == PlayerState.faceRight)
                {
                    // Draws a sprite that stares into your soul
                    sb.Draw(
                        this.image,
                        this.ColliderObject.Bounds,
                        Color.Maroon);
                }
                else if (pState == PlayerState.jumpLeft || pState == PlayerState.walkLeft)
                {
                    // Draws the run sprite facing left
                    sb.Draw(
                        playerRun,
                        this.ColliderObject.Bounds,
                        Color.Maroon);
                }
                else
                {
                    // Draws the run sprite facing right
                    sb.Draw(
                        playerRun,
                        this.ColliderObject.Bounds,
                        null,
                        Color.Maroon,
                        0.0f,
                        Vector2.Zero,
                        SpriteEffects.FlipHorizontally,
                        0.0f);
                }
            }
            // If the player is damageable for this current frame
            else
            {
                // If the player is wall climbing, do the following
                if (wallClimb)
                {
                    // Checks the direction of the player and draws the sprite accordingly
                    if (pState == PlayerState.faceRight || pState == PlayerState.jumpRight
                        || pState == PlayerState.walkRight)
                    {
                        // Draws the sprite facing right
                        sb.Draw(
                            playerWall,
                            this.ColliderObject.Bounds,
                            Color.White);
                    }
                    else
                    {
                        // Draws the sprite facing left
                        sb.Draw(
                            playerWall,
                            this.ColliderObject.Bounds,
                            null,
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.FlipHorizontally,
                            0.0f);
                    }
                }
                // Otherwise, check the player's state and draw accordingly
                else if (pState == PlayerState.faceLeft || pState == PlayerState.faceRight)
                {
                    // Draws a sprite that stares into your soul
                    sb.Draw(
                        this.image,
                        this.ColliderObject.Bounds,
                        Color.White);
                }
                else if (pState == PlayerState.jumpLeft || pState == PlayerState.walkLeft)
                {
                    // Draws the run sprite facing left
                    sb.Draw(
                        playerRun,
                        this.ColliderObject.Bounds,
                        Color.White);
                }
                else
                {
                    // Draws the run sprite facing right
                    sb.Draw(
                        playerRun,
                        this.ColliderObject.Bounds,
                        null,
                        Color.White,
                        0.0f,
                        Vector2.Zero,
                        SpriteEffects.FlipHorizontally,
                        0.0f);
                }
            }
            prevKey = Keyboard.GetState();
        }

        private bool SingleKeyPress(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key) && prevKey.IsKeyUp(key);
        }
        
        private void TickPowerUps(GameTime gameTime)
        {
            for (var i = 0; i < powerUps.Length; i++)
            {
                var time = powerUps[i];

                if (time == -1 || time == 0)
                    continue;

                time -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (time < 0)
                    time = 0;

                powerUps[i] = time;
            }
        }

        public void ApplyPowerUp(PowerUp powerUp, int durationMs)
        {
            if (powerUp == PowerUp.HealthUp)
            {
                if (health < 4)
                    health++;

                return;
            }
            
            powerUps[(int) powerUp] = durationMs;
        }
        
        private bool HasPowerUp(PowerUp powerUp)
        {
            return powerUps[(int) powerUp] != 0;
        }
        
        public int GetDamage() => HasPowerUp(PowerUp.DoubleDamage) ? 2 : 1;
    }
}
