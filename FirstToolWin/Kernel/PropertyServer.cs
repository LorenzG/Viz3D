using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FirstToolWin.Kernel
{
    class PropertyServer
    {
        public static Color ModifyColorLuminosity(Color c, double f)
        {
            c.R *= (byte)f;
            c.G *= (byte)f;
            c.B *= (byte)f;

            return c;
        }
        static public Color SelectedColor
        {
            get { return Colors.ForestGreen; }
        }
        static public DiffuseMaterial SelectedMaterial
        {
            get { return new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(SelectedColor)); }
        }
        static public DiffuseMaterial SelectedMaterialBack
        {
            get { return new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(ModifyColorLuminosity(SelectedColor, 1.1))); }
        }
        static public Color UnSelectedColor
        {
            get { return Colors.DarkRed; }
        }
        static public DiffuseMaterial UnSelectedMaterial
        {
            get { return new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(UnSelectedColor)); }
        }
        static public DiffuseMaterial UnSelectedMaterialBack
        {
            get { return new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(ModifyColorLuminosity(UnSelectedColor, 1.1))); }
        }
        static public double PointSize
        {
            get { return 5; }
        }
        static public double PointSphereSize
        {
            get { return 0.1; }
        }
        static public double LineThickness
        {
            get { return 2.5; }
        }
        public static Brush SelectedRowBackground
        {
            get { return new SolidColorBrush(Colors.IndianRed); }
        }
        public static Brush RowBackground_Light
        {
            get { return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)); }
        }
        public static Brush RowBackground_Dark
        {
            get { return new SolidColorBrush(Color.FromArgb(255, 211, 211, 211)); }
        }
    }
}
