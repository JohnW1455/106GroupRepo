﻿using System;
using System.Collections.Generic;
using System.Text;
using SuperFruitAttack.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperFruitAttack
{
    // Class: Player
    // Author: Jack Doyle
    // Purpose: To represent the player and store info on the player
    //          in game.
    public class Player : GameObject
    {
        // Fields
        private int health;
        private Vector2 moveSpeed;
        private Vector2 playerVelocity;
        private Vector2 jumpVelocity;
        private Vector2 gravity;
        private PlayerState pState;
        private bool isGrounded;
        private double reload;

        // Properties
        public int Health
        {
            get { return health; }
        }
        public Vector2 MoveSpeed
        {
            get { return moveSpeed; }
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

        // Constructor
        public Player(int playerHealth, int playerMS, Texture2D image,
            Collider collide) : base(image, collide)
        {
            health = playerHealth;
            moveSpeed = new Vector2(playerMS, 0);
            gravity = new Vector2(0, 0.5f);
            playerVelocity = new Vector2(0, 0);
            pState = PlayerState.faceRight;
            jumpVelocity = new Vector2(0, -15.0f);
            reload = 0;
        }

        public void TakeDamage()
        {
            health--;
        }

        public void Tick(GameTime time)
        {
            // Gets the current Keyboard State
            KeyboardState kb = Keyboard.GetState();

            // Runs the PlayerState FSM
            // NOTE: Jumps happen on state switch so the game doesn't need to check for
            //       single key presses or count jumps
            switch (pState)
            {
                case PlayerState.faceRight:
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                case PlayerState.faceLeft:
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                case PlayerState.walkRight:
                    if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking left
                        pState = PlayerState.walkLeft;
                    }
                    else if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D)
                        && kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) &&
                        !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to facing right
                        pState = PlayerState.faceRight;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    break;
                case PlayerState.walkLeft:
                    if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to walking right
                        pState = PlayerState.walkRight;
                    }
                    else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)
                        && kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping right
                        pState = PlayerState.jumpRight;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
                    }
                    else if (kb.IsKeyDown(Keys.A) == kb.IsKeyDown(Keys.D) &&
                        !kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to facing left
                        pState = PlayerState.faceLeft;
                    }
                    else if (kb.IsKeyDown(Keys.Space))
                    {
                        // Switches to jumping left
                        pState = PlayerState.jumpLeft;
                        // Causes the player to jump
                        playerVelocity.Y = jumpVelocity.Y;
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
            if (kb.IsKeyDown(Keys.A) && !kb.IsKeyDown(Keys.D))
            {
                // Moves the player left
                this.X -= (int)moveSpeed.X;
            }
            else if (kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A))
            {
                // Moves the player right
                this.X += (int)moveSpeed.X;
            }

            // Fires bullets
            FireGun(time);

            // Applies gravity
            ApplyGravity();

            // Collisions are handled in the object manager
        }

        /// <summary>
		/// Applies gravity to the player
		/// </summary>
		public void ApplyGravity()
        {
            // Adds the acceleration to the player velocity
            Vector2 accel = new Vector2(0f, 0.3f);
            playerVelocity += accel;
            // Adds the velocity to the player position
            this.colliderObject.Position = this.colliderObject.Position + playerVelocity;
        }

        /// <summary>
        /// Fires the players gun in the direction they
        /// are facing. Requires the reload time to have elapsed
        /// </summary>
        /// <param name="time">GameTime parameter used to check how long
        /// since the last bullet was fired</param>
        public void FireGun(GameTime time)
        {
            MouseState mouse = Mouse.GetState();
            // increments the reload countdown
            reload += time.ElapsedGameTime.TotalSeconds;
            // Checks to see if the user wanted to fire the gun and if the reload was over
            if (reload > 0.2 && mouse.LeftButton == ButtonState.Pressed)
            {
                // Checks the state of the character
                if (pState == PlayerState.faceLeft || pState == PlayerState.jumpLeft ||
                pState == PlayerState.walkLeft)
                {
                    // Fires the bullet from the left
                    GameObjectManager.AddObject(
                        new Projectile(
                            Game1.GetTexture("simple ball"),
                            new CircleCollider(this.X, this.Y + (this.Height / 2), 20),
                            true,
                            new Vector2(-5f, 0)));
                }
                if (pState == PlayerState.faceRight || pState == PlayerState.jumpRight ||
                pState == PlayerState.walkRight)
                {
                    // Fires the bullet from the right
                    GameObjectManager.AddObject(
                        new Projectile(
                            Game1.GetTexture("simple ball"),
                            new CircleCollider(this.X + this.Width, 
                                               this.Y + (this.Height / 2), 20),
                            true,
                            new Vector2(-5f, 0)));
                }
            }
        }

        /// <summary>
        /// Draws the player object facing in the right direction
        /// </summary>
        /// <param name="sb">SpriteBatch used for drawing the object</param>
        public override void Draw(SpriteBatch sb)
        {
            // For the purpose of this milestone, only direction is needed, so state machine
            // isn't made yet
            if(pState == PlayerState.faceLeft || pState == PlayerState.jumpLeft ||
                pState == PlayerState.walkLeft)
            {
                // Draws the sprite facing left
                sb.Draw(
                this.Image,
                this.colliderObject.Position,
                this.colliderObject.Bounds,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.FlipHorizontally,
                0.0f);
            }
            if (pState == PlayerState.faceRight || pState == PlayerState.jumpRight ||
                pState == PlayerState.walkRight)
            {
                // Draws the sprite facing right
                sb.Draw(
                this.Image,
                this.colliderObject.Position,
                this.colliderObject.Bounds,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.0f);
            }
        }
    }
}
