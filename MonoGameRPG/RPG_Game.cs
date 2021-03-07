using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using MonoGameRPG.Sprites;
using MonoGameRPG.Tools;

namespace MonoGameRPG
{
    public class RPG_Game : Game
    {
        // constants

        public const int HD_Height = 720;
        public const int HD_Width = 1280;

        // Attributes/variables

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D playerImage;

        private Texture2D walkDownImages;
        private Texture2D walkUpImages;
        private Texture2D walkLeftImages;
        private Texture2D walkRightImages;

        private Texture2D backgroundImage;
        private Texture2D ballImage;
        private Texture2D skullImage;

        private PlayerSprite player;

        //private AndreiCamera camera;
        private Camera camera;
        private Viewport viewPort;

        public RPG_Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Setup the screen to the preferred size in
        /// this example it is HD 720p (1280 x 720)
        /// full HD is 1080P (1920 x 1080) pixells
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = HD_Width;
            graphics.PreferredBackBufferHeight = HD_Height;
            graphics.ApplyChanges();

            viewPort = new Viewport(0,0, HD_Width, HD_Height);

            //camera = new AndreiCamera(viewPort);
            camera = new Camera(graphics.GraphicsDevice);
            
            base.Initialize();
        }

        /// <summary>
        /// Load all the images for the game from the
        /// Content pipeline.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerImage = Content.Load<Texture2D>("Player/player");
            
            walkDownImages = Content.Load<Texture2D>("Player/walkDown");
            walkUpImages = Content.Load<Texture2D>("Player/walkUp");
            walkRightImages = Content.Load<Texture2D>("Player/walkRight");
            walkLeftImages = Content.Load<Texture2D>("Player/walkLeft");

            backgroundImage = Content.Load<Texture2D>("background");
            ballImage = Content.Load<Texture2D>("ball");
            skullImage = Content.Load<Texture2D>("skull");

            SetupSprites();
        }

        private void SetupSprites()
        {
            player = new PlayerSprite(200, 300);
            player.Image = playerImage;

            player.Animations[(int)Directions.Down] = 
                new SpriteAnimation(walkDownImages, 4, 10);

            player.Animations[(int)Directions.Left] =
                new SpriteAnimation(walkLeftImages, 4, 10);

            player.Animations[(int)Directions.Right] =
                new SpriteAnimation(walkRightImages, 4, 10);

            player.Animations[(int)Directions.Up] =
                new SpriteAnimation(walkUpImages, 4, 10);

            player.Animation = player.Animations[(int)Directions.Down];

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            camera.Position = player.Position;
            camera.Update(gameTime);

            //camera.Update(viewPort);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

//            spriteBatch.Begin(transformMatrix: camera.Transform);
            spriteBatch.Begin(camera);

            Vector2 position = new Vector2(0, 0);

            spriteBatch.Draw(
                backgroundImage, position, Color.White);

            //spriteBatch.Draw(player.Image, player.Position, Color.White);

            player.Animation.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
