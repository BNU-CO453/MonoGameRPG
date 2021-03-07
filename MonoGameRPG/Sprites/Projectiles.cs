using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class Projectiles
    {
        private List<Sprite> sprites ;

        private Texture2D image;

        public Projectiles(Texture2D image)
        {
            sprites =new List<Sprite>();
            this.image = image;

        }
        public void Fire(Vector2 position, Directions direction)
        {
            Sprite sprite = new Sprite((int)position.X, (int)position.Y);
            
            sprite.Image = image;
            sprite.Speed = sprite.MinSpeed;
            sprite.IsActive = true;
            sprite.Direction = direction;

            if(direction == Directions.Left)
            {
                position.X -= (float)sprite.Width / 2;
            }
            else if (direction == Directions.Right)
            {
                position.X += (float)sprite.Width / 2;
            }
            else if (direction == Directions.Down)
            {
                position.Y += (float)sprite.Height / 2;
            }
            else if (direction == Directions.Up)
            {
                position.Y -= (float)sprite.Height / 2;
            }

            sprite.Position = position;

            sprites.Add(sprite);
        }

        public void Update(GameTime gameTime)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public bool IsLoaded()
        {
            return sprites.Count > 0;
        }
    }
}
