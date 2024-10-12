using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongGame
{
    public class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D paddleTexture;
        Vector2 paddle1Position;
        Vector2 paddle2Position;
        Texture2D ballTexture;
        Vector2 ballPosition;
        Vector2 ballSpeed;

        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            paddle1Position = new Vector2(10, 100);
            paddle2Position = new Vector2(770, 100);
            ballPosition = new Vector2(400, 200);
            ballSpeed = new Vector2(3, 3);  // Ball movement speed

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paddleTexture = Content.Load<Texture2D>("paddle");
            ballTexture = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Paddle 1 movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                paddle1Position.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                paddle1Position.Y += 5;

            // Paddle 2 movement (we'll handle AI with Python)
            paddle2Position.Y = GetPythonAIPaddlePosition();

            // Ball movement (we'll handle physics with C++)
            ballPosition = GetBallMovementFromCpp();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(paddleTexture, paddle1Position, Color.White);
            spriteBatch.Draw(paddleTexture, paddle2Position, Color.White);
            spriteBatch.Draw(ballTexture, ballPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetPythonAIPaddlePosition()
        {
            // Communicate with Python AI script for paddle movement
            return new Vector2(paddle2Position.X, paddle2Position.Y);  // Example for now
        }

        private Vector2 GetBallMovementFromCpp()
        {
            // Communicate with C++ for ball physics and movement
            return ballPosition;  // Example for now
        }
    }
}
