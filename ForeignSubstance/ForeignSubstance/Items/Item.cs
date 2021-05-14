using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Items
{
    public abstract class Item
    {
        private string _itemName;
        
        private Vector2 _position;
        private Player _player;
        private bool _displayed;

        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public abstract void PickUp(Player player);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
