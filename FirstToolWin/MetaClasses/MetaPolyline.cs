using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using EnvDTE;
using HelixToolkit.Wpf;
using FirstToolWin.MetaClasses;
using FirstToolWin.Kernel;
using FirstToolWin.Geometry;
#if VERBOSE
using System.Windows.Forms;
#endif

namespace FirstToolWin.MetaClasses
{
    public class MetaPolyline : MetaObjectPolyline<MetaPoint3d>
    {
        public const string type_Polyline = "FirstToolWinDEBUG.MetaPolyline";
        
        public MetaPolyline()
            : base() { }

        public override string DebuggerTypeName
        {
            get
            {
                return type_Polyline;
            }
        }

        public override IEnumerable<string> Point_Names
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool IsValid()
        {
            return expression.Type == type_Polyline;
        }        
    }
}
