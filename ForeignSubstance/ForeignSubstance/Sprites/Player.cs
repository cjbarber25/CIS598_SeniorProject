using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ForeignSubstance.Items;

namespace ForeignSubstance.Sprites
{
    public class Player : Sprite
    {

        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _velocity;
        private Inventory _inventory;
        private int _healthMax;
        private int _healthRemaining;
        private Rectangle _bounds;
        Rectangle _textureMapPosition;
        private KeyboardState keyboardState;

        public Player(Vector2 position1)
        {
            this._position = position1;
            _inventory = new Inventory(5,this);
            _healthMax = 6;
            _healthRemaining = 6;
            _textureMapPosition = new Rectangle(32 * (5), 32 * (5), 32, 32);
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("scifi-tileset");
        }

        public override void Update(GameTime gametime)
        {
            keyboardState = Keyboard.GetState();

            Vector2 position = new Vector2(0, 0);
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                _position += new Vector2(0, -2);

            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                _position += new Vector2(0, 2);

            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                _position += new Vector2(-2, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                _position += new Vector2(2, 0);
            }
            _velocity = position - _position;
            //_position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            
        }

    }
}
