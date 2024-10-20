using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_console_sample01
{
    class SimpleWindow : GameWindow
    {
        private Vector2 trianglePosition = Vector2.Zero;
        private float triangleSize = 0.5f;

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            if (e.Key == Key.Up)
                trianglePosition.Y += 0.1f;
            if (e.Key == Key.Down)
                trianglePosition.Y -= 0.1f;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
            CursorVisible = true;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var mouse = Mouse.GetState();

            // Update triangle position based on mouse position
            trianglePosition.X = (mouse.X / (float)Width) * 2 - 1;
            trianglePosition.Y = -((mouse.Y / (float)Height) * 2 - 1);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.MidnightBlue);
            GL.Vertex2(trianglePosition.X - triangleSize / 2, trianglePosition.Y + triangleSize / 2);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(trianglePosition.X, trianglePosition.Y - triangleSize / 2);
            GL.Color3(Color.Ivory);
            GL.Vertex2(trianglePosition.X + triangleSize / 2, trianglePosition.Y + triangleSize / 2);

            GL.End();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}