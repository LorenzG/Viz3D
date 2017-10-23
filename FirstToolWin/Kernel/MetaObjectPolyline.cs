using FirstToolWin.Geometry;
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
    public abstract class MetaObjectPolyline<T> : MetaObject<AKT_Polyline>
        where T : MetaObjectPoint3d
    {
        protected List<T> pnts;

        public MetaObjectPolyline()
            : base()
        {

        }

        protected override void ReadExpression(Expression exp)
        {
            Logger.Log(this, "ReadExpression - Read FindChildren");

            pnts = new List<T>();

            foreach (string pntStr in Point_Names)
            {
                Expression eP = FindChildren(pntStr);
                T p = MetaObjectPoint3d.CreateAndRun<T>(eP);
                pnts.Add(p);
            }

            Logger.Log(this, "ReadExpression - Done creating children");
        }
        public abstract IEnumerable<string> Point_Names
        {
            get;
        }
        public virtual IEnumerable<T> Nodes
        {
            get
            {
                Logger.Log(this, "NODES");

                Logger.Log(this, "Name is: " + expression.Name);
                Logger.Log(this, "Value is: " + expression.Value);
                
                Expression expVertices = FindChildren("Vertices");
                Expression expCount = FindChildren(expVertices, "Count");
                

                int count = int.Parse(expCount.Value);

                Logger.Log(this, "Found " + count + " vertices");


                for (int i = 0; i < count; i++)
                {
                    string pName = "Vertices[" + i + "]";
                    Logger.Log(this, "Vertex" + pName);


                    Logger.Log(this, "VGMesh - iter:" + pName);

                    Expression expVertex = expression.DTE.Debugger.GetExpression(expression.Name + "." + pName);

                    if (!expVertex.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid 1");
                        continue;
                    }
                    

                    Logger.Log(this, "VGMesh - Got Children: " + expVertex.Name);

                    if (!expVertex.IsValidValue)
                    {
                        Logger.Log(this, "VGMesh - Children is invalid");
                        continue;
                    }

                    T pnt = CreateAndRun<T>(expVertex);

                    yield return pnt;
                }

            }
        }
        public override AKT_Polyline To3DObject()
        {
            return new AKT_Polyline(pnts.Select(a => a.To3DObject()));
        }
        public override ModelVisual3D To3DViz()
        {
            ModelVisual3D lines = new ModelVisual3D();

            for (int i = 0; i < pnts.Count; i++)
            {
                int i0 = i;
                int i1 = (i + 1) % pnts.Count;

                LinesVisual3D line = new LinesVisual3D();
                line.Points.Add(pnts[i0].ToPoint3D());
                line.Points.Add(pnts[i1].ToPoint3D());
                line.Thickness = PropertyServer.LineThickness;

                lines.Children.Add(line);
            }

            return lines;
        }
    }
}
