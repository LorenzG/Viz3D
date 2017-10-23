using System;
using System.Windows.Media.Media3D;
using EnvDTE;
using FirstToolWin.Utilities;
using System.Linq;


namespace FirstToolWin.Kernel
{
    public abstract class MetaObject<T> : MetaObject
    {
        public abstract T To3DObject();

    }
    public abstract class MetaObject
    {
        protected string functionName;
        protected Expression expression;

        public MetaObject()
        {
        }
        protected abstract void ReadExpression(Expression exp);
        internal void Run(Expression exp)
        {
            this.expression = exp;

            functionName = exp.DTE.Debugger.CurrentStackFrame.FunctionName;

            if (!IsValid())
            {
                Logger.Log(this, "Wrong argument passed. Expecting " + DebuggerTypeName + ", received of type: " + exp.Type);

                throw new ArgumentException("Wrong argument passed. Expecting " + DebuggerTypeName + ", received of type: " + exp.Type);
            }

            Logger.Log(this, string.Format("We are in {0}, about to start ReadExpression", DebuggerTypeName));

            ReadExpression(exp);

            Logger.Log(this, string.Format("ReadExpression done"));
        }

        [Obsolete("Should ponder how to do this. This only searches the immediate children, I should rather search the tree...")]
        protected Expression FindChildren(string name, bool useAutoExpandRules = true, int maxMillisecs = 1)
        {
            return FindChildren(this.expression, name, useAutoExpandRules, maxMillisecs);
        }
        protected Expression FindChildren(string[] names, bool useAutoExpandRules = true, int maxMillisecs = 1)
        {
            //string name = names.Aggregate((a, b) => a + "." + b);

            Expression exp = FindChildren(names[0], useAutoExpandRules, maxMillisecs);

            for (int i = 1; i < names.Length; i++)
            {
                exp = FindChildren(exp, names[i], useAutoExpandRules, maxMillisecs);
            }

            return exp;
        }
        protected static Expression FindChildren(Expression expression, string[] names, bool useAutoExpandRules = true, int maxMillisecs = 1)
        {
            for (int i = 0; i < names.Length; i++)
            {
                expression = FindChildren(expression, names[i], useAutoExpandRules, maxMillisecs);
            }

            return expression;
        }
        protected static Expression FindChildren(Expression expression, string name, bool useAutoExpandRules = true, int maxMillisecs = 1)
        {
            Logger.Log("", string.Format("Looking for children {0}", expression.Name + "." + name));
            //Logger.Log("", string.Format("exp.collection: {0}", expression.Collection));



            Expression exp1 = null; // LG: Expression exp1 = expression.DataMembers.DTE.Debugger.GetExpression(expression.Name + "." + name, useAutoExpandRules, maxMillisecs);

            if (false)
            {
                exp1 = expression.DTE.Debugger.GetExpression(expression.Name + "." + name, useAutoExpandRules, maxMillisecs);
            }
            else
            {
                foreach (Expression exp in expression.DataMembers)
                {
                    //Logger.Log("", string.Format("children {0}", exp.Name));
                    if (exp.Name == name)
                    {
                        exp1 = exp;
                        Logger.Log("", string.Format("Found children {0}", exp.Name));
                        break;
                    }
                }
            }


            if (exp1 != null && !exp1.IsValidValue)
                Logger.Log("", string.Format("Couldn't Find children {0}", name));

            return exp1;
        }
        [Obsolete("boh")]
        protected static Expression FindChildrenNODOT(Expression expression, string name, bool useAutoExpandRules = true, int maxMillisecs = 1)
        {
            Logger.Log("", string.Format("Looking for children {0}", expression.Name + name));
            //Logger.Log("", string.Format("exp.collection: {0}", expression.Collection));



            Expression exp1 = null; // LG: Expression exp1 = expression.DataMembers.DTE.Debugger.GetExpression(expression.Name + "." + name, useAutoExpandRules, maxMillisecs);

            if (false)
            {
                exp1 = expression.DTE.Debugger.GetExpression(expression.Name + name, useAutoExpandRules, maxMillisecs);
            }
            else
            {
                foreach (Expression exp in expression.DataMembers)
                {
                    //Logger.Log("", string.Format("children {0}", exp.Name));
                    if (exp.Name == name)
                    {
                        exp1 = exp;
                        break;
                    }
                }
            }


            if (exp1 != null && !exp1.IsValidValue)
                Logger.Log("", string.Format("Couldn't Find children {0}", name));

            return exp1;
        }

        public virtual ModelVisual3D To3DViz()
        {
            return null;
        }
        public abstract bool IsValid();
        public abstract string DebuggerTypeName { get; }
        //public abstract Type Visual3DType { get; }
        public virtual void VisualizationProperties(Visual3D obj)
        {

        }
        public static T CreateAndRun<T>(Expression exp)
            where T : MetaObject
        {
            T mo = (T)Activator.CreateInstance(typeof(T));
            mo.Run(exp);
            return mo;
        }

        internal virtual MetaObject InstantiateNew()
        {
            return (MetaObject)Activator.CreateInstance(this.GetType());
        }

        //public T GetFromExpression1<T>(Expression exp)
        //    where T : MetaObject
        //{
        //    return (T)Activator.CreateInstance(GetType(), exp);
        //}
        //public MetaObject GetFromExpression(Expression exp)
        //{
        //    return (MetaObject)Activator.CreateInstance(GetType(), exp);
        //}
    }
}