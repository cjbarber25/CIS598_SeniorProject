using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;
using ForeignSubstance.Screens;

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
        private bool firingCurrent = false;
        private bool firingPrior = false;
        private int firingCounter = 69;
        private List<Bullet> bullets;
        private Vector2 _direction;
        private ContentManager _content;
        private Vector2 _muzzlePosition;
        public GameplayScreen gameScreen;

        public Vector2 MuzzlePosition => _muzzlePosition;
        public Vector2 Direction => _direction;
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
            _muzzlePosition = _position + new Vector2(75,0);
            bullets = new List<Bullet>();
        }

        public override void LoadContent(ContentManager content)
        {
            _content = content;
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
            _direction = _playerPosition - _position;
            _direction.Normalize();
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
                    if(_direction.X < 0)
                    {
                        flipped = true;
                    }
                    else
                    {
                        flipped = false;
                    }
                    _position += _direction * 20 * (float)gametime.ElapsedGameTime.TotalSeconds;
                    if(flipped) _muzzlePosition = _position + new Vector2(-75, 0);
                    else _muzzlePosition = _position + new Vector2(75, 0);
                    if (timer > 2)
                    {
                        State = States.attacking;
                        firingCounter = 69;
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
                    firingPrior = firingCurrent;
                    if (firingCounter > 0) firingCounter--;
                    if (timer >= 0.6f && timer < 0.7f && firingCounter%2 == 0 && !firingPrior) //last frame of attack = fire
                    {
                        firingCurrent = true;
                        var bullet = new Bullet(this, new Rectangle(4, 220, 16, 16));
                        bullets.Add(bullet);
                        bullet.LoadContent(_content);
                    }
                    else firingCurrent = false;
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
            if (bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    bullet.Update(gametime);
                }
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
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].IsRemoved)
                {
                    bullets[i] = null;
                    bullets.RemoveAt(i);
                }
            }
            if (bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    bullet.Draw(gameTime, spriteBatch);
                }
            }
        }

    }
}
