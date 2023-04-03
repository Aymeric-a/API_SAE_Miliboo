using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using System;
using System.Reflection;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class DeleteAllCycles
    {
        readonly MilibooDBContext? milibooDBContext;
        public List<T> DeleteAllCyclesFunction<T>(List<T> list)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (T item in list)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.EndsWith("Navigation"))
                    {
                        object value = property.GetValue(item); 
                        if (value != null) 
                        {
                            Type typeCycle = value.GetType();
                            PropertyInfo[] propertiesCycle = typeCycle.GetProperties();

                            foreach (PropertyInfo propertyCycle in propertiesCycle)
                            {
                                if (propertyCycle.Name.EndsWith("Navigation"))
                                {
                                    propertyCycle.SetValue(value, null);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public List<T> ChargeComposants<T>(List<T> list, List<string> naviguations)
        {
            VarianteManager? varianteManager;

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (T item in list)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.EndsWith("Navigation") && naviguations.Contains(property.Name))
                    {
                        foreach (string manager in naviguations)
                        {
                            Type managerType = Type.GetType(manager + "Manager");
                            object managerInstance = Activator.CreateInstance(managerType);

                            Type DBContextType = Type.GetType(char.ToUpper(manager[0]) + manager.Substring(1) + "s");
                            object DBContextInstance = Activator.CreateInstance(managerType);

                            var dbSet = milibooDBContext.GetType().GetProperty(DBContextInstance.ToString()).GetValue(milibooDBContext) as DbSet<Variante>;
                            var result = dbSet.ToList();
                        }
                    }
                }
            }

            return list;
        }
    }
}
