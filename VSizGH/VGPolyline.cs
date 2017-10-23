using EnvDTE;
using FirstToolWin.Geometry;
using FirstToolWin.Kernel;
using FirstToolWin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSizGH
{
    public class VGPolyline : MetaObjectPolyline<VGPoint3d>
    {
        public VGPolyline()
            : base()
        {
        }

        public override string DebuggerTypeName
        {
            get
            {
                return "Rhino.Geometry.Polyline";
            }
        }

        public override IEnumerable<string> Point_Names
        {
            get
            {
                Expression expCount = expression.DataMembers.DTE.Debugger.GetExpression(expression.Name + "." + "Count");
                //Expression expCount = FindChildren(expression, "Count");
                int count = int.Parse(expCount.Value);

                Logger.Log(this, "Count is: " + count);

                return Enumerable.Range(0, count).Select(a => "[" + a.ToString() + "]");
            }
        }

        public override bool IsValid()
        {
            return expression.Type == DebuggerTypeName;
        }
    }
}
