using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry
{
    public struct AKT_NurbsPoint 
    {
        AKT_Point3d pnt3d;
        double w;


        public AKT_NurbsPoint(AKT_Point3d pnt3d, double w)
        {
            this.pnt3d = pnt3d;
            this.w = w;
        }
        
        
        public AKT_Point3d Location
        {
            get { return pnt3d; }
            set { pnt3d = value; }
        }
        public double Weight
        {
            get { return w; }
            set { w = value; }
        }


        public static implicit operator AKT_Point3d(AKT_NurbsPoint np)
        {
            return new AKT_Point3d(np.Location.X, np.Location.Y, np.Location.Z);
        }

        public override string ToString()
        {
            return pnt3d.ToString() + " w: " + w;
        }
    }
}
