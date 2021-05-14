using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class BoxSprite : Sprite
    {
        private Texture2D _texture;

        private Rectangle _textureAtlasLocation;
        private bool _textureChoice;
        private Vector2 _position;
        private BoundingRectangle _bounds;

        public BoxSprite(Vector2 position,bool textureChoice)
        {
            _position = position;
            _textureChoice = textureChoice;
            _textureAtlasLocation = new Rectangle(0, 0, 16, 19);
            _bounds = new BoundingRectangle(position + new Vector2(-52,-58), 17 * 4, 19 * 4);
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return _bounds.CollidesWith(other);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position,_textureAtlasLocation, Color.White, 0.0f, new Vector2(8, 8), 4.0f, SpriteEffects.None, 1);
        }

        public override void LoadContent(ContentManager content)
        {
            if (_textureChoice)
            {
                _texture = content.Load<Texture2D>("Brown_Crates_1");
            }
            else
            {
                _texture = content.Load<Texture2D>("Brown_Crates_2");
            }
            
        }

        public override void Update(GameTime gametime)
        {
            
        }
    }
}
