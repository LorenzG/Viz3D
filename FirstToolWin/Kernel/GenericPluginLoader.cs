using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstToolWin.Kernel
{
    static class GenericPluginLoader<T>
    {
        public static ICollection<T> LoadPlugins(string path)
        {
            if (!Directory.Exists(path))
                return null;

            string[] dllFileNames = Directory.GetFiles(path, "*.dll");

            Assembly[] assemblies = new Assembly[dllFileNames.Length];
            for (int i = 0; i < assemblies.Length; i++)
            {
                string dllFile = dllFileNames[i];

                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies[i] = assembly;
            }


            List<Type> pluginTypes = new List<Type>();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];

                if (assembly == null)
                    continue;

                Type[] types = assembly.GetTypes();

                for (int j = 0; j < types.Length; j++)
                {
                    Type type = types[j];

                    if (type.IsInterface || type.IsAbstract)
                        continue;

                    if (type.IsSubclassOf(typeof(T)))
                        pluginTypes.Add(type);
                }
            }

            T[] plugins = new T[pluginTypes.Count];
            for (int i = 0; i < pluginTypes.Count; i++)
            {
                Type type = pluginTypes[i];

                T plugin = (T)Activator.CreateInstance(type);
                plugins[i] = plugin;
            }

            return plugins;
        }
    }
}
