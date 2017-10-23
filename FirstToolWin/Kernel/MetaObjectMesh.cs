using FirstToolWin.Geometry.Mesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using FirstToolWin.Utilities;

namespace FirstToolWin.Kernel
{
    public abstract class MetaObjectMesh<T> : MetaObject<AKT_Mesh>
        where T : MetaObjectPoint3d
    {
        protected List<T> pnts;
        protected List<List<int>> connectivity;

        public MetaObjectMesh()
            : base()
        {

        }

        protected override void ReadExpression(Expression exp)
        {
            pnts = new List<T>();

            System.Reflection.PropertyInfo mi = this.GetType().GetProperty("Nodes");
            if (mi != null
                && mi.DeclaringType == this.GetType())
            {
                ReadNodes();
            }
            else
            {
                ReadNode_Names();
            }

            Logger.Log(this, "creating connectivity");
            connectivity = new List<List<int>>();

            foreach (IEnumerable<int> idxs in Face_Names)
            {
                List<int> l = new List<int>();
                connectivity.Add(l);

                foreach (int idx in idxs)
                {
                    l.Add(idx);
                }

                Logger.Log(this, "MetaObjectMesh - Point added: " + l.Select(a => a.ToString()).Aggregate<string>((a, b) => a + ", " + b));
            }
        }

        void ReadNodes()
        {
            pnts.AddRange(Nodes);
        }

        void ReadNode_Names()
        {
            Logger.Log(this, "MetaObjectMesh - iterating");
            foreach (string pName in Node_Names)
            {
                Logger.Log(this, "MetaObjectMesh - iter:" + pName);

                Expression pExp = FindChildren(pName);
                Logger.Log(this, "MetaObjectMesh - Got Children");

                if (pExp == null || !pExp.IsValidValue)
                    continue;

                T p0 = MetaObjectPoint3d.CreateAndRun<T>(pExp);
                Logger.Log(this, "MetaObjectMesh - p is null: " + (p0 == null));
                Logger.Log(this, "MetaObjectMesh - GotExpression");

                if (p0 != null && p0.IsValid())
                {
                    Logger.Log(this, "MetaObjectMesh - Got Point " + pName);
                    pnts.Add(p0);
                    Logger.Log(this, "MetaObjectMesh - Point added: " + pName);
                }
            }
        }

        /// <summary>
        /// Specify the names of the points contained. You might need to derive them from base.expression;
        /// </summary>
        public abstract IEnumerable<string> Node_Names
        {
            get;
        }
        public abstract IEnumerable<IEnumerable<int>> Face_Names
        {
            get;
        }
        public virtual IEnumerable<T> Nodes
        {
            get;
        }
        public override ModelVisual3D To3DViz()
        {
            MeshBuilder builder = new MeshBuilder(true, true);

            for (int i = 0; i < connectivity.Count; i++)
            {
                List<int> l = connectivity[i];

                //builder.AddPolygon(l.Select(idx => pnts[idx].ToPoint3D()).ToList());

                if (l.Count == 3)
                    builder.AddTriangle(pnts[l[0]].ToPoint3D(), pnts[l[1]].ToPoint3D(), pnts[l[2]].ToPoint3D());
                else if (l.Count == 4)
                    builder.AddQuad(pnts[l[0]].ToPoint3D(), pnts[l[1]].ToPoint3D(), pnts[l[2]].ToPoint3D(), pnts[l[3]].ToPoint3D());
            }

            Mesh3D m = new Mesh3D(builder.Positions, builder.TriangleIndices);

            MeshVisual3D mesh = new MeshVisual3D()
            {
                Mesh = m
            };

            return mesh;
        }

        public override AKT_Mesh To3DObject()
        {
            throw new NotImplementedException();
        }
    }
}
