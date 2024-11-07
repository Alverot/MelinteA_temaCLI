using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Tema_4
{
    internal class Camera
    {
        private Vector3 loc;
        private Vector3 target;
        private Vector3 up_vector;

        private const int MOVEMENT_UNIT = 1;
        public Camera() { 
            loc = new Vector3(110,60,30);
            target = new Vector3(0, 0, 0);
            up_vector = new Vector3(0, 1, 0);
            
        }

        //maybe 2 other constructors later
        public void SetCamera()
        {
            Matrix4 lookat = Matrix4.LookAt(loc,target,up_vector);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        public void MoveRight()
        {
            loc.X = loc.X + MOVEMENT_UNIT;
            loc.Z = loc.Z - MOVEMENT_UNIT;
            target.X = target.X + MOVEMENT_UNIT;
            target.Z = target.Z - MOVEMENT_UNIT;
        }
        public void MoveForward()
        {
            loc.X = loc.X - MOVEMENT_UNIT;
            loc.Z = loc.Z - MOVEMENT_UNIT;
            target.X = target.X - MOVEMENT_UNIT;
            target.Z = target.Z - MOVEMENT_UNIT;
        }
        public void MoveLeft()
        {
            loc.X = loc.X - MOVEMENT_UNIT;
            loc.Z = loc.Z + MOVEMENT_UNIT;
            target.X = target.X - MOVEMENT_UNIT;
            target.Z = target.Z + MOVEMENT_UNIT;
        }
        public void MoveBackward()
        {
            loc.X = loc.X + MOVEMENT_UNIT;
            loc.Z = loc.Z + MOVEMENT_UNIT;
            target.X = target.X + MOVEMENT_UNIT;
            target.Z = target.Z + MOVEMENT_UNIT;
        }
        public void MoveUp()
        {
            loc.Y = loc.Y + MOVEMENT_UNIT;
            target.Y = target.Y + MOVEMENT_UNIT;
        }
        public void MoveDown()
        {
            loc.Y = loc.Y - MOVEMENT_UNIT;
            target.Y = target.Y - MOVEMENT_UNIT;
        }

    }
}
