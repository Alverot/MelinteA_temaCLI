using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace Tema_nr5
{
    public class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private Axes xyz;
        private string FILEPATH = "assets/CUBE.txt";
        private Cube cube;

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "Melinte Alexandru";

            xyz = new Axes(200);
            cube = new Cube(FILEPATH);

            displayHelp();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
            }
            if (keyboard[Key.Y] && !previousKeyboard[Key.Y])
            {
                xyz.ToggleVisibility();
            }
            if (keyboard[Key.H] && !previousKeyboard[Key.H])
            {
                displayHelp();
            }
            if (keyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cube.ToggleVisibility();
            }
            if (keyboard[Key.O] && !previousKeyboard[Key.O])
            {
                cube.Sizepp();
            }
            if (keyboard[Key.P] && !previousKeyboard[Key.P])
            {
                cube.Sizemm();
            }
            if (keyboard[Key.R] && !previousKeyboard[Key.R])
            {
                cube.RndColor();
            }
            if (keyboard[Key.F] && !previousKeyboard[Key.F])
            {
                cube.FaceCycle();
            }
            if (keyboard[Key.G] && !previousKeyboard[Key.G])
            {
                cube.RGBCycle();
            }
            if (keyboard[Key.K])
            {   
                cube.DecreaseRGB();
            }
            if (keyboard[Key.J])
            {
                cube.IncreaseRGB();
            }

            previousMouse = mouse;
            previousKeyboard = keyboard;


        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            xyz.Draw();
            
            cube.DrawCube();

            SwapBuffers();
        }




        private void displayHelp()
        {
            Console.WriteLine("         Meniu");
            Console.WriteLine("ESC - parasire aplicatie");
            Console.WriteLine("H - afisare help");
            Console.WriteLine("Y - toglle axes");
            Console.WriteLine("C - toglle cube");
            Console.WriteLine("O - increase cube size");
            Console.WriteLine("P - decrease cube size");
            Console.WriteLine("R - Randomise the colors");
            Console.WriteLine("F - cycle through the cub faces");
            Console.WriteLine("G - cycle through the RGB values");
            Console.WriteLine("J - increases the selected RGB value");
            Console.WriteLine("K - decreases the selected RGB value");

        }

    }
}
