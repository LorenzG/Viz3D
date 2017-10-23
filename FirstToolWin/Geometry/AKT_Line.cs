using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Geometry
{
    public class AKT_Line
    {
        public Guid guid;
        internal AKT_Point3d m_from;
        internal AKT_Point3d m_to;

        /// <summary>
        /// Gets the direction of this AKT_Line segment. 
        /// The length of the direction vector equals the length of 
        /// the AKT_Line segment.
        /// </summary>
        /// <example>
        /// <code source="examples\vbnet\ex_intersectAKT_Lines.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_intersectAKT_Lines.cs" lang="cs" />
        /// <code source="examples\py\ex_intersectAKT_Lines.py" lang="py" />
        /// </example>
        public AKT_Vector3d Direction
        {
            get
            {
                return this.To - this.From;
            }
        }

        /// <summary>
        /// Start point of AKT_Line segment.
        /// </summary>
        public AKT_Point3d From
        {
            get
            {
                return this.m_from;
            }
            set
            {
                this.m_from = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the AKT_Line From point.
        /// </summary>
        public double FromX
        {
            get
            {
                return this.m_from.X;
            }
            set
            {
                this.m_from.X = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the AKT_Line From point.
        /// </summary>
        public double FromY
        {
            get
            {
                return this.m_from.Y;
            }
            set
            {
                this.m_from.Y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z coordinate of the AKT_Line From point.
        /// </summary>
        public double FromZ
        {
            get
            {
                return this.m_from.Z;
            }
            set
            {
                this.m_from.Z = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the length of this AKT_Line segment. 
        /// Note that a negative length will invert the AKT_Line segment without 
        /// making the actual length negative. The AKT_Line From point will remain fixed 
        /// when a new Length is set.
        /// </summary>
        public double Length
        {
            get
            {
                return this.From.DistanceTo(this.To);
            }
            set
            {
                AKT_Vector3d to = this.To - this.From;
                if (!to.Unitize())
                {
                    to = new AKT_Vector3d(0, 0, 1);
                }
                this.To = this.From + (to * value);
            }
        }

        /// <summary>
        /// End point of AKT_Line segment.
        /// </summary>
        public AKT_Point3d To
        {
            get
            {
                return this.m_to;
            }
            set
            {
                this.m_to = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the AKT_Line To point.
        /// </summary>
        public double ToX
        {
            get
            {
                return this.m_to.X;
            }
            set
            {
                this.m_to.X = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the AKT_Line To point.
        /// </summary>
        public double ToY
        {
            get
            {
                return this.m_to.Y;
            }
            set
            {
                this.m_to.Y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z coordinate of the AKT_Line To point.
        /// </summary>
        public double ToZ
        {
            get
            {
                return this.m_to.Z;
            }
            set
            {
                this.m_to.Z = value;
            }
        }

        /// <summary>
        /// Gets the tangent of the AKT_Line segment. 
        /// Note that tangent vectors are always unit vectors.
        /// </summary>
        /// <value>Sets only the direction of the AKT_Line, the length is maintained.</value>
        public AKT_Vector3d UnitTangent
        {
            get
            {
                AKT_Vector3d to = this.To - this.From;
                to.Unitize();
                return to;
            }
        }

        /// <summary>
        /// Gets a AKT_Line segment which has <see cref="P:Rhino.Geometry.AKT_Point3d.Unset" /> end points.
        /// </summary>
        public static AKT_Line Unset
        {
            get
            {
                return new AKT_Line(AKT_Point3d.Unset, AKT_Point3d.Unset);
            }
        }

        /// <summary>
        /// Constructs a new AKT_Line segment between two points.
        /// </summary>
        /// <param name="from">Start point of AKT_Line.</param>
        /// <param name="to">End point of AKT_Line.</param>
        public AKT_Line(AKT_Point3d from, AKT_Point3d to)
        {
            this.m_from = from;
            this.m_to = to;
        }

        /// <summary>
        /// Constructs a new AKT_Line segment from start point and span vector.
        /// </summary>
        /// <param name="start">Start point of AKT_Line segment.</param>
        /// <param name="span">Direction and length of AKT_Line segment.</param>
        public AKT_Line(AKT_Point3d start, AKT_Vector3d span)
        {
            this.m_from = start;
            this.m_to = start + span;
        }

        /// <summary>
        /// Constructs a new AKT_Line segment from start point, direction and length.
        /// </summary>
        /// <param name="start">Start point of AKT_Line segment.</param>
        /// <param name="direction">Direction of AKT_Line segment.</param>
        /// <param name="length">Length of AKT_Line segment.</param>
        public AKT_Line(AKT_Point3d start, AKT_Vector3d direction, double length)
        {
            AKT_Vector3d vector3d = direction;
            if (!vector3d.Unitize())
            {
                vector3d = new AKT_Vector3d(0, 0, 1);
            }
            this.m_from = start;
            this.m_to = start + (vector3d * length);
        }

        /// <summary>
        /// Constructs a new AKT_Line segment between two points.
        /// </summary>
        /// <param name="x0">The X coordinate of the first point.</param>
        /// <param name="y0">The Y coordinate of the first point.</param>
        /// <param name="z0">The Z coordinate of the first point.</param>
        /// <param name="x1">The X coordinate of the second point.</param>
        /// <param name="y1">The Y coordinate of the second point.</param>
        /// <param name="z1">The Z coordinate of the second point.</param>
        public AKT_Line(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            this.m_from = new AKT_Point3d(x0, y0, z0);
            this.m_to = new AKT_Point3d(x1, y1, z1);
        }         

        /// <summary>
        /// Determines whether an object is a AKT_Line that has the same value as this AKT_Line.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>true if obj is a AKT_Line and has the same coordinates as this; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AKT_Line))
            {
                return false;
            }
            return this == (AKT_Line)obj;
        }

        /// <summary>
        /// Determines whether a AKT_Line has the same value as this AKT_Line.
        /// </summary>
        /// <param name="other">A AKT_Line.</param>
        /// <returns>true if other has the same coordinates as this; otherwise false.</returns>
        public bool Equals(AKT_Line other)
        {
            return this == other;
        }

        /// <summary>
        /// Extend the AKT_Line by custom distances on both sides.
        /// </summary>
        /// <param name="startLength">
        /// Distance to extend the AKT_Line at the start point. 
        /// Positive distance result in longer AKT_Lines.
        /// </param>
        /// <param name="endLength">
        /// Distance to extend the AKT_Line at the end point. 
        /// Positive distance result in longer AKT_Lines.
        /// </param>
        /// <returns>true on success, false on failure.</returns>
        public bool Extend(double startLength, double endLength)
        {
            if (this.Length == 0)
            {
                return false;
            }
            AKT_Point3d mFrom = this.m_from;
            AKT_Point3d mTo = this.m_to;
            AKT_Vector3d unitTangent = this.UnitTangent;
            if (startLength != 0)
            {
                mFrom = this.m_from - (startLength * unitTangent);
            }
            if (endLength != 0)
            {
                mTo = this.m_to + (endLength * unitTangent);
            }
            this.m_from = mFrom;
            this.m_to = mTo;
            return true;
        }

        /// <summary>
        /// Flip the endpoints of the AKT_Line segment.
        /// </summary>
        public void Flip()
        {
            AKT_Point3d from = this.From;
            this.From = this.To;
            this.To = from;
        }

        /// <summary>
        /// Computes a hash number that represents this AKT_Line.
        /// </summary>
        /// <returns>A number that is not unique to the value of this AKT_Line.</returns>
        public override int GetHashCode()
        {
            return this.From.GetHashCode() ^ this.To.GetHashCode();
        }

        /// <summary>
        /// Determines whether two AKT_Lines have the same value.
        /// </summary>
        /// <param name="a">A AKT_Line.</param>
        /// <param name="b">Another AKT_Line.</param>
        /// <returns>true if a has the same coordinates as b; otherwise false.</returns>
        public static bool operator ==(AKT_Line a, AKT_Line b)
        {
            if (a.From != b.From)
            {
                return false;
            }
            return a.To == b.To;
        }

        /// <summary>
        /// Determines whether two AKT_Lines have different values.
        /// </summary>
        /// <param name="a">A AKT_Line.</param>
        /// <param name="b">Another AKT_Line.</param>
        /// <returns>true if a has any coordinate that distinguishes it from b; otherwise false.</returns>
        public static bool operator !=(AKT_Line a, AKT_Line b)
        {
            if (a.From != b.From)
            {
                return true;
            }
            return a.To != b.To;
        }

        /// <summary>
        /// Evaluates the AKT_Line at the specified parameter.
        /// </summary>
        /// <param name="t">Parameter to evaluate AKT_Line segment at. AKT_Line parameters are normalised parameters.</param>
        /// <returns>The point at the specified parameter.</returns>
        /// <example>
        /// <code source="examples\vbnet\ex_intersectAKT_Lines.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_intersectAKT_Lines.cs" lang="cs" />
        /// <code source="examples\py\ex_intersectAKT_Lines.py" lang="py" />
        /// </example>
        public AKT_Point3d PointAt(double t)
        {
            double num = 1 - t;
            return new AKT_Point3d((this.From.m_x == this.To.m_x ? this.From.m_x : num * this.From.m_x + t * this.To.m_x), (this.From.m_y == this.To.m_y ? this.From.m_y : num * this.From.m_y + t * this.To.m_y), (this.From.m_z == this.To.m_z ? this.From.m_z : num * this.From.m_z + t * this.To.m_z));
        }

        /// <summary>
        /// Contructs the string representation of this AKT_Line, in the form "From,To".
        /// </summary>
        /// <returns>A text string.</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}", From.ToString(), To.ToString());
        }

        public static double LineToPointDistance2D(AKT_Line l, AKT_Point3d p, bool isSegment = true)
        {
            AKT_Point3d pointA = l.To;
            AKT_Point3d pointB = l.To;
            AKT_Point3d pointC = p;
            AKT_Vector3d pA = new AKT_Vector3d(p, pointA);

            throw new NotImplementedException();
            //AKT_Vector3d dist = AKT_Vector3d.CrossProduct(l.Direction, pA) / l.Length;
            //if (isSegment)
            //{
            //    double dot1 = DotProduct(pointA, pointB, p);
            //    if (dot1 > 0)
            //        return Distance(pointB, p);

            //    double dot2 = DotProduct(pointB, pointA, p);
            //    if (dot2 > 0)
            //        return Distance(pointA, p);
            //}
            //return Math.Abs(dist);
        }
    }
}
