using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class ShopSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private BoundingRectangle _bounds;
        private Rectangle _textureMapPosition;
        

        public ShopSprite(Vector2 position)
        {
            this._position = position;
            _bounds = new BoundingRectangle(_position, 80, 132);
        }
        
        public override bool CheckCollision(BoundingRectangle other)
        {
           return _bounds.CollidesWith(other);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _textureMapPosition = new Rectangle(101, 681, 20, 33);
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 4.0f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            this._texture = content.Load<Texture2D>("19_Hospital_16x16");
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
