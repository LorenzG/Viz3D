using FirstToolWin.Geometry;
using FirstToolWin.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSizGH
{
    public class VGLine : MetaObjectLine<VGPoint3d>
    {
        public VGLine() : base()
        {
        }
        //public override void ReadExpression(EnvDTE.Expression exp)
        //{
        //}

        public override string DebuggerTypeName
        {
            get
            {
                return "Rhino.Geometry.Line";
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
            return expression.Type == DebuggerTypeName;
        }
    }
}
