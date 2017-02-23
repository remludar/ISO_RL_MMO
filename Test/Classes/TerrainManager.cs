using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public static class TerrainManager
    {
        private static float[,] _heightMap;
        private static BasicEffect _basicEffect;
        private static VertexPositionColor[] _vertices;
        private static int[] _indices;
        private static Mesh _terrainMesh;
        private static VertexBuffer _vertexBuffer;
        private static IndexBuffer _indexBuffer;

        public static void Generate(GraphicsDevice graphicsDevice)
        {
            _GenerateHeightMap();
            
            _basicEffect = new BasicEffect(graphicsDevice);
            _basicEffect.Alpha = 1;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.LightingEnabled = false;

            _vertices = new VertexPositionColor[World.WIDTH * World.DEPTH];
            _indices = new int[(((World.WIDTH - 1) * (World.DEPTH - 1)) * 2) * 3];
            _terrainMesh = new Mesh(_vertices, _indices);

            _vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), World.WIDTH * World.DEPTH, BufferUsage.WriteOnly);
            _vertexBuffer.SetData<VertexPositionColor>(_vertices);

            _indexBuffer = new IndexBuffer(graphicsDevice, typeof(int), _indices.Length, BufferUsage.WriteOnly);
            _indexBuffer.SetData(_indices);
            
        }

        public static float[,] GetHeightMap()
        {
            return _heightMap;
        }

        public static void Draw(GraphicsDevice graphicsDevice)
        {
            _basicEffect.Projection = CameraManager.GetProjectionMatrix();
            _basicEffect.View = CameraManager.GetViewMatrix();
            _basicEffect.World = CameraManager.GetWorldMatrix();
            
            graphicsDevice.SetVertexBuffer(_vertexBuffer);
            graphicsDevice.Indices = _indexBuffer;

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                var triCount = ((World.WIDTH - 1) * (World.DEPTH - 1)) * 2;
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, triCount);
            }
        }

        private static void _GenerateHeightMap()
        {
            _heightMap = new float[World.WIDTH, World.DEPTH];
            var rnd = new Random();
            for (int x = 0; x < _heightMap.GetLength(0); x++)
            {
                for (int z = 0; z < _heightMap.GetLength(1); z++)
                {
                    _heightMap[x, z] = (float)rnd.NextDouble() * 0.5f;
                }
            }
        }

    }
}
