using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Rooms
{
    /// <summary>
    /// Abstract room class for the level builder to use.
    /// </summary>
    public abstract class Room
    {

        private Vector2 _position;
        private int _length;
        private int _width;
        public Basic _room;
        public List<Enemy> Enemies;

        /// <summary>
        /// Builds the rooms and assigns textures/bounds to proper locations.
        /// </summary>
        public abstract void Build(int length, int width, Vector2 position,Player player);
        public abstract bool CheckForOutOfBounds(BoundingRectangle playerBounds);

        public abstract void AddDoors(int[,] layout, Tuple<int,int> currentPosition);
        public abstract bool CheckDoorCollision(Player player, out Tuple<int,int> destination);
        public abstract void LoadContent(ContentManager content);

        public abstract void AddEnemy(Player player, Vector2 position);
        /// <summary>
        /// Updates any changes that need to be occurring within the textures of the room.
        /// </summary>
        /// <param name="gametime"></param>
        /// 
        public abstract void Update(GameTime gametime);

        /// <summary>
        /// Draws the room.
        /// </summary>
        /// <param name="gametime"></param>
        public abstract void Draw(GameTime gametime,SpriteBatch spriteBatch);
    }
}
