using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ForeignSubstance.Items;
using ForeignSubstance.Collisions;
using ForeignSubstance.Screens;

namespace ForeignSubstance.Sprites
{
    public class Player : Sprite
    {

        private Texture2D _activeTexture;
        private Texture2D _idleTexture;
        private Texture2D _runningTexture;
        private Texture2D _healthTexture;
        private Vector2 _position;
        private Vector2 _velocity;
        private Inventory _inventory;
        private int _healthMax;
        private int _healthRemaining;
        private BoundingRectangle _bounds;
        Rectangle _textureMapPosition;
        private KeyboardState keyboardState;
        private double animationTimer;
        private short animationFrame = 1;
        private bool running = false;
        private bool flipped = false;
        private ArmSprite arm;
        public GameplayScreen gameScreen;
        public ArmSprite Arm => arm;

        public Color Color { get; set; } = Color.White;
        public BoundingRectangle Bounds => _bounds;


        public Vector2 Position => _position;
        public Player(Vector2 position1)
        {
            this._position = position1;
            _inventory = new Inventory(5,this);
            _healthMax = 6;
            _healthRemaining = 6;
            _textureMapPosition = new Rectangle(0,0,19,25);
            _bounds = new BoundingRectangle(_position.X-9.5f, _position.Y, 19, 25);
            arm = new ArmSprite(this);
        }

        public override void LoadContent(ContentManager content)
        {
            _idleTexture = content.Load<Texture2D>("Scifi Character/idle");
            _runningTexture = content.Load<Texture2D>("Scifi Character/run");
            _activeTexture = _idleTexture;
            arm.LoadContent(content);
            _healthTexture = content.Load<Texture2D>("Health");
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            if (_bounds.CollidesWith(other))
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gametime)
        {
            keyboardState = Keyboard.GetState();
            
            Vector2 positionXChecker = _position;
            Vector2 positionYChecker = _position;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {

                positionYChecker += new Vector2(0, -2);
                running = true;
                _bounds.Y = positionYChecker.Y;
                if (!gameScreen.CheckCollision(_bounds))
                {
                    _position.Y = positionYChecker.Y;
                }
                else
                {
                    _bounds.Y = _position.Y;
                }


            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                positionYChecker += new Vector2(0, 2);
                running = true;
                _bounds.Y = positionYChecker.Y;
                if (!gameScreen.CheckCollision(_bounds))
                {
                    _position.Y = positionYChecker.Y;
                }
                else
                {
                    _bounds.Y = _position.Y;
                }


            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                positionXChecker += new Vector2(-2, 0);
                running = true;
                _bounds.X = positionXChecker.X;
                if (!gameScreen.CheckCollision(_bounds))
                {
                    _position.X = positionXChecker.X;
                }
                else
                {
                    _bounds.X = _position.X;
                }
               
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                positionXChecker += new Vector2(2, 0);
                running = true;
                _bounds.X = positionXChecker.X;
                if (!gameScreen.CheckCollision(_bounds))
                {
                    _position.X = positionXChecker.X;
                }
                else
                {
                    _bounds.X = _position.X;
                }

            }



            if(running)
            {
                _activeTexture = _runningTexture;
                _textureMapPosition = new Rectangle(0, 0, 19, 25);
            }
            else
            {
                _activeTexture = _idleTexture;
                _textureMapPosition = new Rectangle(0, 0, 19, 25);
            }
            
            arm.Update(gametime);
            flipped = arm.Flipped;
            running = false;
            


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.2)
            {
                animationFrame++;
                if (animationFrame > 3)
                {
                    animationFrame = 1;
                }
                animationTimer -= 0.2;
            }
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            _textureMapPosition = new Rectangle(animationFrame * 19, 0, 19,25);
            
            spriteBatch.Draw(_activeTexture, _position, _textureMapPosition, Color, 0.0f, new Vector2(9.5f, 0), 2.5f, spriteEffects, 0);
            int j = _healthRemaining;
            Vector2 k = new Vector2(20, 20);
            for(int i = 0; i < _healthMax/2; i++)
            {
                if(j>= 2)
                {
                    spriteBatch.Draw(_healthTexture, k, new Rectangle(0, 0, 36, 32), Color.White, 0.0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                    j -= 2;
                }
                else if(j ==1)
                {
                    spriteBatch.Draw(_healthTexture, k, new Rectangle(36, 0, 36, 32), Color.White, 0.0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                    j -= 1;
                }
                else
                {
                    spriteBatch.Draw(_healthTexture, k, new Rectangle(74, 0, 36, 32), Color.White, 0.0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                }
                k += new Vector2(60, 0);
            }
            arm.Draw(gameTime, spriteBatch);
        }

    }
}
