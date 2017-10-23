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
    public abstract class MetaObjectLine<T> : MetaObject<AKT_Line>
        where T : MetaObjectPoint3d
    {
        protected T from, to;

        public MetaObjectLine()
            : base()
        {

        }

        protected override void ReadExpression(Expression exp)
        {
            Expression eFrom = FindChildren(From_Name);
            Expression eTo = FindChildren(To_Name);
            Logger.Log(this, "ReadExpression - Read FindChildren");
            from = MetaObjectPoint3d.CreateAndRun<T>(eFrom);
            to = MetaObjectPoint3d.CreateAndRun<T>(eTo);
            Logger.Log(this, "ReadExpression - Done creating children");
        }
        public abstract string From_Name
        {
            get;
        }
        public abstract string To_Name
        {
            get;
        }
        public override AKT_Line To3DObject()
        {
            AKT_Point3d a = from.To3DObject();
            AKT_Point3d b = to.To3DObject();
            AKT_Line line = new AKT_Line(a, b);

            return line;
        }
        public override ModelVisual3D To3DViz()
        {
            LinesVisual3D line = new LinesVisual3D();
            line.Points.Add(from.ToPoint3D());
            line.Points.Add(to.ToPoint3D());

            line.Thickness = PropertyServer.LineThickness;
            return line;
        }
    }
}
