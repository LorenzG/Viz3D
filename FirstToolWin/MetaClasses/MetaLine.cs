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
    public class MetaLine : MetaObjectLine<MetaPoint3d>
    {
        public const string type_Line = "FirstToolWinDEBUG.Line";
        
        public MetaLine()
            : base() { }

        public override string DebuggerTypeName
        {
            get
            {
                return type_Line;
            }
        }
        public override string From_Name
        {
            get
            {
                return "From";
            }
        }
        public override string To_Name
        {
            get
            {
                return "To";
            }
        }
        public override bool IsValid()
        {
            return expression.Type == type_Line;
        }        
    }
}
