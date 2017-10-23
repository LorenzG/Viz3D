using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.NurbsCurves
{
    public class AKT_EllipseNurbs : AKT_NurbsCurve
    {
        AKT_Plane plane;
        double r1, r2;


        public AKT_EllipseNurbs(AKT_NurbsPoint[] pnts, int degree, double[] knots, double[] knotsreparam, double length, AKT_Plane plane, double r1, double r2, AKT_Interval dom)
            : base(pnts, degree, knots, knotsreparam, length, CurveType.Ellipse, dom)
        {
            this.plane = plane;
            this.r1 = r1;
            this.r2 = r2;
        }

        public double Radius1
        {
            get { return r1; }
        }
        public double Radius2
        {
            get { return r2; }
        }

        public AKT_Plane Plane
        {
            get { return plane; }
        }




    }
}