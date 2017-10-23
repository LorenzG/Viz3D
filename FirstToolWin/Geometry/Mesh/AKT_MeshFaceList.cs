using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.Mesh
{
    public class AKT_MeshFaceList : IEnumerable
    {
        private AKT_Mesh aKT_Mesh;

        public AKT_MeshFaceList(AKT_Mesh aKT_Mesh)
        {
            _faces = new List<AKT_MeshFace>();
            this.aKT_Mesh = aKT_Mesh;
        }

        //public AKT_MeshFaceList()
        //{
        //}

        List<AKT_MeshFace> _faces;
        public AKT_MeshFace[] Faces 
        {
            get { return this._faces.ToArray(); } 
        }
        public void AddFaces(AKT_MeshFace face)
        {
            this._faces.Add(face);
        }
        public void AddFaces(IEnumerable<AKT_MeshFace> faces)
        {
            this._faces.AddRange(faces);
        }
        
        public int Count
        {
            get { return this.Faces.Length; }
        }
        public AKT_MeshFace this[int idx]
        {
            get { return this.Faces[idx]; }
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)Faces.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Faces.GetEnumerator();
        }

        public double Area()
        {
            double areaSum = 0;

            foreach (AKT_MeshFace f in Faces)
            {
                AKT_Point3d a = aKT_Mesh.Vertices[f.A];
                AKT_Point3d b = aKT_Mesh.Vertices[f.B];
                AKT_Point3d c = aKT_Mesh.Vertices[f.C];
                AKT_Point3d d = aKT_Mesh.Vertices[f.D];

                AKT_Vector3d ab = a - b;
                AKT_Vector3d bc = b - c;

                double area = AKT_Vector3d.CrossProduct(ab, bc).Length;

                if (f.MeshType == MeshTypology.Triangular)
                    area /= 2;

                areaSum += area;
            }

            return areaSum; 
        }
    }
}
