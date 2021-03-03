using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Vector2 position = new Vector2(0, 0);

            spriteBatch.Draw(
                backgroundImage, position, Color.White);

            spriteBatch.Draw(player.Image, player.Position, Color.Yellow);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
