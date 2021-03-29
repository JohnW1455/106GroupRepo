using System;
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

        // Constructor
        public Player(int playerHealth, float playerMS, Texture2D image,
            Collider collide) : base(image, collide)
        {
            health = playerHealth;
            moveSpeed = new Vector2(playerMS, 0);
            gravity = new Vector2(0, 0.5f);
            pState = PlayerState.faceRight;
            jumpVelocity = new Vector2(0, -15.0f);
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
        }
    }
}
