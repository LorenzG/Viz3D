using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.Mesh
{
    public class AKT_MeshTopologyEdgeList
    {
        AKT_Mesh aKT_Mesh;

        public AKT_MeshTopologyEdgeList(AKT_Mesh aKT_Mesh)
        {
            this.aKT_Mesh = aKT_Mesh;
        }

        public AKT_MeshTopologyEdgeList()
        {
        }


        public void AddEdgeTopology(int s, int e)
        {
            if (Edges == null || Edges.Count() == 0) Edges = new AKT_MeshEdgePairs[] { new AKT_MeshEdgePairs(s, e) };
            else this.Edges = this.Edges.Concat(new AKT_MeshEdgePairs[] { new AKT_MeshEdgePairs(s, e) });
        }
        public IEnumerable<AKT_MeshEdgePairs> Edges { get; set; }

    }

    public class AKT_MeshEdgePairs : IEqualityComparer<AKT_MeshEdgePairs>, IEquatable<AKT_MeshEdgePairs>
    {
        public AKT_MeshEdgePairs(int s, int e)
        {
            this.S = s;
            this.E = e;
        }

        public int E { get; set; }

        public int S { get; set; }

        public static bool operator !=(AKT_MeshEdgePairs a, AKT_MeshEdgePairs b)
        {
            return !(a == b);
        }
        public static bool operator ==(AKT_MeshEdgePairs a, AKT_MeshEdgePairs b)
        {
            return a.Equals(b);
        }
        public override bool Equals(object x)
        {
            return Equals(this, x);
        }
        public new bool Equals(object x, object y)
        {
            return ((AKT_MeshEdgePairs)x).Equals((AKT_MeshEdgePairs)y);
        }
        public int GetHashCode(object obj)
        {
            return ((AKT_MeshEdgePairs)obj).GetHashCode();
        }
        public override int GetHashCode()
        {
            return GetHashCode(this);
        }
        public bool Equals(AKT_MeshEdgePairs other)
        {
            return Equals(this, other);
        }
        public bool Equals(AKT_MeshEdgePairs x, AKT_MeshEdgePairs y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            bool e = x.E == y.E || x.E == y.S;
            bool s = x.S == y.E || x.S == y.S;

            bool a = e && s;

            return e && s;
        }
        public int GetHashCode(AKT_MeshEdgePairs obj)
        {
            return (obj.E ^ obj.S) + (obj.S ^ obj.E);
        }

        public override string ToString()
        {
            return S + "->" + E;
        }

        public class EdgeComparer : IEqualityComparer<AKT_MeshEdgePairs>, IEquatable<AKT_MeshEdgePairs>
        {
            //public static bool operator !=(AKT_MeshEdgePairs a, AKT_MeshEdgePairs b)
            //{
            //    return !(a == b);
            //}
            //public static bool operator ==(AKT_MeshEdgePairs a, AKT_MeshEdgePairs b)
            //{
            //    return a.Equals(b);
            //}
            public override bool Equals(object x)
            {
                return Equals(this, x);
            }
            public new bool Equals(object x, object y)
            {
                return ((AKT_MeshEdgePairs)x).Equals((AKT_MeshEdgePairs)y);
            }
            public int GetHashCode(object obj)
            {
                return ((AKT_MeshEdgePairs)obj).GetHashCode();
            }
            public override int GetHashCode()
            {
                return GetHashCode(this);
            }
            public bool Equals(AKT_MeshEdgePairs other)
            {
                return Equals(this, other);
            }
            public bool Equals(AKT_MeshEdgePairs x, AKT_MeshEdgePairs y)
            {
                if (object.ReferenceEquals(x, y))
                    return true;

                // If one is null, but not both, return false.
                if (((object)x == null) || ((object)y == null))
                {
                    return false;
                }

                bool e = x.E == y.E || x.E == y.S;
                bool s = x.S == y.E || x.S == y.S;

                bool a = e && s;

                return a;
            }
            public int GetHashCode(AKT_MeshEdgePairs obj)
            {
                return (obj.E ^ obj.S) + (obj.S ^ obj.E);
            }


        }

    }
}
