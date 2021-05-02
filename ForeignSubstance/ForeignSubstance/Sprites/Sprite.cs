﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ForeignSubstance.Sprites
{
    public abstract class Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(GameTime gametime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}