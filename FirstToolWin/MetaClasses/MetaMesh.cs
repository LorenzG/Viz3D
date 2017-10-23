using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.Windows.Forms;
using FirstToolWin.Utilities;
using FirstToolWin.Kernel;
using System.Windows.Media.Media3D;
using FirstToolWin.Geometry.Mesh;

namespace FirstToolWin.MetaClasses
{
    public class MetaMesh : MetaObjectMesh<MetaPoint3d>
    {
        public const string type_Mesh = "FirstToolWinDEBUG.Mesh";

        public MetaMesh()
            : base()
        {
        }
        public override bool IsValid()
        {
            return expression.Type == type_Mesh;
        }
        public override string DebuggerTypeName
        { get { return type_Mesh; } }
        public override IEnumerable<string> Node_Names
        {
            get
            {
                foreach (string p in new string[] { "p0", "p1", "p2", "p3" })
                {
                    yield return p;
                }
            }
        }
        public override IEnumerable<IEnumerable<int>> Face_Names
        {
            get
            {
                return new List<int[]>()
                {
                    new int[] {0,1,2,3},
                };
            }
        }
    }
}
