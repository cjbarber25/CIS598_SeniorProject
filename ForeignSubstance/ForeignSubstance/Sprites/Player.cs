using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ForeignSubstance.Items;

namespace ForeignSubstance.Sprites
{
    public class Player : Sprite
    {

        private Texture2D _texture;
        private Vector2 _position;
        private Inventory _inventory;
        private int _healthMax;
        private int _healthRemaining;
        private Rectangle _bounds;
        Rectangle _textureMapPosition;

        public Player(Vector2 position)
        {
            this._position = position;
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
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
        }

    }
}
