using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Geometry
{
    public struct AKT_Vector3d : IEquatable<AKT_Vector3d>, IComparable<AKT_Vector3d>
    {
        internal double m_x;
        internal double m_y;
        internal double m_z;

        /// <summary>
        /// Gets a value indicating whether or not this is a unit vector. 
        /// A unit vector has length 1.
        /// </summary>
        public bool IsUnitVector
        {
            get
            {
                double lengthHelper = AKT_Vector3d.GetLengthHelper(this.m_x, this.m_y, this.m_z);
                return Math.Abs(lengthHelper - 1) <= 1.490116119385E-08;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the X, Y, and Z values are all equal to 0.0.
        /// </summary>
        public bool IsZero
        {
            get
            {
                if (this.m_x != 0 || this.m_y != 0)
                {
                    return false;
                }
                return this.m_z == 0;
            }
        }

        /// <summary>
        /// Gets or sets a vector component at the given index.
        /// </summary>
        /// <param name="index">Index of vector component. Valid values are: 
        /// <para>0 = X-component</para>
        /// <para>1 = Y-component</para>
        /// <para>2 = Z-component</para>
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
        /// Computes the length (or magnitude, or size) of this vector.
        /// This is an application of Pythagoras' theorem.
        /// If this vector is invalid, its length is considered 0.
        /// </summary>
        public double Length
        {
            get
            {
                return AKT_Vector3d.GetLengthHelper(this.m_x, this.m_y, this.m_z);
            }
        }

        /// <summary>
        /// Computes the squared length (or magnitude, or size) of this vector.
        /// This is an application of Pythagoras' theorem.
        /// While the Length property checks for input validity,
        /// this property does not. You should check validity in advance,
        /// if this vector can be invalid.
        /// </summary>
        public double SquareLength
        {
            get
            {
                return this.m_x * this.m_x + this.m_y * this.m_y + this.m_z * this.m_z;
            }
        }

        /// <summary>
        /// Gets the value of the vector with each component set to RhinoMath.UnsetValue.
        /// </summary>
        public static AKT_Vector3d Unset
        {
            get
            {
                return new AKT_Vector3d(-1.23432101234321E+308, -1.23432101234321E+308, -1.23432101234321E+308);
            }
        }

        /// <summary>
        /// Gets or sets the X (first) component of the vector.
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
        /// Gets the value of the vector with components 1,0,0.
        /// </summary>
        public static AKT_Vector3d XAxis
        {
            get
            {
                return new AKT_Vector3d(1, 0, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Y (second) component of the vector.
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
        /// Gets the value of the vector with components 0,1,0.
        /// </summary>
        public static AKT_Vector3d YAxis
        {
            get
            {
                return new AKT_Vector3d(0, 1, 0);
            }
        }

        /// <summary>
        /// Gets or sets the Z (third) component of the vector.
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
        /// Gets the value of the vector with components 0,0,1.
        /// </summary>
        public static AKT_Vector3d ZAxis
        {
            get
            {
                return new AKT_Vector3d(0, 0, 1);
            }
        }

        /// <summary>
        /// Gets the value of the vector with components 0,0,0.
        /// </summary>
        public static AKT_Vector3d Zero
        {
            get
            {
                return new AKT_Vector3d();
            }
        }

        /// <summary>
        /// Initializes a new instance of a vector, copying the three components from the three coordinates of a point.
        /// </summary>
        /// <param name="point">The point to copy from.</param>
        public AKT_Vector3d(AKT_Point3d point)
            : this(point.m_x, point.m_y, point.m_z)
        {

        }

        /// <summary>
        /// Initializes a new instance of a vector, copying the three components from a vector.
        /// </summary>
        /// <param name="vector">A double-precision vector.</param>
        public AKT_Vector3d(AKT_Vector3d vector)
            : this(vector.m_x, vector.m_y, vector.m_z)
        {
        }

        public AKT_Vector3d(AKT_Point3d end, AKT_Point3d start)
            : this(end - start) { }

        /// <summary>
        /// Initializes a new instance of a vector, using its three components.
        /// </summary>
        /// <param name="x">The X (first) component.</param>
        /// <param name="y">The Y (second) component.</param>
        /// <param name="z">The Z (third) component.</param>
        public AKT_Vector3d(double x, double y, double z)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_z = z;
        }

        public double[] ToArray()
        {
            return new double[] { m_x, m_y, m_z };
        }

        /// <summary>
        /// Sums up two vectors.
        /// <para>(Provided for languages that do not support operator overloading. You can use the + operator otherwise)</para>
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>A new vector that results from the componentwise addition of the two vectors.</returns>
        public static AKT_Vector3d Add(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return new AKT_Vector3d(vector1.m_x + vector2.m_x, vector1.m_y + vector2.m_y, vector1.m_z + vector2.m_z);
        }

        /// <summary>
        /// Compares this <see cref="T:Rhino.Geometry.Vector3d" /> with another <see cref="T:Rhino.Geometry.Vector3d" />.
        /// <para>Component evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="other">The other <see cref="T:Rhino.Geometry.Vector3d" /> to use in comparison.</param>
        /// <returns>
        /// <para> 0: if this is identical to other</para>
        /// <para>-1: if this.X &lt; other.X</para>
        /// <para>-1: if this.X == other.X and this.Y &lt; other.Y</para>
        /// <para>-1: if this.X == other.X and this.Y == other.Y and this.Z &lt; other.Z</para>
        /// <para>+1: otherwise.</para>
        /// </returns>
        public int CompareTo(AKT_Vector3d other)
        {
            if (this.m_x < other.m_x)
            {
                return -1;
            }
            if (this.m_x > other.m_x)
            {
                return 1;
            }
            if (this.m_y < other.m_y)
            {
                return -1;
            }
            if (this.m_y > other.m_y)
            {
                return 1;
            }
            if (this.m_z < other.m_z)
            {
                return -1;
            }
            if (this.m_z > other.m_z)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Computes the cross product (or vector product, or exterior product) of two vectors.
        /// <para>This operation is not commutative.</para>
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>A new vector that is perpendicular to both a and b,
        /// <para>has Length == a.Length * b.Length and</para>
        /// <para>with a result that is oriented following the right hand rule.</para>
        /// </returns>
        public static AKT_Vector3d CrossProduct(AKT_Vector3d a, AKT_Vector3d b)
        {
            return new AKT_Vector3d(a.m_y * b.m_z - b.m_y * a.m_z, a.m_z * b.m_x - b.m_z * a.m_x, a.m_x * b.m_y - b.m_x * a.m_y);
        }

        /// <summary>
        /// Divides a <see cref="T:Rhino.Geometry.Vector3d" /> by a number, having the effect of shrinking it.
        /// <para>(Provided for languages that do not support operator overloading. You can use the / operator otherwise)</para>
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is componentwise divided by t.</returns>
        public static AKT_Vector3d Divide(AKT_Vector3d vector, double t)
        {
            return new AKT_Vector3d(vector.m_x / t, vector.m_y / t, vector.m_z / t);
        }

        /// <summary>
        /// Determines whether the specified System.Object is a Vector3d and has the same values as the present vector.
        /// </summary>
        /// <param name="obj">The specified object.</param>
        /// <returns>true if obj is a Vector3d and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AKT_Vector3d))
            {
                return false;
            }
            return this == (AKT_Vector3d)obj;
        }

        /// <summary>
        /// Determines whether the specified vector has the same value as the present vector.
        /// </summary>
        /// <param name="vector">The specified vector.</param>
        /// <returns>true if vector has the same coordinates as this; otherwise false.</returns>
        public bool Equals(AKT_Vector3d vector)
        {
            return this == vector;
        }

        /// <summary>
        /// Computes the hash code for the current vector.
        /// </summary>
        /// <returns>A non-unique number that represents the components of this vector.</returns>
        public override int GetHashCode()
        {
            return this.m_x.GetHashCode() ^ this.m_y.GetHashCode() ^ this.m_z.GetHashCode();
        }

        /// <summary>
        /// Determines whether a <see cref="T:System.Double" /> value is valid within the RhinoCommon context.
        /// <para>Rhino does not use Double.NaN by convention, so this test evaluates to true if:</para>
        /// <para>x is not equal to RhinoMath.UnsetValue</para>
        /// <para>System.Double.IsNaN(x) evaluates to false</para>
        /// <para>System.Double.IsInfinity(x) evaluates to false</para>
        /// </summary>
        /// <param name="x"><see cref="T:System.Double" /> number to test for validity.</param>
        /// <returns>true if the number if valid, false if the number is NaN, Infinity or Unset.</returns>
        public static bool IsValidDouble(double x)
        {
            if (x == -1.23432101234321E+308 || double.IsInfinity(x))
            {
                return false;
            }
            return !double.IsNaN(x);
        }

        internal static double GetLengthHelper(double dx, double dy, double dz)
        {
            double num;
            if (!IsValidDouble(dx) || !IsValidDouble(dy) || !IsValidDouble(dz))
            {
                return 0;
            }
            double num1 = Math.Abs(dx);
            double num2 = Math.Abs(dy);
            double num3 = Math.Abs(dz);
            if (num2 >= num1 && num2 >= num3)
            {
                num = num1;
                num1 = num2;
                num2 = num;
            }
            else if (num3 >= num1 && num3 >= num2)
            {
                num = num1;
                num1 = num3;
                num3 = num;
            }
            if (num1 <= 2.2250738585072E-308)
            {
                num = (num1 <= 0 || !IsValidDouble(num1) ? 0 : num1);
            }
            else
            {
                num = 1 / num1;
                num2 = num2 * num;
                num3 = num3 * num;
                num = num1 * Math.Sqrt(1 + num2 * num2 + num3 * num3);
            }
            return num;
        }

        /// <summary>
        ///  Test to see whether this vector is perpendicular to within one degree of another one. 
        /// </summary>
        ///  <param name="other">Vector to compare to.</param>
        /// <returns>true if both vectors are perpendicular, false if otherwise.</returns>
        public bool IsPerpendicularTo(AKT_Vector3d other)
        {
            return this.IsPerpendicularTo(other, 0.0174532925199433);
        }

        /// <summary>
        ///  Determines whether this vector is perpendicular to another vector, within a provided angle tolerance. 
        /// </summary>
        ///  <param name="other">Vector to use for comparison.</param>
        ///  <param name="angleTolerance">Angle tolerance (in radians).</param>
        /// <returns>true if vectors form Pi-radians (90-degree) angles with each other; otherwise false.</returns>
        public bool IsPerpendicularTo(AKT_Vector3d other, double angleTolerance)
        {
            bool flag = false;
            double length = this.Length * other.Length;
            if (length > 0 && Math.Abs((this.m_x * other.m_x + this.m_y * other.m_y + this.m_z * other.m_z) / length) <= Math.Sin(angleTolerance))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// <para>(Provided for languages that do not support operator overloading. You can use the * operator otherwise)</para>
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static AKT_Vector3d Multiply(AKT_Vector3d vector, double t)
        {
            return new AKT_Vector3d(vector.m_x * t, vector.m_y * t, vector.m_z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// <para>(Provided for languages that do not support operator overloading. You can use the * operator otherwise)</para>
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static AKT_Vector3d Multiply(double t, AKT_Vector3d vector)
        {
            return new AKT_Vector3d(vector.m_x * t, vector.m_y * t, vector.m_z * t);
        }

        /// <summary>
        /// Multiplies two vectors together, returning the dot product (or inner product).
        /// This differs from the cross product.
        /// <para>(Provided for languages that do not support operator overloading. You can use the * operator otherwise)</para>
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>
        /// A value that results from the evaluation of v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z.
        /// <para>This value equals v1.Length * v2.Length * cos(alpha), where alpha is the angle between vectors.</para>
        /// </returns>
        public static double Multiply(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return vector1.m_x * vector2.m_x + vector1.m_y * vector2.m_y + vector1.m_z * vector2.m_z;
        }

        /// <summary>
        /// Computes the opposite vector.
        /// <para>(Provided for languages that do not support operator overloading. You can use the - unary operator otherwise)</para>
        /// </summary>
        /// <param name="vector">A vector to negate.</param>
        /// <returns>A new vector where all components were multiplied by -1.</returns>
        public static AKT_Vector3d Negate(AKT_Vector3d vector)
        {
            return new AKT_Vector3d(-vector.m_x, -vector.m_y, -vector.m_z);
        }

        /// <summary>
        /// Sums up two vectors.
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>A new vector that results from the componentwise addition of the two vectors.</returns>
        public static AKT_Vector3d operator +(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return new AKT_Vector3d(vector1.m_x + vector2.m_x, vector1.m_y + vector2.m_y, vector1.m_z + vector2.m_z);
        }

        /// <summary>
        /// Divides a <see cref="T:Rhino.Geometry.Vector3d" /> by a number, having the effect of shrinking it.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is componentwise divided by t.</returns>
        public static AKT_Vector3d operator /(AKT_Vector3d vector, double t)
        {
            return new AKT_Vector3d(vector.m_x / t, vector.m_y / t, vector.m_z / t);
        }

        /// <summary>
        /// Determines whether two vectors have the same value.
        /// </summary>
        /// <param name="a">A vector.</param>
        /// <param name="b">Another vector.</param>
        /// <returns>true if all coordinates are pairwise equal; false otherwise.</returns>
        public static bool operator ==(AKT_Vector3d a, AKT_Vector3d b)
        {
            if (a.m_x != b.m_x || a.m_y != b.m_y)
            {
                return false;
            }
            return a.m_z == b.m_z;
        }

        /// <summary>
        /// Determines whether the first specified vector comes after (has superior sorting value than)
        /// the second vector.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is larger than b.Z;
        /// otherwise, false.</returns>
        public static bool operator >(AKT_Vector3d a, AKT_Vector3d b)
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
        /// Determines whether the first specified vector comes after (has superior sorting value than)
        /// the second vector, or it is equal to it.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is larger than b.X,
        /// or a.X == b.X and a.Y is larger than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &gt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator >=(AKT_Vector3d a, AKT_Vector3d b)
        {
            return a.CompareTo(b) >= 0;
        }

        /// <summary>
        /// Determines whether two vectors have different values.
        /// </summary>
        /// <param name="a">A vector.</param>
        /// <param name="b">Another vector.</param>
        /// <returns>true if any coordinate pair is different; false otherwise.</returns>
        public static bool operator !=(AKT_Vector3d a, AKT_Vector3d b)
        {
            if (a.m_x != b.m_x || a.m_y != b.m_y)
            {
                return true;
            }
            return a.m_z != b.m_z;
        }

        /// <summary>
        /// Determines whether the first specified vector comes before (has inferior sorting value than) the second vector.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z is smaller than b.Z;
        /// otherwise, false.</returns>
        public static bool operator <(AKT_Vector3d a, AKT_Vector3d b)
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
        /// Determines whether the first specified vector comes before
        /// (has inferior sorting value than) the second vector, or it is equal to it.
        /// <para>Components evaluation priority is first X, then Y, then Z.</para>
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>true if a.X is smaller than b.X,
        /// or a.X == b.X and a.Y is smaller than b.Y,
        /// or a.X == b.X and a.Y == b.Y and a.Z &lt;= b.Z;
        /// otherwise, false.</returns>
        public static bool operator <=(AKT_Vector3d a, AKT_Vector3d b)
        {
            return a.CompareTo(b) <= 0;
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="vector">A vector.</param>
        /// <param name="t">A number.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static AKT_Vector3d operator *(AKT_Vector3d vector, double t)
        {
            return new AKT_Vector3d(vector.m_x * t, vector.m_y * t, vector.m_z * t);
        }

        /// <summary>
        /// Multiplies a vector by a number, having the effect of scaling it.
        /// </summary>
        /// <param name="t">A number.</param>
        /// <param name="vector">A vector.</param>
        /// <returns>A new vector that is the original vector coordinatewise multiplied by t.</returns>
        public static AKT_Vector3d operator *(double t, AKT_Vector3d vector)
        {
            return new AKT_Vector3d(vector.m_x * t, vector.m_y * t, vector.m_z * t);
        }

        /// <summary>
        /// Multiplies two vectors together, returning the dot product (or inner product).
        /// This differs from the cross product.
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>
        /// A value that results from the evaluation of v1.X*v2.X + v1.Y*v2.Y + v1.Z*v2.Z.
        /// <para>This value equals v1.Length * v2.Length * cos(alpha), where alpha is the angle between vectors.</para>
        /// </returns>
        public static double operator *(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return vector1.m_x * vector2.m_x + vector1.m_y * vector2.m_y + vector1.m_z * vector2.m_z;
        }

        /// <summary>
        /// Subtracts the second vector from the first one.
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>A new vector that results from the componentwise difference of vector1 - vector2.</returns>
        public static AKT_Vector3d operator -(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return new AKT_Vector3d(vector1.m_x - vector2.m_x, vector1.m_y - vector2.m_y, vector1.m_z - vector2.m_z);
        }

        /// <summary>
        /// Computes the opposite vector.
        /// </summary>
        /// <param name="vector">A vector to negate.</param>
        /// <returns>A new vector where all components were multiplied by -1.</returns>
        public static AKT_Vector3d operator -(AKT_Vector3d vector)
        {
            return new AKT_Vector3d(-vector.m_x, -vector.m_y, -vector.m_z);
        }

        /// <summary>
        ///  Reverses (inverts) this vector in place.
        ///  <para>If this vector is Invalid, no changes will occur and false will be returned.</para>
        /// </summary>
        /// <returns>true on success or false if the vector is invalid.</returns>
        public bool Reverse()
        {
            this.m_x = -this.m_x;
            this.m_y = -this.m_y;
            this.m_z = -this.m_z;
            return true;
        }


        /// <summary>
        /// Subtracts the second vector from the first one.
        /// <para>(Provided for languages that do not support operator overloading. You can use the - operator otherwise)</para>
        /// </summary>
        /// <param name="vector1">A vector.</param>
        /// <param name="vector2">A second vector.</param>
        /// <returns>A new vector that results from the componentwise difference of vector1 - vector2.</returns>
        public static AKT_Vector3d Subtract(AKT_Vector3d vector1, AKT_Vector3d vector2)
        {
            return new AKT_Vector3d(vector1.m_x - vector2.m_x, vector1.m_y - vector2.m_y, vector1.m_z - vector2.m_z);
        }

        //int System.IComparable.CompareTo(object obj)
        //{
        //    if (!(obj is AKT_Vector3d))
        //    {
        //        throw new ArgumentException("Input must be of type Vector3d", "obj");
        //    }
        //    return this.CompareTo((AKT_Vector3d)obj);
        //}

        /// <summary>
        /// Returns the string representation of the current vector, in the form X,Y,Z.
        /// </summary>
        /// <returns>A string with the current location of the point.</returns>
        public override string ToString()
        {
            if (this == Unset)
            {
                return "<unset>";
            }
            return string.Format("{0},{1},{2}", this.m_x.ToString(), this.m_y.ToString(), this.m_z.ToString());
        }

        /// <summary>
        /// Unitizes the vector in place. A unit vector has length 1 unit. 
        /// <para>An invalid or zero length vector cannot be unitized.</para>
        /// </summary>
        /// <returns>true on success or false on failure.</returns>
        public bool Unitize()
        {
            if (Length == 0)
            {
                this = AKT_Vector3d.Zero;
                return true;
            }
            else
            {
                this.X = this.X / this.Length;
                this.Y = this.Y / this.Length;
                this.Z = this.Z / this.Length;
                return true;
            }
        }

        /// <summary>
        /// Compute the angle between two vectors.
        /// <para>This operation is commutative.</para>
        /// </summary>
        /// <param name="a">First vector for angle.</param>
        /// <param name="b">Second vector for angle.</param>
        /// <returns>If the input is valid, the angle (in radians) between a and b; RhinoMath.UnsetValue otherwise.</returns>
        public static double VectorAngle(AKT_Vector3d a, AKT_Vector3d b)
        {
            if (!a.Unitize() || !b.Unitize())
            {
                return -1.23432101234321E+308;
            }
            double mX = a.m_x * b.m_x + a.m_y * b.m_y + a.m_z * b.m_z;
            if (mX > 1)
            {
                mX = 1;
            }
            if (mX < -1)
            {
                mX = -1;
            }
            return Math.Acos(mX);
        }

        public static double VectorAngle(AKT_Vector3d a, AKT_Vector3d b, AKT_Plane plane)
        {
            AKT_Point3d origin = plane.Origin + a;
            AKT_Point3d point3d = plane.Origin + b;
            origin = plane.ClosestPoint(origin);
            point3d = plane.ClosestPoint(point3d);
            a = origin - plane.Origin;
            b = point3d - plane.Origin;
            if (!a.Unitize())
            {
                return -1.23432101234321E+308;
            }
            if (!b.Unitize())
            {
                return -1.23432101234321E+308;
            }
            double num = a * b;
            if (num >= 1)
            {
                num = 1;
            }
            else if (num < -1)
            {
                num = -1;
            }
            double num1 = Math.Acos(num);
            if (Math.Abs(num1) < 1E-64)
            {
                return 0;
            }
            if (Math.Abs(num1 - 3.14159265358979) < 1E-64)
            {
                return 3.14159265358979;
            }
            AKT_Vector3d vector3d = AKT_Vector3d.CrossProduct(a, b);
            if (plane.ZAxis.IsParallelTo(vector3d) == 1)
            {
                return num1;
            }
            return 6.28318530717959 - num1;
        }

        private int IsParallelTo(AKT_Vector3d a)
        {
            if (AKT_Vector3d.CrossProduct(this, a) == AKT_Vector3d.Zero) return 1;
            else return 0;
        }
    }
}
