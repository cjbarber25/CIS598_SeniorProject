using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ForeignSubstance.Sprites
{
    public class WallSprite : Sprite
    {
        private List<BoundingRectangle> _bounds = new List<BoundingRectangle>();

        private Texture2D _texture;

        private (string,string,string) _type;

        private Vector2 _position;

        private Rectangle _textureMapPosition;

        public Vector2 Position => _position;

        public WallSprite(Vector2 position, (string, string, string) type)
        {
            this._position = position;
            this._type = type;
           

        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load <Texture2D>("scifi-tileset");
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            foreach (BoundingRectangle b in _bounds)
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
            //spriteBatch.Draw(_texture, _position, new Rectangle(0, 0, 32, 32),Color.White);
            if (_type == ("NORMAL", "LEFT", "WALL"))
            {
                _textureMapPosition = new Rectangle(32 * (0), 32 * (1), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add( new BoundingRectangle(_position.X, _position.Y, 10, 64));
            }
            else if (_type == ("TOP", "LEFT", "CORNER"))
            {
                _textureMapPosition = new Rectangle(32 * (0), 32 * (0), 32, 64);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(32 * (0), 32 * (3), 26, 32);
                spriteBatch.Draw(_texture, new Vector2(_position.X+12,_position.Y+13), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(6, 6, 26, 26);
                spriteBatch.Draw(_texture, new Vector2(_position.X + 12, _position.Y + 75), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X, _position.Y, 10, 128));
                _bounds.Add(new BoundingRectangle(_position.X, _position.Y, 64, 34));
            }
            else if (_type == ("BOTTOM", "LEFT", "CORNER"))
            {
                _textureMapPosition = new Rectangle(32 * (0), 32 * (2), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X, _position.Y, 10, 128));
                _bounds.Add(new BoundingRectangle(_position.X - 10, _position.Y + 20, 64, 10));
            }
            else if (_type == ("NORMAL", "RIGHT", "WALL"))
            {
                _textureMapPosition = new Rectangle(32 * (2), 32 * (1), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X + 32, _position.Y, 12, 64));
            }
            else if (_type == ("TOP", "RIGHT", "CORNER"))
            {
                _textureMapPosition = new Rectangle(32 * (2), 32 * (0), 32, 64);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(32 * (2)+6, 32 * (3), 26, 32);
                spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y + 13), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(6, 6, 26, 26);
                spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y + 75), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X+32, _position.Y, 12, 128));
                _bounds.Add(new BoundingRectangle(_position.X, _position.Y, 64, 34));
            }
            else if (_type == ("BOTTOM", "RIGHT", "CORNER"))
            {
                _textureMapPosition = new Rectangle(32 * (2), 32 * (2), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X + 32, _position.Y, 12, 128));
                _bounds.Add(new BoundingRectangle(_position.X - 10, _position.Y + 20, 64, 10));
            }
            else if (_type == ("TOP", "CENTER", "WALL"))
            {
                _textureMapPosition = new Rectangle(32 * (1), 32 * (0), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(32 * (1), 32 * (3), 32, 32);
                spriteBatch.Draw(_texture, new Vector2(_position.X,_position.Y+13), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(32, 6, 32, 26);
                spriteBatch.Draw(_texture, new Vector2(_position.X, _position.Y + 75), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X, _position.Y, 64, 34));


                /*
                _textureMapPosition = new Rectangle(32 * (1), 32 * (3) - 6, 32, 38);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                _textureMapPosition = new Rectangle(32 * (1), 32*(0)+6, 32, 26);
                spriteBatch.Draw(_texture, new Vector2(_position.X,_position.Y+74), _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
                */
            }
            else if (_type == ("BOTTOM", "CENTER", "WALL"))
            {
                _textureMapPosition = new Rectangle(32 * (1), 32 * (2), 32, 32);
                spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);


                _bounds.Add(new BoundingRectangle(_position.X-10, _position.Y+20, 64, 10));
            }
            else
            {

            }
            

        }

    }
}
