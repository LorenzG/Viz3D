using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Geometry
{
    public class AKT_Polyline : IEnumerable<AKT_Point3d>
    {
        public Guid guid;
        List<AKT_Point3d> points;

        public List<AKT_Point3d> Points
        {
            get { return points; }
            set { points = value; }
        }


        /// <summary>
        /// Initializes a new empty AKT_PolyLine.
        /// </summary>
        public AKT_Polyline()
        {
        }
        /// <summary>
        /// Initializes a new AKT_PolyLine from a collection of points.
        /// </summary>
        /// <param name="collection">Points to copy into the local vertex array.</param>
        public AKT_Polyline(params AKT_Point3d[] collection)
        {
            this.points = collection.ToList();
        }
        /// <summary>
        /// Initializes a new AKT_PolyLine from a collection of points.
        /// </summary>
        /// <param name="collection">Points to copy into the local vertex array.</param>
        public AKT_Polyline(IEnumerable<AKT_Point3d> collection)
            : this(collection.ToList()) { }

        /// <summary>
        /// Initializes a new AKT_PolyLine from a collection of points.
        /// </summary>
        /// <param name="collection">Points to copy into the local vertex array.</param>
        public AKT_Polyline(List<AKT_Point3d> collection)
        {
            this.points = collection;
        }


        /// <summary>
        /// Gets the unit tangent vector along the AKT_PolyLine at the given parameter. 
        /// The integer part of the parameter indicates the index of the segment.
        /// </summary>
        /// <param name="t">AKT_PolyLine parameter.</param>
        /// <returns>The tangent along the AKT_PolyLine at t.</returns>
        public AKT_Vector3d TangentAt(double t)
        {
            int count = this.points.Count();
            if (count < 2)
            {
                return AKT_Vector3d.Zero;
            }
            int num = (int)Math.Floor(t);
            if (num < 0)
            {
                num = 0;
            }
            else if (num > count - 2)
            {
                num = count - 2;
            }
            AKT_Vector3d mItems = this.points[num + 1] - this.points[num];
            mItems.Unitize();
            return mItems;
        }

        public AKT_Point3d this[int i]
        {
            get { return this.points[i]; }
            set { this.points[i] = value; }
        }
        
        public static List<AKT_Polyline> JoinPolylines(IEnumerable<AKT_Polyline> polylines)//, List<AKT_Polyline> newReg)
        {
            AKT_Polyline[] _polylines = polylines.ToArray();
            Dictionary<AKT_Point3d, List<AKT_Polyline>> dict = new Dictionary<AKT_Point3d, List<AKT_Polyline>>();

            foreach (AKT_Polyline poly in _polylines)
            {
                List<AKT_Polyline> list;

                if (dict.TryGetValue(poly.Start, out list))
                {
                    list.Add(poly);
                }
                else
                {
                    list = new List<AKT_Polyline>()
                    {
                        poly
                    };

                    dict.Add(poly.Start, list);
                }

                if (dict.TryGetValue(poly.End, out list))
                {
                    list.Add(poly);
                }
                else
                {
                    list = new List<AKT_Polyline>()
                    {
                        poly
                    };

                    dict.Add(poly.End, list);
                }
            }

            //List<AKT_Polyline> newPolys = 

            List<AKT_Polyline> merged = new List<AKT_Polyline>();

            AKT_Polyline polyy = dict.First().Value[0];

            polyy = dict[polyy.Start][0];

            merged.Add(polyy);
            
            dict.Remove(polyy.Start);

            while (dict.Count > 0)
            {
                AKT_Polyline pol = merged.Last();
                
                List<AKT_Polyline> nexts;
                if (!dict.TryGetValue(pol.End, out nexts))
                    if (!dict.TryGetValue(pol.Start, out nexts))
                    {
                        merged.Add(dict.First().Value[0]);
                        continue;
                    }

                AKT_Polyline next = nexts[0].Start == pol.End ? nexts[0] : nexts[1];

                pol.AddRange(next);

                dict.Remove(next.Start);

                if (pol.End == pol.Start)
                {
                    if (dict.Count == 0)
                        break;

                    polyy = dict.First().Value[0];

                    polyy = dict[polyy.Start][0];

                    merged.Add(polyy);

                    dict.Remove(polyy.Start);
                }
            }

            //foreach (AKT_Polyline poly in _polylines)
            //{
            //    List<AKT_Polyline> polys = dict[poly.Start];

            //    foreach (AKT_Polyline pol in polys)
            //    {
            //        poly.AddRange(pol);
            //    }

            //    polys.Clear();
            //}
            return merged;

            return dict.Values.SelectMany(a => a).ToList();
        }

        private void AddRange(List<AKT_Point3d> list)
        {
            if (list == null || list.Count == 0)
                return;

            if (list[0] == Start)
            {
                points.InsertRange(0, list.Skip(1).Reverse());
            }
            else if (list[0] == End)
            {
                points.AddRange(list.Skip(1));
            }
            else if (list[list.Count - 1] == Start)
            {

            }
            else
                points.AddRange(list);
        }

        public int Length
        {
            get { return points.Count; }
        }
        public AKT_Point3d Start
        {
            get { return points[0]; }
        }
        public AKT_Point3d End
        {
            get { return points[points.Count - 1]; }
        }

        public IEnumerator<AKT_Point3d> GetEnumerator()
        {
            return (IEnumerator<AKT_Point3d>)points.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return points.GetEnumerator();
        }

        public static implicit operator List<AKT_Point3d>(AKT_Polyline poly)
        {
            return poly.points;
        }
        public static implicit operator AKT_Polyline(List<AKT_Point3d> list)
        {
            return new AKT_Polyline(list);
        }
    }
}
