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
        //private static float _rotationAmount = 0;
        //private static float _pitchAmount = 0;
        private static Dictionary<TYPE, Camera3D> _cameraDictionary = new Dictionary<TYPE, Camera3D>();

        public static void Init()
        {
            //Debug Camera
            var position = new Vector3(((float)World.WIDTH / 2f) / 2f, 1, -20f);
            var moveSpeed = 30.0f;
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
        }

        public static void Draw(BasicEffect basicEffect)
        {
            basicEffect.Projection = _activeCamera.ProjectionMatrix;
            basicEffect.View = _activeCamera.ViewMatrix;
            basicEffect.World = _activeCamera.WorldMatrix;
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
