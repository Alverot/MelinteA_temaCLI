using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Tema_nr5
{
    public class Axes
    {
        private bool visibility;
        private int xyzSize;

        //constructors
        public Axes() {
            xyzSize = 100;
            visibility = true;
        }

        public Axes(int size)
        { 
            xyzSize = size;
            visibility = true;
        }

        //metodes
        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public bool GetVisibility() { return visibility; }

        public void Draw()
        {
            if(visibility)
            {
                GL.LineWidth(3.0f);

                // Desenează axa Ox (cu roșu).
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Red);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(75, 0, 0);
                GL.End();

                // Desenează axa Oy (cu galben).
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Green);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 75, 0); ;
                GL.End();

                // Desenează axa Oz (cu verde).
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(Color.Blue);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 0, 75);
                GL.End();
            }
        }



    }
}
