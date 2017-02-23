using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public static class CameraManager
    {

        public enum TYPE { DEBUG, GAME };

        private static Camera3D _debugCamera;
        private static Camera3D _gameCamera;
        private static Camera3D _activeCamera;
        private static Dictionary<TYPE, Camera3D> _cameraDictionary = new Dictionary<TYPE, Camera3D>();
        private static VertexBuffer _vertexBuffer;
        private static IndexBuffer _indexBuffer;
        private static BasicEffect _basicEffect;

        public static void Init()
        {
            //Debug Camera
            var position = new Vector3(((float)World.WIDTH / 2f) / 2f, 1, -20f);
            var moveSpeed = 100.0f;
            var rotateSpeed = 10f;
            _debugCamera = new Camera3D(position, Vector3.Forward,  Vector3.Up, moveSpeed, rotateSpeed, true);

            //Game Camera
            position = new Vector3(((float)World.WIDTH / 2f) / 2f, 10, -20f);
            moveSpeed = 10.0f;
            rotateSpeed = 10f;
            _gameCamera = new Camera3D(position, Vector3.Forward, Vector3.Up, moveSpeed, rotateSpeed);

            //Store Cameras
            _cameraDictionary.Add(TYPE.DEBUG, _debugCamera);
            _cameraDictionary.Add(TYPE.GAME, _gameCamera);

            //Default to Debug Camera
            _activeCamera = _debugCamera;
        }

        public static void Update(float deltaTime)
        {
            
            _HandleInput(deltaTime);
            foreach (KeyValuePair<TYPE, Camera3D> kvp in _cameraDictionary)
            {
                kvp.Value.Update(deltaTime);
            }

            //Build vertex buffer here for drawing camera shapes.
        }

        public static Matrix GetProjectionMatrix()
        {
            return _activeCamera.ProjectionMatrix;
        }

        public static Matrix GetViewMatrix()
        {
            return _activeCamera.ViewMatrix;
        }

        public static Matrix GetWorldMatrix()
        {
            return _activeCamera.WorldMatrix;
        }

        public static void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetVertexBuffer(_vertexBuffer);
            graphicsDevice.Indices = _indexBuffer;

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                var triCount = ((World.WIDTH - 1) * (World.DEPTH - 1)) * 2;
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, triCount);
            }
        }

        private static void _HandleInput(float deltaTime)
        {
            //Camera Switching
            if (InputManager.isKeyF1)
            {
                _Activate(TYPE.DEBUG);
            }
            if (InputManager.isKeyF2)
            {
                _Activate(TYPE.GAME);
            }
        }

        
        private static void _Activate(TYPE type)
        {
            foreach (KeyValuePair<TYPE, Camera3D> kvp in _cameraDictionary)
            {
                if (kvp.Key != type)
                    kvp.Value.IsActive = false;
                else
                {
                    kvp.Value.IsActive = true;
                    _activeCamera = kvp.Value;
                }
            }
        }
    }
}
