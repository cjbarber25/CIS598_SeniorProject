using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Rooms
{
    public class Hallway : Room
    {
        private Sprite[,] _sprites;
        private bool _vertical;

        public Hallway(bool direction)
        {
            _vertical = direction;
        }

        public override void Build(int length, int width, Vector2 position)
        {
            throw new NotImplementedException();
        }

        public override bool CheckForOutOfBounds(BoundingRectangle playerBounds)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
