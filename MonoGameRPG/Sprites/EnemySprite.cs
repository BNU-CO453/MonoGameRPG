using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class EnemySprite : AnimatedSprite
    {
        public PlayerSprite Player { get; set; }

        public EnemySprite(int x, int y) : base(x, y)
        {
        }

        /// <summary>
        /// Move this sprite towards the player's position
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Update Animation but do not move

            Speed = 0;
            base.Update(gameTime);

            
            Speed = MinSpeed;
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 direction = Player.Position - Position;
            direction.Normalize();
            Position += direction * Speed * deltaTime;
        }
    }
}
