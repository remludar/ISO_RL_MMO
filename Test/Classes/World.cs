using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public static class World
    {
        public static int WIDTH = 128;
        public static int DEPTH = 128;

        public static void Generate(GraphicsDevice graphicsDevice)
        {
            TerrainManager.Generate(graphicsDevice);
        }
    }
}
