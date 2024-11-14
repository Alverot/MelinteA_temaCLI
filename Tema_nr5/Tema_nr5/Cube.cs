using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;
using System.IO;
using OpenTK.Graphics;



namespace Tema_nr5
{
    internal class Cube
    {

        private bool visibility;
        private int size;
        private List<Vector3> coordList;
        private int OFFSET = 5;
        private int OFFSET_HIGHT = 3;
        private int RGBChange;
        private int FaceChange;
        Randomizer randomizer = new Randomizer();

        private Color[] colorVertices = { Color.White, Color.LawnGreen, Color.WhiteSmoke, Color.Tomato, Color.Turquoise, Color.OldLace, Color.Olive, Color.MidnightBlue, Color.PowderBlue, Color.PeachPuff, Color.LavenderBlush, Color.MediumAquamarine };

        public Cube(string filepath) { 
            visibility = true;
            size = 5;
            RGBChange = 1;
            Console.WriteLine("R");
            FaceChange = 1;
            Console.WriteLine("Face 1");
            coordList = new List<Vector3>();
            foreach(string line in File.ReadLines(filepath))
            {
                string[] parts = line.Split(new[] {" "},StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(parts[0],out int x);
                int.TryParse(parts[1], out int y);
                int.TryParse(parts[2], out int z);
                coordList.Add(new Vector3(x, y, z));
            }

        }
        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void RGBCycle()
        {
            if (RGBChange < 3)
            {
                RGBChange++;
            }
            else
            {
                RGBChange = 1;
            }
            if(RGBChange == 1)
            {
                Console.WriteLine("Changing R");
            }
            if (RGBChange == 2)
            {
                Console.WriteLine("Changing G");
            }
            if (RGBChange == 3)
            {
                Console.WriteLine("Changing B");
            }
        }
        public void FaceCycle()
        {
            if (FaceChange < 6)
            {
                FaceChange++;
            }
            else
            {
                FaceChange = 1;
            }
            Console.WriteLine("Face "+ FaceChange);
        }

        public void IncreaseRGB()
        {
            if(RGBChange == 1)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;

                getR = CheckRGBInterval(getR);
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange]= c;
            }
            if (RGBChange == 2)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;
                getG = CheckRGBInterval(getG);
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange] = c;
            }
            if (RGBChange == 3)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;
                getB = CheckRGBInterval(getB);
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange] = c;
            }
        }
        private int CheckRGBInterval(int nr)
        {
            if (nr + 5 < 255)
            {
                nr = nr + 5;
                return nr;
            }
            return 0;
        }
        private int CheckRGBIntervalM(int nr)
        {
            if (nr - 5 > 0)
            {
                nr =nr - 5;
                return nr;
            }
            return 255;
        }
        public void DecreaseRGB()
        {
            if (RGBChange == 1)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;
                getR = CheckRGBIntervalM(getR); 
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange] = c;
            }
            if (RGBChange == 2)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;
                getG = CheckRGBIntervalM(getG);
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange] = c;
            }
            if (RGBChange == 3)
            {
                int getR = colorVertices[FaceChange].R;
                int getG = colorVertices[FaceChange].G;
                int getB = colorVertices[FaceChange].B;
                getB = CheckRGBIntervalM(getB);
                Color c = Color.FromArgb(getR, getG, getB);
                colorVertices[FaceChange] = c;
            }
            
        }



        public void Sizepp()
        {
            size++;
        }
        public void Sizemm()
        {
            if (size - 1 > 0)
                size--;
        }


        public void RndColor()
        {
             for(int i = 0; i<  colorVertices.Length ; i++)
            {
                colorVertices[i] = randomizer.GetRandomColor();
            }
        }


        public void DrawCube()
        {
            if (visibility)
            {
                int i = 4;
                GL.Begin(PrimitiveType.Quads);
                foreach (Vector3 v in coordList)
                {
                    GL.Color3(colorVertices[i/4]);
                    GL.Vertex3(v.X*size + OFFSET,v.Y * size + OFFSET_HIGHT, v.Z * size + OFFSET);
                    i++;
                }
                GL.End();
                GL.End();
            }
        }


    }
}
