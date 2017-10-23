using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using FirstToolWin.MetaClasses;
using FirstToolWin.Utilities;
using FirstToolWin.Kernel;
using System.Windows.Data;

#if VERBOSE
using System.Windows;
#endif

namespace FirstToolWin.Readers
{
    public class ReaderUtils
    {
        // Enumerable identifiers
        static string[] enums = new string[]
            {
                "[]",
                "IEnumerable<",
                "List<"
            };

        static IEnumerable<EnvDTE.Expression> ReadEnumerable(EnvDTE.Expression exp)
        {
            Logger.Log(exp, string.Format("Reading IENumerable. Name: {0}, val: {1}, type: {2}", exp.Name, exp.Value, exp.Type));

            //if (!enums.Any(e => exp.Type.Contains(e)))
            //    yield return null;

            foreach (EnvDTE.Expression exp1 in exp.DataMembers)
            {
                Logger.Log(exp, string.Format(exp1.Name));

                if (exp1.Name.Contains("["))
                    yield return exp1;
            }
        }
        static EnvDTE.Expression GetDataMember(EnvDTE.Expression exp, string name)
        {
            foreach (EnvDTE.Expression exp1 in exp.DataMembers)
            {
                if (exp1.Name == name)
                    return exp1;
            }

            return null;
        }

        // Type dictionary       
        static Dictionary<string, MetaObject> dictType_ = new Dictionary<string, MetaObject>();

        internal static bool TryAddObject(EnvDTE.Expression exp, HelixViewport3D hv, Map<string, ModelVisual3D> _dict)
        {
            if (!exp.IsValidValue)
                return false;

            // get type
            string type = exp.Type;

            // create the 3d
            ModelVisual3D _mv3d;

            Logger.Log("", string.Format("Searching this type in dictionary: {0}", type));
            // try get object from dictionary
            MetaObject obj;
            if (dictType_.TryGetValue(type, out obj))
            {
                // let's create a brand new one
                obj = obj.InstantiateNew();

                obj.Run(exp);

                _mv3d = obj.To3DViz();

                obj.VisualizationProperties(_mv3d);
            }
            else if (IsEnumerable(exp))
            {
                // clean from enumerables' characters
                type = CleanFromEnumerables(type);

                if (!dictType_.TryGetValue(type, out obj))
                {
                    Logger.Log("", string.Format("Could not find this type in dictionary: {0}", type));
                    return false;
                }


                // let's create a brand new one
                obj = obj.InstantiateNew();

                _mv3d = new ModelVisual3D();

                foreach (EnvDTE.Expression exp1 in ReaderUtils.ReadEnumerable(exp))
                {
                    obj.Run(exp1);

                    ModelVisual3D mv3d = obj.To3DViz();

                    obj.VisualizationProperties(mv3d);

                    _mv3d.Children.Add(mv3d);
                }

            }
            else
            {
                Logger.Log("", string.Format("Could not find this type in dictionary: {0}", type));
                return false;
            }

            if (_mv3d.Children != null && _mv3d.Children.Count > 0)
            {
                for (int i = 0; i < _mv3d.Children.Count; i++)
                {
                    Visual3D mv3d = _mv3d.Children[i];

                    // new bindings
                    Binding myBinding = new Binding();
                    myBinding.Source = mv3d;
                    myBinding.Path = new PropertyPath("MyProperties");
                    myBinding.Mode = BindingMode.TwoWay;
                    myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                    // set bindings
                    BindingOperations.SetBinding(mv3d, MyProperties.MyParentProperty, myBinding);

                    // set value
                    mv3d.SetValue(MyProperties.MyParentProperty, _mv3d);
                }
            }

            Logger.Log(exp, string.Format("Add3D"));

            _mv3d.SetName(exp.Name);

            // add to dict and viewport
            _dict.Add(exp.Name, _mv3d);
            hv.Items.Add(_mv3d);

            // exit
            Logger.Log(exp, string.Format("Add3D - Added to dict"));
            return true;
        }

