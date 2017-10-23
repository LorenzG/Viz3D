using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FirstToolWin.Readers
{
    public class MyProperties
    {
        public static readonly System.Windows.DependencyProperty MyParentProperty;

        static MyProperties()
        {
            MyParentProperty = System.Windows.DependencyProperty.RegisterAttached(
                "Parent", typeof(ModelVisual3D), typeof(MyProperties));
        }

        public static void SetMyInt(System.Windows.UIElement element, ModelVisual3D value)
        {
            element.SetValue(MyParentProperty, value);
        }

        public static ModelVisual3D GetMyInt(System.Windows.UIElement element)
        {
            return (ModelVisual3D)element.GetValue(MyParentProperty);
        }
    }
}
