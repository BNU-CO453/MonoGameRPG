using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    /// <summary>
    /// This is the base class for all 2D Sprites
    /// A Sprite is a 2D object which has at least
    /// one image, a postion and a speed of movement.
    /// </summary>
    /// <Author>
    /// Derek Peacock
    /// </Author>
    public class Sprite
    {
        // Structures

        public Rectangle Boundary { get; set; }

        public Vector2 StartPosition { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 CenterPosition 
        {
            get 
            {
                if (Image == null)
                    return Vector2.Zero;
                else
                    return  new Vector2(Position.X - Image.Width / 2,
                                        Position.Y - Image.Height / 2);
            }
        }
        // Properties
        public static int MaxSpeed { get; set; }

        public static int MinSpeed { get; set; }

        public int Speed { get; set; }

        public Directions Direction { get; set; }

        public Texture2D Image{ get; set; }

        public Color Color = Color.White;

        public Vector2 Origin;

        public float Rotation = 0f;

        public float Scale = 1f;

        public SpriteEffects SpriteEffect;

        public bool IsVisible { get; set; }

        public bool IsAlive { get; set; }

        public bool IsActive { get; set; }

        public int Width 
        {
            get { return Image.Width; }
        }

        public int Height 
        {
            get { return Image.Height; }
        }

        // The rectangle occupied by the unscaled image
        public Rectangle BoundingBox 
        {
            get 
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y, Width, Height);
            }
        }
        
        // Variables

        protected float deltaTime;

        protected int frameWidth;
        protected int frameHeight;
       
        /// <summary>
        /// Constructor sets the starting position of
        /// the Sprite and current speed of a visible
        /// and alive sprite.
        /// </summary>
        public Sprite(int x, int y)
        {
            Position = new Vector2(x, y);
            StartPosition = Position;
            Direction = Directions.Right;

            MaxSpeed = 1000;
            MinSpeed = 100;

            Speed = MinSpeed;

            IsVisible = true;
            IsAlive = true;
            IsActive = true;
        }


        public void ResetPosition()
        {
            Position = StartPosition;
            Speed = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            if(IsActive)
            {
                int newX, newY;
                
                switch(Direction)
                {
                    case Directions.Left:
                        newX = (int)(Position.X - Speed * deltaTime);
                        Position = new Vector2(newX, Position.Y);
                        break;

                    case Directions.Right:
                        newX = (int)(Position.X + Speed * deltaTime);
                        Position = new Vector2(newX, Position.Y);
                        break;

                    case Directions.Down:
                        newY = (int)(Position.Y + Speed * deltaTime);
                        Position = new Vector2(Position.X, newY);
                        break;

                    case Directions.Up:
                        newY = (int)(Position.Y - Speed * deltaTime);
                        Position = new Vector2(Position.X, newY);
                        break;
                }
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image,
                Position,
                new Rectangle(0, 0, Width, Height),
                Color, Rotation, Origin,
                Scale, SpriteEffect, 0f);
        }

    }
}
