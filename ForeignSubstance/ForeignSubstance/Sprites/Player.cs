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
            _bounds = new BoundingRectangle(_position.X, _position.Y, 19, 25);
            arm = new ArmSprite(this);
        }

        public override void LoadContent(ContentManager content)
        {
            _idleTexture = content.Load<Texture2D>("Scifi Character/idle");
            _runningTexture = content.Load<Texture2D>("Scifi Character/run");
            _activeTexture = _idleTexture;
            arm.LoadContent(content);
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
            
            Vector2 position = _position;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {

                position += new Vector2(0, -2);
                running = true;
                

            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 2);
                running = true;
                

            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-2, 0);
                running = true;
                
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(2, 0);
                running = true;
                
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
            _velocity = position - _position;

            _bounds.Y = position.Y;
            _bounds.X = position.X;
            if (!gameScreen.CheckCollision(_bounds))
            {
                _position = position;
                
            }
            else
            {
                _bounds.X = _position.X;
                _bounds.Y = _position.Y;
            }


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
            spriteBatch.Draw(_activeTexture, _position, _textureMapPosition, Color, 0.0f, new Vector2(0, 0), 2.5f, spriteEffects, 0);
            arm.Draw(gameTime, spriteBatch);
        }

    }
}
