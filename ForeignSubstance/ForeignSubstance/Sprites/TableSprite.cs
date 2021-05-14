using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class TableSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _tableAtlasLocation;
        private Rectangle _bottomBenchAtlasLocation;
        private Rectangle _topBenchAtlasLocation;
        private List<BoundingRectangle> _bounds;
        private float _scale;

        public TableSprite(Vector2 position)
        {
            int scale = 4;
            _scale = 4.0f;
            _position = position;
            _tableAtlasLocation = new Rectangle(57, 304, 29, 25);
            _bottomBenchAtlasLocation = new Rectangle(153, 308, 29, 13);
            _topBenchAtlasLocation = new Rectangle(153, 308, 29, 5);
            _bounds = new List<BoundingRectangle>();

            _bounds.Add(new BoundingRectangle(position+new Vector2(-50,-57), 29*scale, 37*scale));
            
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            foreach(BoundingRectangle b in _bounds)
            {
                if (b.CollidesWith(other))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.Gray;
            spriteBatch.Draw(_texture, _position, _topBenchAtlasLocation, color, 0.0f, new Vector2(8, 8), _scale, SpriteEffects.None, 1);
            spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y+20), _tableAtlasLocation, color, 0.0f, new Vector2(8, 8), _scale, SpriteEffects.None, 0);
            spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y+100), _bottomBenchAtlasLocation, color, 0.0f, new Vector2(8, 8), _scale, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("12_Kitchen_16x16");
        }

        public override void Update(GameTime gametime)
        {
            
        }
    }
}
