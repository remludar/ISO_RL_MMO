using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public class Camera3D
    {
        public Vector3 Position { get; private set; }
        public Vector3 Up { get; private set; }
        public Vector3 Forward { get; private set; }
        public float MoveSpeed { get; private set; }
        public float RotateSpeed { get; private set; }

        public Matrix ProjectionMatrix { get; private set; }
        public Matrix ViewMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }

        private float _rotationAmount = 0;
        private float _pitchAmount = 0;

        public bool IsActive { get; set; }


        /// <summary>
        ///
        /// </summary>
        /// <param name="position">Position of the camera</param>
        /// <param name="forward">Direction which the camera looks in, equal to target - position</param>
        /// <param name="up">Up direction of the camera</param>
        public Camera3D(Vector3 position, Vector3 forward, Vector3 up, float moveSpeed, float rotateSpeed, bool isActive = false)
        {
            Position = position;
            Forward = forward;
            Up = up;
            MoveSpeed = moveSpeed;
            RotateSpeed = rotateSpeed;
            IsActive = isActive;

            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                                     MathHelper.ToRadians(45f),
                                     GameManager.self.GraphicsDevice.DisplayMode.AspectRatio,
                                     1f,
                                     1000f);
            ViewMatrix = Matrix.CreateLookAt(Position, Forward + Position, Up);
            WorldMatrix = Matrix.CreateWorld(
                          forward,
                          Vector3.Forward,
                          Vector3.Up);
        }

        public void Update(float deltaTime)
        {
            if (IsActive)
            {
                _HandleInput(deltaTime);
                ViewMatrix = Matrix.CreateLookAt(Position, Forward + Position, Up);
            }
        }

        /// <summary>
        /// Move forward with respect to camera
        /// </summary>
        /// <param name="amount"></param>
        public void Thrust(float amount)
        {
            Forward.Normalize();
            Position += Forward * amount;
        }

        /// <summary>
        /// Strafe left with respect to camera
        /// </summary>
        /// <param name="amount"></param>
        public void StrafeHorz(float amount)
        {
            var left = Vector3.Cross(Up, Forward);
            left.Normalize();
            Position += left * amount;
        }

        /// <summary>
        /// Strafe up with respect to camera
        /// </summary>
        /// <param name="amount"></param>
        public void StrafeVert(float amount)
        {
            Up.Normalize();
            Position += Up * amount;
        }

        /// <summary>
        /// Roll (around forward axis) clockwise with respect to camera
        /// TODO: Is it CW or CCW?
        /// </summary>
        /// <param name="amount">Angle in degrees</param>
        public void Roll(float amount)
        {
            Up.Normalize();
            var left = Vector3.Cross(Up, Forward);
            left.Normalize();

            Up = Vector3.Transform(Up, Matrix.CreateFromAxisAngle(Forward, MathHelper.ToRadians(amount)));
        }

        /// <summary>
        /// Yaw (turn/steer around up vector) to the left
        /// </summary>
        /// <param name="amount">Angle in degrees</param>
        public void Yaw(float amount)
        {
            Forward.Normalize();

            Forward = Vector3.Transform(Forward, Matrix.CreateFromAxisAngle(Up, MathHelper.ToRadians(amount)));
        }

        /// <summary>
        /// Pitch (around leftward axis) upward
        /// </summary>
        /// <param name="amount"></param>
        public void Pitch(float amount)
        {
            Forward.Normalize();
            var left = Vector3.Cross(Up, Forward);
            left.Normalize();

            Forward = Vector3.Transform(Forward, Matrix.CreateFromAxisAngle(left, MathHelper.ToRadians(amount)));
        }

        private void _HandleInput(float deltaTime)
        {
            //Keyboard move Direction
            if (InputManager.isKeyD)
            {
                StrafeHorz(-MoveSpeed * deltaTime);
            }

            if (InputManager.isKeyA)
            {
                StrafeHorz(MoveSpeed * deltaTime);
            }

            if (InputManager.isKeyW)
            {
                Thrust(MoveSpeed * deltaTime);
            }

            if (InputManager.isKeyS)
            {
                Thrust(-MoveSpeed * deltaTime);
            }

            if (InputManager.isKeyE)
            {
                StrafeVert(MoveSpeed * deltaTime);
            }

            if (InputManager.isKeyQ)
            {
                StrafeVert(-MoveSpeed * deltaTime);
            }

            //Mouse look move
            if (InputManager.mouseDelta != Vector2.Zero)
            {
                _pitchAmount = InputManager.mouseDelta.Y * RotateSpeed * deltaTime;
                _rotationAmount = -InputManager.mouseDelta.X * RotateSpeed * deltaTime;
            }
            else
            {
                _rotationAmount = 0;
                _pitchAmount = 0;
            }

            Yaw(_rotationAmount);
            Pitch(_pitchAmount);
        }

       
    }
}