        private static string CleanFromEnumerables(string type)
        {
            // list
            do
            {
                string list = "System.Collections.Generic.List<";
                int idx = type.IndexOf(list);

                if (idx >= 0)
                {
                    type = type.Substring(idx + list.Length);
                    type = type.Substring(0, type.Length - 1);
                }
                else
                {
                    break;
                }
            } while (true);

            // IEnumerable
            do
            {
                string list = "System.Collections.IEnumerable<";
                int idx = type.IndexOf(list);

                if (idx >= 0)
                {
                    type = type.Substring(idx + list.Length);
                    type = type.Substring(0, type.Length - 1);
                }
                else
                {
                    break;
                }
            } while (true);

            // array
            type = type.Replace("[]", "");

            return type;
        }

        static bool IsEnumerable(EnvDTE.Expression exp)
        {
            string varName = "_" + Guid.NewGuid().ToString("N");
            exp.DTE.Debugger.ExecuteStatement(string.Format("System.Collections.IEnumerable {0} = (System.Collections.IEnumerable) {1};", varName, exp.Name));

            EnvDTE.Expression tempVar = exp.DTE.Debugger.GetExpression(varName);

            if (!tempVar.IsValidValue
                || tempVar.Value == "null")
                return false;

            exp.DTE.Debugger.ExecuteStatement(string.Format("{0} = null;", varName/*, exp.Name*/));

            return true;
        }

        //static T AddObject<T>(EnvDTE.Expression exp)
        //    where T : Visual3D
        //{
        //    Logger.Log(exp, string.Format("Reading ReadEnumerableMesh. Name: {0}, val: {1}, type: {2}", exp.Name, exp.Value, exp.Type));

        //    string type = "";
        //    Add3DGenericObjectDelegate obj3d = dictType2[type];

        //    MetaObject<T> obj = obj3d(exp) as MetaObject<T>;

        //    T v3d = obj.To3DObject();

        //    obj.VisualizationProperties(v3d);

        //    return v3d;
        //}
        //static ModelVisual3D AddEnumerableObject<T>(EnvDTE.Expression exp)
        //    where T : Visual3D
        //{
        //    Logger.Log(exp, string.Format("Reading ReadEnumerableMesh. Name: {0}, val: {1}, type: {2}", exp.Name, exp.Value, exp.Type));

        //    ModelVisual3D objs = new ModelVisual3D();

        //    foreach (EnvDTE.Expression exp1 in ReaderUtils.ReadEnumerable(exp))
        //    {
        //        if (!exp1.IsValidValue)// LG 170515 .Value == "null"
        //            continue;

        //        T obj = AddObject<T>(exp1);

        //        objs.Children.Add(obj);
        //    }

        //    return objs;
        //}


        internal static void LoadDlls()
        {
            ICollection<MetaObject> objs = GenericPluginLoader<MetaObject>.LoadPlugins(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Dodo\3D VSualizer");

            foreach (MetaObject obj in objs)
            {
                string name = obj.DebuggerTypeName;
                string type = obj.GetType().FullName;
                try
                {
                    dictType_.Add(name, obj);
                    Logger.Log("", "added to dict this type " + type + " linking to: " + name);
                }
                catch (Exception e)
                {
                    Logger.Log("", "dictionary already has this type " + type);
                }

                //_Add3DObject add = Add(obj);

                //Add3DObjectDelegate addDel = new Add3DObjectDelegate((e, hv, dict, b) => Add3D(e, hv, dict, b, add));
                //// add to type dict
                //dictType.Add(name, addDel);
            }
        }
        internal static void LoadDefaultDlls()
        {
            try
            {
                dictType_.Add(MetaPoint3d.type_Point3d, new MetaPoint3d());
                dictType_.Add(MetaLine.type_Line, new MetaLine());
                dictType_.Add(MetaMesh.type_Mesh, new MetaMesh());
            }
            catch (Exception e)
            {
                Logger.Log("", "dictionary already has this type");
            }
        }

    }
}
