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
        private bool flipped = false;
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
            Vector2 direction = _playerPosition - _position;
            direction.Normalize();
            switch (State)
            {
                case States.idle:
                    _textureActive = _textureIdle;
                    animationFrameNum = 3;
                    if(timer > 1)
                    {
                        State = States.walking;
                        stateChangeCurrent = true;
                        timer -= 1;
                    }
                    if (stateChangeCurrent && !stateChangePrior)
                    {
                        animationFrame = 0;
                    }
                    break;
                case States.walking:
                    _textureActive = _textureWalking;
                    animationFrameNum = 5;
                    if(direction.X < 0)
                    {
                        flipped = true;
                    }
                    else
                    {
                        flipped = false;
                    }
                    _position += direction * 20 * (float)gametime.ElapsedGameTime.TotalSeconds;
                    if(timer > 2)
                    {
                        State = States.attacking;
                        stateChangeCurrent = true;
                        timer -= 2;
                    }
                    if (stateChangeCurrent && !stateChangePrior)
                    {
                        animationFrame = 0;
                    }
                    break;
                case States.attacking:
                    _textureActive = _textureAttack;
                    animationFrameNum = 2;


                    if(timer > 0.9f)
                    {
                        State = States.walking;
                        stateChangeCurrent = true;
                        timer -= 0.9f;
                    }
                    if (stateChangeCurrent && !stateChangePrior)
                    {
                        animationFrame = 0;
                    }
                    break;
                case States.damaged:

                    break;
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
            if (flipped) _spriteEffects = SpriteEffects.FlipHorizontally;
            else _spriteEffects = SpriteEffects.None;
            _sourceRect = new Rectangle(0, animationFrame * 60, 75, 60);
            spriteBatch.Draw(_textureActive, _position, _sourceRect, Color.White, 0.0f, new Vector2(37.5f, 30), 2.5f, _spriteEffects, 0);
        }

    }
}
