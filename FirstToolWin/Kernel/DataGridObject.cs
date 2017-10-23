using EnvDTE80;
using FirstToolWin.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FirstToolWin.Kernel
{
    class DataGridObject
    {
        string name, _value;
        internal EnvDTE.Expression exp;
        public DataGridObject()
        {

        }
        public DataGridObject(string name)
        {
            Name = name;
        }

        public string GetActualValue()
        {
            if (exp.DTE.Debugger.CurrentStackFrame == null)
            {
                MessageBox.Show("You need to have the Debugger running");
                return "";
            }

            Logger.Log("", "GetActualValue will return " + exp.Value);
            return exp.Value;
        }

        public string Name
        {
            get { return name; }
            set
            {
                Logger.Log(this, "Setting Name of DataGridObject to " + value);
                name = value;

                DTE2 dte = FirstToolWindowPackage.WindowToolPackage.GetService();

                Logger.Log(this, "DataGridObject - Got dte");

                // Ensure that debugger is running
                if (dte.Debugger.CurrentStackFrame == null)
                {
                    MessageBox.Show("You need to have the Debugger running");
                    return;
                }

                exp = dte.Debugger.GetExpression(name, true, 1);
                Logger.Log(this, "DataGridObject - Got expression");

                if (exp == null)
                {
                    Logger.Log(this, string.Format("Expression not found: {0}", name));
                }
                else
                {
                    Logger.Log(this, string.Format("Expression found: {0}", exp.Value));
                }

                this._value = exp.Value;
            }
        }
        public string Value
        {
            get
            {
                Logger.Log(this, "Value will return " + _value);
                return _value;
            }
            set { Logger.Log(this, string.Format("Trying to set Value of {2} to: {0}, but this DataGridObject has {1}", value, _value, name)); }
        }

        public override string ToString()
        {
            return Name + " - " + Value;
        }
    }
}
