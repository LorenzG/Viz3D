using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.NurbsCurves
{
    public class AKT_CircleNurbs : AKT_EllipseNurbs
    {
        public AKT_CircleNurbs(AKT_NurbsPoint[] pnts, int degree, double[] knots,double[] knotsreparam, double length, AKT_Plane plane, double r, AKT_Interval dom)
            : base(pnts, degree, knots,knotsreparam, length, plane, r, r,dom)
        {
            this.CurveType |= NurbsCurves.CurveType.Circle;
        }

        public double Radius
        {
            get { return base.Radius1; }
        }

    }
}
