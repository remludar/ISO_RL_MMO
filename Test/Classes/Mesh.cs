using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public class Mesh
    {
        private Vector3[,] _vertexPositions;
        private Color[,] _vertexColors;


        public Mesh(VertexPositionColor[] verts, int[] indices)
        {
            _vertexPositions = new Vector3[World.WIDTH, World.DEPTH];
            _vertexColors = new Color[World.WIDTH, World.DEPTH];
            var rnd = new Random();
            float[,] heightMap = TerrainManager.GetHeightMap();
            for (int x = 0; x < World.WIDTH; x++)
            {
                for (int z = 0; z < World.DEPTH; z++)
                {
                    float height = heightMap[x,z];
                    _vertexPositions[x, z] = new Vector3(x, height, -z);

                    if (rnd.Next(3) == 0)
                        _vertexColors[x, z] = Color.Green;
                    else
                        _vertexColors[x, z] = Color.DarkGray;

                    var temp = x * World.DEPTH + z;
                    verts[x * World.DEPTH + z].Position = _vertexPositions[x, z];
                    verts[x * World.DEPTH + z].Color = _vertexColors[x, z];
                }
            }

            int indexCount = 0;
            for (int x = 0; x < World.WIDTH-1; x++)
            {
                for (int z = 0; z < World.DEPTH-1; z++)
                {
                    indices[indexCount++] = (x + 0) * World.DEPTH + (z + 0);
                    indices[indexCount++] = (x + 0) * World.DEPTH + (z + 1);
                    indices[indexCount++] = (x + 1) * World.DEPTH + (z + 1);

                    indices[indexCount++] = (x + 1) * World.DEPTH + (z + 1);
                    indices[indexCount++] = (x + 1) * World.DEPTH + (z + 0);
                    indices[indexCount++] = (x + 0) * World.DEPTH + (z + 0);
                    

                }
            }
        }
    }
}
