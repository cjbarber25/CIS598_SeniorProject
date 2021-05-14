using ForeignSubstance.Rooms;
using ForeignSubstance.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public abstract class Enemy : Sprite
    {
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
        private int _damageValue;
        private int _healthMax;
        private int _healthRemaining;
        private Room _room;
        private Tuple<int, int> _roomPosition;
        enum States
        {
            idle,
            attacking,
            walking
        }
    }
}
