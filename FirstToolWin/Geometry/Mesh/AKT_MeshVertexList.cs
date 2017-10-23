using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.Mesh
{
    public class AKT_MeshVertexList : IEnumerable
    {
        private AKT_Mesh aKT_Mesh;

        AKT_Point3d[] _v;
        public AKT_Point3d[] Vertices
        {
            get { return _v; }
        }

        public AKT_MeshVertexList(AKT_Mesh aKT_Mesh)
        {
            this.aKT_Mesh = aKT_Mesh;
        }

        public AKT_MeshVertexList(IEnumerable<AKT_Point3d> vertices)
        {
            this._v = vertices.ToArray();
        }

        public void Add(object obj)
        {
            AKT_Point3d k = (AKT_Point3d)obj;
            if (k != null) AddPoints(k);
        }

        private void AddPoints(AKT_Point3d pnts)
        {
            AKT_Point3d[] p = new AKT_Point3d[] { pnts };
            if (_v == null || _v.Count() == 0) this._v = p;
            else _v = this._v.Concat(p).ToArray();
        }


        public AKT_Point3d this[int idx]
        {
            get { return this.Vertices[idx]; }
        }


        public IEnumerator GetEnumerator()
        {
            return (IEnumerator<AKT_Point3d>)this.Vertices.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Vertices.GetEnumerator();
        }
        public int Count
        {
            get { return Vertices.Length; }
        }
    }
}
