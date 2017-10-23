using FirstToolWin.Geometry.NurbsCurves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry
{
    public class AKT_NurbsSurface 
    {
        static bool hi;
        public Guid guid;
        int u_degree;
        int v_degree;
        int u_count;
        int v_count;
        IEnumerable<Tuple<AKT_NurbsPoint, int, int>> nodes;
        AKT_NurbsCurve[] edgesOuter;
        AKT_NurbsCurve[][] edgesInner;
        SurfType surfType;

        public AKT_NurbsSurface(AKT_NurbsCurve[] edgesOuter, AKT_NurbsCurve[][]  edgesInner, IEnumerable<Tuple<AKT_NurbsPoint, int, int>> nodes, int u_count, int v_count, int u_degree, int v_degree, SurfType st)
        {
            this.u_count = u_count;
            this.v_count = v_count;
            this.nodes = nodes;
            this.u_degree = u_degree;
            this.v_degree = v_degree;
            this.edgesOuter = edgesOuter;
            this.edgesInner = edgesInner;
            this.surfType = st;
        }
        
        //public void SetControlPoint(AKT_NurbsPoint pnt, int u, int v)
        //{
        //    AKT_NurbsPoint[] p = new AKT_NurbsPoint[] { pnt };
        //    if (Points == null || Points.Count() == 0)
        //    {
        //        this.points = p;
        //    }
        //    else
        //    {
        //        this.points = this.points.Concat(p).ToArray();
        //    }
        //}

        public static AKT_NurbsSurface PlaneSurface(AKT_Interval u, AKT_Interval v, SurfType st)
        {
            AKT_NurbsPoint p00 = new AKT_NurbsPoint(new AKT_Point3d(u.T0, v.T0, 0), 1);
            AKT_NurbsPoint p01 = new AKT_NurbsPoint(new AKT_Point3d(u.T0, v.T1, 0), 1);
            AKT_NurbsPoint p10 = new AKT_NurbsPoint(new AKT_Point3d(u.T1, v.T0, 0), 1);
            AKT_NurbsPoint p11 = new AKT_NurbsPoint(new AKT_Point3d(u.T1, v.T1, 0), 1);

            List<Tuple<AKT_NurbsPoint, int, int>> tups = new List<Tuple<AKT_NurbsPoint,int,int>>();
            tups.Add(new Tuple<AKT_NurbsPoint,int,int>(p00, 0,0));
            tups.Add(new Tuple<AKT_NurbsPoint,int,int>(p01, 0,1));
            tups.Add(new Tuple<AKT_NurbsPoint,int,int>(p10, 1,0));
            tups.Add(new Tuple<AKT_NurbsPoint,int,int>(p11, 1,1));

            AKT_NurbsCurve e1 = new AKT_NurbsCurve(new AKT_NurbsPoint[] { p00, p01 }, 2, CurveType.Line, new AKT_Interval(0, 1));
            AKT_NurbsCurve e2 = new AKT_NurbsCurve(new AKT_NurbsPoint[] { p01, p11 }, 2, CurveType.Line, new AKT_Interval(0, 1));
            AKT_NurbsCurve e3 = new AKT_NurbsCurve(new AKT_NurbsPoint[] { p11, p10 }, 2, CurveType.Line, new AKT_Interval(0, 1));
            AKT_NurbsCurve e4 = new AKT_NurbsCurve(new AKT_NurbsPoint[] { p10, p00 }, 2, CurveType.Line, new AKT_Interval(0, 1));
            
            AKT_NurbsCurve[] edgeOuter = new AKT_NurbsCurve[] { e1, e2, e3, e4 };

            return new AKT_NurbsSurface(edgeOuter, new  AKT_NurbsCurve[0][], tups, 2, 2, 3, 3, st);
        }

        public AKT_NurbsPoint GetControlPoint(int u, int v)
        {
            return nodes.Where(p => p.Item2 == u).Single(q => q.Item3 == v).Item1;
        }

        public IEnumerable<AKT_Point3d> Points 
        { get { return this.nodes.Select(p => p.Item1.Location); } }
        public IEnumerable<double> Weigths 
        { get { return this.nodes.Select(p => p.Item1.Weight); } }
        public int U_Degree 
        { get { return this.u_degree; } }
        public int V_Degree
        { get { return this.v_degree; } }
        public int U_Count 
        { get { return this.u_count; } }
        public int V_Count 
        { get { return this.v_count; } }
        public AKT_NurbsCurve[] BrepEdgesOuter 
        { get { return edgesOuter; } }
        public AKT_NurbsCurve[][] BrepEdgesInner
        { get { return edgesInner; } }
        public SurfType SurfType
        { get { return surfType; } }

    }


    [Flags]
    public enum SurfType
    {
        Unknown = 0,
        Planar = 1,
        Cone = 2,
        Cylinder = 4,
        Sphere = 8,
        Torus= 16,
    }



}
