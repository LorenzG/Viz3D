using FirstToolWin.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using FirstToolWin.Utilities;

namespace VSizGH
{
    class VGMeshFace : MetaObject
    {
        public int a, b, c, d;
        public bool isQuad;

        public VGMeshFace()
            : base()
        {
            isQuad = false;
            d = -1;
        }
        public override string DebuggerTypeName
        {
            get
            {
                return "Rhino.Geometry.MeshFace";
            }
        }

        public override bool IsValid()
        {
            return expression.IsValidValue;
        }

        protected override void ReadExpression(Expression exp)
        {
            Expression expA = FindChildren(exp, "A");
            Expression expB = FindChildren(exp, "B");
            Expression expC = FindChildren(exp, "C");


            Logger.Log(expA, "int A is valid: " + expA.IsValidValue);
            Logger.Log(expA, "int A is : " + expA.Value);

            a = int.Parse(expA.Value);
            b = int.Parse(expB.Value);
            c = int.Parse(expC.Value);

            Expression expIsQuad = FindChildren(exp, "IsQuad");

            isQuad = bool.Parse(expIsQuad.Value);

            if (isQuad)
            {
                Logger.Log(this, "Getting quad face");
                Expression expD = FindChildren(exp, "D");
                d = int.Parse(expD.Value);
            }

        }
    }
}
