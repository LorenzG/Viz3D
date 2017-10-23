using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.Mesh
{
    public class AKT_MeshFace
    {
        int a;
        int b ;
        int c ;
        int d;


        public int A { get {return a;} }
        public int B { get {return b;} }
        public int C { get {return c;} }
        public int D { get { return d; } }

        public AKT_MeshFace(int a, int b, int c, int d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            MeshType = MeshTypology.Quad;
        }

        public AKT_MeshFace(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            MeshType = MeshTypology.Triangular;
        }

        public MeshTypology MeshType 
        { get; private set; }
    }
}
