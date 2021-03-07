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
              return  new Vector2(Position.X - Image.Width / 2,
                                  Position.Y - Image.Height / 2);
            }
        }
        // Properties
        public int MaxSpeed { get; set; }

        public int MinSpeed { get; set; }

        public int Speed { get; set; }

        public Directions Direction { get; set; }

        public Texture2D Image { get; set; }

        public SpriteAnimation Animation { get; set; }

        public SpriteAnimation [] Animations { get; set; }

        public bool IsVisible { get; set; }

        public bool IsAlive { get; set; }

        public bool IsMoving { get; set; }

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
            MinSpeed = 200;
            Speed = MinSpeed;

            IsVisible = true;
            IsAlive = true;
            IsMoving = false;

            Animations = new SpriteAnimation[4];
        }


        public void ResetPosition()
        {
            Position = StartPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Animation != null)
            {
                Animation = Animations[(int)Direction];

                Animation.Position = Position;

                if (IsMoving)
                    Animation.Update(gameTime);
                else
                    Animation.SetFrame(1);
            }
        }

    }
}
