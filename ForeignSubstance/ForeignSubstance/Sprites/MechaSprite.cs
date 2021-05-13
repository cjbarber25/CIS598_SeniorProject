using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;

namespace ForeignSubstance.Sprites
{
    public class MechaSprite : Sprite
    {
        #region textures
        private Texture2D _textureActive;
        private Texture2D _textureIdle;
        private Texture2D _textureAttack;
        private Texture2D _textureWalking;
        private Texture2D _textureDamaged;
        #endregion textures

        private Vector2 _position;
        private Rectangle _sourceRect;
        private States State;
        private SpriteEffects _spriteEffects;
        private double timer;
        private double animationTimer;
        private short animationFrame = 0;
        private short animationFrameNum;
        private Player _player;
        private Vector2 _playerPosition;
        private bool stateChangeCurrent = false;
        private bool stateChangePrior = false;
        enum States
        {
            idle,
            attacking,
            walking,
            damaged
        }
        public MechaSprite(Vector2 spawnLocation, Player player)
        {
            _spriteEffects = SpriteEffects.None;
            _player = player;
            State = States.idle;
            _position = spawnLocation;
        }

        public override void LoadContent(ContentManager content)
        {
            _textureIdle = content.Load<Texture2D>("Mecha/idle");
            _textureAttack = content.Load<Texture2D>("Mecha/prep attack");
            _textureWalking = content.Load<Texture2D>("Mecha/walk");
            _textureDamaged = content.Load<Texture2D>("Mecha/damaged and death");
            _textureActive = _textureIdle;
        }

        public override void Update(GameTime gametime)
        {
            stateChangePrior = stateChangeCurrent;
            timer += gametime.ElapsedGameTime.TotalSeconds;
            _playerPosition = _player.Position;
            State = States.walking;
            switch (State)
            {
                case States.idle:
                    _textureActive = _textureIdle;
                    animationFrameNum = 3;
                    break;
                case States.walking:
                    Vector2 direction = _playerPosition - _position;
                    direction.Normalize();
                    _textureActive = _textureWalking;
                    animationFrameNum = 5;
                    if(timer > 2)
                    {
                        _position += direction * 200 * (float)gametime.ElapsedGameTime.TotalSeconds;
                        timer -= 2;
                    }
                    //State = States.attacking;
                    //stateChangeCurrent = true;
                    break;
                case States.attacking:

                    break;
                case States.damaged:

                    break;
            }
            if (stateChangeCurrent && !stateChangePrior)
            {
                animationFrame = 0;
            }
            stateChangeCurrent = false;
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.3f)
            {
                animationFrame++;
                if (animationFrame > animationFrameNum)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.3f;
            }
            _sourceRect = new Rectangle(0, animationFrame * 60, 135, 60);
            spriteBatch.Draw(_textureActive, _position, _sourceRect, Color.White, 0.0f, new Vector2(67.5f, 30), 2.5f, _spriteEffects, 0);
        }

    }
}
