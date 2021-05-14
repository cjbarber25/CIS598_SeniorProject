using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForeignSubstance.Screens;
namespace ForeignSubstance.Sprites
{
    public class Bullet : Sprite
    {

        private double _timer;
        private Texture2D _texture;
        private float _velocity;
        private Vector2 _direction;
        private float _lifeSpan;
        private Vector2 _position;
        private Rectangle _textureMapPosition;
        private Player _player;
        private MechaSprite _mecha;
        private bool _isRemoved;
        private float _scale;
        private BoundingRectangle _bounds;
        public GameplayScreen gameScreen;

        public Vector2 Direction { get { return _direction; } set { _direction = value; } }
        public bool IsRemoved { get { return _isRemoved; } set { _isRemoved = value; } }
        public Bullet(Player player, Rectangle textureMapPosition)
        {
            _player = player;
            gameScreen = _player.gameScreen;
            _lifeSpan = 3.0f;
            _velocity = 7.0f;
            _textureMapPosition = textureMapPosition;
            _direction.Normalize();
            _position = _player.Arm.MuzzlePosition;
            _scale = 1.5f;
            _bounds = new BoundingRectangle(_position.X+5,_position.Y,1 ,1);
            _isRemoved = true;
        }
        public Bullet(MechaSprite shooter, Rectangle textureMapPosition)
        {
            _mecha = shooter;
            _lifeSpan = 3.0f;
            _velocity = 5.0f;
            _textureMapPosition = textureMapPosition;
            _direction = _mecha.Direction;
            _position = _mecha.MuzzlePosition;
            _scale = 2f;
            _bounds = new BoundingRectangle(0, 0, 16, 16);
            _isRemoved = false;
        }

        public void BulletReset(Bullet bullet, bool modified)
        {
            if(!modified) _direction = _player.Arm.Direction;
            _direction.Normalize();
            _position = _player.Arm.MuzzlePosition;
            _bounds = new BoundingRectangle(_position.X+5, _position.Y, 1, 1);
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            if (_bounds.CollidesWith(other))
            {
                return true;
            }
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!this.IsRemoved) spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(8, 8), _scale, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("M484BulletCollection1");
        }

        public override void Update(GameTime gametime)
        {
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (_timer > _lifeSpan) _isRemoved = true;
            _timer -= _lifeSpan;
            _position += _direction * _velocity;
            _bounds.X = (_position.X - 8 * _scale)+5;
            _bounds.Y = (_position.Y - 8 * _scale);
            if (gameScreen.CheckCollision(_bounds)) _isRemoved = true;
        }
    }
}
