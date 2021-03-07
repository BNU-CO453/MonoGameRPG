using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameRPG.Sprites
{
    public class PlayerSprite : Sprite
    {
        public PlayerSprite(int x, int y) : base(x, y) { }

        /// <summary>
        /// delta time is the time elapsed in second (usualy 1/60 second)
        /// 0.01667 seconds.  So to move 1 pixel at 60 fps the speed should
        /// be set to 60.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyState = Keyboard.GetState();

            float newX, newY;
            IsMoving = false;

            if(keyState.IsKeyDown(Keys.Right))
            {
                Direction = Directions.Right;
                newX = Position.X + Speed * deltaTime;
                Position = new Vector2(newX , Position.Y);
                IsMoving = true;
            }
            
            if (keyState.IsKeyDown(Keys.Left))
            {
                Direction = Directions.Left;
                newX = Position.X - Speed * deltaTime;
                Position = new Vector2(newX, Position.Y);
                IsMoving = true;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                Direction = Directions.Up;
                newY = Position.Y - Speed * deltaTime;
                Position = new Vector2(Position.X, newY);
                IsMoving = true;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                Direction = Directions.Down;
                newY = Position.Y + Speed * deltaTime;
                Position = new Vector2(Position.X, newY);
                IsMoving = true;
            }

        }
    }
}
