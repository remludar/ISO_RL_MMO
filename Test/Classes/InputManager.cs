using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO_RL_MM.Classes
{
    public static class InputManager
    {
        public static bool isKeyD = false;
        public static bool isKeyA = false;
        public static bool isKeyW = false;
        public static bool isKeyS = false;
        public static bool isKeyE = false;
        public static bool isKeyQ = false;
        public static bool isKeyUp = false;
        public static bool isKeyDown = false;
        public static bool isKeyRight = false;
        public static bool isKeyLeft = false;
        public static bool isKeyF1 = false;
        public static bool isKeyF2 = false;

        public static Vector2 mouseDelta = Vector2.Zero;

        static Vector2 mousePosition = Vector2.Zero;
        static int screenCenterX = GameManager.self.GraphicsDevice.Viewport.Width / 2;
        static int screenCenterY = GameManager.self.GraphicsDevice.Viewport.Height / 2;

        public static void Init()
        {
            Mouse.SetPosition(screenCenterX, screenCenterY);
        }

        public static void Update()
        {

            //Keyboard
            var keyboardState = Keyboard.GetState();
            isKeyD = keyboardState.IsKeyDown(Keys.D);
            isKeyA = keyboardState.IsKeyDown(Keys.A);              
            isKeyW = keyboardState.IsKeyDown(Keys.W);
            isKeyS = keyboardState.IsKeyDown(Keys.S);
            isKeyE = keyboardState.IsKeyDown(Keys.E);
            isKeyQ = keyboardState.IsKeyDown(Keys.Q);
            isKeyF1 = keyboardState.IsKeyDown(Keys.F1);
            isKeyF2 = keyboardState.IsKeyDown(Keys.F2);

            //Mouse          
            var mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);
            mouseDelta = mousePosition - new Vector2(screenCenterX, screenCenterY);
            if (mouseDelta != Vector2.Zero)
                Mouse.SetPosition(screenCenterX, screenCenterY);
        }
    }
}
