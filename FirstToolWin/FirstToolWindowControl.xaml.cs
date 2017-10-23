//#undef VERBOSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using HelixToolkit.Wpf;
using EnvDTE;
using EnvDTE80;
using System.Windows.Media.Media3D;
using System.Collections.Generic;
using FirstToolWin.Readers;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using FirstToolWin.Utilities;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using FirstToolWin.Kernel;

namespace FirstToolWin
{

    [Microsoft.VisualStudio.Shell.ProvideBindingPath]
    /// <summary>
    /// Interaction logic for FirstToolWindowControl.
    /// </summary>
    public partial class FirstToolWindowControl : UserControl
    {
        DTE2 dte;
        Map<string, ModelVisual3D> _dict;
        string oldText = "";
        int id;


        /// <summary>
        /// Initializes a new instance of the <see cref="FirstToolWindowControl"/> class.
        /// </summary>
        public FirstToolWindowControl()
        {
            this.InitializeComponent();
            this.Initialize();
            //Create3DViewPort();
        }
        public FirstToolWindowControl(int id)
            : this()
        {
            this.id = id;
        }
        void Initialize()
        {
            Logger.Log(this, "Program started");
            _dict = new Map<string, ModelVisual3D>();

            ResetDataGrid();

            cb_LoadDLLs_Click(null, null);
        }

        void ResetDataGrid()
        {
            List<DataGridObject> objs = new List<DataGridObject>()
            {
                //new DataGridObject("Gianni"),
                //new DataGridObject("Luca"),
                //new DataGridObject("Ugo"),
            };
            //dg_Variables.DataContext = objs;
            dg_Variables.ItemsSource = objs;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        void bnt_Add_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log(sender, string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Boil the tea", this.ToString()));
            var teaPot = new Teapot();
            hv.Children.Add(teaPot);
            return;

            DTE2 dte = FirstToolWindowPackage.WindowToolPackage.GetService();
            if (dte.Debugger.CurrentStackFrame != null) // Ensure that debugger is running
            {
                //DTEClass c = new DTEClass();
                Expressions locals = dte.Debugger.CurrentStackFrame.Locals;
                foreach (EnvDTE.Expression local in locals)
                {
                    Expressions members = local.DataMembers;
                    // Do this section recursively, looking down in each expression for 
                    // the next set of data members. This will build the tree.
                    // DataMembers is never null, instead just iterating over a 0-length list.


                    Logger.Log(this, string.Format("name: {0}, val: {1}, type: {2}", local.Name, local.Value, local.Type));

                    string varName = local.Name;

                    //if (local.Type.Contains(ReaderUtils.type_Point3d))
                    //    AddPointsToViewport(ReaderUtils.AddEnumerablePoint3D(local), varName);
                }
            }
        }
        void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            Logger.Log(this, string.Format("removing elements from hv"));

            RemoveAllGeometries();

            ResetDataGrid();
        }


        void AddFakeVariablesDebug()
        {
            dg_Variables.BeginningEdit -= dg_Variables_BeginningEdit;
            dg_Variables.BeginningEdit += dg_Variables_BeginningEdit_DEBUG;

            // datagrid objs
            DataGridObject obj = new DataGridObject()
            {
                Name = "Points"
            };

            DataGridObject sphere = new DataGridObject()
            {
                Name = "Sphere"
            };

            DataGridObject mesh = new DataGridObject()
            {
                Name = "Mesh"
            };

            DataGridObject line = new DataGridObject()
            {
                Name = "Line"
            };

            List<DataGridObject> objs = new List<DataGridObject>()
            {
                obj, sphere, line, mesh
            };

            dg_Variables.ItemsSource = objs;


            // pnts
            PointsVisual3D points = new PointsVisual3D();
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                Point3D p = new Point3D(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
                points.Points.Add(p);
            }
            points.Size = PropertyServer.PointSize;

            _dict.Add("Points", points);
            hv.Items.Add(points);

            // sphere
            SphereVisual3D sph = new SphereVisual3D()
            {
                Center = new Point3D(0, 0, 0),
                Radius = 0.1,
            };
            _dict.Add("Sphere", sph);
            hv.Items.Add(sph);

            // mesh
            MeshBuilder builder = new MeshBuilder(true, true);
            builder.AddTriangle(new Point3D(1, 1, 1), new Point3D(2, 2, 4), new Point3D(5, 3, 3));

            MeshVisual3D m = new MeshVisual3D()
            {
                Mesh = new Mesh3D(builder.Positions, Enumerable.Range(0, builder.Positions.Count))
            };
            _dict.Add("Mesh", m);
            hv.Items.Add(m);

            // line
            LinesVisual3D l = new LinesVisual3D();
            l.Points.Add(new Point3D(4, 5, 7));
            l.Points.Add(new Point3D(4, 8, 5));
            _dict.Add("Line", l);
            hv.Items.Add(l);

        }

