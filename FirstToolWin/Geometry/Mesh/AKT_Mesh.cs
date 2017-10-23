using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstToolWin.Geometry;
using System.Runtime.Serialization;

namespace FirstToolWin.Geometry.Mesh
{
    public class AKT_Mesh 
    {
        private AKT_MeshVertexList m_vertices;

        private AKT_MeshTopologyVertexList m_topology_vertices;

        private AKT_MeshTopologyEdgeList m_topology_edges;

        private AKT_MeshFaceList m_faces;

        /// <summary>
        /// Gets access to the faces collection in this AKT_Mesh.
        /// </summary>
        /// <example>
        /// <code source="examples\vbnet\ex_addAKT_Mesh.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_addAKT_Mesh.cs" lang="cs" />
        /// <code source="examples\py\ex_addAKT_Mesh.py" lang="py" />
        /// </example>
        public AKT_MeshFaceList Faces
        {
            get
            {
                AKT_MeshFaceList mFaces = this.m_faces;
                if (mFaces == null)
                {
                    AKT_MeshFaceList AKT_MeshFaceLists = new AKT_MeshFaceList(this);
                    AKT_MeshFaceList AKT_MeshFaceLists1 = AKT_MeshFaceLists;
                    this.m_faces = AKT_MeshFaceLists;
                    mFaces = AKT_MeshFaceLists1;
                }
                return mFaces;
            }
            set { this.m_faces = value; }
        }

