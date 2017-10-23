using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry.NurbsCurves
{
    public class AKT_NurbsCurve
    {
        public Guid guid;
        AKT_NurbsPoint[] pnts;
        double[] knots;
        double[] knotsreparam;
        double length;
        int degree;
        CurveType curveType;
        AKT_Interval dom;
        AKT_NurbsCurve split1;
        AKT_NurbsCurve split2;


        public AKT_NurbsCurve(AKT_NurbsPoint[] pnts, int degree, CurveType ct, AKT_Interval dom)
            : this(pnts, degree, Enumerable.Repeat(1.0, pnts.Length).ToArray(), Enumerable.Repeat(1.0, pnts.Length).ToArray(), double.NaN, ct, dom)
        { }
        public AKT_NurbsCurve(AKT_NurbsPoint[] pnts, int degree, double[] knots, double[] knotsreparam, double length, CurveType ct, AKT_Interval dom)
        {
            this.pnts = pnts;
            this.degree = degree;
            this.knots = knots;
            this.knotsreparam = knotsreparam;
            this.length = length;
            this.curveType = ct;
            this.dom = dom;
        }

        public AKT_NurbsPoint[] Points
        {
            get { return this.pnts; }
        }
        public AKT_NurbsPoint StartPoint
        {
            get { return this.pnts[0]; }
        }
        public AKT_NurbsPoint EndPoint
        {
            get { return this.pnts[this.pnts.Length - 1]; }
        }
        public int Degree
        {
            get { return degree; }
        }
        public double[] Knots
        {
            get { return knots; }
        }
        public double[] KnotsReParam
        {
            get { return knotsreparam; }
        }
        public double Length
        {
            get { return length; }
        }
        public CurveType CurveType
        {
            get { return curveType; }
            set { curveType = value; }
        }
        public AKT_Interval Domain
        {
            get { return dom; }
        }

        public AKT_NurbsCurve Split1
        {
            get { return split1; }
            set { split1 = value; }
        }
        public AKT_NurbsCurve Split2
        {
            get { return split2; }
            set { split2 = value; }
        }

        public AKT_NurbsCurve NormalizedDomainCurve()
        {
            double[] reparKnots;
            reparKnots = knots.Select(r => (r - knots[0]) / (knots[0] - knots[knots.Length])).ToArray();
            //throw new Exception("Jeg implement me please");
            return new AKT_NurbsCurve(pnts, degree, reparKnots, reparKnots, length, curveType, dom);
        }
    }

    [Flags]
    public enum CurveType
    {
        Nurbs = 1 << 0,
        Arc = 1 << 1,
        Circle = 1 << 2,
        Line = 1 << 3,
        Ellipse = 1 << 4
    }

    //{
    //    private const int idxBooleanUnion = 0;

    //    private const int idxBooleanIntersection = 1;

    //    private const int idxBooleanDifference = 2;

    //    private const int idxIgnoreNone = 0;

    //    private const int idxIgnorePlane = 1;

    //    private const int idxIgnorePlaneArcOrEllipse = 2;

    //    private const int idxIsClosed = 0;

    //    private const int idxIsPeriodic = 1;

    //    private const int idxPointAtT = 0;

    //    private const int idxPointAtStart = 1;

    //    private const int idxPointAtEnd = 2;

    //    private const int idxDerivativeAt = 0;

    //    private const int idxTangentAt = 1;

    //    private const int idxCurvatureAt = 2;

    //    internal IntPtr m_pCurveDisplay = IntPtr.Zero;

    //    /// <summary>
    //    /// Gets the maximum algebraic degree of any span
    //    /// or a good estimate if curve spans are not algebraic.
    //    /// </summary>
    //    public int Degree
    //    {
    //        get
    //        {
    //            return UnsafeNativeMethods.ON_Curve_Degree(base.ConstPointer());
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the dimension of the object.
    //    /// <para>The dimension is typically three. For parameter space trimming
    //    /// curves the dimension is two. In rare cases the dimension can
    //    /// be one or greater than three.</para>
    //    /// </summary>
    //    public int Dimension
    //    {
    //        get
    //        {
    //            return UnsafeNativeMethods.ON_Curve_Dimension(base.ConstPointer());
    //        }
    //    }

    //    /// <summary>
    //    /// Gets or sets the domain of the curve.
    //    /// </summary>
    //    public Interval Domain
    //    {
    //        get
    //        {
    //            Interval interval = new Interval();
    //            IntPtr intPtr = base.ConstPointer();
    //            UnsafeNativeMethods.ON_Curve_Domain(intPtr, false, ref interval);
    //            return interval;
    //        }
    //        set
    //        {
    //            IntPtr intPtr = this.NonConstPointer();
    //            UnsafeNativeMethods.ON_Curve_Domain(intPtr, true, ref value);
    //        }
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether or not this curve is a closed curve.
    //    /// </summary>
    //    public bool IsClosed
    //    {
    //        get
    //        {
    //            return UnsafeNativeMethods.ON_Curve_GetBool(base.ConstPointer(), 0);
    //        }
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether or not this curve is considered to be Periodic.
    //    /// </summary>
    //    public bool IsPeriodic
    //    {
    //        get
    //        {
    //            return UnsafeNativeMethods.ON_Curve_GetBool(base.ConstPointer(), 1);
    //        }
    //    }

    //    /// <summary>
    //    /// Evaluates point at the end of the curve.
    //    /// </summary>
    //    public Point3d PointAtEnd
    //    {
    //        get
    //        {
    //            Point3d point3d = new Point3d();
    //            UnsafeNativeMethods.ON_Curve_PointAt(base.ConstPointer(), 1, ref point3d, 2);
    //            return point3d;
    //        }
    //    }

    //    /// <summary>
    //    /// Evaluates point at the start of the curve.
    //    /// </summary>
    //    public Point3d PointAtStart
    //    {
    //        get
    //        {
    //            Point3d point3d = new Point3d();
    //            UnsafeNativeMethods.ON_Curve_PointAt(base.ConstPointer(), 0, ref point3d, 1);
    //            return point3d;
    //        }
    //    }

    //    /// <summary>
    //    /// Gets the number of non-empty smooth (c-infinity) spans in the curve.
    //    /// </summary>
    //    public int SpanCount
    //    {
    //        get
    //        {
    //            return UnsafeNativeMethods.ON_Curve_SpanCount(base.ConstPointer());
    //        }
    //    }

    //    /// <summary>Evaluate unit tangent vector at the end of the curve.</summary>
    //    /// <returns>Unit tangent vector of the curve at the end point.</returns>
    //    /// <remarks>No error handling.</remarks>
    //    public Vector3d TangentAtEnd
    //    {
    //        get
    //        {
    //            return this.TangentAt(this.Domain.Max);
    //        }
    //    }

    //    /// <summary>Evaluates the unit tangent vector at the start of the curve.</summary>
    //    /// <returns>Unit tangent vector of the curve at the start point.</returns>
    //    /// <remarks>No error handling.</remarks>
    //    public Vector3d TangentAtStart
    //    {
    //        get
    //        {
    //            return this.TangentAt(this.Domain.Min);
    //        }
    //    }

    //    /// <summary>
    //    /// Protected constructor for internal use.
    //    /// </summary>
    //    protected Curve()
    //    {
    //    }

    //    internal Curve(IntPtr ptr, object parent) : this(ptr, parent, -1)
    //    {
    //    }

    //    internal Curve(IntPtr ptr, object parent, int subobject_index) : base(ptr, parent, subobject_index)
    //    {
    //    }

    //    /// <summary>
    //    /// Protected serialization constructor for internal use.
    //    /// </summary>
    //    /// <param name="info">Serialization data.</param>
    //    /// <param name="context">Serialization stream.</param>
    //    protected Curve(SerializationInfo info, StreamingContext context) : base(info, context)
    //    {
    //    }

    //    internal override IntPtr _InternalDuplicate(out bool applymempressure)
    //    {
    //        applymempressure = true;
    //        return UnsafeNativeMethods.ON_Curve_DuplicateCurve(base.ConstPointer());
    //    }

    //    internal override IntPtr _InternalGetConstPointer()
    //    {
    //        CurveHolder m_parent = this.m__parent as CurveHolder;
    //        if (m_parent != null)
    //        {
    //            return m_parent.ConstCurvePointer();
    //        }
    //        if (this.m_subobject_index >= 0)
    //        {
    //            PolyCurve polyCurve = this.m__parent as PolyCurve;
    //            if (polyCurve != null)
    //            {
    //                return UnsafeNativeMethods.ON_PolyCurve_SegmentCurve(polyCurve.ConstPointer(), this.m_subobject_index);
    //            }
    //        }
    //        return base._InternalGetConstPointer();
    //    }

    //    /// <summary>
    //    /// If this curve is closed, then modify it so that the start/end point is at curve parameter t.
    //    /// </summary>
    //    /// <param name="t">
    //    /// Curve parameter of new start/end point. The returned curves domain will start at t.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool ChangeClosedCurveSeam(double t)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_ChangeClosedCurveSeam(this.NonConstPointer(), t);
    //    }

    //    /// <summary>
    //    /// Changes the dimension of a curve.
    //    /// </summary>
    //    /// <param name="desiredDimension">The desired dimension.</param>
    //    /// <returns>
    //    /// true if the curve's dimension was already desiredDimension
    //    /// or if the curve's dimension was successfully changed to desiredDimension;
    //    /// otherwise false.
    //    /// </returns>
    //    public bool ChangeDimension(int desiredDimension)
    //    {
    //        if (this.Dimension == desiredDimension)
    //        {
    //            return true;
    //        }
    //        return UnsafeNativeMethods.ON_Curve_ChangeDimension(this.NonConstPointer(), desiredDimension);
    //    }

    //    /// <summary>
    //    /// Determines the orientation (counterclockwise or clockwise) of a closed planar curve in a given plane.
    //    /// Only works with simple (no self intersections) closed planar curves.
    //    /// </summary>
    //    /// <param name="upDirection">A vector that is considered "up".</param>
    //    /// <returns>The orientation of this curve with respect to a defined up direction.</returns>
    //    public CurveOrientation ClosedCurveOrientation(Vector3d upDirection)
    //    {
    //        return this.ClosedCurveOrientation(new Plane(Point3d.Origin, upDirection));
    //    }

    //    /// <summary>
    //    /// Determines the orientation (counterclockwise or clockwise) of a closed planar curve in a given plane.
    //    /// Only works with simple (no self intersections) closed planar curves.
    //    /// </summary>
    //    /// <param name="plane">
    //    /// The plane in which to solve the orientation.
    //    /// </param>
    //    /// <returns>The orientation of this curve in the given plane.</returns>
    //    public CurveOrientation ClosedCurveOrientation(Plane plane)
    //    {
    //        if (!plane.IsValid)
    //        {
    //            return CurveOrientation.Undefined;
    //        }
    //        Transform transform = Transform.ChangeBasis(Plane.WorldXY, plane);
    //        return this.ClosedCurveOrientation(transform);
    //    }

    //    /// <summary>
    //    /// Determines the orientation (counterclockwise or clockwise) of a closed planar curve.
    //    /// Only works with simple (no self intersections) closed planar curves.
    //    /// </summary>
    //    /// <param name="xform">
    //    /// Transformation to map the curve to the xy plane. If the curve is parallel
    //    /// to the xy plane, you may pass Identity matrix.
    //    /// </param>
    //    /// <returns>The orientation of this curve in the world xy-plane.</returns>
    //    public CurveOrientation ClosedCurveOrientation(Transform xform)
    //    {
    //        int num = UnsafeNativeMethods.ON_Curve_ClosedCurveOrientation(base.ConstPointer(), ref xform);
    //        if (num == 1)
    //        {
    //            return CurveOrientation.Clockwise;
    //        }
    //        if (num == -1)
    //        {
    //            return CurveOrientation.CounterClockwise;
    //        }
    //        return CurveOrientation.Undefined;
    //    }

    //    /// <summary>
    //    /// Finds parameter of the point on a curve that is closest to testPoint.
    //    /// If the maximumDistance parameter is &gt; 0, then only points whose distance
    //    /// to the given point is &lt;= maximumDistance will be returned.  Using a 
    //    /// positive value of maximumDistance can substantially speed up the search.
    //    /// </summary>
    //    /// <param name="testPoint">Point to search from.</param>
    //    /// <param name="t">Parameter of local closest point.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool ClosestPoint(Point3d testPoint, out double t)
    //    {
    //        return this.ClosestPoint(testPoint, out t, -1);
    //    }

    //    /// <summary>
    //    /// Finds the parameter of the point on a curve that is closest to testPoint.
    //    /// If the maximumDistance parameter is &gt; 0, then only points whose distance
    //    /// to the given point is &lt;= maximumDistance will be returned.  Using a 
    //    /// positive value of maximumDistance can substantially speed up the search.
    //    /// </summary>
    //    /// <param name="testPoint">Point to project.</param>
    //    /// <param name="t">parameter of local closest point returned here.</param>
    //    /// <param name="maximumDistance">The maximum allowed distance.
    //    /// <para>Past this distance, the search is given up and false is returned.</para>
    //    /// <para>Use 0 to turn off this parameter.</para></param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool ClosestPoint(Point3d testPoint, out double t, double maximumDistance)
    //    {
    //        t = 0;
    //        return UnsafeNativeMethods.ON_Curve_GetClosestPoint(base.ConstPointer(), testPoint, ref t, maximumDistance);
    //    }

    //    /// <summary>
    //    /// Finds the object (and the closest point in that object) that is closest to
    //    /// this curve. <para><see cref="T:Rhino.Geometry.Brep">Breps</see>, <see cref="T:Rhino.Geometry.Surface">surfaces</see>,
    //    /// <see cref="T:Rhino.Geometry.Curve">curves</see> and <see cref="T:Rhino.Geometry.PointCloud">point clouds</see> are examples of
    //    /// objects that can be passed to this function.</para>
    //    /// </summary>
    //    /// <param name="geometry">A list, an array or any enumerable set of geometry to search.</param>
    //    /// <param name="pointOnCurve">The point on curve. This out parameter is assigned during this call.</param>
    //    /// <param name="pointOnObject">The point on geometry. This out parameter is assigned during this call.</param>
    //    /// <param name="whichGeometry">The index of the geometry. This out parameter is assigned during this call.</param>
    //    /// <param name="maximumDistance">Maximum allowable distance. Past this distance, the research is given up and false is returned.</param>
    //    /// <returns>true on success; false if no object was found or selected.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If geometry is null.</exception>
    //    public bool ClosestPoints(IEnumerable<GeometryBase> geometry, out Point3d pointOnCurve, out Point3d pointOnObject, out int whichGeometry, double maximumDistance)
    //    {
    //        bool flag;
    //        if (geometry == null)
    //        {
    //            throw new ArgumentNullException("geometry");
    //        }
    //        using (SimpleArrayGeometryPointer simpleArrayGeometryPointer = new SimpleArrayGeometryPointer(geometry))
    //        {
    //            pointOnCurve = Point3d.Unset;
    //            pointOnObject = Point3d.Unset;
    //            IntPtr intPtr = base.ConstPointer();
    //            IntPtr intPtr1 = simpleArrayGeometryPointer.ConstPointer();
    //            whichGeometry = 0;
    //            bool flag1 = UnsafeNativeMethods.RHC_RhinoGetClosestPoint(intPtr, intPtr1, maximumDistance, ref pointOnCurve, ref pointOnObject, ref whichGeometry);
    //            flag = flag1;
    //        }
    //        return flag;
    //    }

    //    /// <summary>
    //    /// Finds the object (and the closest point in that object) that is closest to
    //    /// this curve. <para><see cref="T:Rhino.Geometry.Brep">Breps</see>, <see cref="T:Rhino.Geometry.Surface">surfaces</see>,
    //    /// <see cref="T:Rhino.Geometry.Curve">curves</see> and <see cref="T:Rhino.Geometry.PointCloud">point clouds</see> are examples of
    //    /// objects that can be passed to this function.</para>
    //    /// </summary>
    //    /// <param name="geometry">A list, an array or any enumerable set of geometry to search.</param>
    //    /// <param name="pointOnCurve">The point on curve. This out parameter is assigned during this call.</param>
    //    /// <param name="pointOnObject">The point on geometry. This out parameter is assigned during this call.</param>
    //    /// <param name="whichGeometry">The index of the geometry. This out parameter is assigned during this call.</param>
    //    /// <returns>true on success; false if no object was found or selected.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If geometry is null.</exception>
    //    public bool ClosestPoints(IEnumerable<GeometryBase> geometry, out Point3d pointOnCurve, out Point3d pointOnObject, out int whichGeometry)
    //    {
    //        return this.ClosestPoints(geometry, out pointOnCurve, out pointOnObject, out whichGeometry, 0);
    //    }

    //    /// <summary>
    //    /// Gets closest points between this and another curves.
    //    /// </summary>
    //    /// <param name="otherCurve">The other curve.</param>
    //    /// <param name="pointOnThisCurve">The point on this curve. This out parameter is assigned during this call.</param>
    //    /// <param name="pointOnOtherCurve">The point on other curve. This out parameter is assigned during this call.</param>
    //    /// <returns>true on success; false on error.</returns>
    //    public bool ClosestPoints(Curve otherCurve, out Point3d pointOnThisCurve, out Point3d pointOnOtherCurve)
    //    {
    //        int num;
    //        GeometryBase[] geometryBaseArray = new GeometryBase[] { otherCurve };
    //        return this.ClosestPoints(geometryBaseArray, out pointOnThisCurve, out pointOnOtherCurve, out num, 0);
    //    }

    //    /// <summary>
    //    /// Computes the relationship between a point and a closed curve region. 
    //    /// This curve must be closed or the return value will be Unset.
    //    /// Both curve and point are projected to the World XY plane.
    //    /// </summary>
    //    /// <param name="testPoint">Point to test.</param>
    //    /// <returns>Relationship between point and curve region.</returns>
    //    public PointContainment Contains(Point3d testPoint)
    //    {
    //        return this.Contains(testPoint, Plane.WorldXY, 0);
    //    }

    //    /// <summary>
    //    /// Computes the relationship between a point and a closed curve region. 
    //    /// This curve must be closed or the return value will be Unset.
    //    /// </summary>
    //    /// <param name="testPoint">Point to test.</param>
    //    /// <param name="plane">Plane in in which to compare point and region.</param>
    //    /// <returns>Relationship between point and curve region.</returns>
    //    public PointContainment Contains(Point3d testPoint, Plane plane)
    //    {
    //        return this.Contains(testPoint, plane, 0);
    //    }

    //    /// <summary>
    //    /// Computes the relationship between a point and a closed curve region. 
    //    /// This curve must be closed or the return value will be Unset.
    //    /// </summary>
    //    /// <param name="testPoint">Point to test.</param>
    //    /// <param name="plane">Plane in in which to compare point and region.</param>
    //    /// <param name="tolerance">Tolerance to use during comparison.</param>
    //    /// <returns>Relationship between point and curve region.</returns>
    //    public PointContainment Contains(Point3d testPoint, Plane plane, double tolerance)
    //    {
    //        if (testPoint.IsValid && plane.IsValid && this.IsClosed)
    //        {
    //            IntPtr intPtr = base.ConstPointer();
    //            int num = UnsafeNativeMethods.RHC_PointInClosedRegion(intPtr, testPoint, plane, tolerance);
    //            if (num == 0)
    //            {
    //                return PointContainment.Outside;
    //            }
    //            if (1 == num)
    //            {
    //                return PointContainment.Inside;
    //            }
    //            if (2 == num)
    //            {
    //                return PointContainment.Coincident;
    //            }
    //        }
    //        return PointContainment.Unset;
    //    }

    //    private static UnsafeNativeMethods.ExtendCurveConsts ConvertExtensionStyle(CurveExtensionStyle style)
    //    {
    //        if (style == CurveExtensionStyle.Arc)
    //        {
    //            return UnsafeNativeMethods.ExtendCurveConsts.ExtendTypeArc;
    //        }
    //        if (style == CurveExtensionStyle.Line)
    //        {
    //            return UnsafeNativeMethods.ExtendCurveConsts.ExtendTypeLine;
    //        }
    //        return UnsafeNativeMethods.ExtendCurveConsts.ExtendTypeSmooth;
    //    }

    //    /// <summary>
    //    /// Create a Blend curve between two existing curves.
    //    /// </summary>
    //    /// <param name="curveA">Curve to blend from (blending will occur at curve end point).</param>
    //    /// <param name="curveB">Curve to blend to (blending will occur at curve start point).</param>
    //    /// <param name="continuity">Continuity of blend.</param>
    //    /// <returns>A curve representing the blend between A and B or null on failure.</returns>
    //    public static Curve CreateBlendCurve(Curve curveA, Curve curveB, BlendContinuity continuity)
    //    {
    //        return Curve.CreateBlendCurve(curveA, curveB, continuity, 1, 1);
    //    }

    //    /// <summary>
    //    /// Create a Blend curve between two existing curves.
    //    /// </summary>
    //    /// <param name="curveA">Curve to blend from (blending will occur at curve end point).</param>
    //    /// <param name="curveB">Curve to blend to (blending will occur at curve start point).</param>
    //    /// <param name="continuity">Continuity of blend.</param>
    //    /// <param name="bulgeA">Bulge factor at curveA end of blend. Values near 1.0 work best.</param>
    //    /// <param name="bulgeB">Bulge factor at curveB end of blend. Values near 1.0 work best.</param>
    //    /// <returns>A curve representing the blend between A and B or null on failure.</returns>
    //    public static Curve CreateBlendCurve(Curve curveA, Curve curveB, BlendContinuity continuity, double bulgeA, double bulgeB)
    //    {
    //        if (curveA == null)
    //        {
    //            throw new ArgumentNullException("curveA");
    //        }
    //        if (curveB == null)
    //        {
    //            throw new ArgumentNullException("curveB");
    //        }
    //        IntPtr intPtr = curveA.ConstPointer();
    //        IntPtr intPtr1 = curveB.ConstPointer();
    //        switch (continuity)
    //        {
    //            case BlendContinuity.Position:
    //            {
    //                return new LineCurve(curveA.PointAtEnd, curveB.PointAtStart);
    //            }
    //            case BlendContinuity.Tangency:
    //            {
    //                IntPtr intPtr2 = UnsafeNativeMethods.RHC_RhinoBlendG1Curve(intPtr, intPtr1, bulgeA, bulgeB);
    //                return GeometryBase.CreateGeometryHelper(intPtr2, null) as Curve;
    //            }
    //            case BlendContinuity.Curvature:
    //            {
    //                IntPtr intPtr3 = UnsafeNativeMethods.RHC_RhinoBlendG2Curve(intPtr, intPtr1, bulgeA, bulgeB);
    //                return GeometryBase.CreateGeometryHelper(intPtr3, null) as Curve;
    //            }
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Makes a curve blend between 2 curves at the parameters specified
    //    /// with the directions and continuities specified
    //    /// </summary>
    //    /// <param name="curve0">First curve to blend from</param>
    //    /// <param name="t0">Parameter on first curve for blend endpoint</param>
    //    /// <param name="reverse0">
    //    /// If false, the blend will go in the natural direction of the curve.
    //    /// If true, the blend will go in the opposite direction to the curve
    //    /// </param>
    //    /// <param name="continuity0">continuity for the blend at the start</param>
    //    /// <param name="curve1">Second curve to blend from</param>
    //    /// <param name="t1">Parameter on second curve for blend endpoint</param>
    //    /// <param name="reverse1">
    //    /// If false, the blend will go in the natural direction of the curve.
    //    /// If true, the blend will go in the opposite direction to the curve
    //    /// </param>
    //    /// <param name="continuity1">continuity for the blend at the end</param>
    //    /// <returns>the blend curve on success. null on failure</returns>
    //    public static Curve CreateBlendCurve(Curve curve0, double t0, bool reverse0, BlendContinuity continuity0, Curve curve1, double t1, bool reverse1, BlendContinuity continuity1)
    //    {
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        IntPtr intPtr2 = UnsafeNativeMethods.CRhinoBlend_CurveBlend(intPtr, t0, reverse0, (int)continuity0, intPtr1, t1, reverse1, (int)continuity1);
    //        return GeometryBase.CreateGeometryHelper(intPtr2, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Calculates the boolean difference between two closed, planar curves. 
    //    /// Note, curves must be co-planar.
    //    /// </summary>
    //    /// <param name="curveA">The first closed, planar curve.</param>
    //    /// <param name="curveB">The second closed, planar curve.</param>
    //    /// <returns>Result curves on success, empty array if no difference could be calculated.</returns>
    //    public static Curve[] CreateBooleanDifference(Curve curveA, Curve curveB)
    //    {
    //        if (curveA == null || curveB == null)
    //        {
    //            throw new ArgumentNullException((curveA == null ? "curveA" : "curveB"));
    //        }
    //        return Curve.CreateBooleanDifference(curveA, new Curve[] { curveB });
    //    }

    //    /// <summary>
    //    /// Calculates the boolean difference between a closed planar curve, and a list of closed planar curves. 
    //    /// Note, curves must be co-planar.
    //    /// </summary>
    //    /// <param name="curveA">The first closed, planar curve.</param>
    //    /// <param name="subtractors">curves to subtract from the first closed curve.</param>
    //    /// <returns>Result curves on success, empty array if no difference could be calculated.</returns>
    //    public static Curve[] CreateBooleanDifference(Curve curveA, IEnumerable<Curve> subtractors)
    //    {
    //        if (curveA == null || subtractors == null)
    //        {
    //            throw new ArgumentNullException((curveA == null ? "curveA" : "subtractors"));
    //        }
    //        List<Curve> curves = new List<Curve>()
    //        {
    //            curveA
    //        };
    //        curves.AddRange(subtractors);
    //        IntPtr intPtr = (new SimpleArrayCurvePointer(curves)).ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (UnsafeNativeMethods.ON_Curve_BooleanOperation(intPtr, simpleArrayCurvePointer.NonConstPointer(), 2) >= 1)
    //        {
    //            return simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return new Curve[0];
    //    }

    //    /// <summary>
    //    /// Calculates the boolean intersection of two closed, planar curves. 
    //    /// Note, curves must be co-planar.
    //    /// </summary>
    //    /// <param name="curveA">The first closed, planar curve.</param>
    //    /// <param name="curveB">The second closed, planar curve.</param>
    //    /// <returns>Result curves on success, empty array if no intersection could be calculated.</returns>
    //    public static Curve[] CreateBooleanIntersection(Curve curveA, Curve curveB)
    //    {
    //        if (curveA == null || curveB == null)
    //        {
    //            throw new ArgumentNullException((curveA == null ? "curveA" : "curveB"));
    //        }
    //        Curve[] curveArray = new Curve[] { curveA, curveB };
    //        IntPtr intPtr = (new SimpleArrayCurvePointer(curveArray)).ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (UnsafeNativeMethods.ON_Curve_BooleanOperation(intPtr, simpleArrayCurvePointer.NonConstPointer(), 1) >= 1)
    //        {
    //            return simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return new Curve[0];
    //    }

    //    /// <summary>
    //    /// Calculates the boolean union of two or more closed, planar curves. 
    //    /// Note, curves must be co-planar.
    //    /// </summary>
    //    /// <param name="curves">The co-planar curves to union.</param>
    //    /// <returns>Result curves on success, empty array if no union could be calculated.</returns>
    //    public static Curve[] CreateBooleanUnion(IEnumerable<Curve> curves)
    //    {
    //        if (curves == null)
    //        {
    //            throw new ArgumentNullException("curves");
    //        }
    //        IntPtr intPtr = (new SimpleArrayCurvePointer(curves)).ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (UnsafeNativeMethods.ON_Curve_BooleanOperation(intPtr, simpleArrayCurvePointer.NonConstPointer(), 0) >= 1)
    //        {
    //            return simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return new Curve[0];
    //    }

    //    /// <summary>
    //    /// Constructs a curve from a set of control-point locations.
    //    /// </summary>
    //    /// <param name="points">Control points.</param>
    //    /// <param name="degree">Degree of curve. The number of control points must be at least degree+1.</param>
    //    public static Curve CreateControlPointCurve(IEnumerable<Point3d> points, int degree)
    //    {
    //        int num;
    //        Point3d[] constPointArray = Point3dList.GetConstPointArray(points, out num);
    //        if (constPointArray == null || num < 2)
    //        {
    //            return null;
    //        }
    //        if (2 == num)
    //        {
    //            return new LineCurve(constPointArray[0], constPointArray[1]);
    //        }
    //        if (1 == degree && num > 2)
    //        {
    //            return PolylineCurve.FromArray(constPointArray);
    //        }
    //        IntPtr intPtr = UnsafeNativeMethods.ON_NurbsCurve_CreateControlPointCurve(num, constPointArray, degree);
    //        return GeometryBase.CreateGeometryHelper(intPtr, null) as NurbsCurve;
    //    }

    //    /// <summary>
    //    /// Constructs a control-point of degree=3 (or less).
    //    /// </summary>
    //    /// <param name="points">Control points of curve.</param>
    //    public static Curve CreateControlPointCurve(IEnumerable<Point3d> points)
    //    {
    //        return Curve.CreateControlPointCurve(points, 3);
    //    }

    //    /// <summary>
    //    /// Computes the fillet arc for a curve filleting operation.
    //    /// </summary>
    //    /// <param name="curve0">First curve to fillet.</param>
    //    /// <param name="curve1">Second curve to fillet.</param>
    //    /// <param name="radius">Fillet radius.</param>
    //    /// <param name="t0Base">Parameter on curve0 where the fillet ought to start (approximately).</param>
    //    /// <param name="t1Base">Parameter on curve1 where the fillet ought to end (approximately).</param>
    //    /// <returns>The fillet arc on success, or Arc.Unset on failure.</returns>
    //    public static Arc CreateFillet(Curve curve0, Curve curve1, double radius, double t0Base, double t1Base)
    //    {
    //        double num;
    //        double num1;
    //        Plane plane;
    //        Arc unset = Arc.Unset;
    //        if (Curve.GetFilletPoints(curve0, curve1, radius, t0Base, t1Base, out num, out num1, out plane))
    //        {
    //            Vector3d vector3d = curve0.PointAt(num) - plane.Origin;
    //            Vector3d vector3d1 = curve1.PointAt(num1) - plane.Origin;
    //            vector3d.Unitize();
    //            vector3d1.Unitize();
    //            double num2 = Math.Acos(vector3d * vector3d1);
    //            Plane plane1 = new Plane(plane.Origin, vector3d, vector3d1);
    //            unset = new Arc(plane1, plane.Origin, radius, num2);
    //        }
    //        return unset;
    //    }

    //    /// <summary>
    //    /// Creates a tangent arc between two curves and trims or extends the curves to the arc.
    //    /// </summary>
    //    /// <param name="curve0">The first curve to fillet.</param>
    //    /// <param name="point0">
    //    /// A point on the first curve that is near the end where the fillet will
    //    /// be created.
    //    /// </param>
    //    /// <param name="curve1">The second curve to fillet.</param>
    //    /// <param name="point1">
    //    /// A point on the second curve that is near the end where the fillet will
    //    /// be created.
    //    /// </param>
    //    /// <param name="radius">The radius of the fillet.</param>
    //    /// <param name="join">Join the output curves.</param>
    //    /// <param name="trim">
    //    /// Trim copies of the input curves to the output fillet curve.
    //    /// </param>
    //    /// <param name="arcExtension">
    //    /// Applies when arcs are filleted but need to be extended to meet the
    //    /// fillet curve or chamfer line. If true, then the arc is extended
    //    /// maintaining its validity. If false, then the arc is extended with a
    //    /// line segment, which is joined to the arc converting it to a polycurve.
    //    /// </param>
    //    /// <param name="tolerance">
    //    /// The tolerance, generally the document's absolute tolerance.
    //    /// </param>
    //    /// <param name="angleTolerance"></param>
    //    /// <returns>
    //    /// The results of the fillet operation. The number of output curves depends
    //    /// on the input curves and the values of the parameters that were used
    //    /// during the fillet operation. In most cases, the output array will contain
    //    /// either one or three curves, although two curves can be returned if the
    //    /// radius is zero and join = false.
    //    /// For example, if both join and trim = true, then the output curve
    //    /// will be a polycurve containing the fillet curve joined with trimmed copies
    //    /// of the input curves. If join = false and trim = true, then three curves,
    //    /// the fillet curve and trimmed copies of the input curves, will be returned.
    //    /// If both join and trim = false, then just the fillet curve is returned.
    //    /// </returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_filletcurves.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_filletcurves.cs" lang="cs" />
    //    /// <code source="examples\py\ex_filletcurves.py" lang="py" />
    //    /// </example>
    //    public static Curve[] CreateFilletCurves(Curve curve0, Point3d point0, Curve curve1, Point3d point1, double radius, bool join, bool trim, bool arcExtension, double tolerance, double angleTolerance)
    //    {
    //        Curve[] nonConstArray;
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            IntPtr intPtr2 = simpleArrayCurvePointer.NonConstPointer();
    //            UnsafeNativeMethods.RHC_RhFilletCurve(intPtr, point0, intPtr1, point1, radius, join, trim, arcExtension, tolerance, angleTolerance, intPtr2);
    //            nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Interpolates a sequence of points. Used by InterpCurve Command
    //    /// This routine works best when degree=3.
    //    /// </summary>
    //    /// <param name="degree">The degree of the curve &gt;=1.  Degree must be odd.</param>
    //    /// <param name="points">
    //    /// Points to interpolate (Count must be &gt;= 2)
    //    /// </param>
    //    /// <returns>interpolated curve on success. null on failure.</returns>
    //    public static Curve CreateInterpolatedCurve(IEnumerable<Point3d> points, int degree)
    //    {
    //        return Curve.CreateInterpolatedCurve(points, degree, CurveKnotStyle.Uniform);
    //    }

    //    /// <summary>
    //    /// Interpolates a sequence of points. Used by InterpCurve Command
    //    /// This routine works best when degree=3.
    //    /// </summary>
    //    /// <param name="degree">The degree of the curve &gt;=1.  Degree must be odd.</param>
    //    /// <param name="points">
    //    /// Points to interpolate. For periodic curves if the final point is a
    //    /// duplicate of the initial point it is  ignored. (Count must be &gt;=2)
    //    /// </param>
    //    /// <param name="knots">
    //    /// Knot-style to use  and specifies if the curve should be periodic.
    //    /// </param>
    //    /// <returns>interpolated curve on success. null on failure.</returns>
    //    public static Curve CreateInterpolatedCurve(IEnumerable<Point3d> points, int degree, CurveKnotStyle knots)
    //    {
    //        return Curve.CreateInterpolatedCurve(points, degree, knots, Vector3d.Unset, Vector3d.Unset);
    //    }

    //    /// <summary>
    //    /// Interpolates a sequence of points. Used by InterpCurve Command
    //    /// This routine works best when degree=3.
    //    /// </summary>
    //    /// <param name="degree">The degree of the curve &gt;=1.  Degree must be odd.</param>
    //    /// <param name="points">
    //    /// Points to interpolate. For periodic curves if the final point is a
    //    /// duplicate of the initial point it is  ignored. (Count must be &gt;=2)
    //    /// </param>
    //    /// <param name="knots">
    //    /// Knot-style to use  and specifies if the curve should be periodic.
    //    /// </param>
    //    /// <param name="startTangent">A starting tangent.</param>
    //    /// <param name="endTangent">An ending tangent.</param>
    //    /// <returns>interpolated curve on success. null on failure.</returns>
    //    public static Curve CreateInterpolatedCurve(IEnumerable<Point3d> points, int degree, CurveKnotStyle knots, Vector3d startTangent, Vector3d endTangent)
    //    {
    //        int num;
    //        if (points == null)
    //        {
    //            throw new ArgumentNullException("points");
    //        }
    //        Point3d[] constPointArray = Point3dList.GetConstPointArray(points, out num);
    //        if (num < 2)
    //        {
    //            throw new InvalidOperationException("Insufficient points for an interpolated curve");
    //        }
    //        if (2 == num && !startTangent.IsValid && !endTangent.IsValid)
    //        {
    //            return new LineCurve(constPointArray[0], constPointArray[1]);
    //        }
    //        if (1 == degree && num > 2 && !startTangent.IsValid && !endTangent.IsValid)
    //        {
    //            return PolylineCurve.FromArray(constPointArray);
    //        }
    //        IntPtr intPtr = UnsafeNativeMethods.RHC_RhinoInterpCurve(degree, num, constPointArray, startTangent, endTangent, (int)knots);
    //        return GeometryBase.CreateGeometryHelper(intPtr, null) as NurbsCurve;
    //    }

    //    /// <summary>
    //    /// Constructs a mean, or average, curve from two curves.
    //    /// </summary>
    //    /// <param name="curveA">A first curve.</param>
    //    /// <param name="curveB">A second curve.</param>
    //    /// <param name="angleToleranceRadians">
    //    /// The angle tolerance, in radians, used to match kinks between curves.
    //    /// If you are unsure how to set this parameter, then either use the
    //    /// document's angle tolerance RhinoDoc.AngleToleranceRadians,
    //    /// or the default value (RhinoMath.UnsetValue)
    //    /// </param>
    //    /// <returns>The average curve, or null on error.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If curveA or curveB are null.</exception>
    //    public static Curve CreateMeanCurve(Curve curveA, Curve curveB, double angleToleranceRadians)
    //    {
    //        if (curveA == null)
    //        {
    //            throw new ArgumentNullException("curveA");
    //        }
    //        if (curveB == null)
    //        {
    //            throw new ArgumentNullException("curveB");
    //        }
    //        IntPtr intPtr = curveA.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoMeanCurve(intPtr, curveB.ConstPointer(), angleToleranceRadians);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Constructs a mean, or average, curve from two curves.
    //    /// </summary>
    //    /// <param name="curveA">A first curve.</param>
    //    /// <param name="curveB">A second curve.</param>
    //    /// <returns>The average curve, or null on error.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If curveA or curveB are null.</exception>
    //    public static Curve CreateMeanCurve(Curve curveA, Curve curveB)
    //    {
    //        return Curve.CreateMeanCurve(curveA, curveB, -1.23432101234321E+308);
    //    }

    //    /// <summary>
    //    /// Creates curves between two open or closed input curves. Uses the control points of the curves for finding tween curves.
    //    /// That means the first control point of first curve is matched to first control point of the second curve and so on.
    //    /// There is no matching of curves direction. Caller must match input curves direction before calling the function.
    //    /// </summary>
    //    /// <param name="curve0">The first, or starting, curve.</param>
    //    /// <param name="curve1">The second, or ending, curve.</param>
    //    /// <param name="numCurves">Number of tween curves to create.</param>
    //    /// <returns>An array of joint curves. This array can be empty.</returns>
    //    public static Curve[] CreateTweenCurves(Curve curve0, Curve curve1, int numCurves)
    //    {
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (!UnsafeNativeMethods.RHC_RhinoTweenCurves(intPtr, intPtr1, numCurves, simpleArrayCurvePointer.NonConstPointer()))
    //        {
    //            return new Curve[0];
    //        }
    //        return simpleArrayCurvePointer.ToNonConstArray();
    //    }

    //    /// <summary>
    //    /// Creates curves between two open or closed input curves. Make the structure of input curves compatible if needed.
    //    /// Refits the input curves to have the same structure. The resulting curves are usually more complex than input unless
    //    /// input curves are compatible and no refit is needed. There is no matching of curves direction.
    //    /// Caller must match input curves direction before calling the function.
    //    /// </summary>
    //    /// <param name="curve0">The first, or starting, curve.</param>
    //    /// <param name="curve1">The second, or ending, curve.</param>
    //    /// <param name="numCurves">Number of tween curves to create.</param>
    //    /// <returns>An array of joint curves. This array can be empty.</returns>
    //    public static Curve[] CreateTweenCurvesWithMatching(Curve curve0, Curve curve1, int numCurves)
    //    {
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (!UnsafeNativeMethods.RHC_RhinoTweenCurvesWithMatching(intPtr, intPtr1, numCurves, simpleArrayCurvePointer.NonConstPointer()))
    //        {
    //            return new Curve[0];
    //        }
    //        return simpleArrayCurvePointer.ToNonConstArray();
    //    }

    //    /// <summary>
    //    /// Creates curves between two open or closed input curves. Use sample points method to make curves compatible.
    //    /// This is how the algorithm workd: Divides the two curves into an equal number of points, finds the midpoint between the 
    //    /// corresponding points on the curves and interpolates the tween curve through those points. There is no matching of curves
    //    /// direction. Caller must match input curves direction before calling the function.
    //    /// </summary>
    //    /// <param name="curve0">The first, or starting, curve.</param>
    //    /// <param name="curve1">The second, or ending, curve.</param>
    //    /// <param name="numCurves">Number of tween curves to create.</param>
    //    /// <param name="numSamples">Number of sample points along input curves.</param>
    //    /// <returns>&gt;An array of joint curves. This array can be empty.</returns>
    //    public static Curve[] CreateTweenCurvesWithSampling(Curve curve0, Curve curve1, int numCurves, int numSamples)
    //    {
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (!UnsafeNativeMethods.RHC_RhinoTweenCurveWithSampling(intPtr, intPtr1, numCurves, numSamples, simpleArrayCurvePointer.NonConstPointer()))
    //        {
    //            return new Curve[0];
    //        }
    //        return simpleArrayCurvePointer.ToNonConstArray();
    //    }

    //    /// <summary>Evaluate the curvature vector at a curve parameter.</summary>
    //    /// <param name="t">Evaluation parameter.</param>
    //    /// <returns>Curvature vector of the curve at the parameter t.</returns>
    //    /// <remarks>No error handling.</remarks>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_addradialdimension.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_addradialdimension.cs" lang="cs" />
    //    /// <code source="examples\py\ex_addradialdimension.py" lang="py" />
    //    /// </example>
    //    public Vector3d CurvatureAt(double t)
    //    {
    //        Vector3d vector3d = new Vector3d();
    //        UnsafeNativeMethods.ON_Curve_GetVector(base.ConstPointer(), 2, t, ref vector3d);
    //        return vector3d;
    //    }

    //    /// <summary>
    //    /// Evaluate the derivatives at the specified curve parameter.
    //    /// </summary>
    //    /// <param name="t">Curve parameter to evaluate.</param>
    //    /// <param name="derivativeCount">Number of derivatives to evaluate, must be at least 0.</param>
    //    /// <returns>An array of vectors that represents all the derivatives starting at zero.</returns>
    //    public Vector3d[] DerivativeAt(double t, int derivativeCount)
    //    {
    //        return this.DerivativeAt(t, derivativeCount, CurveEvaluationSide.Default);
    //    }

    //    /// <summary>
    //    /// Evaluate the derivatives at the specified curve parameter.
    //    /// </summary>
    //    /// <param name="t">Curve parameter to evaluate.</param>
    //    /// <param name="derivativeCount">Number of derivatives to evaluate, must be at least 0.</param>
    //    /// <param name="side">Side of parameter to evaluate. If the parameter is at a kink, 
    //    /// it makes a big difference whether the evaluation is from below or above.</param>
    //    /// <returns>An array of vectors that represents all the derivatives starting at zero.</returns>
    //    public Vector3d[] DerivativeAt(double t, int derivativeCount, CurveEvaluationSide side)
    //    {
    //        if (derivativeCount < 0)
    //        {
    //            throw new InvalidOperationException("The derivativeCount must be larger than or equal to zero");
    //        }
    //        Vector3d[] vector3d = null;
    //        SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d();
    //        IntPtr intPtr = simpleArrayPoint3d.NonConstPointer();
    //        if (UnsafeNativeMethods.ON_Curve_Evaluate(base.ConstPointer(), derivativeCount, (int)side, t, intPtr))
    //        {
    //            Point3d[] array = simpleArrayPoint3d.ToArray();
    //            vector3d = new Vector3d[(int)array.Length];
    //            for (int i = 0; i < (int)array.Length; i++)
    //            {
    //                vector3d[i] = new Vector3d(array[i]);
    //            }
    //        }
    //        simpleArrayPoint3d.Dispose();
    //        return vector3d;
    //    }

    //    /// <summary>
    //    /// For derived class implementers.
    //    /// <para>This method is called with argument true when class user calls Dispose(), while with argument false when
    //    /// the Garbage Collector invokes the finalizer, or Finalize() method.</para>
    //    /// <para>You must reclaim all used unmanaged resources in both cases, and can use this chance to call Dispose on disposable fields if the argument is true.</para>
    //    /// <para>Also, you must call the base virtual method within your overriding method.</para>
    //    /// </summary>
    //    /// <param name="disposing">true if the call comes from the Dispose() method; false if it comes from the Garbage Collector finalizer.</param>
    //    protected override void Dispose(bool disposing)
    //    {
    //        if (IntPtr.Zero != this.m_pCurveDisplay)
    //        {
    //            UnsafeNativeMethods.CurveDisplay_Delete(this.m_pCurveDisplay);
    //            this.m_pCurveDisplay = IntPtr.Zero;
    //        }
    //        base.Dispose(disposing);
    //    }

    //    /// <summary>
    //    /// Divides this curve at fixed steps along a defined contour line.
    //    /// </summary>
    //    /// <param name="contourStart">The start of the contouring line.</param>
    //    /// <param name="contourEnd">The end of the contouring line.</param>
    //    /// <param name="interval">A distance to measure on the contouring axis.</param>
    //    /// <returns>An array of points; or null on error.</returns>
    //    public Point3d[] DivideAsContour(Point3d contourStart, Point3d contourEnd, double interval)
    //    {
    //        Point3d[] array = null;
    //        using (SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d())
    //        {
    //            if (UnsafeNativeMethods.RHC_MakeRhinoContours1(base.ConstPointer(), contourStart, contourEnd, interval, simpleArrayPoint3d.NonConstPointer(), RhinoDoc.ActiveDoc.ModelAbsoluteTolerance))
    //            {
    //                array = simpleArrayPoint3d.ToArray();
    //            }
    //        }
    //        return array;
    //    }

    //    /// <summary>
    //    /// Divide the curve into a number of equal-length segments.
    //    /// </summary>
    //    /// <param name="segmentCount">Segment count. Note that the number of division points may differ from the segment count.</param>
    //    /// <param name="includeEnds">If true, then the points at the start and end of the curve are included.</param>
    //    /// <returns>
    //    /// List of curve parameters at the division points on success, null on failure.
    //    /// </returns>
    //    public double[] DivideByCount(int segmentCount, bool includeEnds)
    //    {
    //        if (segmentCount < 1)
    //        {
    //            return null;
    //        }
    //        int num = segmentCount - 1;
    //        if (this.IsClosed && includeEnds)
    //        {
    //            num = segmentCount;
    //        }
    //        else if (includeEnds)
    //        {
    //            num = segmentCount + 1;
    //        }
    //        double[] numArray = new double[num];
    //        if (!UnsafeNativeMethods.RHC_RhinoDivideCurve1(base.ConstPointer(), segmentCount, includeEnds, num, numArray))
    //        {
    //            return null;
    //        }
    //        return numArray;
    //    }

    //    /// <summary>
    //    /// Divide the curve into a number of equal-length segments.
    //    /// </summary>
    //    /// <param name="segmentCount">Segment count. Note that the number of division points may differ from the segment count.</param>
    //    /// <param name="includeEnds">If true, then the points at the start and end of the curve are included.</param>
    //    /// <param name="points">A list of division points. If the function returns successfully, this point-array will be filled in.</param>
    //    /// <returns>Array containing division curve parameters on success, null on failure.</returns>
    //    public double[] DivideByCount(int segmentCount, bool includeEnds, out Point3d[] points)
    //    {
    //        points = null;
    //        if (segmentCount < 1)
    //        {
    //            return null;
    //        }
    //        int num = segmentCount - 1;
    //        if (this.IsClosed && includeEnds)
    //        {
    //            num = segmentCount;
    //        }
    //        else if (includeEnds)
    //        {
    //            num = segmentCount + 1;
    //        }
    //        double[] numArray = new double[num];
    //        IntPtr intPtr = base.ConstPointer();
    //        SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d();
    //        IntPtr intPtr1 = simpleArrayPoint3d.NonConstPointer();
    //        bool flag = UnsafeNativeMethods.RHC_RhinoDivideCurve2(intPtr, segmentCount, includeEnds, num, intPtr1, ref numArray[0]);
    //        if (flag)
    //        {
    //            points = simpleArrayPoint3d.ToArray();
    //        }
    //        simpleArrayPoint3d.Dispose();
    //        if (!flag)
    //        {
    //            return null;
    //        }
    //        return numArray;
    //    }

    //    /// <summary>
    //    /// Divide the curve into specific length segments.
    //    /// </summary>
    //    /// <param name="segmentLength">The length of each and every segment (except potentially the last one).</param>
    //    /// <param name="includeStart">If true, then the points at the start of the curve is included.</param>
    //    /// <returns>Array containing division curve parameters if successful, null on failure.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_dividebylength.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_dividebylength.cs" lang="cs" />
    //    /// <code source="examples\py\ex_dividebylength.py" lang="py" />
    //    /// </example>
    //    public double[] DivideByLength(double segmentLength, bool includeStart)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        SimpleArrayDouble simpleArrayDouble = new SimpleArrayDouble();
    //        IntPtr intPtr1 = simpleArrayDouble.NonConstPointer();
    //        bool flag = UnsafeNativeMethods.RHC_RhinoDivideCurve3(intPtr, segmentLength, includeStart, intPtr1);
    //        double[] array = null;
    //        if (flag)
    //        {
    //            array = simpleArrayDouble.ToArray();
    //        }
    //        simpleArrayDouble.Dispose();
    //        if (!flag)
    //        {
    //            return null;
    //        }
    //        return array;
    //    }

    //    /// <summary>
    //    /// Divide the curve into specific length segments.
    //    /// </summary>
    //    /// <param name="segmentLength">The length of each and every segment (except potentially the last one).</param>
    //    /// <param name="includeStart">If true, then the point at the start of the curve is included.</param>
    //    /// <param name="points">If function is successful, points at each parameter value are returned in points.</param>
    //    /// <returns>Array containing division curve parameters if successful, null on failure.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_dividebylength.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_dividebylength.cs" lang="cs" />
    //    /// <code source="examples\py\ex_dividebylength.py" lang="py" />
    //    /// </example>
    //    public double[] DivideByLength(double segmentLength, bool includeStart, out Point3d[] points)
    //    {
    //        points = null;
    //        double[] numArray = this.DivideByLength(segmentLength, includeStart);
    //        if (numArray != null && (int)numArray.Length > 0)
    //        {
    //            IntPtr intPtr = base.ConstPointer();
    //            points = new Point3d[(int)numArray.Length];
    //            for (int i = 0; i < (int)numArray.Length; i++)
    //            {
    //                double num = numArray[i];
    //                Point3d point3d = new Point3d();
    //                UnsafeNativeMethods.ON_Curve_PointAt(intPtr, num, ref point3d, 0);
    //                points[i] = point3d;
    //            }
    //        }
    //        return numArray;
    //    }

    //    /// <summary>
    //    /// Calculates 3d points on a curve where the linear distance between the points is equal.
    //    /// </summary>
    //    /// <param name="distance">The distance betwen division points.</param>
    //    /// <returns>An array of equidistant points, or null on error.</returns>
    //    public Point3d[] DivideEquidistant(double distance)
    //    {
    //        Point3d[] array = null;
    //        SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d();
    //        if (UnsafeNativeMethods.RHC_RhinoDivideCurveEquidistant(base.ConstPointer(), distance, simpleArrayPoint3d.NonConstPointer()) > 0)
    //        {
    //            array = simpleArrayPoint3d.ToArray();
    //        }
    //        simpleArrayPoint3d.Dispose();
    //        return array;
    //    }

    //    /// <summary>
    //    /// Determines whether two curves travel more or less in the same direction.
    //    /// </summary>
    //    /// <param name="curveA">First curve to test.</param>
    //    /// <param name="curveB">Second curve to test.</param>
    //    /// <returns>true if both curves more or less point in the same direction, 
    //    /// false if they point in the opposite directions.</returns>
    //    public static bool DoDirectionsMatch(Curve curveA, Curve curveB)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_DoCurveDirectionsMatch(curveA.ConstPointer(), curveB.ConstPointer());
    //    }

    //    internal virtual void Draw(DisplayPipeline pipeline, Color color, int thickness)
    //    {
    //        IntPtr intPtr = pipeline.NonConstPointer();
    //        IntPtr intPtr1 = base.ConstPointer();
    //        UnsafeNativeMethods.CRhinoDisplayPipeline_DrawCurve(intPtr, intPtr1, color.ToArgb(), thickness);
    //    }

    //    /// <summary>
    //    /// Constructs an exact duplicate of this Curve.
    //    /// </summary>
    //    /// <seealso cref="M:Rhino.Geometry.Curve.DuplicateCurve" />
    //    public override GeometryBase Duplicate()
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        return GeometryBase.CreateGeometryHelper(UnsafeNativeMethods.ON_Curve_DuplicateCurve(intPtr), null) as Curve;
    //    }

    //    /// <summary>
    //    /// Constructs an exact duplicate of this curve.
    //    /// </summary>
    //    /// <returns>An exact copy of this curve.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_curvereverse.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_curvereverse.cs" lang="cs" />
    //    /// <code source="examples\py\ex_curvereverse.py" lang="py" />
    //    /// </example>
    //    public Curve DuplicateCurve()
    //    {
    //        return this.Duplicate() as Curve;
    //    }

    //    /// <summary>
    //    /// Polylines will be exploded into line segments. ExplodeCurves will
    //    /// return the curves in topological order.
    //    /// </summary>
    //    /// <returns>
    //    /// An array of all the segments that make up this curve.
    //    /// </returns>
    //    public Curve[] DuplicateSegments()
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (UnsafeNativeMethods.RHC_RhinoDuplicateCurveSegments(intPtr, simpleArrayCurvePointer.NonConstPointer()) >= 1)
    //        {
    //            return simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return new Curve[0];
    //    }

    //    internal override GeometryBase DuplicateShallowHelper()
    //    {
    //        return new Curve(IntPtr.Zero, null);
    //    }

    //    /// <summary>
    //    /// Where possible, analytically extends curve to include the given domain. 
    //    /// This will not work on closed curves. The original curve will be identical to the 
    //    /// restriction of the resulting curve to the original curve domain.
    //    /// </summary>
    //    /// <param name="t0">Start of extension domain, if the start is not inside the 
    //    /// Domain of this curve, an attempt will be made to extend the curve.</param>
    //    /// <param name="t1">End of extension domain, if the end is not inside the 
    //    /// Domain of this curve, an attempt will be made to extend the curve.</param>
    //    /// <returns>Extended curve on success, null on failure.</returns>
    //    public Curve Extend(double t0, double t1)
    //    {
    //        return this.TrimExtendHelper(t0, t1, false);
    //    }

    //    /// <summary>
    //    /// Where possible, analytically extends curve to include the given domain. 
    //    /// This will not work on closed curves. The original curve will be identical to the 
    //    /// restriction of the resulting curve to the original curve domain.
    //    /// </summary>
    //    /// <param name="domain">Extension domain.</param>
    //    /// <returns>Extended curve on success, null on failure.</returns>
    //    public Curve Extend(Interval domain)
    //    {
    //        return this.Extend(domain.T0, domain.T1);
    //    }

    //    /// <summary>
    //    /// Extends a curve by a specific length.
    //    /// </summary>
    //    /// <param name="side">Curve end to extend.</param>
    //    /// <param name="length">Length to add to the curve end.</param>
    //    /// <param name="style">Extension style.</param>
    //    /// <returns>A curve with extended ends or null on failure.</returns>
    //    public Curve Extend(CurveEnd side, double length, CurveExtensionStyle style)
    //    {
    //        if (side == CurveEnd.None)
    //        {
    //            return this.DuplicateCurve();
    //        }
    //        length = Math.Max(length, 0);
    //        double num = length;
    //        double num1 = length;
    //        if (side == CurveEnd.End)
    //        {
    //            num = 0;
    //        }
    //        if (side == CurveEnd.Start)
    //        {
    //            num1 = 0;
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoExtendCurve(intPtr, num, num1, Curve.ConvertExtensionStyle(style));
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Extends a curve until it intersects a collection of objects.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="style">The style or type of extension to use.</param>
    //    /// <param name="geometry">A collection of objects. Allowable object types are Curve, Surface, Brep.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_extendcurve.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_extendcurve.cs" lang="cs" />
    //    /// <code source="examples\py\ex_extendcurve.py" lang="py" />
    //    /// </example>
    //    public Curve Extend(CurveEnd side, CurveExtensionStyle style, IEnumerable<GeometryBase> geometry)
    //    {
    //        Curve curve;
    //        if (side == CurveEnd.None)
    //        {
    //            return null;
    //        }
    //        int num = 0;
    //        if (CurveEnd.End == side)
    //        {
    //            num = 1;
    //        }
    //        else if (CurveEnd.Both == side)
    //        {
    //            num = 2;
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        using (SimpleArrayGeometryPointer simpleArrayGeometryPointer = new SimpleArrayGeometryPointer(geometry))
    //        {
    //            IntPtr intPtr1 = simpleArrayGeometryPointer.ConstPointer();
    //            IntPtr intPtr2 = UnsafeNativeMethods.RHC_RhinoExtendCurve1(intPtr, Curve.ConvertExtensionStyle(style), num, intPtr1);
    //            curve = GeometryBase.CreateGeometryHelper(intPtr2, null) as Curve;
    //        }
    //        return curve;
    //    }

    //    /// <summary>
    //    /// Extends a curve to a point.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="style">The style or type of extension to use.</param>
    //    /// <param name="endPoint">A new end point.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    public Curve Extend(CurveEnd side, CurveExtensionStyle style, Point3d endPoint)
    //    {
    //        if (side == CurveEnd.None)
    //        {
    //            return null;
    //        }
    //        int num = 0;
    //        if (CurveEnd.End == side)
    //        {
    //            num = 1;
    //        }
    //        else if (CurveEnd.Both == side)
    //        {
    //            num = 2;
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoExtendCurve2(intPtr, Curve.ConvertExtensionStyle(style), num, endPoint);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Extends a curve by an Arc until it intersects a collection of objects.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="geometry">A collection of objects. Allowable object types are Curve, Surface, Brep.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    public Curve ExtendByArc(CurveEnd side, IEnumerable<GeometryBase> geometry)
    //    {
    //        return this.Extend(side, CurveExtensionStyle.Arc, geometry);
    //    }

    //    /// <summary>
    //    /// Extends a curve by a line until it intersects a collection of objects.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="geometry">A collection of objects. Allowable object types are Curve, Surface, Brep.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    public Curve ExtendByLine(CurveEnd side, IEnumerable<GeometryBase> geometry)
    //    {
    //        return this.Extend(side, CurveExtensionStyle.Line, geometry);
    //    }

    //    /// <summary>
    //    /// Extends a curve on a surface.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="surface">Surface that contains the curve.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    public Curve ExtendOnSurface(CurveEnd side, Surface surface)
    //    {
    //        if (surface == null)
    //        {
    //            throw new ArgumentNullException("surface");
    //        }
    //        Brep brep = surface.ToBrep();
    //        if (brep == null)
    //        {
    //            return null;
    //        }
    //        return this.ExtendOnSurface(side, brep.Faces[0]);
    //    }

    //    /// <summary>
    //    /// Extends a curve on a surface.
    //    /// </summary>
    //    /// <param name="side">The end of the curve to extend.</param>
    //    /// <param name="face">BrepFace that contains the curve.</param>
    //    /// <returns>New extended curve result on success, null on failure.</returns>
    //    public Curve ExtendOnSurface(CurveEnd side, BrepFace face)
    //    {
    //        if (face == null)
    //        {
    //            throw new ArgumentNullException("face");
    //        }
    //        if (side == CurveEnd.None)
    //        {
    //            return null;
    //        }
    //        int num = 0;
    //        if (CurveEnd.End == side)
    //        {
    //            num = 1;
    //        }
    //        else if (CurveEnd.Both == side)
    //        {
    //            num = 2;
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoExtendCrvOnSrf(intPtr, face.ConstPointer(), num);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Fairs a curve object. Fair works best on degree 3 (cubic) curves. Attempts to 
    //    /// remove large curvature variations while limiting the geometry changes to be no 
    //    /// more than the specified tolerance. 
    //    /// </summary>
    //    /// <param name="distanceTolerance">Maximum allowed distance the faired curve is allowed to deviate from the input.</param>
    //    /// <param name="angleTolerance">(in radians) kinks with angles &lt;= angleTolerance are smoothed out 0.05 is a good default.</param>
    //    /// <param name="clampStart">The number of (control vertices-1) to preserve at start. 
    //    /// <para>0 = preserve start point</para>
    //    /// <para>1 = preserve start point and 1st derivative</para>
    //    /// <para>2 = preserve start point, 1st and 2nd derivative</para>
    //    /// </param>
    //    /// <param name="clampEnd">Same as clampStart.</param>
    //    /// <param name="iterations">The number of iteratoins to use in adjusting the curve.</param>
    //    /// <returns>Returns new faired Curve on success, null on failure.</returns>
    //    public Curve Fair(double distanceTolerance, double angleTolerance, int clampStart, int clampEnd, int iterations)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoFairCurve(intPtr, distanceTolerance, angleTolerance, clampStart, clampEnd, iterations);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Fits a new curve through an existing curve.
    //    /// </summary>
    //    /// <param name="degree">The degree of the returned Curve. Must be bigger than 1.</param>
    //    /// <param name="fitTolerance">The fitting tolerance. If fitTolerance is RhinoMath.UnsetValue or &lt;=0.0,
    //    /// the document absolute tolerance is used.</param>
    //    /// <param name="angleTolerance">The kink smoothing tolerance in radians.
    //    /// <para>If angleTolerance is 0.0, all kinks are smoothed</para>
    //    /// <para>If angleTolerance is &gt;0.0, kinks smaller than angleTolerance are smoothed</para>  
    //    /// <para>If angleTolerance is RhinoMath.UnsetValue or &lt;0.0, the document angle tolerance is used for the kink smoothing</para>
    //    /// </param>
    //    /// <returns>Returns a new fitted Curve if successful, null on failure.</returns>
    //    public Curve Fit(int degree, double fitTolerance, double angleTolerance)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoFitCurve(intPtr, degree, fitTolerance, angleTolerance);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>Returns a 3d frame at a parameter.</summary>
    //    /// <param name="t">Evaluation parameter.</param>
    //    /// <param name="plane">The frame is returned here.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool FrameAt(double t, out Plane plane)
    //    {
    //        plane = Plane.WorldXY;
    //        return UnsafeNativeMethods.ON_Curve_FrameAt(base.ConstPointer(), t, ref plane, false);
    //    }

    //    /// <summary>
    //    /// Convert a NURBS curve parameter to a curve parameter.
    //    /// </summary>
    //    /// <param name="nurbsParameter">Nurbs form parameter.</param>
    //    /// <param name="curveParameter">Curve parameter.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>
    //    /// If HasNurbForm returns 2, this function converts the curve parameter to the NURBS curve parameter.
    //    /// </remarks>
    //    public bool GetCurveParameterFromNurbsFormParameter(double nurbsParameter, out double curveParameter)
    //    {
    //        curveParameter = 0;
    //        return UnsafeNativeMethods.ON_Curve_GetNurbParameter(base.ConstPointer(), nurbsParameter, ref curveParameter, true);
    //    }

    //    /// <summary>
    //    /// Computes the distances between two arbitrary curves that overlap.
    //    /// </summary>
    //    /// <param name="curveA">A curve.</param>
    //    /// <param name="curveB">Another curve.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <param name="maxDistance">The maximum distance value. This is an out reference argument.</param>
    //    /// <param name="maxDistanceParameterA">The maximum distance parameter on curve A. This is an out reference argument.</param>
    //    /// <param name="maxDistanceParameterB">The maximum distance parameter on curve B. This is an out reference argument.</param>
    //    /// <param name="minDistance">The minimum distance value. This is an out reference argument.</param>
    //    /// <param name="minDistanceParameterA">The minimum distance parameter on curve A. This is an out reference argument.</param>
    //    /// <param name="minDistanceParameterB">The minimum distance parameter on curve B. This is an out reference argument.</param>
    //    /// <returns>true if the operation succeeded; otherwise false.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_crvdeviation.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_crvdeviation.cs" lang="cs" />
    //    /// <code source="examples\py\ex_crvdeviation.py" lang="py" />
    //    /// </example>
    //    public static bool GetDistancesBetweenCurves(Curve curveA, Curve curveB, double tolerance, out double maxDistance, out double maxDistanceParameterA, out double maxDistanceParameterB, out double minDistance, out double minDistanceParameterA, out double minDistanceParameterB)
    //    {
    //        IntPtr intPtr = curveA.ConstPointer();
    //        IntPtr intPtr1 = curveB.ConstPointer();
    //        maxDistance = 0;
    //        maxDistanceParameterA = 0;
    //        maxDistanceParameterB = 0;
    //        minDistance = 0;
    //        minDistanceParameterA = 0;
    //        minDistanceParameterB = 0;
    //        bool flag = UnsafeNativeMethods.RHC_RhinoGetOverlapDistance(intPtr, intPtr1, tolerance, ref maxDistanceParameterA, ref maxDistanceParameterB, ref maxDistance, ref minDistanceParameterA, ref minDistanceParameterB, ref minDistance);
    //        return flag;
    //    }

    //    /// <summary>
    //    /// Finds points at which to cut a pair of curves so that a fillet of given radius can be inserted.
    //    /// </summary>
    //    /// <param name="curve0">First curve to fillet.</param>
    //    /// <param name="curve1">Second curve to fillet.</param>
    //    /// <param name="radius">Fillet radius.</param>
    //    /// <param name="t0Base">Parameter value for base point on curve0.</param>
    //    /// <param name="t1Base">Parameter value for base point on curve1.</param>
    //    /// <param name="t0">Parameter value of fillet point on curve 0.</param>
    //    /// <param name="t1">Parameter value of fillet point on curve 1.</param>
    //    /// <param name="filletPlane">
    //    /// The fillet is contained in this plane with the fillet center at the plane origin.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>
    //    /// A fillet point is a pair of curve parameters (t0,t1) such that there is a circle
    //    /// of radius point3 tangent to curve c0 at t0 and tangent to curve c1 at t1. Of all possible
    //    /// fillet points this function returns the one which is the closest to the base point
    //    /// t0Base, t1Base. Distance from the base point is measured by the sum of arc lengths
    //    /// along the two curves. 
    //    /// </remarks>
    //    public static bool GetFilletPoints(Curve curve0, Curve curve1, double radius, double t0Base, double t1Base, out double t0, out double t1, out Plane filletPlane)
    //    {
    //        t0 = 0;
    //        t1 = 0;
    //        filletPlane = new Plane();
    //        if (curve0 == null || curve1 == null)
    //        {
    //            return false;
    //        }
    //        IntPtr intPtr = curve0.ConstPointer();
    //        IntPtr intPtr1 = curve1.ConstPointer();
    //        bool flag = UnsafeNativeMethods.RHC_GetFilletPoints(intPtr, intPtr1, radius, t0Base, t1Base, ref t0, ref t1, ref filletPlane);
    //        return flag;
    //    }

    //    /// <summary>
    //    /// Gets the length of the curve with a fractional tolerance of 1.0e-8.
    //    /// </summary>
    //    /// <returns>The length of the curve on success, or zero on failure.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_arclengthpoint.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_arclengthpoint.cs" lang="cs" />
    //    /// <code source="examples\py\ex_arclengthpoint.py" lang="py" />
    //    /// </example>
    //    public double GetLength()
    //    {
    //        return this.GetLength(1E-08);
    //    }

    //    /// <summary>Get the length of the curve.</summary>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision. 
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <returns>The length of the curve on success, or zero on failure.</returns>
    //    public double GetLength(double fractionalTolerance)
    //    {
    //        double num = 0;
    //        Interval unset = Interval.Unset;
    //        if (UnsafeNativeMethods.ON_Curve_GetLength(base.ConstPointer(), ref num, fractionalTolerance, unset, true))
    //        {
    //            return num;
    //        }
    //        return 0;
    //    }

    //    /// <summary>Get the length of a sub-section of the curve with a fractional tolerance of 1e-8.</summary>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve (must be non-decreasing).
    //    /// </param>
    //    /// <returns>The length of the sub-curve on success, or zero on failure.</returns>
    //    public double GetLength(Interval subdomain)
    //    {
    //        return this.GetLength(1E-08, subdomain);
    //    }

    //    /// <summary>Get the length of a sub-section of the curve.</summary>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision. 
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve (must be non-decreasing).
    //    /// </param>
    //    /// <returns>The length of the sub-curve on success, or zero on failure.</returns>
    //    public double GetLength(double fractionalTolerance, Interval subdomain)
    //    {
    //        double num = 0;
    //        if (!UnsafeNativeMethods.ON_Curve_GetLength(base.ConstPointer(), ref num, fractionalTolerance, subdomain, false))
    //        {
    //            return 0;
    //        }
    //        return num;
    //    }

    //    /// <summary>
    //    /// Searches for a derivative, tangent, or curvature discontinuity.
    //    /// </summary>
    //    /// <param name="continuityType">Type of continuity to search for.</param>
    //    /// <param name="t0">
    //    /// Search begins at t0. If there is a discontinuity at t0, it will be ignored. This makes it
    //    /// possible to repeatedly call GetNextDiscontinuity() and step through the discontinuities.
    //    /// </param>
    //    /// <param name="t1">
    //    /// (t0 != t1)  If there is a discontinuity at t1 it will be ignored unless continuityType is
    //    /// a locus discontinuity type and t1 is at the start or end of the curve.
    //    /// </param>
    //    /// <param name="t">If a discontinuity is found, then t reports the parameter at the discontinuity.</param>
    //    /// <returns>
    //    /// Parametric continuity tests c = (C0_continuous, ..., G2_continuous):
    //    ///  true if a parametric discontinuity was found strictly between t0 and t1. Note well that
    //    ///  all curves are parametrically continuous at the ends of their domains.
    //    /// Locus continuity tests c = (C0_locus_continuous, ...,G2_locus_continuous):
    //    ///  true if a locus discontinuity was found strictly between t0 and t1 or at t1 is the at the end
    //    ///  of a curve. Note well that all open curves (IsClosed()=false) are locus discontinuous at the
    //    ///  ends of their domains.  All closed curves (IsClosed()=true) are at least C0_locus_continuous at 
    //    ///  the ends of their domains.
    //    /// </returns>
    //    public bool GetNextDiscontinuity(Continuity continuityType, double t0, double t1, out double t)
    //    {
    //        t = -1.23432101234321E+308;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_GetNextDiscontinuity(intPtr, (int)continuityType, t0, t1, ref t);
    //    }

    //    /// <summary>Convert a curve parameter to a NURBS curve parameter.</summary>
    //    /// <param name="curveParameter">Curve parameter.</param>
    //    /// <param name="nurbsParameter">Nurbs form parameter.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>
    //    /// If GetNurbForm returns 2, this function converts the curve parameter to the NURBS curve parameter.
    //    /// </remarks>
    //    public bool GetNurbsFormParameterFromCurveParameter(double curveParameter, out double nurbsParameter)
    //    {
    //        nurbsParameter = 0;
    //        return UnsafeNativeMethods.ON_Curve_GetNurbParameter(base.ConstPointer(), curveParameter, ref nurbsParameter, false);
    //    }

    //    /// <summary>
    //    /// Gets a collection of perpendicular frames along the curve. Perpendicular frames 
    //    /// are also known as 'Zero-twisting frames' and they minimize rotation from one frame to the next.
    //    /// </summary>
    //    /// <param name="parameters">A collection of <i>strictly increasing</i> curve parameters to place perpendicular frames on.</param>
    //    /// <returns>An array of perpendicular frames on success or null on failure.</returns>
    //    /// <exception cref="T:System.InvalidOperationException">Thrown when the curve parameters are not increasing.</exception>
    //    public Plane[] GetPerpendicularFrames(IEnumerable<double> parameters)
    //    {
    //        RhinoList<double> nums = new RhinoList<double>();
    //        double num = double.MinValue;
    //        foreach (double parameter in parameters)
    //        {
    //            if (parameter <= num)
    //            {
    //                throw new InvalidOperationException("Curve parameters must be strictly increasing");
    //            }
    //            nums.Add(parameter);
    //            num = parameter;
    //        }
    //        if (nums.Count < 2)
    //        {
    //            return null;
    //        }
    //        double[] array = nums.ToArray();
    //        int length = (int)array.Length;
    //        Plane[] planeArray = new Plane[length];
    //        IntPtr intPtr = base.ConstPointer();
    //        int num1 = UnsafeNativeMethods.RHC_RhinoGet1RailFrames(intPtr, length, array, planeArray);
    //        if (num1 == length)
    //        {
    //            return planeArray;
    //        }
    //        if (num1 <= 0)
    //        {
    //            return null;
    //        }
    //        Plane[] planeArray1 = new Plane[num1];
    //        Array.Copy(planeArray, planeArray1, num1);
    //        return planeArray1;
    //    }

    //    /// <summary>
    //    /// Does a NURBS curve representation of this curve exist?
    //    /// </summary>
    //    /// <returns>
    //    /// 0   unable to create NURBS representation with desired accuracy.
    //    /// 1   success - NURBS parameterization matches the curve's to the desired accuracy
    //    /// 2   success - NURBS point locus matches the curve's and the domain of the NURBS
    //    ///               curve is correct. However, This curve's parameterization and the
    //    ///               NURBS curve parameterization may not match. This situation happens
    //    ///               when getting NURBS representations of curves that have a
    //    ///               transendental parameterization like circles.
    //    /// </returns>
    //    public int HasNurbsForm()
    //    {
    //        return UnsafeNativeMethods.ON_Curve_HasNurbForm(base.ConstPointer());
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by an arc or circle within RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <returns>
    //    /// true if the curve can be represented by an arc or a circle within tolerance.
    //    /// </returns>
    //    public bool IsArc()
    //    {
    //        return this.IsArc(1E-12);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by an arc or circle within the given tolerance.
    //    /// </summary>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>
    //    /// true if the curve can be represented by an arc or a circle within tolerance.
    //    /// </returns>
    //    public bool IsArc(double tolerance)
    //    {
    //        Arc arc = new Arc();
    //        Plane worldXY = Plane.WorldXY;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsArc(intPtr, 2, ref worldXY, ref arc, tolerance);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by a circle within RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <returns>
    //    /// true if the Curve can be represented by a circle within tolerance.
    //    /// </returns>
    //    public bool IsCircle()
    //    {
    //        return this.IsCircle(1E-12);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by a circle within the given tolerance.
    //    /// </summary>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>
    //    /// true if the curve can be represented by a circle to within tolerance.
    //    /// </returns>
    //    public bool IsCircle(double tolerance)
    //    {
    //        Arc arc;
    //        if (!this.TryGetArc(out arc, tolerance))
    //        {
    //            return false;
    //        }
    //        return arc.IsCircle;
    //    }

    //    /// <summary>
    //    /// Decide if it makes sense to close off this curve by moving the endpoint 
    //    /// to the start based on start-end gap size and length of curve as 
    //    /// approximated by chord defined by 6 points.
    //    /// </summary>
    //    /// <param name="tolerance">
    //    /// Maximum allowable distance between start and end. 
    //    /// If start - end gap is greater than tolerance, this function will return false.
    //    /// </param>
    //    /// <returns>true if start and end points are close enough based on above conditions.</returns>
    //    public bool IsClosable(double tolerance)
    //    {
    //        return this.IsClosable(tolerance, 0, 10);
    //    }

    //    /// <summary>
    //    /// Decide if it makes sense to close off this curve by moving the endpoint
    //    /// to the start based on start-end gap size and length of curve as
    //    /// approximated by chord defined by 6 points.
    //    /// </summary>
    //    /// <param name="tolerance">
    //    /// Maximum allowable distance between start and end. 
    //    /// If start - end gap is greater than tolerance, this function will return false.
    //    /// </param>
    //    /// <param name="minimumAbsoluteSize">
    //    /// If greater than 0.0 and none of the interior sampled points are at
    //    /// least minimumAbsoluteSize from start, this function will return false.
    //    /// </param>
    //    /// <param name="minimumRelativeSize">
    //    /// If greater than 1.0 and chord length is less than 
    //    /// minimumRelativeSize*gap, this function will return false.
    //    /// </param>
    //    /// <returns>true if start and end points are close enough based on above conditions.</returns>
    //    public bool IsClosable(double tolerance, double minimumAbsoluteSize, double minimumRelativeSize)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_IsClosable(base.ConstPointer(), tolerance, minimumAbsoluteSize, minimumRelativeSize);
    //    }

    //    /// <summary>
    //    /// Test continuity at a curve parameter value.
    //    /// </summary>
    //    /// <param name="continuityType">Type of continuity to test for.</param>
    //    /// <param name="t">Parameter to test.</param>
    //    /// <returns>
    //    /// true if the curve has at least the c type continuity at the parameter t.
    //    /// </returns>
    //    public bool IsContinuous(Continuity continuityType, double t)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_IsContinuous(base.ConstPointer(), (int)continuityType, t);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by an ellipse within RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <returns>
    //    /// true if the Curve can be represented by an ellipse within tolerance.
    //    /// </returns>
    //    public bool IsEllipse()
    //    {
    //        return this.IsEllipse(1E-12);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it can be represented by an ellipse within a given tolerance.
    //    /// </summary>
    //    /// <param name="tolerance">Tolerance to use for checking.</param>
    //    /// <returns>
    //    /// true if the Curve can be represented by an ellipse within tolerance.
    //    /// </returns>
    //    public bool IsEllipse(double tolerance)
    //    {
    //        Plane worldXY = Plane.WorldXY;
    //        Ellipse ellipse = new Ellipse();
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsEllipse(intPtr, 2, ref worldXY, ref ellipse, tolerance);
    //    }

    //    /// <summary>Test a curve to see if it lies in a specific plane.</summary>
    //    /// <param name="testPlane">Plane to test for.</param>
    //    /// <returns>
    //    /// true if the maximum distance from the curve to the testPlane is &lt;= RhinoMath.ZeroTolerance.
    //    /// </returns>
    //    public bool IsInPlane(Plane testPlane)
    //    {
    //        return this.IsInPlane(testPlane, 1E-12);
    //    }

    //    /// <summary>Test a curve to see if it lies in a specific plane.</summary>
    //    /// <param name="testPlane">Plane to test for.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>
    //    /// true if the maximum distance from the curve to the testPlane is &lt;= tolerance.
    //    /// </returns>
    //    public bool IsInPlane(Plane testPlane, double tolerance)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_IsInPlane(base.ConstPointer(), ref testPlane, tolerance);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it is linear to within RhinoMath.ZeroTolerance units (1e-12).
    //    /// </summary>
    //    /// <returns>true if the curve is linear.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_addradialdimension.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_addradialdimension.cs" lang="cs" />
    //    /// <code source="examples\py\ex_addradialdimension.py" lang="py" />
    //    /// </example>
    //    public bool IsLinear()
    //    {
    //        return this.IsLinear(1E-12);
    //    }

    //    /// <summary>
    //    /// Test a curve to see if it is linear to within the custom tolerance.
    //    /// </summary>
    //    /// <param name="tolerance">Tolerance to use when checking linearity.</param>
    //    /// <returns>
    //    /// true if the ends of the curve are farther than tolerance apart
    //    /// and the maximum distance from any point on the curve to
    //    /// the line segment connecting the curve ends is &lt;= tolerance.
    //    /// </returns>
    //    public bool IsLinear(double tolerance)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_IsLinear(base.ConstPointer(), tolerance);
    //    }

    //    /// <summary>Test a curve for planarity.</summary>
    //    /// <returns>
    //    /// true if the curve is planar (flat) to within RhinoMath.ZeroTolerance units (1e-12).
    //    /// </returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_addradialdimension.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_addradialdimension.cs" lang="cs" />
    //    /// <code source="examples\py\ex_addradialdimension.py" lang="py" />
    //    /// </example>
    //    public bool IsPlanar()
    //    {
    //        return this.IsPlanar(1E-12);
    //    }

    //    /// <summary>Test a curve for planarity.</summary>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>
    //    /// true if there is a plane such that the maximum distance from the curve to the plane is &lt;= tolerance.
    //    /// </returns>
    //    public bool IsPlanar(double tolerance)
    //    {
    //        Plane worldXY = Plane.WorldXY;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsPlanar(intPtr, true, ref worldXY, tolerance);
    //    }

    //    /// <summary>
    //    /// Several types of Curve can have the form of a polyline
    //    /// including a degree 1 NurbsCurve, a PolylineCurve,
    //    /// and a PolyCurve all of whose segments are some form of
    //    /// polyline. IsPolyline tests a curve to see if it can be
    //    /// represented as a polyline.
    //    /// </summary>
    //    /// <returns>true if this curve can be represented as a polyline; otherwise, false.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_addradialdimension.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_addradialdimension.cs" lang="cs" />
    //    /// <code source="examples\py\ex_addradialdimension.py" lang="py" />
    //    /// </example>
    //    public bool IsPolyline()
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsPolyline1(intPtr, IntPtr.Zero) != 0;
    //    }

    //    /// <summary>Used to quickly find short curves.</summary>
    //    /// <param name="tolerance">Length threshold value for "shortness".</param>
    //    /// <returns>true if the length of the curve is &lt;= tolerance.</returns>
    //    /// <remarks>Faster than calling Length() and testing the result.</remarks>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_dividebylength.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_dividebylength.cs" lang="cs" />
    //    /// <code source="examples\py\ex_dividebylength.py" lang="py" />
    //    /// </example>
    //    public bool IsShort(double tolerance)
    //    {
    //        Interval unset = Interval.Unset;
    //        return UnsafeNativeMethods.ON_Curve_IsShort(base.ConstPointer(), tolerance, unset, true);
    //    }

    //    /// <summary>Used to quickly find short curves.</summary>
    //    /// <param name="tolerance">Length threshold value for "shortness".</param>
    //    /// <param name="subdomain">
    //    /// The test is performed on the interval that is the intersection of subdomain with Domain()
    //    /// </param>
    //    /// <returns>true if the length of the curve is &lt;= tolerance.</returns>
    //    /// <remarks>Faster than calling Length() and testing the result.</remarks>
    //    public bool IsShort(double tolerance, Interval subdomain)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_IsShort(base.ConstPointer(), tolerance, subdomain, false);
    //    }

    //    /// <summary>
    //    /// Joins a collection of curve segments together.
    //    /// </summary>
    //    /// <param name="inputCurves">Curve segments to join.</param>
    //    /// <returns>An array of curves which contains.</returns>
    //    public static Curve[] JoinCurves(IEnumerable<Curve> inputCurves)
    //    {
    //        return Curve.JoinCurves(inputCurves, 0, false);
    //    }

    //    /// <summary>
    //    /// Joins a collection of curve segments together.
    //    /// </summary>
    //    /// <param name="inputCurves">An array, a list or any enumerable set of curve segments to join.</param>
    //    /// <param name="joinTolerance">Joining tolerance, 
    //    /// i.e. the distance between segment end-points that is allowed.</param>
    //    /// <returns>An array of joint curves. This array can be empty.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_dividebylength.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_dividebylength.cs" lang="cs" />
    //    /// <code source="examples\py\ex_dividebylength.py" lang="py" />
    //    /// </example>
    //    /// <exception cref="T:System.ArgumentNullException">If inputCurves is null.</exception>
    //    public static Curve[] JoinCurves(IEnumerable<Curve> inputCurves, double joinTolerance)
    //    {
    //        return Curve.JoinCurves(inputCurves, joinTolerance, false);
    //    }

    //    /// <summary>
    //    /// Joins a collection of curve segments together.
    //    /// </summary>
    //    /// <param name="inputCurves">An array, a list or any enumerable set of curve segments to join.</param>
    //    /// <param name="joinTolerance">Joining tolerance, 
    //    /// i.e. the distance between segment end-points that is allowed.</param>
    //    /// <param name="preserveDirection">
    //    /// <para>If true, curve endpoints will be compared to curve startpoints.</para>
    //    /// <para>If false, all start and endpoints will be compared and copies of input curves may be reversed in output.</para>
    //    /// </param>
    //    /// <returns>An array of joint curves. This array can be empty.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If inputCurves is null.</exception>
    //    public static Curve[] JoinCurves(IEnumerable<Curve> inputCurves, double joinTolerance, bool preserveDirection)
    //    {
    //        if (inputCurves == null)
    //        {
    //            throw new ArgumentNullException("inputCurves");
    //        }
    //        IntPtr intPtr = (new SimpleArrayCurvePointer(inputCurves)).ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        if (!UnsafeNativeMethods.RHC_RhinoMergeCurves(intPtr, simpleArrayCurvePointer.NonConstPointer(), joinTolerance, preserveDirection))
    //        {
    //            return new Curve[0];
    //        }
    //        return simpleArrayCurvePointer.ToNonConstArray();
    //    }

    //    /// <summary>
    //    /// Gets the parameter along the curve which coincides with a given length along the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="segmentLength">
    //    /// Length of segment to measure. Must be less than or equal to the length of the curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from the curve start point to t equals length.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool LengthParameter(double segmentLength, out double t)
    //    {
    //        return this.LengthParameter(segmentLength, out t, 1E-08);
    //    }

    //    /// <summary>
    //    /// Gets the parameter along the curve which coincides with a given length along the curve.
    //    /// </summary>
    //    /// <param name="segmentLength">
    //    /// Length of segment to measure. Must be less than or equal to the length of the curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from the curve start point to t equals s.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision.
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool LengthParameter(double segmentLength, out double t, double fractionalTolerance)
    //    {
    //        t = 0;
    //        double length = this.GetLength(fractionalTolerance);
    //        if (segmentLength > length)
    //        {
    //            return false;
    //        }
    //        if (length == 0)
    //        {
    //            return false;
    //        }
    //        segmentLength = segmentLength / length;
    //        return this.NormalizedLengthParameter(segmentLength, out t, fractionalTolerance);
    //    }

    //    /// <summary>
    //    /// Gets the parameter along the curve which coincides with a given length along the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="segmentLength">
    //    /// Length of segment to measure. Must be less than or equal to the length of the subdomain.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from the start of the subdomain to t is s.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve rather than the whole curve.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool LengthParameter(double segmentLength, out double t, Interval subdomain)
    //    {
    //        return this.LengthParameter(segmentLength, out t, 1E-08, subdomain);
    //    }

    //    /// <summary>
    //    /// Gets the parameter along the curve which coincides with a given length along the curve.
    //    /// </summary>
    //    /// <param name="segmentLength">
    //    /// Length of segment to measure. Must be less than or equal to the length of the subdomain.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from the start of the subdomain to t is s.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision. 
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve rather than the whole curve.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool LengthParameter(double segmentLength, out double t, double fractionalTolerance, Interval subdomain)
    //    {
    //        t = 0;
    //        double length = this.GetLength(fractionalTolerance);
    //        if (segmentLength > length)
    //        {
    //            return false;
    //        }
    //        if (length == 0)
    //        {
    //            return false;
    //        }
    //        segmentLength = segmentLength / length;
    //        return this.NormalizedLengthParameter(segmentLength, out t, fractionalTolerance, subdomain);
    //    }

    //    /// <summary>
    //    /// If IsClosed, just return true. Otherwise, decide if curve can be closed as 
    //    /// follows: Linear curves polylinear curves with 2 segments, Nurbs with 3 or less 
    //    /// control points cannot be made closed. Also, if tolerance &gt; 0 and the gap between 
    //    /// start and end is larger than tolerance, curve cannot be made closed. 
    //    /// Adjust the curve's endpoint to match its start point.
    //    /// </summary>
    //    /// <param name="tolerance">
    //    /// If nonzero, and the gap is more than tolerance, curve cannot be made closed.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool MakeClosed(double tolerance)
    //    {
    //        if (this.IsClosed)
    //        {
    //            return true;
    //        }
    //        return UnsafeNativeMethods.RHC_RhinoMakeCurveClosed(this.NonConstPointer(), tolerance);
    //    }

    //    /// <summary>
    //    /// Makes adjustments to the ends of one or both input curves so that they meet at a point.
    //    /// </summary>
    //    /// <param name="curveA">1st curve to adjust.</param>
    //    /// <param name="adjustStartCurveA">
    //    /// Which end of the 1st curve to adjust: true is start, false is end.
    //    /// </param>
    //    /// <param name="curveB">2nd curve to adjust.</param>
    //    /// <param name="adjustStartCurveB">
    //    /// which end of the 2nd curve to adjust true==start, false==end.
    //    /// </param>
    //    /// <returns>true on success.</returns>
    //    public static bool MakeEndsMeet(Curve curveA, bool adjustStartCurveA, Curve curveB, bool adjustStartCurveB)
    //    {
    //        IntPtr intPtr = curveA.NonConstPointer();
    //        return UnsafeNativeMethods.RHC_RhinoMakeCurveEndsMeet(intPtr, adjustStartCurveA, curveB.NonConstPointer(), adjustStartCurveB);
    //    }

    //    /// <summary>
    //    /// For derived classes implementers.
    //    /// <para>Defines the necessary implementation to free the instance from being const.</para>
    //    /// </summary>
    //    protected override void NonConstOperation()
    //    {
    //        if (IntPtr.Zero != this.m_pCurveDisplay)
    //        {
    //            UnsafeNativeMethods.CurveDisplay_Delete(this.m_pCurveDisplay);
    //            this.m_pCurveDisplay = IntPtr.Zero;
    //        }
    //        base.NonConstOperation();
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Normalized arc length parameter. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from its start to t is arc_length.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool NormalizedLengthParameter(double s, out double t)
    //    {
    //        return this.NormalizedLengthParameter(s, out t, 1E-08);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Normalized arc length parameter. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from its start to t is arc_length.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision. 
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool NormalizedLengthParameter(double s, out double t, double fractionalTolerance)
    //    {
    //        t = 0;
    //        Interval unset = Interval.Unset;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_GetNormalizedArcLengthPoint(intPtr, s, ref t, fractionalTolerance, unset, true);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Normalized arc length parameter. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from its start to t is arc_length.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool NormalizedLengthParameter(double s, out double t, Interval subdomain)
    //    {
    //        return this.NormalizedLengthParameter(s, out t, 1E-08, subdomain);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Normalized arc length parameter. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="t">
    //    /// Parameter such that the length of the curve from its start to t is arc_length.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision. 
    //    /// fabs(("exact" length from start to t) - arc_length)/arc_length &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve.
    //    /// </param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool NormalizedLengthParameter(double s, out double t, double fractionalTolerance, Interval subdomain)
    //    {
    //        t = 0;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_GetNormalizedArcLengthPoint(intPtr, s, ref t, fractionalTolerance, subdomain, false);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Array of normalized arc length parameters. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="absoluteTolerance">
    //    /// If absoluteTolerance &gt; 0, then the difference between (s[i+1]-s[i])*curve_length 
    //    /// and the length of the curve segment from t[i] to t[i+1] will be &lt;= absoluteTolerance.
    //    /// </param>
    //    /// <returns>
    //    /// If successful, array of curve parameters such that the length of the curve from its start to t[i] is s[i]*curve_length. 
    //    /// Null on failure.
    //    /// </returns>
    //    public double[] NormalizedLengthParameters(double[] s, double absoluteTolerance)
    //    {
    //        return this.NormalizedLengthParameters(s, absoluteTolerance, 1E-08);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Array of normalized arc length parameters. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="absoluteTolerance">
    //    /// If absoluteTolerance &gt; 0, then the difference between (s[i+1]-s[i])*curve_length 
    //    /// and the length of the curve segment from t[i] to t[i+1] will be &lt;= absoluteTolerance.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision for each segment. 
    //    /// fabs("true" length - actual length)/(actual length) &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <returns>
    //    /// If successful, array of curve parameters such that the length of the curve from its start to t[i] is s[i]*curve_length. 
    //    /// Null on failure.
    //    /// </returns>
    //    public double[] NormalizedLengthParameters(double[] s, double absoluteTolerance, double fractionalTolerance)
    //    {
    //        int length = (int)s.Length;
    //        double[] numArray = new double[length];
    //        Interval unset = Interval.Unset;
    //        if (UnsafeNativeMethods.ON_Curve_GetNormalizedArcLengthPoints(base.ConstPointer(), length, s, numArray, absoluteTolerance, fractionalTolerance, unset, true))
    //        {
    //            return numArray;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve. 
    //    /// A fractional tolerance of 1e-8 is used in this version of the function.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Array of normalized arc length parameters. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="absoluteTolerance">
    //    /// If absoluteTolerance &gt; 0, then the difference between (s[i+1]-s[i])*curve_length 
    //    /// and the length of the curve segment from t[i] to t[i+1] will be &lt;= absoluteTolerance.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve. 
    //    /// A 0.0 s value corresponds to subdomain-&gt;Min() and a 1.0 s value corresponds to subdomain-&gt;Max().
    //    /// </param>
    //    /// <returns>
    //    /// If successful, array of curve parameters such that the length of the curve from its start to t[i] is s[i]*curve_length. 
    //    /// Null on failure.
    //    /// </returns>
    //    public double[] NormalizedLengthParameters(double[] s, double absoluteTolerance, Interval subdomain)
    //    {
    //        return this.NormalizedLengthParameters(s, absoluteTolerance, 1E-08, subdomain);
    //    }

    //    /// <summary>
    //    /// Input the parameter of the point on the curve that is a prescribed arc length from the start of the curve.
    //    /// </summary>
    //    /// <param name="s">
    //    /// Array of normalized arc length parameters. 
    //    /// E.g., 0 = start of curve, 1/2 = midpoint of curve, 1 = end of curve.
    //    /// </param>
    //    /// <param name="absoluteTolerance">
    //    /// If absoluteTolerance &gt; 0, then the difference between (s[i+1]-s[i])*curve_length 
    //    /// and the length of the curve segment from t[i] to t[i+1] will be &lt;= absoluteTolerance.
    //    /// </param>
    //    /// <param name="fractionalTolerance">
    //    /// Desired fractional precision for each segment. 
    //    /// fabs("true" length - actual length)/(actual length) &lt;= fractionalTolerance.
    //    /// </param>
    //    /// <param name="subdomain">
    //    /// The calculation is performed on the specified sub-domain of the curve. 
    //    /// A 0.0 s value corresponds to subdomain-&gt;Min() and a 1.0 s value corresponds to subdomain-&gt;Max().
    //    /// </param>
    //    /// <returns>
    //    /// If successful, array of curve parameters such that the length of the curve from its start to t[i] is s[i]*curve_length. 
    //    /// Null on failure.
    //    /// </returns>
    //    public double[] NormalizedLengthParameters(double[] s, double absoluteTolerance, double fractionalTolerance, Interval subdomain)
    //    {
    //        int length = (int)s.Length;
    //        double[] numArray = new double[length];
    //        if (UnsafeNativeMethods.ON_Curve_GetNormalizedArcLengthPoints(base.ConstPointer(), length, s, numArray, absoluteTolerance, fractionalTolerance, subdomain, false))
    //        {
    //            return numArray;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// Offsets this curve. If you have a nice offset, then there will be one entry in 
    //    /// the array. If the original curve had kinks or the offset curve had self 
    //    /// intersections, you will get multiple segments in the offset_curves[] array.
    //    /// </summary>
    //    /// <param name="plane">Offset solution plane.</param>
    //    /// <param name="distance">The positive or negative distance to offset.</param>
    //    /// <param name="tolerance">The offset or fitting tolerance.</param>
    //    /// <param name="cornerStyle">Corner style for offset kinks.</param>
    //    /// <returns>Offset curves on success, null on failure.</returns>
    //    public Curve[] Offset(Plane plane, double distance, double tolerance, CurveOffsetCornerStyle cornerStyle)
    //    {
    //        Point3d origin = plane.Origin + plane.XAxis;
    //        Random random = new Random(1);
    //        for (int i = 0; i < 100; i++)
    //        {
    //            double num = this.Domain.ParameterAt(random.NextDouble());
    //            if (this.IsContinuous(Continuity.G1_continuous, num))
    //            {
    //                Point3d point3d = this.PointAt(num);
    //                Vector3d vector3d = this.TangentAt(num);
    //                if (vector3d.IsParallelTo(plane.ZAxis, RhinoMath.ToRadians(0.1)) == 0)
    //                {
    //                    Vector3d vector3d1 = Vector3d.CrossProduct(vector3d, plane.ZAxis);
    //                    vector3d1.Unitize();
    //                    origin = point3d + (0.001 * vector3d1);
    //                    break;
    //                }
    //            }
    //        }
    //        return this.Offset(origin, plane.Normal, distance, tolerance, cornerStyle);
    //    }

    //    /// <summary>
    //    /// Offsets this curve. If you have a nice offset, then there will be one entry in 
    //    /// the array. If the original curve had kinks or the offset curve had self 
    //    /// intersections, you will get multiple segments in the offset_curves[] array.
    //    /// </summary>
    //    /// <param name="directionPoint">A point that indicates the direction of the offset.</param>
    //    /// <param name="normal">The normal to the offset plane.</param>
    //    /// <param name="distance">The positive or negative distance to offset.</param>
    //    /// <param name="tolerance">The offset or fitting tolerance.</param>
    //    /// <param name="cornerStyle">Corner style for offset kinks.</param>
    //    /// <returns>Offset curves on success, null on failure.</returns>
    //    public Curve[] Offset(Point3d directionPoint, Vector3d normal, double distance, double tolerance, CurveOffsetCornerStyle cornerStyle)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        IntPtr intPtr1 = simpleArrayCurvePointer.NonConstPointer();
    //        bool flag = UnsafeNativeMethods.RHC_RhinoOffsetCurve2(intPtr, distance, directionPoint, normal, (int)cornerStyle, tolerance, intPtr1);
    //        Curve[] nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        simpleArrayCurvePointer.Dispose();
    //        if (!flag)
    //        {
    //            return null;
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Finds a curve by offsetting an existing curve normal to a surface.
    //    /// The caller is responsible for ensuring that the curve lies on the input surface.
    //    /// </summary>
    //    /// <param name="surface">Surface from which normals are calculated.</param>
    //    /// <param name="height">offset distance (distance from surface to result curve)</param>
    //    /// <returns>
    //    /// Offset curve at distance height from the surface.  The offset curve is
    //    /// interpolated through a small number of points so if the surface is irregular
    //    /// or complicated, the result will not be a very accurate offset.
    //    /// </returns>
    //    public Curve OffsetNormalToSurface(Surface surface, double height)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoOffsetCurveNormal(intPtr, surface.ConstPointer(), height);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Offset this curve on a brep face surface. This curve must lie on the surface.
    //    /// </summary>
    //    /// <param name="face">The brep face on which to offset.</param>
    //    /// <param name="distance">A distance to offset (+)left, (-)right.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If face is null.</exception>
    //    public Curve[] OffsetOnSurface(BrepFace face, double distance, double fittingTolerance)
    //    {
    //        if (face == null)
    //        {
    //            throw new ArgumentNullException("face");
    //        }
    //        int mIndex = face.m_index;
    //        IntPtr intPtr = face.m_brep.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        IntPtr intPtr1 = simpleArrayCurvePointer.NonConstPointer();
    //        IntPtr intPtr2 = base.ConstPointer();
    //        int num = UnsafeNativeMethods.RHC_RhinoOffsetCurveOnSrf(intPtr2, intPtr, mIndex, distance, fittingTolerance, intPtr1);
    //        Curve[] nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        simpleArrayCurvePointer.Dispose();
    //        if (num < 1)
    //        {
    //            return null;
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Offset a curve on a brep face surface. This curve must lie on the surface.
    //    /// <para>This overload allows to specify a surface point at which the offset will pass.</para>
    //    /// </summary>
    //    /// <param name="face">The brep face on which to offset.</param>
    //    /// <param name="throughPoint">2d point on the brep face to offset through.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If face is null.</exception>
    //    public Curve[] OffsetOnSurface(BrepFace face, Point2d throughPoint, double fittingTolerance)
    //    {
    //        if (face == null)
    //        {
    //            throw new ArgumentNullException("face");
    //        }
    //        int mIndex = face.m_index;
    //        IntPtr intPtr = face.m_brep.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        IntPtr intPtr1 = simpleArrayCurvePointer.NonConstPointer();
    //        IntPtr intPtr2 = base.ConstPointer();
    //        int num = UnsafeNativeMethods.RHC_RhinoOffsetCurveOnSrf2(intPtr2, intPtr, mIndex, throughPoint, fittingTolerance, intPtr1);
    //        Curve[] nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        simpleArrayCurvePointer.Dispose();
    //        if (num < 1)
    //        {
    //            return null;
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Offset a curve on a brep face surface. This curve must lie on the surface.
    //    /// <para>This overload allows to specify different offsets for different curve parameters.</para>
    //    /// </summary>
    //    /// <param name="face">The brep face on which to offset.</param>
    //    /// <param name="curveParameters">Curve parameters corresponding to the offset distances.</param>
    //    /// <param name="offsetDistances">distances to offset (+)left, (-)right.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If face, curveParameters or offsetDistances are null.</exception>
    //    public Curve[] OffsetOnSurface(BrepFace face, double[] curveParameters, double[] offsetDistances, double fittingTolerance)
    //    {
    //        if (face == null)
    //        {
    //            throw new ArgumentNullException("face");
    //        }
    //        if (curveParameters == null)
    //        {
    //            throw new ArgumentNullException("curveParameters");
    //        }
    //        if (offsetDistances == null)
    //        {
    //            throw new ArgumentNullException("offsetDistances");
    //        }
    //        int length = (int)curveParameters.Length;
    //        if ((int)offsetDistances.Length != length)
    //        {
    //            throw new ArgumentException("curveParameters and offsetDistances must be the same length");
    //        }
    //        int mIndex = face.m_index;
    //        IntPtr intPtr = face.m_brep.ConstPointer();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer();
    //        IntPtr intPtr1 = simpleArrayCurvePointer.NonConstPointer();
    //        IntPtr intPtr2 = base.ConstPointer();
    //        int num = UnsafeNativeMethods.RHC_RhinoOffsetCurveOnSrf3(intPtr2, intPtr, mIndex, length, curveParameters, offsetDistances, fittingTolerance, intPtr1);
    //        Curve[] nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        simpleArrayCurvePointer.Dispose();
    //        if (num < 1)
    //        {
    //            return null;
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Offset a curve on a surface. This curve must lie on the surface.
    //    /// </summary>
    //    /// <param name="surface">A surface on which to offset.</param>
    //    /// <param name="distance">A distance to offset (+)left, (-)right.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If surface is null.</exception>
    //    public Curve[] OffsetOnSurface(Surface surface, double distance, double fittingTolerance)
    //    {
    //        if (surface == null)
    //        {
    //            throw new ArgumentNullException("surface");
    //        }
    //        Brep brep = Brep.CreateFromSurface(surface);
    //        return this.OffsetOnSurface(brep.Faces[0], distance, fittingTolerance);
    //    }

    //    /// <summary>
    //    /// Offset a curve on a surface. This curve must lie on the surface.
    //    /// <para>This overload allows to specify a surface point at which the offset will pass.</para>
    //    /// </summary>
    //    /// <param name="surface">A surface on which to offset.</param>
    //    /// <param name="throughPoint">2d point on the brep face to offset through.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If surface is null.</exception>
    //    public Curve[] OffsetOnSurface(Surface surface, Point2d throughPoint, double fittingTolerance)
    //    {
    //        if (surface == null)
    //        {
    //            throw new ArgumentNullException("surface");
    //        }
    //        Brep brep = Brep.CreateFromSurface(surface);
    //        return this.OffsetOnSurface(brep.Faces[0], throughPoint, fittingTolerance);
    //    }

    //    /// <summary>
    //    /// Offset this curve on a surface. This curve must lie on the surface.
    //    /// <para>This overload allows to specify different offsets for different curve parameters.</para>
    //    /// </summary>
    //    /// <param name="surface">A surface on which to offset.</param>
    //    /// <param name="curveParameters">Curve parameters corresponding to the offset distances.</param>
    //    /// <param name="offsetDistances">Distances to offset (+)left, (-)right.</param>
    //    /// <param name="fittingTolerance">A fitting tolerance.</param>
    //    /// <returns>Offset curves on success, or null on failure.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If surface, curveParameters or offsetDistances are null.</exception>
    //    public Curve[] OffsetOnSurface(Surface surface, double[] curveParameters, double[] offsetDistances, double fittingTolerance)
    //    {
    //        if (surface == null)
    //        {
    //            throw new ArgumentNullException("surface");
    //        }
    //        Brep brep = Brep.CreateFromSurface(surface);
    //        return this.OffsetOnSurface(brep.Faces[0], curveParameters, offsetDistances, fittingTolerance);
    //    }

    //    /// <summary>
    //    /// Return a 3d frame at a parameter. This is slightly different than FrameAt in
    //    /// that the frame is computed in a way so there is minimal rotation from one
    //    /// frame to the next.
    //    /// </summary>
    //    /// <param name="t">Evaluation parameter.</param>
    //    /// <param name="plane">The frame is returned here.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    public bool PerpendicularFrameAt(double t, out Plane plane)
    //    {
    //        plane = Plane.WorldXY;
    //        return UnsafeNativeMethods.ON_Curve_FrameAt(base.ConstPointer(), t, ref plane, true);
    //    }

    //    /// <summary>
    //    /// Determines whether two coplanar simple closed curves are disjoint or intersect;
    //    /// otherwise, if the regions have a containment relationship, discovers
    //    /// which curve encloses the other.
    //    /// </summary>
    //    /// <param name="curveA">A first curve.</param>
    //    /// <param name="curveB">A second curve.</param>
    //    /// <param name="testPlane">A plane.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <returns>
    //    /// A value indicating the relationship between the first and the second curve.
    //    /// </returns>
    //    public static RegionContainment PlanarClosedCurveRelationship(Curve curveA, Curve curveB, Plane testPlane, double tolerance)
    //    {
    //        IntPtr intPtr = curveA.ConstPointer();
    //        IntPtr intPtr1 = curveB.ConstPointer();
    //        return (RegionContainment)UnsafeNativeMethods.RHC_RhinoPlanarClosedCurveContainmentTest(intPtr, intPtr1, ref testPlane, tolerance);
    //    }

    //    /// <summary>
    //    /// Determines if two coplanar curves collide (intersect).
    //    /// </summary>
    //    /// <param name="curveA">A curve.</param>
    //    /// <param name="curveB">Another curve.</param>
    //    /// <param name="testPlane">A valid plane containing the curves.</param>
    //    /// <param name="tolerance">A tolerance value for intersection.</param>
    //    /// <returns>true if the curves intersect, otherwise false</returns>
    //    public static bool PlanarCurveCollision(Curve curveA, Curve curveB, Plane testPlane, double tolerance)
    //    {
    //        IntPtr intPtr = curveA.ConstPointer();
    //        IntPtr intPtr1 = curveB.ConstPointer();
    //        return UnsafeNativeMethods.RHC_RhinoPlanarCurveCollisionTest(intPtr, intPtr1, ref testPlane, tolerance);
    //    }

    //    /// <summary>Evaluates point at a curve parameter.</summary>
    //    /// <param name="t">Evaluation parameter.</param>
    //    /// <returns>Point (location of curve at the parameter t).</returns>
    //    /// <remarks>No error handling.</remarks>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_addradialdimension.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_addradialdimension.cs" lang="cs" />
    //    /// <code source="examples\py\ex_addradialdimension.py" lang="py" />
    //    /// </example>
    //    public Point3d PointAt(double t)
    //    {
    //        Point3d point3d = new Point3d();
    //        UnsafeNativeMethods.ON_Curve_PointAt(base.ConstPointer(), t, ref point3d, 0);
    //        return point3d;
    //    }

    //    /// <summary>
    //    /// Gets a point at a certain length along the curve. The length must be 
    //    /// non-negative and less than or equal to the length of the curve. 
    //    /// Lengths will not be wrapped when the curve is closed or periodic.
    //    /// </summary>
    //    /// <param name="length">Length along the curve between the start point and the returned point.</param>
    //    /// <returns>Point on the curve at the specified length from the start point or Poin3d.Unset on failure.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_arclengthpoint.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_arclengthpoint.cs" lang="cs" />
    //    /// <code source="examples\py\ex_arclengthpoint.py" lang="py" />
    //    /// </example>
    //    public Point3d PointAtLength(double length)
    //    {
    //        double num;
    //        if (!this.LengthParameter(length, out num))
    //        {
    //            return Point3d.Unset;
    //        }
    //        return this.PointAt(num);
    //    }

    //    /// <summary>
    //    /// Gets a point at a certain normalized length along the curve. The length must be 
    //    /// between or including 0.0 and 1.0, where 0.0 equals the start of the curve and 
    //    /// 1.0 equals the end of the curve. 
    //    /// </summary>
    //    /// <param name="length">Normalized length along the curve between the start point and the returned point.</param>
    //    /// <returns>Point on the curve at the specified normalized length from the start point or Poin3d.Unset on failure.</returns>
    //    public Point3d PointAtNormalizedLength(double length)
    //    {
    //        double num;
    //        if (!this.NormalizedLengthParameter(length, out num))
    //        {
    //            return Point3d.Unset;
    //        }
    //        return this.PointAt(num);
    //    }

    //    /// <summary>
    //    /// Projects a Curve onto a Brep along a given direction.
    //    /// </summary>
    //    /// <param name="curve">Curve to project.</param>
    //    /// <param name="brep">Brep to project onto.</param>
    //    /// <param name="direction">Direction of projection.</param>
    //    /// <param name="tolerance">Tolerance to use for projection.</param>
    //    /// <returns>An array of projected curves or empty array if the projection set is empty.</returns>
    //    public static Curve[] ProjectToBrep(Curve curve, Brep brep, Vector3d direction, double tolerance)
    //    {
    //        Curve[] curveArray;
    //        IntPtr intPtr = brep.ConstPointer();
    //        IntPtr intPtr1 = curve.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            curveArray = (UnsafeNativeMethods.RHC_RhinoProjectCurveToBrep(intPtr, intPtr1, direction, tolerance, simpleArrayCurvePointer.NonConstPointer()) ? simpleArrayCurvePointer.ToNonConstArray() : new Curve[0]);
    //        }
    //        return curveArray;
    //    }

    //    /// <summary>
    //    /// Projects a Curve onto a collection of Breps along a given direction.
    //    /// </summary>
    //    /// <param name="curve">Curve to project.</param>
    //    /// <param name="breps">Breps to project onto.</param>
    //    /// <param name="direction">Direction of projection.</param>
    //    /// <param name="tolerance">Tolerance to use for projection.</param>
    //    /// <returns>An array of projected curves or empty array if the projection set is empty.</returns>
    //    public static Curve[] ProjectToBrep(Curve curve, IEnumerable<Brep> breps, Vector3d direction, double tolerance)
    //    {
    //        int[] numArray;
    //        return Curve.ProjectToBrep(curve, breps, direction, tolerance, out numArray);
    //    }

    //    /// <summary>
    //    /// Projects a Curve onto a collection of Breps along a given direction.
    //    /// </summary>
    //    /// <param name="curve">Curve to project.</param>
    //    /// <param name="breps">Breps to project onto.</param>
    //    /// <param name="direction">Direction of projection.</param>
    //    /// <param name="tolerance">Tolerance to use for projection.</param>
    //    /// <param name="brepIndices">(out) Integers that identify for each resulting curve which Brep it was projected onto.</param>
    //    /// <returns>An array of projected curves or null if the projection set is empty.</returns>
    //    public static Curve[] ProjectToBrep(Curve curve, IEnumerable<Brep> breps, Vector3d direction, double tolerance, out int[] brepIndices)
    //    {
    //        int[] numArray;
    //        IEnumerable<Curve> curves = new Curve[] { curve };
    //        return Curve.ProjectToBrep(curves, breps, direction, tolerance, out numArray, out brepIndices);
    //    }

    //    /// <summary>
    //    /// Projects a collection of Curves onto a collection of Breps along a given direction.
    //    /// </summary>
    //    /// <param name="curves">Curves to project.</param>
    //    /// <param name="breps">Breps to project onto.</param>
    //    /// <param name="direction">Direction of projection.</param>
    //    /// <param name="tolerance">Tolerance to use for projection.</param>
    //    /// <returns>An array of projected curves or empty array if the projection set is empty.</returns>
    //    public static Curve[] ProjectToBrep(IEnumerable<Curve> curves, IEnumerable<Brep> breps, Vector3d direction, double tolerance)
    //    {
    //        int[] numArray;
    //        int[] numArray1;
    //        return Curve.ProjectToBrep(curves, breps, direction, tolerance, out numArray, out numArray1);
    //    }

    //    /// <summary>
    //    /// Projects a collection of Curves onto a collection of Breps along a given direction.
    //    /// </summary>
    //    /// <param name="curves">Curves to project.</param>
    //    /// <param name="breps">Breps to project onto.</param>
    //    /// <param name="direction">Direction of projection.</param>
    //    /// <param name="tolerance">Tolerance to use for projection.</param>
    //    /// <param name="curveIndices">Index of which curve in the input list was the source for a curve in the return array.</param>
    //    /// <param name="brepIndices">Index of which brep was used to generate a curve in the return array.</param>
    //    /// <returns>An array of projected curves. Array is empty if the projection set is empty.</returns>
    //    public static Curve[] ProjectToBrep(IEnumerable<Curve> curves, IEnumerable<Brep> breps, Vector3d direction, double tolerance, out int[] curveIndices, out int[] brepIndices)
    //    {
    //        curveIndices = null;
    //        brepIndices = null;
    //        foreach (Curve curf in curves)
    //        {
    //            if (curf != null)
    //            {
    //                continue;
    //            }
    //            throw new ArgumentNullException("curves");
    //        }
    //        foreach (Brep brep in breps)
    //        {
    //            if (brep != null)
    //            {
    //                continue;
    //            }
    //            throw new ArgumentNullException("breps");
    //        }
    //        SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer(curves);
    //        SimpleArrayBrepPointer simpleArrayBrepPointer = new SimpleArrayBrepPointer();
    //        foreach (Brep brep1 in breps)
    //        {
    //            simpleArrayBrepPointer.Add(brep1, true);
    //        }
    //        IntPtr intPtr = simpleArrayCurvePointer.ConstPointer();
    //        IntPtr intPtr1 = simpleArrayBrepPointer.ConstPointer();
    //        SimpleArrayInt simpleArrayInt = new SimpleArrayInt();
    //        SimpleArrayInt simpleArrayInt1 = new SimpleArrayInt();
    //        SimpleArrayCurvePointer simpleArrayCurvePointer1 = new SimpleArrayCurvePointer();
    //        if (!UnsafeNativeMethods.RHC_RhinoProjectCurveToBrepEx(intPtr1, intPtr, direction, tolerance, simpleArrayCurvePointer1.NonConstPointer(), simpleArrayInt.m_ptr, simpleArrayInt1.m_ptr))
    //        {
    //            return new Curve[0];
    //        }
    //        brepIndices = simpleArrayInt.ToArray();
    //        curveIndices = simpleArrayInt1.ToArray();
    //        return simpleArrayCurvePointer1.ToNonConstArray();
    //    }

    //    /// <summary>
    //    /// Projects a curve to a mesh using a direction and tolerance.
    //    /// </summary>
    //    /// <param name="curve">A curve.</param>
    //    /// <param name="mesh">A mesh.</param>
    //    /// <param name="direction">A direction vector.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <returns>A curve array.</returns>
    //    public static Curve[] ProjectToMesh(Curve curve, Mesh mesh, Vector3d direction, double tolerance)
    //    {
    //        Curve[] curveArray = new Curve[] { curve };
    //        Mesh[] meshArray = new Mesh[] { mesh };
    //        return Curve.ProjectToMesh(curveArray, meshArray, direction, tolerance);
    //    }

    //    /// <summary>
    //    /// Projects a curve to a set of meshes using a direction and tolerance.
    //    /// </summary>
    //    /// <param name="curve">A curve.</param>
    //    /// <param name="meshes">A list, an array or any enumerable of meshes.</param>
    //    /// <param name="direction">A direction vector.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <returns>A curve array.</returns>
    //    public static Curve[] ProjectToMesh(Curve curve, IEnumerable<Mesh> meshes, Vector3d direction, double tolerance)
    //    {
    //        Curve[] curveArray = new Curve[] { curve };
    //        return Curve.ProjectToMesh(curveArray, meshes, direction, tolerance);
    //    }

    //    /// <summary>
    //    /// Projects a curve to a set of meshes using a direction and tolerance.
    //    /// </summary>
    //    /// <param name="curves">A list, an array or any enumerable of curves.</param>
    //    /// <param name="meshes">A list, an array or any enumerable of meshes.</param>
    //    /// <param name="direction">A direction vector.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <returns>A curve array.</returns>
    //    public static Curve[] ProjectToMesh(IEnumerable<Curve> curves, IEnumerable<Mesh> meshes, Vector3d direction, double tolerance)
    //    {
    //        Curve[] curveArray;
    //        foreach (Curve curf in curves)
    //        {
    //            if (curf != null)
    //            {
    //                continue;
    //            }
    //            throw new ArgumentNullException("curves");
    //        }
    //        List<GeometryBase> geometryBases = new List<GeometryBase>();
    //        foreach (Mesh mesh in meshes)
    //        {
    //            if (mesh == null)
    //            {
    //                throw new ArgumentNullException("meshes");
    //            }
    //            geometryBases.Add(mesh);
    //        }
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer(curves))
    //        {
    //            using (SimpleArrayGeometryPointer simpleArrayGeometryPointer = new SimpleArrayGeometryPointer(geometryBases))
    //            {
    //                using (SimpleArrayCurvePointer simpleArrayCurvePointer1 = new SimpleArrayCurvePointer())
    //                {
    //                    IntPtr intPtr = simpleArrayCurvePointer.ConstPointer();
    //                    IntPtr intPtr1 = simpleArrayGeometryPointer.ConstPointer();
    //                    IntPtr intPtr2 = simpleArrayCurvePointer1.NonConstPointer();
    //                    Curve[] nonConstArray = new Curve[0];
    //                    if (UnsafeNativeMethods.RHC_RhinoProjectCurveToMesh(intPtr1, intPtr, direction, tolerance, intPtr2))
    //                    {
    //                        nonConstArray = simpleArrayCurvePointer1.ToNonConstArray();
    //                    }
    //                    curveArray = nonConstArray;
    //                }
    //            }
    //        }
    //        return curveArray;
    //    }

    //    /// <summary>
    //    /// Constructs a curve by projecting an existing curve to a plane.
    //    /// </summary>
    //    /// <param name="curve">A curve.</param>
    //    /// <param name="plane">A plane.</param>
    //    /// <returns>The projected curve on success; null on failure.</returns>
    //    public static Curve ProjectToPlane(Curve curve, Plane plane)
    //    {
    //        IntPtr intPtr = UnsafeNativeMethods.RHC_RhinoProjectCurveToPlane(curve.ConstPointer(), ref plane);
    //        return GeometryBase.CreateGeometryHelper(intPtr, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Pull a curve to a BrepFace using closest point projection.
    //    /// </summary>
    //    /// <param name="curve">Curve to pull.</param>
    //    /// <param name="face">Brepface that pulls.</param>
    //    /// <param name="tolerance">Tolerance to use for pulling.</param>
    //    /// <returns>An array of pulled curves, or an empty array on failure.</returns>
    //    public static Curve[] PullToBrepFace(Curve curve, BrepFace face, double tolerance)
    //    {
    //        Curve[] curveArray;
    //        IntPtr intPtr = face.m_brep.ConstPointer();
    //        IntPtr intPtr1 = curve.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            IntPtr intPtr2 = simpleArrayCurvePointer.NonConstPointer();
    //            curveArray = (!UnsafeNativeMethods.RHC_RhinoPullCurveToBrep(intPtr, face.FaceIndex, intPtr1, tolerance, intPtr2) ? new Curve[0] : simpleArrayCurvePointer.ToNonConstArray());
    //        }
    //        return curveArray;
    //    }

    //    /// <summary>
    //    /// Pulls this curve to a brep face and returns the result of that operation.
    //    /// </summary>
    //    /// <param name="face">A brep face.</param>
    //    /// <param name="tolerance">A tolerance value.</param>
    //    /// <returns>An array containing the resulting curves after pulling. This array could be empty.</returns>
    //    /// <exception cref="T:System.ArgumentNullException">If face is null.</exception>
    //    public Curve[] PullToBrepFace(BrepFace face, double tolerance)
    //    {
    //        Curve[] nonConstArray;
    //        if (face == null)
    //        {
    //            throw new ArgumentNullException("face");
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = face.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            IntPtr intPtr2 = simpleArrayCurvePointer.NonConstPointer();
    //            UnsafeNativeMethods.RHC_RhinoPullCurveToFace(intPtr, intPtr1, intPtr2, tolerance);
    //            nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Makes a polyline approximation of the curve and gets the closest point on the mesh for each point on the curve. 
    //    /// Then it "connects the points" so that you have a polyline on the mesh.
    //    /// </summary>
    //    /// <param name="mesh">Mesh to project onto.</param>
    //    /// <param name="tolerance">Input tolerance (RhinoDoc.ModelAbsoluteTolerance is a good default)</param>
    //    /// <returns>A polyline curve on success, null on failure.</returns>
    //    public PolylineCurve PullToMesh(Mesh mesh, double tolerance)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoPullCurveToMesh(intPtr, mesh.ConstPointer(), tolerance);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as PolylineCurve;
    //    }

    //    /// <summary>
    //    /// Rebuild a curve with a specific point count.
    //    /// </summary>
    //    /// <param name="pointCount">Number of control points in the rebuild curve.</param>
    //    /// <param name="degree">Degree of curve. Valid values are between and including 1 and 11.</param>
    //    /// <param name="preserveTangents">If true, the end tangents of the input curve will be preserved.</param>
    //    /// <returns>A Nurbs curve on success or null on failure.</returns>
    //    public NurbsCurve Rebuild(int pointCount, int degree, bool preserveTangents)
    //    {
    //        pointCount = Math.Max(pointCount, 2);
    //        degree = Math.Max(degree, 1);
    //        degree = Math.Min(degree, 11);
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoRebuildCurve(intPtr, pointCount, degree, preserveTangents);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as NurbsCurve;
    //    }

    //    /// <summary>
    //    /// Looks for segments that are shorter than tolerance that can be removed. 
    //    /// Does not change the domain, but it will change the relative parameterization.
    //    /// </summary>
    //    /// <param name="tolerance">Tolerance which defines "short" segments.</param>
    //    /// <returns>
    //    /// true if removable short segments were found. 
    //    /// false if no removable short segments were found.
    //    /// </returns>
    //    public bool RemoveShortSegments(double tolerance)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_RemoveShortSegments(this.NonConstPointer(), tolerance);
    //    }

    //    /// <summary>
    //    /// Reverses the direction of the curve.
    //    /// </summary>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>If reversed, the domain changes from [a,b] to [-b,-a]</remarks>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_curvereverse.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_curvereverse.cs" lang="cs" />
    //    /// <code source="examples\py\ex_curvereverse.py" lang="py" />
    //    /// </example>
    //    public bool Reverse()
    //    {
    //        return UnsafeNativeMethods.ON_Curve_Reverse(this.NonConstPointer());
    //    }

    //    /// <summary>Forces the curve to end at a specified point. 
    //    /// Not all curve types support this operation.</summary>
    //    /// <param name="point">New end point of curve.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>Some end points cannot be moved. Be sure to check return code</remarks>
    //    public bool SetEndPoint(Point3d point)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_SetPoint(this.NonConstPointer(), point, false);
    //    }

    //    /// <summary>Forces the curve to start at a specified point. 
    //    /// Not all curve types support this operation.</summary>
    //    /// <param name="point">New start point of curve.</param>
    //    /// <returns>true on success, false on failure.</returns>
    //    /// <remarks>Some start points cannot be moved. Be sure to check return code.</remarks>
    //    public bool SetStartPoint(Point3d point)
    //    {
    //        return UnsafeNativeMethods.ON_Curve_SetPoint(this.NonConstPointer(), point, true);
    //    }

    //    /// <summary>
    //    /// Returns a geometrically equivalent PolyCurve.
    //    /// <para>The PolyCurve has the following properties</para>
    //    /// <para>
    //    /// 1. All the PolyCurve segments are LineCurve, PolylineCurve, ArcCurve, or NurbsCurve.
    //    /// </para>
    //    /// <para>
    //    /// 2. The Nurbs Curves segments do not have fully multiple interior knots.
    //    /// </para>
    //    /// <para>
    //    /// 3. Rational Nurbs curves do not have constant weights.
    //    /// </para>
    //    /// <para>
    //    /// 4. Any segment for which IsLinear() or IsArc() is true is a Line, 
    //    ///    Polyline segment, or an Arc.
    //    /// </para>
    //    /// <para>
    //    /// 5. Adjacent Colinear or Cocircular segments are combined.
    //    /// </para>
    //    /// <para>
    //    /// 6. Segments that meet with G1-continuity have there ends tuned up so
    //    ///    that they meet with G1-continuity to within machine precision.
    //    /// </para>
    //    /// </summary>
    //    /// <param name="options">Simplification options.</param>
    //    /// <param name="distanceTolerance">A distance tolerance for the simplification.</param>
    //    /// <param name="angleToleranceRadians">An angle tolerance for the simplification.</param>
    //    /// <returns>New simplified curve on success, null on failure.</returns>
    //    public Curve Simplify(CurveSimplifyOptions options, double distanceTolerance, double angleToleranceRadians)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        int num = Curve.SimplifyOptionsToInt(options);
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoSimplifyCurve(intPtr, num, distanceTolerance, angleToleranceRadians);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Same as SimplifyCurve, but simplifies only the last two segments at "side" end.
    //    /// </summary>
    //    /// <param name="end">If CurveEnd.Start the function simplifies the last two start 
    //    /// side segments, otherwise if CurveEnd.End the last two end side segments are simplified.
    //    /// </param>
    //    /// <param name="options">Simplification options.</param>
    //    /// <param name="distanceTolerance">A distance tolerance for the simplification.</param>
    //    /// <param name="angleToleranceRadians">An angle tolerance for the simplification.</param>
    //    /// <returns>New simplified curve on success, null on failure.</returns>
    //    public Curve SimplifyEnd(CurveEnd end, CurveSimplifyOptions options, double distanceTolerance, double angleToleranceRadians)
    //    {
    //        if (end != CurveEnd.Start && end != CurveEnd.End)
    //        {
    //            return null;
    //        }
    //        int num = 0;
    //        if (CurveEnd.End == end)
    //        {
    //            num = 1;
    //        }
    //        int num1 = Curve.SimplifyOptionsToInt(options);
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.RHC_RhinoSimplifyCurveEnd(intPtr, num, num1, distanceTolerance, angleToleranceRadians);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    private static int SimplifyOptionsToInt(CurveSimplifyOptions options)
    //    {
    //        int num = 63;
    //        if ((options & CurveSimplifyOptions.SplitAtFullyMultipleKnots) == CurveSimplifyOptions.SplitAtFullyMultipleKnots)
    //        {
    //            num--;
    //        }
    //        if ((options & CurveSimplifyOptions.RebuildLines) == CurveSimplifyOptions.RebuildLines)
    //        {
    //            num = num - 2;
    //        }
    //        if ((options & CurveSimplifyOptions.RebuildArcs) == CurveSimplifyOptions.RebuildArcs)
    //        {
    //            num = num - 4;
    //        }
    //        if ((options & CurveSimplifyOptions.RebuildRationals) == CurveSimplifyOptions.RebuildRationals)
    //        {
    //            num = num - 8;
    //        }
    //        if ((options & CurveSimplifyOptions.AdjustG1) == CurveSimplifyOptions.AdjustG1)
    //        {
    //            num = num - 16;
    //        }
    //        if ((options & CurveSimplifyOptions.Merge) == CurveSimplifyOptions.Merge)
    //        {
    //            num = num - 32;
    //        }
    //        return num;
    //    }

    //    /// <summary>
    //    /// Get the domain of the curve span with the given index. 
    //    /// Use the SpanCount property to test how many spans there are.
    //    /// </summary>
    //    /// <param name="spanIndex">Index of span.</param>
    //    /// <returns>Interval of the span with the given index.</returns>
    //    public Interval SpanDomain(int spanIndex)
    //    {
    //        if (spanIndex < 0)
    //        {
    //            throw new IndexOutOfRangeException("spanIndex must be larger than or equal to zero");
    //        }
    //        if (spanIndex >= this.SpanCount)
    //        {
    //            throw new IndexOutOfRangeException("spanIndex must be smaller than spanCount");
    //        }
    //        Interval unset = Interval.Unset;
    //        if (UnsafeNativeMethods.ON_Curve_SpanInterval(base.ConstPointer(), spanIndex, ref unset))
    //        {
    //            return unset;
    //        }
    //        return Interval.Unset;
    //    }

    //    /// <summary>
    //    /// Splits (divides) the curve at the specified parameter. 
    //    /// The parameter must be in the interior of the curve's domain.
    //    /// </summary>
    //    /// <param name="t">
    //    /// Parameter to split the curve at in the interval returned by Domain().
    //    /// </param>
    //    /// <returns>
    //    /// Two curves on success, null on failure.
    //    /// </returns>
    //    public Curve[] Split(double t)
    //    {
    //        IntPtr zero = IntPtr.Zero;
    //        IntPtr intPtr = IntPtr.Zero;
    //        IntPtr intPtr1 = base.ConstPointer();
    //        bool flag = UnsafeNativeMethods.ON_Curve_Split(intPtr1, t, ref zero, ref intPtr);
    //        Curve[] curveArray = new Curve[2];
    //        if (zero != IntPtr.Zero)
    //        {
    //            curveArray[0] = GeometryBase.CreateGeometryHelper(zero, null) as Curve;
    //        }
    //        if (intPtr != IntPtr.Zero)
    //        {
    //            curveArray[1] = GeometryBase.CreateGeometryHelper(intPtr, null) as Curve;
    //        }
    //        if (!flag)
    //        {
    //            return null;
    //        }
    //        return curveArray;
    //    }

    //    /// <summary>
    //    /// Splits (divides) the curve at a series of specified parameters. 
    //    /// The parameter must be in the interior of the curve's domain.
    //    /// </summary>
    //    /// <param name="t">
    //    /// Parameters to split the curve at in the interval returned by Domain().
    //    /// </param>
    //    /// <returns>
    //    /// Multiple curves on success, null on failure.
    //    /// </returns>
    //    public Curve[] Split(IEnumerable<double> t)
    //    {
    //        Interval domain = this.Domain;
    //        RhinoList<double> nums = new RhinoList<double>(t)
    //        {
    //            domain.Min,
    //            domain.Max
    //        };
    //        nums.Sort();
    //        RhinoList<Curve> curves = new RhinoList<Curve>();
    //        for (int i = 0; i < nums.Count - 1; i++)
    //        {
    //            double item = nums[i];
    //            double num = nums[i + 1];
    //            if (num - item > 1E-12)
    //            {
    //                Curve curve = this.Trim(item, num);
    //                if (curve != null)
    //                {
    //                    curves.Add(curve);
    //                }
    //            }
    //        }
    //        if (curves.Count == 0)
    //        {
    //            return null;
    //        }
    //        return curves.ToArray();
    //    }

    //    /// <summary>
    //    /// Splits a curve into pieces using a polysurface.
    //    /// </summary>
    //    /// <param name="cutter">A cutting surface or polysurface.</param>
    //    /// <param name="tolerance">A tolerance for computing intersections.</param>
    //    /// <returns>An array of curves. This array can be empty.</returns>
    //    public Curve[] Split(Brep cutter, double tolerance)
    //    {
    //        Curve[] nonConstArray;
    //        if (cutter == null)
    //        {
    //            throw new ArgumentNullException("cutter");
    //        }
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = cutter.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            IntPtr intPtr2 = simpleArrayCurvePointer.NonConstPointer();
    //            UnsafeNativeMethods.RHC_RhinoCurveSplit(intPtr, intPtr1, tolerance, intPtr2);
    //            nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>
    //    /// Splits a curve into pieces using a surface.
    //    /// </summary>
    //    /// <param name="cutter">A cutting surface or polysurface.</param>
    //    /// <param name="tolerance">A tolerance for computing intersections.</param>
    //    /// <returns>An array of curves. This array can be empty.</returns>
    //    public Curve[] Split(Surface cutter, double tolerance)
    //    {
    //        Curve[] nonConstArray;
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = cutter.ConstPointer();
    //        using (SimpleArrayCurvePointer simpleArrayCurvePointer = new SimpleArrayCurvePointer())
    //        {
    //            IntPtr intPtr2 = simpleArrayCurvePointer.NonConstPointer();
    //            UnsafeNativeMethods.RHC_RhinoCurveSplit(intPtr, intPtr1, tolerance, intPtr2);
    //            nonConstArray = simpleArrayCurvePointer.ToNonConstArray();
    //        }
    //        return nonConstArray;
    //    }

    //    /// <summary>Evaluates the unit tangent vector at a curve parameter.</summary>
    //    /// <param name="t">Evaluation parameter.</param>
    //    /// <returns>Unit tangent vector of the curve at the parameter t.</returns>
    //    /// <remarks>No error handling.</remarks>
    //    public Vector3d TangentAt(double t)
    //    {
    //        Vector3d vector3d = new Vector3d();
    //        UnsafeNativeMethods.ON_Curve_GetVector(base.ConstPointer(), 1, t, ref vector3d);
    //        return vector3d;
    //    }

    //    /// <summary>
    //    /// Constructs a NURBS curve representation of this curve.
    //    /// </summary>
    //    /// <returns>NURBS representation of the curve on success, null on failure.</returns>
    //    public NurbsCurve ToNurbsCurve()
    //    {
    //        Interval unset = Interval.Unset;
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.ON_Curve_NurbsCurve(intPtr, 0, unset, true);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as NurbsCurve;
    //    }

    //    /// <summary>
    //    /// Constructs a NURBS curve representation of this curve.
    //    /// </summary>
    //    /// <param name="subdomain">The NURBS representation for this portion of the curve is returned.</param>
    //    /// <returns>NURBS representation of the curve on success, null on failure.</returns>
    //    public NurbsCurve ToNurbsCurve(Interval subdomain)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.ON_Curve_NurbsCurve(intPtr, 0, subdomain, false);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as NurbsCurve;
    //    }

    //    /// <summary>
    //    /// Gets a polyline approximation of a curve.
    //    /// </summary>
    //    /// <param name="mainSegmentCount">
    //    /// If mainSegmentCount &lt;= 0, then both subSegmentCount and mainSegmentCount are ignored. 
    //    /// If mainSegmentCount &gt; 0, then subSegmentCount must be &gt;= 1. In this 
    //    /// case the nurb will be broken into mainSegmentCount equally spaced 
    //    /// chords. If needed, each of these chords can be split into as many 
    //    /// subSegmentCount sub-parts if the subdivision is necessary for the 
    //    /// mesh to meet the other meshing constraints. In particular, if 
    //    /// subSegmentCount = 0, then the curve is broken into mainSegmentCount 
    //    /// pieces and no further testing is performed.</param>
    //    /// <param name="subSegmentCount">An amount of subsegments.</param>
    //    /// <param name="maxAngleRadians">
    //    /// ( 0 to pi ) Maximum angle (in radians) between unit tangents at 
    //    /// adjacent vertices.</param>
    //    /// <param name="maxChordLengthRatio">Maximum permitted value of 
    //    /// (distance chord midpoint to curve) / (length of chord).</param>
    //    /// <param name="maxAspectRatio">If maxAspectRatio &lt; 1.0, the parameter is ignored. 
    //    /// If 1 &lt;= maxAspectRatio &lt; sqrt(2), it is treated as if maxAspectRatio = sqrt(2). 
    //    /// This parameter controls the maximum permitted value of 
    //    /// (length of longest chord) / (length of shortest chord).</param>
    //    /// <param name="tolerance">If tolerance = 0, the parameter is ignored. 
    //    /// This parameter controls the maximum permitted value of the 
    //    /// distance from the curve to the polyline.</param>
    //    /// <param name="minEdgeLength">The minimum permitted edge length.</param>
    //    /// <param name="maxEdgeLength">If maxEdgeLength = 0, the parameter 
    //    /// is ignored. This parameter controls the maximum permitted edge length.
    //    /// </param>
    //    /// <param name="keepStartPoint">If true the starting point of the curve 
    //    /// is added to the polyline. If false the starting point of the curve is 
    //    /// not added to the polyline.</param>
    //    /// <returns>PolylineCurve on success, null on error.</returns>
    //    public PolylineCurve ToPolyline(int mainSegmentCount, int subSegmentCount, double maxAngleRadians, double maxChordLengthRatio, double maxAspectRatio, double tolerance, double minEdgeLength, double maxEdgeLength, bool keepStartPoint)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        PolylineCurve polylineCurve = new PolylineCurve();
    //        if (!UnsafeNativeMethods.RHC_RhinoConvertCurveToPolyline(intPtr, mainSegmentCount, subSegmentCount, maxAngleRadians, maxChordLengthRatio, maxAspectRatio, tolerance, minEdgeLength, maxEdgeLength, polylineCurve.NonConstPointer(), keepStartPoint, Interval.Unset, true))
    //        {
    //            polylineCurve.Dispose();
    //            polylineCurve = null;
    //        }
    //        return polylineCurve;
    //    }

    //    /// <summary>
    //    /// Gets a polyline approximation of a curve.
    //    /// </summary>
    //    /// <param name="mainSegmentCount">
    //    /// If mainSegmentCount &lt;= 0, then both subSegmentCount and mainSegmentCount are ignored. 
    //    /// If mainSegmentCount &gt; 0, then subSegmentCount must be &gt;= 1. In this 
    //    /// case the nurb will be broken into mainSegmentCount equally spaced 
    //    /// chords. If needed, each of these chords can be split into as many 
    //    /// subSegmentCount sub-parts if the subdivision is necessary for the 
    //    /// mesh to meet the other meshing constraints. In particular, if 
    //    /// subSegmentCount = 0, then the curve is broken into mainSegmentCount 
    //    /// pieces and no further testing is performed.</param>
    //    /// <param name="subSegmentCount">An amount of subsegments.</param>
    //    /// <param name="maxAngleRadians">
    //    /// ( 0 to pi ) Maximum angle (in radians) between unit tangents at 
    //    /// adjacent vertices.</param>
    //    /// <param name="maxChordLengthRatio">Maximum permitted value of 
    //    /// (distance chord midpoint to curve) / (length of chord).</param>
    //    /// <param name="maxAspectRatio">If maxAspectRatio &lt; 1.0, the parameter is ignored. 
    //    /// If 1 &lt;= maxAspectRatio &lt; sqrt(2), it is treated as if maxAspectRatio = sqrt(2). 
    //    /// This parameter controls the maximum permitted value of 
    //    /// (length of longest chord) / (length of shortest chord).</param>
    //    /// <param name="tolerance">If tolerance = 0, the parameter is ignored. 
    //    /// This parameter controls the maximum permitted value of the 
    //    /// distance from the curve to the polyline.</param>
    //    /// <param name="minEdgeLength">The minimum permitted edge length.</param>
    //    /// <param name="maxEdgeLength">If maxEdgeLength = 0, the parameter 
    //    /// is ignored. This parameter controls the maximum permitted edge length.
    //    /// </param>
    //    /// <param name="keepStartPoint">If true the starting point of the curve 
    //    /// is added to the polyline. If false the starting point of the curve is 
    //    /// not added to the polyline.</param>
    //    /// <param name="curveDomain">This subdomain of the NURBS curve is approximated.</param>
    //    /// <returns>PolylineCurve on success, null on error.</returns>
    //    public PolylineCurve ToPolyline(int mainSegmentCount, int subSegmentCount, double maxAngleRadians, double maxChordLengthRatio, double maxAspectRatio, double tolerance, double minEdgeLength, double maxEdgeLength, bool keepStartPoint, Interval curveDomain)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        PolylineCurve polylineCurve = new PolylineCurve();
    //        if (!UnsafeNativeMethods.RHC_RhinoConvertCurveToPolyline(intPtr, mainSegmentCount, subSegmentCount, maxAngleRadians, maxChordLengthRatio, maxAspectRatio, tolerance, minEdgeLength, maxEdgeLength, polylineCurve.NonConstPointer(), keepStartPoint, curveDomain, false))
    //        {
    //            polylineCurve.Dispose();
    //            polylineCurve = null;
    //        }
    //        return polylineCurve;
    //    }

    //    /// <summary>
    //    /// Removes portions of the curve outside the specified interval.
    //    /// </summary>
    //    /// <param name="t0">
    //    /// Start of the trimming interval. Portions of the curve before curve(t0) are removed.
    //    /// </param>
    //    /// <param name="t1">
    //    /// End of the trimming interval. Portions of the curve after curve(t1) are removed.
    //    /// </param>
    //    /// <returns>Trimmed portion of this curve is successfull, null on failure.</returns>
    //    public Curve Trim(double t0, double t1)
    //    {
    //        return this.TrimExtendHelper(t0, t1, true);
    //    }

    //    /// <summary>
    //    /// Removes portions of the curve outside the specified interval.
    //    /// </summary>
    //    /// <param name="domain">
    //    /// Trimming interval. Portions of the curve before curve(domain[0])
    //    /// and after curve(domain[1]) are removed.
    //    /// </param>
    //    /// <returns>Trimmed curve if successful, null on failure.</returns>
    //    public Curve Trim(Interval domain)
    //    {
    //        return this.Trim(domain.T0, domain.T1);
    //    }

    //    /// <summary>
    //    /// Shortens a curve by a given length
    //    /// </summary>
    //    /// <param name="side"></param>
    //    /// <param name="length"></param>
    //    /// <returns>Trimmed curve if successful, null on failure.</returns>
    //    public Curve Trim(CurveEnd side, double length)
    //    {
    //        if (length <= 0)
    //        {
    //            throw new ArgumentException("length must be > 0", "length");
    //        }
    //        double num = this.GetLength();
    //        if (this.IsClosed || length >= num)
    //        {
    //            return null;
    //        }
    //        if (side == CurveEnd.Both && length >= 2 * num)
    //        {
    //            return null;
    //        }
    //        double item = this.Domain[0];
    //        double item1 = this.Domain[1];
    //        if (side == CurveEnd.Start || side == CurveEnd.Both)
    //        {
    //            this.NormalizedLengthParameter(length / num, out item);
    //        }
    //        if (side == CurveEnd.End || side == CurveEnd.Both)
    //        {
    //            double num1 = (num - length) / num;
    //            this.NormalizedLengthParameter(num1, out item1);
    //        }
    //        return this.Trim(item, item1);
    //    }

    //    private Curve TrimExtendHelper(double t0, double t1, bool trimming)
    //    {
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = UnsafeNativeMethods.ON_Curve_TrimExtend(intPtr, t0, t1, trimming);
    //        return GeometryBase.CreateGeometryHelper(intPtr1, null) as Curve;
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Arc using RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <param name="arc">On success, the Arc will be filled in.</param>
    //    /// <returns>true if the curve could be converted into an arc.</returns>
    //    public bool TryGetArc(out Arc arc)
    //    {
    //        return this.TryGetArc(out arc, 1E-12);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Arc using a custom tolerance.
    //    /// </summary>
    //    /// <param name="arc">On success, the Arc will be filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>true if the curve could be converted into an arc.</returns>
    //    public bool TryGetArc(out Arc arc, double tolerance)
    //    {
    //        arc = new Arc();
    //        Plane worldXY = Plane.WorldXY;
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsArc(intPtr, 1, ref worldXY, ref arc, tolerance);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Arc using RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <param name="plane">Plane in which the comparison is performed.</param>
    //    /// <param name="arc">On success, the Arc will be filled in.</param>
    //    /// <returns>true if the curve could be converted into an arc within the given plane.</returns>
    //    public bool TryGetArc(Plane plane, out Arc arc)
    //    {
    //        return this.TryGetArc(plane, out arc, 1E-12);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Arc using a custom tolerance.
    //    /// </summary>
    //    /// <param name="plane">Plane in which the comparison is performed.</param>
    //    /// <param name="arc">On success, the Arc will be filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>true if the curve could be converted into an arc within the given plane.</returns>
    //    public bool TryGetArc(Plane plane, out Arc arc, double tolerance)
    //    {
    //        arc = new Arc();
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsArc(intPtr, 0, ref plane, ref arc, tolerance);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into a circle using RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <param name="circle">On success, the Circle will be filled in.</param>
    //    /// <returns>true if the curve could be converted into a Circle.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_customgeometryfilter.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_customgeometryfilter.cs" lang="cs" />
    //    /// <code source="examples\py\ex_customgeometryfilter.py" lang="py" />
    //    /// </example>
    //    public bool TryGetCircle(out Circle circle)
    //    {
    //        return this.TryGetCircle(out circle, 1E-12);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into a Circle using a custom tolerance.
    //    /// </summary>
    //    /// <param name="circle">On success, the Circle will be filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>true if the curve could be converted into a Circle within tolerance.</returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_circlecenter.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_circlecenter.cs" lang="cs" />
    //    /// <code source="examples\py\ex_circlecenter.py" lang="py" />
    //    /// </example>
    //    public bool TryGetCircle(out Circle circle, double tolerance)
    //    {
    //        Arc arc;
    //        circle = new Circle();
    //        if (!this.TryGetArc(out arc, tolerance) || !arc.IsCircle)
    //        {
    //            return false;
    //        }
    //        circle = new Circle(arc);
    //        return true;
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Ellipse within RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <param name="ellipse">On success, the Ellipse will be filled in.</param>
    //    /// <returns>true if the curve could be converted into an Ellipse.</returns>
    //    public bool TryGetEllipse(out Ellipse ellipse)
    //    {
    //        return this.TryGetEllipse(out ellipse, 1E-12);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Ellipse using a custom tolerance.
    //    /// </summary>
    //    /// <param name="ellipse">On success, the Ellipse will be filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>true if the curve could be converted into an Ellipse.</returns>
    //    public bool TryGetEllipse(out Ellipse ellipse, double tolerance)
    //    {
    //        Plane worldXY = Plane.WorldXY;
    //        ellipse = new Ellipse();
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsEllipse(intPtr, 1, ref worldXY, ref ellipse, tolerance);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Ellipse within RhinoMath.ZeroTolerance.
    //    /// </summary>
    //    /// <param name="plane">Plane in which the comparison is performed.</param>
    //    /// <param name="ellipse">On success, the Ellipse will be filled in.</param>
    //    /// <returns>true if the curve could be converted into an Ellipse within the given plane.</returns>
    //    public bool TryGetEllipse(Plane plane, out Ellipse ellipse)
    //    {
    //        return this.TryGetEllipse(plane, out ellipse, 1E-12);
    //    }

    //    /// <summary>
    //    /// Try to convert this curve into an Ellipse using a custom tolerance.
    //    /// </summary>
    //    /// <param name="plane">Plane in which the comparison is performed.</param>
    //    /// <param name="ellipse">On success, the Ellipse will be filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>true if the curve could be converted into an Ellipse within the given plane.</returns>
    //    public bool TryGetEllipse(Plane plane, out Ellipse ellipse, double tolerance)
    //    {
    //        ellipse = new Ellipse();
    //        IntPtr intPtr = base.ConstPointer();
    //        return UnsafeNativeMethods.ON_Curve_IsEllipse(intPtr, 0, ref plane, ref ellipse, tolerance);
    //    }

    //    /// <summary>Test a curve for planarity and return the plane.</summary>
    //    /// <param name="plane">On success, the plane parameters are filled in.</param>
    //    /// <returns>
    //    /// true if there is a plane such that the maximum distance from the curve to the plane is &lt;= RhinoMath.ZeroTolerance.
    //    /// </returns>
    //    /// <example>
    //    /// <code source="examples\vbnet\ex_constrainedcopy.vb" lang="vbnet" />
    //    /// <code source="examples\cs\ex_constrainedcopy.cs" lang="cs" />
    //    /// <code source="examples\py\ex_constrainedcopy.py" lang="py" />
    //    /// </example>
    //    public bool TryGetPlane(out Plane plane)
    //    {
    //        return this.TryGetPlane(out plane, 1E-12);
    //    }

    //    /// <summary>Test a curve for planarity and return the plane.</summary>
    //    /// <param name="plane">On success, the plane parameters are filled in.</param>
    //    /// <param name="tolerance">Tolerance to use when checking.</param>
    //    /// <returns>
    //    /// true if there is a plane such that the maximum distance from the curve to the plane is &lt;= tolerance.
    //    /// </returns>
    //    public bool TryGetPlane(out Plane plane, double tolerance)
    //    {
    //        plane = Plane.WorldXY;
    //        return UnsafeNativeMethods.ON_Curve_IsPlanar(base.ConstPointer(), false, ref plane, tolerance);
    //    }

    //    /// <summary>
    //    /// Several types of Curve can have the form of a polyline 
    //    /// including a degree 1 NurbsCurve, a PolylineCurve, 
    //    /// and a PolyCurve all of whose segments are some form of 
    //    /// polyline. IsPolyline tests a curve to see if it can be 
    //    /// represented as a polyline.
    //    /// </summary>
    //    /// <param name="polyline">
    //    /// If true is returned, then the polyline form is returned here.
    //    /// </param>
    //    /// <returns>true if this curve can be represented as a polyline; otherwise, false.</returns>
    //    public bool TryGetPolyline(out Polyline polyline)
    //    {
    //        polyline = null;
    //        SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d();
    //        int num = 0;
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = simpleArrayPoint3d.NonConstPointer();
    //        UnsafeNativeMethods.ON_Curve_IsPolyline2(intPtr, intPtr1, ref num, IntPtr.Zero);
    //        if (num > 0)
    //        {
    //            polyline = Polyline.PolyLineFromNativeArray(simpleArrayPoint3d);
    //        }
    //        simpleArrayPoint3d.Dispose();
    //        return num != 0;
    //    }

    //    /// <summary>
    //    /// Several types of Curve can have the form of a polyline 
    //    /// including a degree 1 NurbsCurve, a PolylineCurve, 
    //    /// and a PolyCurve all of whose segments are some form of 
    //    /// polyline. IsPolyline tests a curve to see if it can be 
    //    /// represented as a polyline.
    //    /// </summary>
    //    /// <param name="polyline">
    //    /// If true is returned, then the polyline form is returned here.
    //    /// </param>
    //    /// <param name="parameters">
    //    /// if true is returned, then the parameters of the polyline
    //    /// points are returned here.
    //    /// </param>
    //    /// <returns>true if this curve can be represented as a polyline; otherwise, false.</returns>
    //    public bool TryGetPolyline(out Polyline polyline, out double[] parameters)
    //    {
    //        polyline = null;
    //        parameters = null;
    //        SimpleArrayPoint3d simpleArrayPoint3d = new SimpleArrayPoint3d();
    //        int num = 0;
    //        IntPtr intPtr = base.ConstPointer();
    //        IntPtr intPtr1 = simpleArrayPoint3d.NonConstPointer();
    //        SimpleArrayDouble simpleArrayDouble = new SimpleArrayDouble();
    //        UnsafeNativeMethods.ON_Curve_IsPolyline2(intPtr, intPtr1, ref num, simpleArrayDouble.NonConstPointer());
    //        if (num > 0)
    //        {
    //            polyline = Polyline.PolyLineFromNativeArray(simpleArrayPoint3d);
    //            parameters = simpleArrayDouble.ToArray();
    //        }
    //        simpleArrayDouble.Dispose();
    //        simpleArrayPoint3d.Dispose();
    //        return num != 0;
    //    }
    //}
}
