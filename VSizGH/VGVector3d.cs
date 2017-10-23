using FirstToolWin.Geometry;
using FirstToolWin.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;

namespace VSizGH
{
    public class VGVector3d : MetaObjectVector
    {
        public VGVector3d()
            : base()
        {
        }

        public override string DebuggerTypeName
        {
            get
            {
                return "Rhino.Geometry.Vector3d";
            }
        }

        protected override string X_Name
        {
            get
            {
                return "X";
            }
        }

        protected override string Y_Name
        {
            get
            {
                return "Y";
            }
        }

        protected override string Z_Name
        {
            get
            {
                return "Z";
            }
        }

        public override bool IsValid()
        {
            return expression.Type == DebuggerTypeName && expression.IsValidValue;
        }
    }
}