        async Task<bool> AddGeometry(string text)
        {
            Logger.Log(this, string.Format("AddGeometry: {0}", text));

            // first call to method
            if (this.dte == null)
            {
                Logger.Log(this, string.Format("First call to AddGeometry."));
                this.dte = FirstToolWindowPackage.WindowToolPackage.GetService();
                Logger.Log(this, string.Format("Created DTE object."));

                if (cb_UpdateOnContextChange.IsChecked.Value)
                {
                    dte.Events.DebuggerEvents.OnContextChanged += DebuggerEvents_OnContextChanged;
                    Logger.Log(this, string.Format("Added DebuggerEvents_OnContextChanged event."));
                }
            }

            // Ensure that debugger is running
            if (dte.Debugger.CurrentStackFrame == null)
            {
                MessageBox.Show("You need to have the Debugger running");
                return false;
            }

            EnvDTE.Expression local = dte.Debugger.GetExpression(text, true, 1);

            if (local == null)
            {
                Logger.Log(this, string.Format("dte.Debugger.GetExpression for variable \"{0}\" return null", local.Name));
                return false;
            }

            Logger.Log(this, string.Format("name: {0}, val: {1}, type: {2}", local.Name, local.Value, local.Type));

            // since there is a check for ennumerable that creates a new variable, 
            // we don't want to update the viz for that
            dte.Events.DebuggerEvents.OnContextChanged -= DebuggerEvents_OnContextChanged;



            if (ReaderUtils.TryAddObject(local, hv, _dict))
                Logger.Log(this, string.Format("All good"));
            else
                Logger.Log(this, string.Format("No good"));

            // we reattach the OnContextChanged
            if (cb_UpdateOnContextChange.IsChecked.Value)
                dte.Events.DebuggerEvents.OnContextChanged += DebuggerEvents_OnContextChanged;


            return true;
        }
        void RemoveGeometry(string name)
        {

            if (!_dict.ContainsKey(name))
            {
                Logger.Log(this, string.Format("Dictionary does not have variable: {0}", name));
                return;
            }


            Visual3D v3d = _dict[name];

            hv.Children.Remove(v3d);

            _dict.Remove(name);
        }
        void RemoveAllGeometries()
        {
            for (int i = hv.Children.Count - 1; i > 0; i--)
                hv.Children.RemoveAt(i);

            _dict = new Map<string, ModelVisual3D>();

            ResetDataGrid();
        }
        void UpdateVariables()
        {
            Logger.Log("", "UpdateVariables");
            foreach (DataGridObject item in dg_Variables.ItemsSource)
            {
                if (item.Value != item.GetActualValue())
                {
                    /*
                     * todo: should parse the result and check if it's the same?
                     * maybe not because it will take double the time if the .ToString() 
                     * is the same
                     */
                }

                Logger.Log("", "UpdateVariables - Updating " + item.Name);

                ModelVisual3D v3d = _dict[item.Name];
                Logger.Log("", "UpdateVariables - Found v3d " + v3d);
                _dict.Remove(item.Name);

                hv.Children.Remove(v3d);
                Logger.Log("", "UpdateVariables - Removed item from hv");

                if (!item.exp.IsValidValue)
                {
                    Logger.Log("", "UpdateVariables - Variable '" + item.Name + "' is not valide anymore");
                    return;
                }

                AddGeometry(item.Name);
                Logger.Log("", "UpdateVariables - Geometry added");
            }
        }

        void dg_Variables_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            //            DataGrid tb = (DataGrid)sender;

            //#if VERBOSE
            //            MessageBox.Show(string.Format("Variable affected: {0}, old value: {1}", tb.Text, oldText));
            //#endif

            //            List<DataGridObject> objs = tb.ItemsSource as List<DataGridObject>;

            //bool added = NewVariableAdded(objs.Last().Name).Result;

            //if (added)
            //    AddNewRow();
        }
        void dg_Variables_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //dg_Variables.GotFocus += Row_GotFocus;

            DataGrid dg = (DataGrid)sender;

