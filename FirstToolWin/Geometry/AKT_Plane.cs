using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Geometry
{
    public struct AKT_Plane : IEquatable<AKT_Plane>
    {
        public Guid guid;
        internal AKT_Point3d m_origin;
        internal AKT_Vector3d m_xaxis;
        internal AKT_Vector3d m_yaxis;
        internal AKT_Vector3d m_zaxis;

        /// <summary>
        /// Gets the normal of this plane. This is essentially the ZAxis of the plane.
        /// </summary>
        public AKT_Vector3d Normal
        {
            get
            {
                return this.ZAxis;
            }
        }

        /// <summary>
        /// Gets or sets the origin point of this plane.
        /// </summary>
        public AKT_Point3d Origin
        {
            get
            {
                return this.m_origin;
            }
            set
            {
                this.m_origin = value;
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the origin of this plane.
        /// </summary>
        public double OriginX
        {
            get
            {
                return this.m_origin.X;
            }
            set
            {
                this.m_origin.X = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the origin of this plane.
        /// </summary>
        public double OriginY
        {
            get
            {
                return this.m_origin.Y;
            }
            set
            {
                this.m_origin.Y = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z coordinate of the origin of this plane.
        /// </summary>
        public double OriginZ
        {
            get
            {
                return this.m_origin.Z;
            }
            set
            {
                this.m_origin.Z = value;
            }
        }

        /// <summary>
        /// Gets a plane that contains Unset origin and axis vectors.
        /// </summary>
        public static AKT_Plane Unset
        {
            get
            {
                AKT_Plane plane = new AKT_Plane()
                {
                    Origin = AKT_Point3d.Unset,
                    XAxis = AKT_Vector3d.Unset,
                    YAxis = AKT_Vector3d.Unset,
                    ZAxis = AKT_Vector3d.Unset
                };
                return plane;
            }
        }

        /// <summary>
        /// plane coincident with the World XY plane.
        /// </summary>
        public static AKT_Plane WorldXY
        {
            get
            {
                AKT_Plane plane = new AKT_Plane()
                {
                    XAxis = new AKT_Vector3d(1, 0, 0),
                    YAxis = new AKT_Vector3d(0, 1, 0),
                    ZAxis = new AKT_Vector3d(0, 0, 1)
                };
                return plane;
            }
        }

        /// <summary>
        /// plane coincident with the World YZ plane.
        /// </summary>
        public static AKT_Plane WorldYZ
        {
            get
            {
                AKT_Plane plane = new AKT_Plane()
                {
                    XAxis = new AKT_Vector3d(0, 1, 0),
                    YAxis = new AKT_Vector3d(0, 0, 1),
                    ZAxis = new AKT_Vector3d(1, 0, 0)
                };
                return plane;
            }
        }

        /// <summary>
        /// plane coincident with the World ZX plane.
        /// </summary>
        public static AKT_Plane WorldZX
        {
            get
            {
                AKT_Plane plane = new AKT_Plane()
                {
                    XAxis = new AKT_Vector3d(0, 0, 1),
                    YAxis = new AKT_Vector3d(1, 0, 0),
                    ZAxis = new AKT_Vector3d(0, 1, 0)
                };
                return plane;
            }
        }

        /// <summary>
        /// Gets or sets the X axis vector of this plane.
        /// </summary>
        public AKT_Vector3d XAxis
        {
            get
            {
                return this.m_xaxis;
            }
            set
            {
                this.m_xaxis = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y axis vector of this plane.
        /// </summary>
        public AKT_Vector3d YAxis
        {
            get
            {
                return this.m_yaxis;
            }
            set
            {
                this.m_yaxis = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z axis vector of this plane.
        /// </summary>
        public AKT_Vector3d ZAxis
        {
            get
            {
                return this.m_zaxis;
            }
            set
            {
                this.m_zaxis = value;
            }
        }

        /// <summary>Copy constructor.
        /// <para>This is nothing special and performs the same as assigning to another variable.</para>
        /// </summary>
        /// <param name="other">The source plane value.</param>
        public AKT_Plane(AKT_Plane other)
        {
            this = other;
        }

         public AKT_Plane(AKT_Point3d origin)
            : this(origin, AKT_Vector3d.XAxis, AKT_Vector3d.YAxis)
        {

        }

        public AKT_Plane(AKT_Point3d origin, AKT_Point3d pntX, AKT_Point3d pntY)
            : this(origin, new AKT_Vector3d(pntX, origin), new AKT_Vector3d(pntY, origin))
        { }

        public AKT_Plane(AKT_Point3d origin, AKT_Vector3d vX, AKT_Vector3d vY)
        {
            this.m_origin = origin;
            this.m_xaxis = vX;
            this.m_yaxis = vY;
            this.m_zaxis = AKT_Vector3d.CrossProduct(vX, vY);
            this.guid = Guid.Empty;
        }

        //public AKT_Plane(AKT_Point3d p, AKT_Vector3d direction)
        //{
        //    AKT_Vector3d dir;
        //    //if (direction == AKT_Vector3d.ZAxis) dir = 
        //    AKT_Vector3d.CrossProduct()
        //}


        /// <summary>
        /// Determines if an object is a plane and has the same components as this plane.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>true if obj is a plane and has the same components as this plane; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AKT_Plane))
            {
                return false;
            }
            return this == (AKT_Plane)obj;
        }

        /// <summary>
        /// Determines if another plane has the same components as this plane.
        /// </summary>
        /// <param name="plane">A plane.</param>
        /// <returns>true if plane has the same components as this plane; false otherwise.</returns>
        public bool Equals(AKT_Plane plane)
        {
            if (!(this.m_origin == plane.m_origin) || !(this.m_xaxis == plane.m_xaxis) || !(this.m_yaxis == plane.m_yaxis))
            {
                return false;
            }
            return this.m_zaxis == plane.m_zaxis;
        }


        /// <summary>
        /// Determines if two planes are equal.
        /// </summary>
        /// <param name="a">A first plane.</param>
        /// <param name="b">A second plane.</param>
        /// <returns>true if the two planes have all equal components; false otherwise.</returns>
        public static bool operator ==(AKT_Plane a, AKT_Plane b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines if two planes are different.
        /// </summary>
        /// <param name="a">A first plane.</param>
        /// <param name="b">A second plane.</param>
        /// <returns>true if the two planes have any different componet components; false otherwise.</returns>
        public static bool operator !=(AKT_Plane a, AKT_Plane b)
        {
            if (a.m_origin != b.m_origin || a.m_xaxis != b.m_xaxis || a.m_yaxis != b.m_yaxis)
            {
                return true;
            }
            return a.m_zaxis != b.m_zaxis;
        }


        /// <summary>
        /// Gets the point on the plane closest to a test point.
        /// </summary>
        /// <param name="testPoint">Point to get close to.</param>
        /// <returns>
        /// The point on the plane that is closest to testPoint, 
        /// or Point3d.Unset on failure.
        /// </returns>
        public AKT_Point3d ClosestPoint(AKT_Point3d testPoint)
        {
            double num;
            double num1;
            if (!this.ClosestParameter(testPoint, out num, out num1))
            {
                return AKT_Point3d.Unset;
            }
            return this.PointAt(num, num1);
        }

        /// <summary>
        /// Evaluate a point on the plane.
        /// </summary>
        /// <param name="u">evaulation parameter.</param>
        /// <param name="v">evaulation parameter.</param>
        /// <returns>plane.origin + u*plane.xaxis + v*plane.yaxis.</returns>
        public AKT_Point3d PointAt(double u, double v)
        {
            return (this.Origin + (u * this.XAxis)) + (v * this.YAxis);
        }


        /// <summary>
        /// Gets the parameters of the point on the plane closest to a test point.
        /// </summary>
        /// <param name="testPoint">Point to get close to.</param>
        /// <param name="s">Parameter along plane X-direction.</param>
        /// <param name="t">Parameter along plane Y-direction.</param>
        /// <returns>
        /// true if a parameter could be found, 
        /// false if the point could not be projected successfully.
        /// </returns>
        /// <example>
        /// <code source="examples\vbnet\ex_addlineardimension2.vb" lang="vbnet" />
        /// <code source="examples\cs\ex_addlineardimension2.cs" lang="cs" />
        /// <code source="examples\py\ex_addlineardimension2.py" lang="py" />
        /// </example>
        public bool ClosestParameter(AKT_Point3d testPoint, out double s, out double t)
        {
            AKT_Vector3d vector3d = testPoint - this.Origin;
            s = vector3d * this.XAxis;
            t = vector3d * this.YAxis;
            return true;
        }
    }
}