        /// <summary>
        /// Gets the <see cref="T:Rhino.Geometry.Collections.AKT_MeshTopologyEdgeList" /> object associated with this AKT_Mesh.
        /// <para>This object stores edge connectivity.</para>
        /// </summary>
        public AKT_MeshTopologyEdgeList TopologyEdges
        {
            get
            {
                AKT_MeshTopologyEdgeList mTopologyEdges = this.m_topology_edges;
                if (mTopologyEdges == null)
                {
                    AKT_MeshTopologyEdgeList AKT_MeshTopologyEdgeList = new AKT_MeshTopologyEdgeList(this);
                    AKT_MeshTopologyEdgeList AKT_MeshTopologyEdgeList1 = AKT_MeshTopologyEdgeList;
                    this.m_topology_edges = AKT_MeshTopologyEdgeList;
                    mTopologyEdges = AKT_MeshTopologyEdgeList1;
                }
                return mTopologyEdges;
            }
            set
            {
                this.m_topology_edges = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="T:Rhino.Geometry.Collections.AKT_MeshTopologyVertexList" /> object associated with this AKT_Mesh.
        /// <para>This object stores vertex connectivity and the indices of vertices
        /// that were unified while computing the edge topology.</para>
        /// </summary>
        public AKT_MeshTopologyVertexList TopologyVertices
        {
            get
            {
                AKT_MeshTopologyVertexList mTopologyVertices = this.m_topology_vertices;
                if (mTopologyVertices == null)
                {
                    AKT_MeshTopologyVertexList AKT_MeshTopologyVertexLists = new AKT_MeshTopologyVertexList(this);
                    AKT_MeshTopologyVertexList AKT_MeshTopologyVertexLists1 = AKT_MeshTopologyVertexLists;
                    this.m_topology_vertices = AKT_MeshTopologyVertexLists;
                    mTopologyVertices = AKT_MeshTopologyVertexLists1;
                }
                return mTopologyVertices;
            }
        }

        /// <summary>
        /// Gets access to the vertices set of this AKT_Mesh.
        /// </summary>
        /// <example>
        /// <code source="examples\vbnet\ex_addAKT_Mesh.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_addAKT_Mesh.cs" lang="cs" />
        /// <code source="examples\py\ex_addAKT_Mesh.py" lang="py" />
        /// </example>
        public AKT_MeshVertexList Vertices
        {
            get
            {
                AKT_MeshVertexList mVertices = this.m_vertices;
                if (mVertices == null)
                {
                    AKT_MeshVertexList AKT_MeshVertexLists = new AKT_MeshVertexList(this);
                    AKT_MeshVertexList AKT_MeshVertexLists1 = AKT_MeshVertexLists;
                    this.m_vertices = AKT_MeshVertexLists;
                    mVertices = AKT_MeshVertexLists1;
                }
                return mVertices;
            }
            set
            {
                this.m_vertices = value;
            }
        }

        /// <summary>Initializes a new empty AKT_Mesh.</summary>
        /// <example>
        /// <code source="examples\vbnet\ex_addAKT_Mesh.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_addAKT_Mesh.cs" lang="cs" />
        /// <code source="examples\py\ex_addAKT_Mesh.py" lang="py" />
        /// </example>
        public AKT_Mesh()
        {
        }

        public List<AKT_Polyline> NakedEdges()
        {
            //IEnumerable<AKT_Polyline> polys = TopologyEdges.Edges.Distinct()
            //    .Select(a => new AKT_Polyline(m_vertices[a.S], m_vertices[a.E]));

            //IEnumerable<AKT_Polyline> polys = TopologyEdges.Edges.GroupBy(a => a, new AKT_MeshEdgePairs.EdgeComparer())
            //    .Where(b => b.Count() == 1)
            //    .Select(a => new AKT_Polyline(m_vertices[a.First().S], m_vertices[a.First().E]));

            List<AKT_MeshEdgePairs> edges = new List<AKT_MeshEdgePairs>();

            foreach (AKT_MeshFace f in Faces.Faces)
            {
                edges.Add(new AKT_MeshEdgePairs(f.A, f.B));
                edges.Add(new AKT_MeshEdgePairs(f.B, f.C));

                if (f.MeshType == MeshTypology.Triangular)
                {
                    edges.Add(new AKT_MeshEdgePairs(f.C, f.A));

                }
                else
                {
                    edges.Add(new AKT_MeshEdgePairs(f.C, f.D));
                    edges.Add(new AKT_MeshEdgePairs(f.D, f.A));
                }
            }

            var polys1 = edges.GroupBy(a => a, new AKT_MeshEdgePairs.EdgeComparer());
            var polys2 = polys1.Where(b => b.Count() == 1);
            var polys3 = polys2.Select(a => new AKT_Polyline(m_vertices[a.First().S], m_vertices[a.First().E]));


            //var polys1Arr = polys1.ToArray();
            //var polys2Arr = polys2.ToArray();
            //var polys3Arr = polys3.ToArray();


            return AKT_Polyline.JoinPolylines(polys3);

            return polys3.ToList();

            AKT_Polyline[] polpo = polys3.ToArray();

            //List<List<AKT_MeshEdgePairs>> lll = new List<List<AKT_MeshEdgePairs>>();

            //foreach (var x in TopologyEdges.Edges)
            //{
            //    List<AKT_MeshEdgePairs> ll = new List<AKT_MeshEdgePairs>();

            //    foreach (var y in TopologyEdges.Edges)
            //    {
            //        bool e = x.E == y.E || x.E == y.S;
            //        bool s = x.S == y.E || x.S == y.S;

            //        bool a = e && s;

            //        if (a)
            //        {
            //            ll.Add(y);
            //        }

            //    }

            //    lll.Add(ll);

            //}

            return AKT_Polyline.JoinPolylines(polys3);











            AKT_Mesh mesh = this;
            List<AKT_Interval> intervals = new List<AKT_Interval>();

            bool[] nakedEdgePointStatus = mesh.GetNakedEdgePointStatus();
            int i = 0;
            while (true)
            {
                if (i >= mesh.m_faces.Count)
                {
                    break;
                }
                AKT_MeshFace face = mesh.m_faces[i];
                int a = face.A;
                int b = face.B;
                if ((!nakedEdgePointStatus[a] ? false : nakedEdgePointStatus[b]))
                {
                    if (intervals.IndexOf(new AKT_Interval((double)b, (double)a)) != -1)
                    {
                        intervals.Remove(new AKT_Interval((double)b, (double)a));
                    }
                    else if (intervals.IndexOf(new AKT_Interval((double)a, (double)b)) == -1)
                    {
                        intervals.Add(new AKT_Interval((double)a, (double)b));
                    }
                    else
                    {
                        intervals.Remove(new AKT_Interval((double)a, (double)b));
                    }
                }
                a = face.B;
                b = face.C;
                if ((!nakedEdgePointStatus[a] ? false : nakedEdgePointStatus[b]))
                {
                    if (intervals.IndexOf(new AKT_Interval((double)b, (double)a)) != -1)
                    {
                        intervals.Remove(new AKT_Interval((double)b, (double)a));
                    }
                    else if (intervals.IndexOf(new AKT_Interval((double)a, (double)b)) == -1)
                    {
                        intervals.Add(new AKT_Interval((double)a, (double)b));
                    }
                    else
                    {
                        intervals.Remove(new AKT_Interval((double)a, (double)b));
                    }
                }
                if (face.MeshType != MeshTypology.Triangular)
                {
                    a = face.C;
                    b = face.D;
                    if ((!nakedEdgePointStatus[a] ? false : nakedEdgePointStatus[b]))
                    {
                        if (intervals.IndexOf(new AKT_Interval((double)b, (double)a)) != -1)
                        {
                            intervals.Remove(new AKT_Interval((double)b, (double)a));
                        }
                        else if (intervals.IndexOf(new AKT_Interval((double)a, (double)b)) == -1)
                        {
                            intervals.Add(new AKT_Interval((double)a, (double)b));
                        }
                        else
                        {
                            intervals.Remove(new AKT_Interval((double)a, (double)b));
                        }
                    }
                    a = face.D;
                    b = face.A;
                    if ((!nakedEdgePointStatus[a] ? false : nakedEdgePointStatus[b]))
                    {
                        if (intervals.IndexOf(new AKT_Interval((double)b, (double)a)) != -1)
                        {
                            intervals.Remove(new AKT_Interval((double)b, (double)a));
                        }
                        else if (intervals.IndexOf(new AKT_Interval((double)a, (double)b)) == -1)
                        {
                            intervals.Add(new AKT_Interval((double)a, (double)b));
                        }
                        else
                        {
                            intervals.Remove(new AKT_Interval((double)a, (double)b));
                        }
                    }
                }
                else
                {
                    a = face.C;
                    b = face.A;
                    if ((!nakedEdgePointStatus[a] ? false : nakedEdgePointStatus[b]))
                    {
                        if (intervals.IndexOf(new AKT_Interval((double)b, (double)a)) != -1)
                        {
                            intervals.Remove(new AKT_Interval((double)b, (double)a));
                        }
                        else if (intervals.IndexOf(new AKT_Interval((double)a, (double)b)) == -1)
                        {
                            intervals.Add(new AKT_Interval((double)a, (double)b));
                        }
                        else
                        {
                            intervals.Remove(new AKT_Interval((double)a, (double)b));
                        }
                    }
                }
                i++;
            }
            if (intervals.Count > 0)
            {
                List<int> nums = new List<int>(intervals.Count);
                List<int> nums1 = new List<int>(intervals.Count);
                for (i = 0; i < intervals.Count; i++)
                {
                    AKT_Interval item = intervals[i];
                    nums.Add((int)item.T0);
                    item = intervals[i];
                    nums1.Add((int)item.T1);
                }
            }
            return null;
            //throw new NotImplementedException();
        }

        bool[] GetNakedEdgePointStatus()
        {
            throw new NotImplementedException();
        }

        public double Area
        {
            get { return Faces.Area(); }
        }
    }

    [Flags]
    public enum MeshTypology
    {
        Error = 0x00,
        Degenerate = 0x01,
        Triangular = 0x02,
        Quad = 0x04,
        Ngon = 0x08,
        //Esa = 0x16,        
    }
}