            TextBox tb = e.EditingElement as TextBox;


            Logger.Log(sender, string.Format("Variable affected: {0}, old value: {1}", tb.Text, oldText));

            // if new text != old text -> remove old var
            if (tb.Text != oldText)
            {
                try { RemoveGeometry(oldText); }
                catch (Exception)
                {
                }
            }

            // if empty return and don't add row
            if (string.IsNullOrEmpty(tb.Text))
            {
                e.Cancel = true;
                return;
            }

            // if new var is already in dict exit
            if (_dict.ContainsKey(tb.Text))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                bool added = AddGeometry(tb.Text).Result;

                if (added)
                {

                }
                else
                {
                    e.Row.Background = BrushHelper.CreateRainbowBrush();
                    //e.Cancel = true;
                }
            }


        }
        void dg_Variables_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            TextBlock tb = (TextBlock)e.Column.GetCellContent(e.Row);
            //DataGridObject item = (DataGridObject)cp.DataContext;

            oldText = tb.Text;
            Logger.Log(sender, "oldText set to: " + oldText);

            // dg_Variables.GotFocus -= Row_GotFocus;
        }
        void dg_Variables_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg == null)
                return;

            DataGridRow dgr = (DataGridRow)(dg.ItemContainerGenerator.ContainerFromIndex(dg.SelectedIndex));

            if (e.Key == System.Windows.Input.Key.Delete && !dgr.IsEditing) ;
            else return;

            // User is attempting to delete the row

            TextBox tb = dg.Columns[0].GetCellContent(dgr) as TextBox;

            try { RemoveGeometry(tb.Text); }
            catch (Exception) { };

            e.Handled = true;
        }
        void Row_GotFocus(object sender, RoutedEventArgs e)
        {
            Logger.Log(this, "got focus");


            DataGridRow row = sender as DataGridRow;

            FrameworkElement fe = dg_Variables.Columns[0].GetCellContent(row);
            TextBox tb0 = fe as TextBox;
            TextBlock tbx = fe as TextBlock;

            string name = tb0 != null ? tb0.Text : tbx.Text;

            Logger.Log(this, "got " + name);
            if (!_dict.ContainsKey(name))
            {
                Logger.Log(this, string.Format("dictionary doesn't have {0}", name));
                //e.Handled = true;
                return;
            }

            ModelVisual3D v3d = _dict[name];

            Logger.Log(this, "checking the type: " + v3d.GetType().Name);
            if (v3d.GetType() == typeof(ModelVisual3D))
            {
                for (int i = 0; i < v3d.Children.Count; i++)
                    UpdateColor(v3d.Children[i], true);
            }
            else
                UpdateColor(v3d, true);
        }        
        void Row_LostFocus(object sender, RoutedEventArgs e)
        {
            Logger.Log(this, "lost focus");
            DataGridRow row = sender as DataGridRow;


            FrameworkElement fe = dg_Variables.Columns[0].GetCellContent(row);
            TextBox tb0 = fe as TextBox;
            TextBlock tbx = fe as TextBlock;

            string name = tb0 != null ? tb0.Text : tbx.Text;

            Logger.Log(this, "got " + name);
            if (!_dict.ContainsKey(name))
            {
                Logger.Log(this, string.Format("dictionary doesn't have {0}", name));
                //e.Handled = true;
                return;
            }

            ModelVisual3D v3d = _dict[name];

            Logger.Log(this, "checking the type");
            if (v3d.GetType() == typeof(ModelVisual3D))
            {
                for (int i = 0; i < v3d.Children.Count; i++)
                    UpdateColor(v3d.Children[i], false);
            }
            else
                UpdateColor(v3d, false);

            Logger.Log(this, "NOTYPE");
        }
        static void UpdateColor(Visual3D v3d, bool isClicked)
        {
            DiffuseMaterial material = isClicked ? PropertyServer.SelectedMaterial : PropertyServer.UnSelectedMaterial;
            DiffuseMaterial materialBack = isClicked ? PropertyServer.SelectedMaterialBack : PropertyServer.UnSelectedMaterialBack;
            System.Windows.Media.Color color = isClicked ? PropertyServer.SelectedColor : PropertyServer.UnSelectedColor;

            if (v3d is MeshVisual3D)
            {
                MeshVisual3D mesh = v3d as MeshVisual3D;
                mesh.FaceMaterial = material;
                mesh.FaceBackMaterial = materialBack;
                mesh.EdgeMaterial = material;
                mesh.VertexMaterial = material;
            }
            else if (v3d is LinesVisual3D)
            {
                LinesVisual3D line = v3d as LinesVisual3D;
                line.Color = color;
            }

            else if (v3d is PointsVisual3D)
            {
                PointsVisual3D pnts = v3d as PointsVisual3D;
                pnts.Color = color;
            }

            else if (v3d is SphereVisual3D)
            {
                SphereVisual3D sphere = v3d as SphereVisual3D;
                sphere.Material = material;
            }
            else
                Logger.Log(v3d, "NOTYPE");
        }
        void btn_ZoomAll(object sender, RoutedEventArgs e)
        {
            Rect3D rect = new Rect3D();
            foreach (Visual3D v in _dict.Values1)
            {
                Rect3D r = v.FindBounds(Transform3D.Identity);
                rect.Union(r);
            }
            hv.ZoomExtents(rect, 10);
        }
        void dg_Variables_BeginningEdit_DEBUG(object sender, DataGridBeginningEditEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void DebuggerEvents_OnContextChanged(Process NewProcess, Program NewProgram, Thread NewThread, StackFrame NewStackFrame)
        {
            Logger.Log("", "DebuggerEvents_OnContextChanged");
            Logger.Log(null, "NewProcess: " + NewProcess.Name);
            Logger.Log(null, "NewThread: " + NewThread.Name);
            Logger.Log(null, "NewStackFrame: " + NewStackFrame.FunctionName);

            if (cb_UpdateOnContextChange.IsChecked.Value)
            {
                UpdateVariables();
            }
        }
        void MyToolWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            RemoveAllGeometries();
        }
        void btn_Debug_Click(object sender, RoutedEventArgs e)
        {
            AddFakeVariablesDebug();
        }

        int idxSelectedRow;
        void hv_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HelixViewport3D hv = e.Source as HelixViewport3D;

            if (hv == null)
                return;

            Point3D p;
            Vector3D v;
            DependencyObject d;
            if (hv.FindNearest(e.GetPosition(hv), out p, out v, out d))
            {
                ModelVisual3D mv3d = d as ModelVisual3D;

                ModelVisual3D parent = mv3d.GetValue(MyProperties.MyParentProperty) as ModelVisual3D;

               // object o  = mv3d.GetValue(MyProperties.MyParentProperty);


                string mouseDownObjName = parent != null ? _dict[parent] : _dict[mv3d];

                List<DataGridObject> objs = dg_Variables.ItemsSource as List<DataGridObject>;

                idxSelectedRow = objs.FindIndex(a => a.Name == mouseDownObjName);

                DataGridRow row = dg_Variables.ItemContainerGenerator.ContainerFromIndex(idxSelectedRow) as DataGridRow;

                row.Background = PropertyServer.SelectedRowBackground;

                Logger.Log(sender, "Row background color changed - selected");
            }

        }
        void hv_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGridRow row = dg_Variables.ItemContainerGenerator.ContainerFromIndex(idxSelectedRow) as DataGridRow;

            row.Background = idxSelectedRow % 2 == 1 ? PropertyServer.RowBackground_Dark : PropertyServer.RowBackground_Light;

            Logger.Log(sender, "Row background color changed - UNselected");
        }
        void cb_LoadDLLs_Click(object sender, RoutedEventArgs e)
        {
            ReaderUtils.LoadDlls();
        }
        void cb_LoadDefaultDLLs_Click(object sender, RoutedEventArgs e)
        {
            ReaderUtils.LoadDefaultDlls();
        }
        void cb_UpdateOnContextChange_Click(object sender, RoutedEventArgs e)
        {
            if (dte != null)
            {
                if (cb_UpdateOnContextChange.IsChecked.Value)
                {
                    dte.Events.DebuggerEvents.OnContextChanged += DebuggerEvents_OnContextChanged;
                    Logger.Log(this, string.Format("Added DebuggerEvents_OnContextChanged event."));
                }
                else
                {
                    dte.Events.DebuggerEvents.OnContextChanged -= DebuggerEvents_OnContextChanged;
                    Logger.Log(this, string.Format("Removed DebuggerEvents_OnContextChanged event."));
                }
            }
            else
            {
                MessageBox.Show("Run Debug first");
            }

        }

        private void btn_Properties_Click(object sender, RoutedEventArgs e)
        {
            PropertyForm prop = new PropertyForm();
            prop.ShowDialog();
        }
    }
}
