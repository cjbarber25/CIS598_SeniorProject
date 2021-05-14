using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public class DoorSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private BoundingRectangle _bounds;
        public bool _activated;
        private Rectangle _textureMapPosition;
        private Tuple<int, int> _destination;
        

        public DoorSprite(Vector2 position,Tuple<int,int> destination)
        {
            _destination = destination;
            _position = position;
            _bounds = new BoundingRectangle(_position.X, _position.Y, 32, 32);
            _activated = true ;
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("scifi-tileset");
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return _bounds.CollidesWith(other);
        }

        public void ActivateDoor()
        {
            _activated = true;
        }
        public BoundingRectangle GetBounds()
        {
            return _bounds;
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public bool GoThrough(Player player, out Tuple<int,int> destination)
        {
            destination = _destination;
            if (this.CheckCollision(player.Bounds) && _activated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_activated)
            {
                _textureMapPosition = new Rectangle(96, 128, 32, 32);
            }
            else
            {
                _textureMapPosition = new Rectangle(96, 96, 32, 32);
            }
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0) ;
        }
    }
}
