using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace projeto1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D player1Red;
        Texture2D player2;
        Texture2D bola;
        Texture2D fundo;

        Vector2 player1Redposition;
        Vector2 player2position;
        Vector2 bolaposition;

        Rectangle player1RedSize;
        Rectangle player2Size;
        Rectangle bolaSize;

        float player1RedVelY;
        float player2VelY;
        float bolaVelY;
        float bolaVelX;

        KeyboardState keyThisFrame;
        KeyboardState keyLastFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            player1Red = Content.Load<Texture2D>("paddle_green");

            player1Redposition = new Vector2(0, 0);
            player1RedSize = new Rectangle(0, 0, 20, 60);
            player1RedVelY = 5.0f;


            player2 = Content.Load<Texture2D>("paddle_green");
            
            player2position = new Vector2(780, 0);
            player2Size = new Rectangle(0, 0, 20, 60);
            player2VelY = 5.0f;

            bola = Content.Load<Texture2D>("ball");
            
            bolaposition = new Vector2(400, 240);
            bolaSize = new Rectangle(0, 0, 114, 105);
            bolaVelY = 3.0f;
            bolaVelX = 3.0f;

            fundo = Content.Load<Texture2D>("25462_1dc55ffa06e810d0b8a14a0028787e6f");



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyThisFrame = Keyboard.GetState();

            //player1Red

            if(player1Redposition.Y < 0)
            {
                player1Redposition.Y = 0;
            }
            
            if(player1Redposition.Y > 420)
            {
                player1Redposition.Y = 420;
            }

            if(keyThisFrame.IsKeyDown(Keys.S))
            {
                player1Redposition.Y += player1RedVelY;
            }
            
            if(keyThisFrame.IsKeyDown(Keys.W))
            {
                player1Redposition.Y -= player1RedVelY;
            }

            //player2

            if(player2position.Y < 0)
            {
                player2position.Y = 0;
            }
            
            if(player2position.Y > 420)
            {
                player2position.Y = 420;
            }

            if(keyThisFrame.IsKeyDown(Keys.Down ))
            {
                player2position.Y += player2VelY;
            }
            
            if(keyThisFrame.IsKeyDown(Keys.Up ))
            {
                player2position.Y -= player2VelY;
            }

            keyLastFrame = keyThisFrame;

            //bola

            bolaposition.X += bolaVelX;
            bolaposition.Y += bolaVelY;

            //bola quicando para baixo
            
            if(bolaposition.Y > 480-24)
            {
                bolaVelY *=-1;
            }

            //bola quicando para direita

            if (bolaposition.X > 810-24)
            {
                //bolaVelX *= -1;
            }

            //bola quicando para cima
            
            if (bolaposition.Y < 0)
            {
                bolaVelY *= -1;
            }
            
            //bola quicando para esquerda
 
            if (bolaposition .X < -0)
            {
                //bolaVelX *= -1;
            }

            //colisao com player2

            if (bolaposition.X + bolaSize.Width/2  > player2position.X)
            {
                if (bolaposition.Y + bolaSize.Height/2  > player2position.Y)
                {
                    if (bolaposition.Y < player2position.Y + player2Size.Height)
                    {
                        bolaVelX *= -1;
                        bolaVelX -= 1;                  
                    }

                }
            } 

            //colisao com player1Red

            if (bolaposition.X < player1Redposition.X + player1RedSize.Width)
            {
                if (bolaposition.Y + bolaSize.Height > player1Redposition.Y)
                {
                    if (bolaposition.Y < player1Redposition.Y + player1RedSize.Height)
                    {
                        bolaVelX *= -1;
                        bolaVelX += 1;
                    }
                }
            } 


            //bola sai pela direita
            if(bolaposition.X > Window.ClientBounds.Width - bolaSize.Width)
            {
                bolaVelX *= -1;
                bolaVelX = -5;
                //bolaVelY = 0;
                bolaposition = new Vector2(585, 200);
            }

            //bola sai pela esquerda
            if(bolaposition.X <0)
            {
                bolaVelX *= -1;
                bolaVelX = 5;
                //bolaVelY=0;
                bolaposition = new Vector2(185, 200);
            }



            base.Update(gameTime);
        }
        
      
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            //fundo
            spriteBatch.Draw(fundo, new Rectangle(0,
                                                 0,
                                                 Window.ClientBounds.Width,
                                                 Window.ClientBounds.Height),
                                           Color.White);
            
            //player1Red

            spriteBatch.Draw(player1Red,
                new Rectangle((int)player1Redposition.X,
                              (int)player1Redposition.Y,
                              (int)player1RedSize.Width/1,
                              (int)player1RedSize.Height/1),
                player1RedSize,
                Color.White);

            //player2

            spriteBatch.Draw(player2,
                new Rectangle((int)player2position.X,
                              (int)player2position.Y,
                              (int)player2Size.Width /1 ,
                              (int)player2Size.Height /1),
                player2Size,
                Color.White);

            //bola

            spriteBatch.Draw(bola,
                new Rectangle((int)bolaposition.X,
                              (int)bolaposition.Y,
                              (int)bolaSize.Width / 5,
                              (int)bolaSize.Height / 5),
                bolaSize,
                Color.White);
                        
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
