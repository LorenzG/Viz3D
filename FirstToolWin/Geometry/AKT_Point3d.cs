using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Geometry
{
    /// <summary>
    /// Represents the three coordinates of a point in three-dimensional space,
    /// using <see cref="T:System.Double" />-precision floating point values.
    /// </summary>
    public struct AKT_Point3d : IEquatable<AKT_Point3d>, IEqualityComparer<AKT_Point3d>
    {
        static decimal tolerance = 0.001m;
        static double toleranceD = 0.001;
        static int decimPlaces = 3;
        static NumberFormatInfo nfi;
        static string stringDec = "N3";

        public static decimal Tolerance
        {
            get { return tolerance; }
        }
        public static double ToleranceD
        {
            get { return toleranceD; }
        }
        public static int DecimalPlaceTolerance
        {
            get { return decimPlaces; }
        }
        public static NumberFormatInfo StringFormatting
        { get { return nfi; } }
        public static string StringDecimal
        {
            get { return AKT_Point3d.stringDec; }
        }

        public static void SetTolerance(double tol)
        {
            toleranceD = tol;
            tolerance = (decimal)tol;
            decimPlaces = (int)Math.Floor(-Math.Log10(tol));
            stringDec = "N" + decimPlaces.ToString();
            nfi = new NumberFormatInfo()
            {
                NumberDecimalDigits = decimPlaces
            };

        }

        internal double m_x;
        internal double m_y;
        internal double m_z;

        public static AKT_Point3d GetTolerancePoint()
        {
            return new AKT_Point3d(AKT_Point3d.ToleranceD, AKT_Point3d.ToleranceD, AKT_Point3d.ToleranceD);
        }

        /// <summary>
        /// Gets or sets an indexed coordinate of this point.
        /// </summary>
        /// <param name="index">
        /// The coordinate index. Valid values are:
        /// <para>0 = X coordinate</para>
        /// <para>1 = Y coordinate</para>
        /// <para>2 = Z coordinate</para>
        /// .</param>
        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.m_x;
                }
                if (1 == index)
                {
                    return this.m_y;
                }
                if (2 != index)
                {
                    throw new IndexOutOfRangeException();
                }
                return this.m_z;
            }
            set
            {
                if (index == 0)
                {
                    this.m_x = value;
                    return;
                }
                if (1 == index)
                {
                    this.m_y = value;
                    return;
                }
                if (2 != index)
                {
                    throw new IndexOutOfRangeException();
                }
                this.m_z = value;
            }
        }

        /// <summary>
        /// Gets the value of a point at location 0,0,0.
        /// </summary>
        public static AKT_Point3d Origin
        {
            get
            {
                return new AKT_Point3d(0, 0, 0);
            }
        }

        /// <summary>
        /// Gets the value of a point at location RhinoMath.UnsetValue,RhinoMath.UnsetValue,RhinoMath.UnsetValue.
        /// </summary>
        public static AKT_Point3d Unset
        {
            get
            {
                return new AKT_Point3d(-1.23432101234321E+308, -1.23432101234321E+308, -1.23432101234321E+308);
            }
        }


        /// <summary>
        /// Gets or sets the X (first) coordinate of this point.
        /// </summary>
        public double X
        {
            get
            {
                return this.m_x;
            }
            set
            {
                this.m_x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y (second) coordinate of this point.
        /// </summary>
        public double Y
        {
            get
            {
                return this.m_y;
            }
            set
            {
                this.m_y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z (third) coordinate of this point.
        /// </summary>
        public double Z
        {
            get
            {
                return this.m_z;
            }
            set
            {
                this.m_z = value;
            }
        }

        /// <summary>
        /// Initializes a new point by defining the X, Y and Z coordinates.
        /// </summary>
        /// <param name="x">The value of the X (first) coordinate.</param>
        /// <param name="y">The value of the Y (second) coordinate.</param>
        /// <param name="z">The value of the Z (third) coordinate.</param>
        /// <example>
        /// <code source="examples\vbnet\ex_addcircle.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_addcircle.cs" lang="cs" />
        /// <code source="examples\py\ex_addcircle.py" lang="py" />
        /// </example>
        public AKT_Point3d(double x, double y, double z)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_z = z;
            //this.guid = Guid.Empty;
        }

        /// <summary>
        /// Initializes a new point by copying coordinates from the components of a vector.
        /// </summary>
        /// <param name="vector">A vector.</param>
        public AKT_Point3d(AKT_Vector3d vector)
        {
            this.m_x = vector.m_x;
            this.m_y = vector.m_y;
            this.m_z = vector.m_z;
        }

        /// <summary>
        /// Initializes a new point by copying coordinates from another point.
        /// </summary>
        /// <param name="point">A point.</param>
        public AKT_Point3d(AKT_Point3d point)
        {
            this.m_x = point.X;
            this.m_y = point.Y;
            this.m_z = point.Z;
            //this.guid = Guid.Empty;
        }

        /// <summary>
        /// Sums two <see cref="T:Rhino.Geometry.AKT_Point3d" /> instances.
        /// <para>(Provided for languages that do not support operator overloading. You can use the + operator otherwise)</para>
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">A point.</param>
        /// <returns>A new point that results from the addition of point1 and point2.</returns>
        public static AKT_Point3d Add(AKT_Point3d point1, AKT_Point3d point2)
        {
            return new AKT_Point3d(point1.m_x + point2.m_x, point1.m_y + point2.m_y, point1.m_z + point2.m_z);
        }

        /// <summary>
        /// Sums up a point and a vector, and returns a new point.
        /// <para>(Provided for languages that do not support operator overloading. You can use the + operator otherwise)</para>
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new point that results from the addition of point and vector.</returns>
        public static AKT_Point3d Add(AKT_Point3d point, AKT_Vector3d vector)
        {
            return new AKT_Point3d(point.m_x + vector.m_x, point.m_y + vector.m_y, point.m_z + vector.m_z);
        }

        /// <summary>
        /// Computes the distance between two points.
        /// </summary>
        /// <param name="other">Other point for distance measurement.</param>
        /// <returns>The length of the AKT_Line between this and the other point; or 0 if any of the points is not valid.</returns>
        /// <example>
        /// <code source="examples\vbnet\ex_intersectcurves.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_intersectcurves.cs" lang="cs" />
        /// <code source="examples\py\ex_intersectcurves.py" lang="py" />
        /// </example>
        public double DistanceTo(AKT_Point3d other)
        {
            double mX = other.m_x - this.m_x;
            double mY = other.m_y - this.m_y;
            double mZ = other.m_z - this.m_z;
            return AKT_Vector3d.GetLengthHelper(mX, mY, mZ);
        }

        /// <summary>
        /// Divides a <see cref="T:Rhino.Geometry.AKT_Point3d" /> by a number.
        /// <para>(Provided for languages that do not support operator overloading. You can use the / operator otherwise)</para>
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new point that is coordinatewise divided by t.</returns>
        public static AKT_Point3d Divide(AKT_Point3d point, double t)
        {
            return new AKT_Point3d(point.m_x / t, point.m_y / t, point.m_z / t);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is a <see cref="T:Rhino.Geometry.AKT_Point3d" /> and has the same values as the present point.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is a AKT_Point3d and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AKT_Point3d))
            {
                return false;
            }
            return this == (AKT_Point3d)obj;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:Rhino.Geometry.AKT_Point3d" /> has the same values as the present point.
        /// </summary>
        /// <param name="point">The specified point.</param>
        /// <returns>true if point has the same coordinates as this; otherwise false.</returns>
        public bool Equals(AKT_Point3d point)
        {
            return this == point;
        }

        public bool Equals(AKT_Point3d x, AKT_Point3d y)
        {
            return x == y;
        }

        public int GetHashCode(AKT_Point3d obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Computes a hash code for the present point.
        /// </summary>
        /// <returns>A non-unique integer that represents this point.</returns>
        public override int GetHashCode()
        {
            return this.m_x.GetHashCode() ^ this.m_y.GetHashCode() ^ this.m_z.GetHashCode();
        }

        /// <summary>
        /// Sums two <see cref="T:Rhino.Geometry.AKT_Point3d" /> instances.
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">A point.</param>
        /// <returns>A new point that results from the addition of point1 and point2.</returns>
        public static AKT_Point3d operator +(AKT_Point3d point1, AKT_Point3d point2)
        {
            return new AKT_Point3d(point1.m_x + point2.m_x, point1.m_y + point2.m_y, point1.m_z + point2.m_z);
        }

        /// <summary>
        /// Sums up a point and a vector, and returns a new point.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new point that results from the addition of point and vector.</returns>
        public static AKT_Point3d operator +(AKT_Point3d point, AKT_Vector3d vector)
        {
            return new AKT_Point3d(point.m_x + vector.m_x, point.m_y + vector.m_y, point.m_z + vector.m_z);
        }

        /// <summary>
        /// Sums up a point and a vector, and returns a new point.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="point">A point.</param>
        /// <returns>A new point that results from the addition of point and vector.</returns>
        public static AKT_Point3d operator +(AKT_Vector3d vector, AKT_Point3d point)
        {
            return new AKT_Point3d(point.m_x + vector.m_x, point.m_y + vector.m_y, point.m_z + vector.m_z);
        }

        /// <summary>
        /// Divides a <see cref="T:Rhino.Geometry.AKT_Point3d" /> by a number.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new point that is coordinatewise divided by t.</returns>
        public static AKT_Point3d operator /(AKT_Point3d point, double t)
        {
            return new AKT_Point3d(point.m_x / t, point.m_y / t, point.m_z / t);
        }

        /// <summary>
        /// Determines whether two AKT_Point3d have equal values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the coordinates of the two points are exactly equal; otherwise false.</returns>
        public static bool operator ==(AKT_Point3d a, AKT_Point3d b)
        {
            if (Math.Abs(a.X - b.X) > toleranceD)
                return false;
            if (Math.Abs(a.Y - b.Y) > toleranceD)
                return false;
            if (Math.Abs(a.Z - b.Z) > toleranceD)
                return false;

            return true;
            //double dist = a.DistanceTo(b);
            return a.DistanceTo(b) <= toleranceD;
            //if (a.m_x != b.m_x || a.m_y != b.m_y)
            //{
            //    return false;
            //}
            //return a.m_z == b.m_z;
        }

        //public static explicit operator AKT_Point3d
        public static explicit operator AKT_Vector3d(AKT_Point3d point)
        {
            return new AKT_Vector3d(point);
        }

        public static explicit operator AKT_Point3d(AKT_Vector3d vector)
        {
            return new AKT_Point3d(vector);
        }

        /// <summary>
        /// Determines whether the first specified point comes after (has superior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;
        /// otherwise, false.</returns>
        public static bool operator >(AKT_Point3d a, AKT_Point3d b)
        {
            if (a.X > b.X)
            {
                return true;
            }
            if (a.X == b.X)
            {
                if (a.Y > b.Y)
                {
                    return true;
                }
                if (a.Y == b.Y && a.Z > b.Z)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether two AKT_Point3d have different values.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if the two points differ in any coordinate; false otherwise.</returns>
        public static bool operator !=(AKT_Point3d a, AKT_Point3d b)
        {
            if (a.m_x != b.m_x || a.m_y != b.m_y)
            {
                return true;
            }
            return a.m_z != b.m_z;
        }

        /// <summary>
        /// Determines whether the first specified point comes before (has inferior sorting value than) the second point.
        /// <para>Coordinates evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z;
        /// otherwise, false.</returns>
        public static bool operator <(AKT_Point3d a, AKT_Point3d b)
        {
            if (a.X < b.X)
            {
                return true;
            }
            if (a.X == b.X)
            {
                if (a.Y < b.Y)
                {
                    return true;
                }
                if (a.Y == b.Y && a.Z < b.Z)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Multiplies a <see cref="T:Rhino.Geometry.AKT_Point3d" /> by a number.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new point that is coordinatewise multiplied by t.</returns>
        public static AKT_Point3d operator *(AKT_Point3d point, double t)
        {
            return new AKT_Point3d(point.m_x * t, point.m_y * t, point.m_z * t);
        }

        /// <summary>
        /// Multiplies a <see cref="T:Rhino.Geometry.AKT_Point3d" /> by a number.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new point that is coordinatewise multiplied by t.</returns>
        public static AKT_Point3d operator *(double t, AKT_Point3d point)
        {
            return new AKT_Point3d(point.m_x * t, point.m_y * t, point.m_z * t);
        }

        /// <summary>
        /// Subtracts a vector from a point.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new point that is the difference of point minus vector.</returns>
        public static AKT_Point3d operator -(AKT_Point3d point, AKT_Vector3d vector)
        {
            return new AKT_Point3d(point.m_x - vector.m_x, point.m_y - vector.m_y, point.m_z - vector.m_z);
        }

        /// <summary>
        /// Subtracts a point from another point.
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">Another point.</param>
        /// <returns>A new vector that is the difference of point minus vector.</returns>
        public static AKT_Vector3d operator -(AKT_Point3d point1, AKT_Point3d point2)
        {
            return new AKT_Vector3d(point1.m_x - point2.m_x, point1.m_y - point2.m_y, point1.m_z - point2.m_z);
        }

        /// <summary>
        /// Computes the additive inverse of all coordinates in the point, and returns the new point.
        /// </summary>
        /// <param name="point">A point.</param>
        /// <returns>A point value that, when summed with the point input, yields the <see cref="P:Rhino.Geometry.AKT_Point3d.Origin" />.</returns>
        public static AKT_Point3d operator -(AKT_Point3d point)
        {
            return new AKT_Point3d(-point.m_x, -point.m_y, -point.m_z);
        }

        /// <summary>
        /// Subtracts a vector from a point.
        /// <para>(Provided for languages that do not support operator overloading. You can use the - operator otherwise)</para>
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="point">A point.</param>
        /// <returns>A new point that is the difference of point minus vector.</returns>
        public static AKT_Point3d Subtract(AKT_Point3d point, AKT_Vector3d vector)
        {
            return new AKT_Point3d(point.m_x - vector.m_x, point.m_y - vector.m_y, point.m_z - vector.m_z);
        }

        /// <summary>
        /// Subtracts a point from another point.
        /// <para>(Provided for languages that do not support operator overloading. You can use the - operator otherwise)</para>
        /// </summary>
        /// <param name="point1">A point.</param>
        /// <param name="point2">Another point.</param>
        /// <returns>A new vector that is the difference of point minus vector.</returns>
        public static AKT_Vector3d Subtract(AKT_Point3d point1, AKT_Point3d point2)
        {
            return new AKT_Vector3d(point1.m_x - point2.m_x, point1.m_y - point2.m_y, point1.m_z - point2.m_z);
        }

        /// <summary>
        /// Constructs the string representation for the current point.
        /// </summary>
        /// <returns>The point representation in the form X,Y,Z.</returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Constructs the string representation for the current point.
        /// </summary>
        /// <returns>The point representation in the form X,Y,Z and trims the numbers according to the current tolerance</returns>
        public string ToString(bool trimDecimals)
        {
            if (this == Unset)
            {
                return "<unset>";
            }

            if (trimDecimals)
                return string.Format("{0}, {1}, {2}", this.m_x.ToString(StringDecimal), this.m_y.ToString(StringDecimal), this.m_z.ToString(StringDecimal));
            else
                return string.Format("{0}, {1}, {2}", this.m_x.ToString(), this.m_y.ToString(), this.m_z.ToString());
        }

        public AKT_Point3d ShrinkToTolerance()
        {
            m_x = (double)RoundNumber((decimal)X, Tolerance, DecimalPlaceTolerance);
            m_y = (double)RoundNumber((decimal)Y, Tolerance, DecimalPlaceTolerance);
            m_z = (double)RoundNumber((decimal)Z, Tolerance, DecimalPlaceTolerance);

            return this;


            //double x = Math.Round(Math.Round(X / Tolerance) * Tolerance, DecimalPlaceTolerance);
            //double y = Math.Round(Math.Round(Y / Tolerance) * Tolerance, DecimalPlaceTolerance);
            //double z = Math.Round(Math.Round(Z / Tolerance) * Tolerance, DecimalPlaceTolerance);
            //return new AKT_Point3d(x, y, z);
        }

        public static AKT_Point3d ShrinkToTolerance(AKT_Point3d p)
        {
            double x = (double)RoundNumber((decimal)p.X, Tolerance, DecimalPlaceTolerance);
            double y = (double)RoundNumber((decimal)p.Y, Tolerance, DecimalPlaceTolerance);
            double z = (double)RoundNumber((decimal)p.Z, Tolerance, DecimalPlaceTolerance);

            return new AKT_Point3d(x, y, z);
        }

        public static decimal RoundNumber(decimal d, decimal tolerance, int decimalPlaces)
        {
            decimal d1 = d / tolerance;

            decimal dt = Math.Round(d1, MidpointRounding.AwayFromZero);

            decimal dd = dt * tolerance;

            return Math.Round(dd, decimalPlaces, MidpointRounding.AwayFromZero);
        }

        public class BadComparer : IEqualityComparer<AKT_Point3d>
        {
            public static double tolerance = 0.001;

            public BadComparer()
                : this(AKT_Point3d.toleranceD)
            {

            }
            public BadComparer(double tol)
            {
                tolerance = tol;
            }
            public bool Equals(AKT_Point3d a, AKT_Point3d b)
            {
                return a.DistanceTo(b) <= tolerance;
            }
            public int GetHashCode(AKT_Point3d s)
            {
                return 0;
            }
        }
    }
}


