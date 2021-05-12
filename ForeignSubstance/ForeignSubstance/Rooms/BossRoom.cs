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
    public class BossRoom : Room
    {
        private Sprite[,] _sprites;

        public override void Build(int length, int width, Vector2 position)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].LoadContent(content);
                }
            }
        }

        public override bool CheckForOutOfBounds(BoundingRectangle playerBounds)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j].CheckCollision(playerBounds))
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public override void Update(GameTime gametime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}

