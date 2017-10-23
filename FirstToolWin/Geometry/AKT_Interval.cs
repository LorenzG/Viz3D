using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FirstToolWin.Geometry
{
    public struct  AKT_Interval
    {
        double t1;
        double t0;
              

        public AKT_Interval(double t0, double t1) 
        {
            this.t0 = t0;
            this.t1 = t1;
        }


        public double T0
        {
            get { return t0; }
        }
        public double T1
        {
            get { return t1; }
        }

    }
}
