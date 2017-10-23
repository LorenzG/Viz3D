using EnvDTE;
using FirstToolWin.Geometry;
using FirstToolWin.Kernel;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FirstToolWin.MetaClasses
{

    public class MetaPoint3d : MetaObjectPoint3d
    {
        public const string type_Point3d = "FirstToolWinDEBUG.Point3d";
        
        public MetaPoint3d()
            : base()
        {
        }
        
        public override string DebuggerTypeName
        {
            get
            {
                return "FirstToolWinDEBUG.Point3d";
            }
        }

        protected override string X_Name
        {
            get
            {
                return "x";
            }
        }

        protected override string Y_Name
        {
            get
            {
                return "y";
            }
        }

        protected override string Z_Name
        {
            get
            {
                return "z";
            }
        }

        public override bool IsValid()
        {
            return expression.Type == type_Point3d;
        }
    }
}
