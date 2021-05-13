﻿using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ForeignSubstance.Sprites
{
    public class Bullet : Sprite
    {
        private float _fireRate;
        private double _timer;
        private Texture2D _texture;
        private float _velocity;
        private Vector2 _direction;
        private float _lifeSpan;
        private Vector2 _position;
        private Rectangle _textureMapPosition;
        private Player _player;
        public bool IsRemoved;
        public Bullet(Player player)
        {
            _player = player;
            _lifeSpan = 3.0f;
            _velocity = 5.0f;
            _textureMapPosition = new Rectangle(4,220,16,16);
            _direction = _player.Arm.Direction;
            _direction.Normalize();
            _position = _player.Arm.MuzzlePosition;
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(10, 8), 1.5f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("M484BulletCollection1");
        }

        public override void Update(GameTime gametime)
        {
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (_timer > _lifeSpan) IsRemoved = true;
            _position += _direction * _velocity;
        }
    }
}
