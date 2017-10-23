using FirstToolWin.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace FirstToolWin.Kernel
{
    public abstract class MetaObjectPoint3d : MetaObject<AKT_Point3d>
    {
        protected double x, y, z;

        //private MetaObjectPoint3d()
        //    : base(null)
        //{

        //}
        public MetaObjectPoint3d()
            : base()
        {

        }
        protected override void ReadExpression(Expression exp)
        {
            EnvDTE.Expression x = FindChildren(X_Name);
            this.x = double.Parse(x.Value);

            EnvDTE.Expression y = FindChildren(Y_Name);
            this.y = double.Parse(y.Value);

            EnvDTE.Expression z = FindChildren(Z_Name);
            this.z = double.Parse(z.Value);
        }
        public Point3D ToPoint3D()
        {
            return new Point3D(x, y, z);
        }
        public override ModelVisual3D To3DViz()
        {
            return new SphereVisual3D()
            {
                Center = new Point3D(x, y, z),
                Radius = PropertyServer.PointSphereSize
            };
        }
        public override AKT_Point3d To3DObject()
        {
            return new AKT_Point3d(x, y, z);
        }
        protected abstract string X_Name
        {
            get;
        }
        protected abstract string Y_Name
        {
            get;
        }
        protected abstract string Z_Name
        {
            get;
        }
        public double X
        {
            get { return x; }
        }
        public double Y
        {
            get { return y; }
        }
        public double Z
        {
            get { return z; }
        }

        //public new static T GetFromExpression<T>(Expression exp)
        //    where T : MetaObjectPoint3d
        //{
        //    return (T)Activator.CreateInstance(typeof(T), exp);
        //}
    }
}
