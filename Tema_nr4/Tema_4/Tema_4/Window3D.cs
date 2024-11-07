using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Tema_4;

namespace OpenTK_immediate_mode
{
    class ImmediateMode : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;
        private Randomizer rando;
        private Camera camera;
        private bool GRAVITY = true;

        private bool AXES;
        private bool GRID;

        private List<Objectoid> rainofObjects;


        //defaults 
        private const int XYZ_SIZE = 300;
        private const int GRID_SIZE = 1000;


        public ImmediateMode() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;
            AXES = true;
            GRID = true;

            rando = new Randomizer();
            camera = new Camera();

            rainofObjects = new List<Objectoid>();
            

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "Melinte Alexandru";
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

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 10024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            camera.SetCamera();
        }

        /** Secțiunea pentru "game logic"/"business logic". Tot ce se execută în această secțiune va fi randat
            automat pe ecran în pasul următor - control utilizator, actualizarea poziției obiectelor, etc. */
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
            }
            if (keyboard[Key.D])
            {
                camera.MoveRight();
                camera.SetCamera();
            }
            if (keyboard[Key.A])
            {
                camera.MoveLeft();
                camera.SetCamera();
            }
            if (keyboard[Key.W])
            {
                camera.MoveForward();
                camera.SetCamera();

            }
            if (keyboard[Key.S])
            {
                camera.MoveBackward();
                camera.SetCamera();
            }
            if (keyboard[Key.Q])
            {
                camera.MoveUp();
                camera.SetCamera();
            }
            if (keyboard[Key.E])
            {
                camera.MoveDown();
                camera.SetCamera();
            }
            if (keyboard[Key.V] && !previousKeyboard[Key.V])
            {
                if (GRID == true)
                {
                    GRID = false;
                }
                else
                {
                    GRID = true;
                }
            }
            if (keyboard[Key.K] && !previousKeyboard[Key.K])
            {
                if (AXES == true)
                {
                    AXES = false;
                }
                else
                {
                    AXES = true;
                }
            }
            if (keyboard[Key.H] && !previousKeyboard[Key.H])
            {
                displayHelp();
            }


            // object spawn
            if (mouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                rainofObjects.Add(new Objectoid(GRAVITY));
            }
            // remove objects
            if (mouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
            {
                rainofObjects.Clear();
            }
            if (keyboard[Key.G] && !previousKeyboard[Key.G])
            {
                GRAVITY = !GRAVITY;
            }
            //update falling logic
            foreach (Objectoid obj in rainofObjects)
            {
                obj.UpdatePosition(GRAVITY);
            }

            previousMouse = mouse;
            previousKeyboard = keyboard;
        }

        /** Secțiunea pentru randarea scenei 3D. Controlată de modulul logic din metoda ONUPDATEFRAME().
            Parametrul de intrare "e" conține informatii de timing pentru randare. */
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            if (AXES == true) {
                DrawAxes();
            }
            if (GRID == true) {
                DrawGrid();
            }

            foreach(Objectoid obj in rainofObjects)
            {
                obj.Draw();
            }
            
            // Se lucrează în modul DOUBLE BUFFERED - câtă vreme se afișează o imagine randată, o alta se randează în background apoi cele 2 sunt schimbate...
            SwapBuffers();
        }


        private void DrawGrid()
        {
            GL.Color3(Color.White);
            for (int i = -500; i < 500; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Vertex3(-GRID_SIZE,0,i*10);
                GL.Vertex3(GRID_SIZE, 0, i * 10);
                GL.End();
            }
            for (int i = -500; i < 500; i++)
            {
                GL.Begin(PrimitiveType.Lines);
                GL.Vertex3(i*10, 0, -GRID_SIZE);
                GL.Vertex3(i * 10, 0, GRID_SIZE);
                GL.End();
            }
        }

        private void DrawAxes()
        {

            //GL.LineWidth(3.0f);

            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }





        private void displayHelp()
        {
            Console.WriteLine("         Meniu");
            Console.WriteLine("ESC - parasire aplicatie");
            Console.WriteLine("H - afisare help");
            Console.WriteLine("V - Schimba vizibilitate grid");
            Console.WriteLine("K - Schimba vizibilitate axe");
            Console.WriteLine("A S D W Q E - deplasare camera");
            Console.WriteLine("K - Schimba vizibilitate axe");
            Console.WriteLine("G - Controleaza gravitatia");
            Console.WriteLine("Click stanga - genereaza un cub afectat de gravitatie ");
            Console.WriteLine("Click dreapta - Sterge toate cuburile de pe ecran");

        }


        [STAThread]
        static void Main(string[] args)
        {
            using (ImmediateMode example = new ImmediateMode())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

}

