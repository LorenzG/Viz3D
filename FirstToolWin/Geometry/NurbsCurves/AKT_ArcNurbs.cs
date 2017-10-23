using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.NurbsCurves
{
    public class AKT_ArcNurbs : AKT_EllipseNurbs
    {
        double AngStart;
        double AngEnd;

        public AKT_ArcNurbs(AKT_NurbsPoint[] pnts, int degree, double[] knots, double[]knotsreparam,double length, AKT_Plane plane, double r, double AngStart, double AngEnd, AKT_Interval dom)
            : base(pnts, degree, knots, knotsreparam,length, plane, r, r, dom) 
        {
            this.AngStart = AngStart;
            this.AngEnd = AngEnd;
            this.CurveType |= NurbsCurves.CurveType.Arc;
        }

        public double AngleStart
        {
            get { return AngStart; }
        }
        public double AngleEnd
        {
            get { return AngEnd; }
        }




    }
}
