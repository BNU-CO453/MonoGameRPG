using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class EnemySprite : AnimatedSprite
    {
        public int EnemyNo { get; set; }

        public PlayerSprite Player { get; set; }

        private bool debug = true;

        private int enemySpeed = 120;

        public EnemySprite(Texture2D image, int x, int y) : 
            base(image, x, y)
        {
            Speed = enemySpeed;
        }

        /// <summary>
        /// Move this sprite towards the player's position
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            Vector2 newDirection = Player.Position - this.Position;
            newDirection.Normalize();
            Direction = newDirection;

            base.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if(debug)
            {
                //spriteBatch.DrawString()
            }

        }
    }
}
