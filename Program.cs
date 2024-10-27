using System;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_console_sample01
{
    class SimpleWindow : GameWindow
    {
        // Coordonatele triunghiului
        private readonly float[] x = new float[3];
        private readonly float[] y = new float[3];

        // Culorile pentru fiecare varf
        private readonly float[] red = new float[3];
        private readonly float[] green = new float[3];
        private readonly float[] blue = new float[3];

        // Pentru rotirea cu mouse-ul
        private float unghi = 0.0f;
        private bool mouseApasat = false;
        private int mouseXVechi;

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            CitesteDateDinFisier("coordonate.txt");
            GenereazaCuloriRandom();
        }

        private void CitesteDateDinFisier(string numeFisier)
        {
            try
            {
                string caleFisier = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, numeFisier);

                string[] linii = File.ReadAllLines(caleFisier);

                for (int i = 0; i < 3; i++)
                {
                    string[] coordonate = linii[i].Split(',');
                    x[i] = float.Parse(coordonate[0]);
                    y[i] = float.Parse(coordonate[1]);
                }
                Console.WriteLine("Coordonate citite cu succes!");
            }
            catch
            {
                Console.WriteLine("Nu s-a putut citi fisierul, se folosesc coordonate implicite!");
                // Coordonate implicite pentru triunghi
                x[0] = -0.5f; y[0] = 0.288f;
                x[1] = 0.0f; y[1] = -0.577f;
                x[2] = 0.5f; y[2] = 0.288f;
            }
        }

        private void GenereazaCuloriRandom()
        {
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                red[i] = (float)rnd.NextDouble();
                green[i] = (float)rnd.NextDouble();
                blue[i] = (float)rnd.NextDouble();
            }
            AfiseazaCulori();
        }

        private void AfiseazaCulori()
        {
            Console.WriteLine("\nCulorile varfurilor:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Varf {i + 1}: R={red[i]:F2}, G={green[i]:F2}, B={blue[i]:F2}");
            }
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Exit();

            Random rnd = new Random();
            if (e.Key == Key.R)
            {
                for (int i = 0; i < 3; i++)
                    red[i] = (float)rnd.NextDouble();
            }
            else if (e.Key == Key.G)
            {
                for (int i = 0; i < 3; i++)
                    green[i] = (float)rnd.NextDouble();
            }
            else if (e.Key == Key.B)
            {
                for (int i = 0; i < 3; i++)
                    blue[i] = (float)rnd.NextDouble();
            }

            AfiseazaCulori();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (!mouseApasat)
                {
                    mouseApasat = true;
                    mouseXVechi = mouse.X;
                }
                else
                {
                    unghi += (mouse.X - mouseXVechi) * 0.01f;
                    mouseXVechi = mouse.X;
                }
            }
            else
            {
                mouseApasat = false;
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Rotate(unghi * 180 / Math.PI, 0, 1, 0);

            // Desenare triunghi normal
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 3; i++)
            {
                GL.Color3(red[i], green[i], blue[i]);
                GL.Vertex2(x[i], y[i]);
            }
            GL.End();

            // Desenare triunghi in mod strip (translatat)
            GL.Translate(0.5f, 0.5f, 0);
            GL.Begin(PrimitiveType.TriangleStrip);
            for (int i = 0; i < 3; i++)
            {
                GL.Color3(red[i], green[i], blue[i]);
                GL.Vertex2(x[i] * 0.5f, y[i] * 0.5f);
            }
            GL.End();

            SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow window = new SimpleWindow())
            {
                window.Run(30.0);
            }
        }
    }
}