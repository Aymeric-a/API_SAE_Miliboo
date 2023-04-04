using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAE_S4_MILIBOO.Models.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SAE_S4_MILIBOO.Models.DataManager
{
    public class DeleteAllCycles
    {
        public VarianteManager varianteManager;
        readonly MilibooDBContext? milibooDBContext;

        public DeleteAllCycles(MilibooDBContext context)
        {
            milibooDBContext = context;
            varianteManager = new VarianteManager(context);
        }

        public List<T> DeleteAllCyclesFunction<T>(List<T> list)
        {
            Type type = typeof(T);

            foreach (T item in list)
            {
                var properties = type.GetProperties().Where(p => p.Name.EndsWith("Navigation"));
                foreach (PropertyInfo property in properties)
                {
                    var firstNavigation = property.GetValue(item);
                    Type typeCycle = null;
                    if (firstNavigation != null)
                    {
                        if (firstNavigation.GetType().IsGenericType && firstNavigation.GetType().GetGenericTypeDefinition() == typeof(List<>))
                        {
                            typeCycle = firstNavigation.GetType().GetGenericArguments()[0];
                        }
                        else
                        {
                            typeCycle = firstNavigation.GetType();
                        }

                        var propertiesCycle = typeCycle.GetProperties().Where(p => p.Name.EndsWith("Navigation") && p.Name.StartsWith(type.Name));

                        foreach (PropertyInfo propertyCycle in propertiesCycle)
                        {
                            object secondNavigation = null;
                            if (firstNavigation.GetType().IsGenericType && firstNavigation.GetType().GetGenericTypeDefinition() == typeof(List<>))
                            {
                                foreach (var firstNav in (IEnumerable)firstNavigation)
                                {
                                    secondNavigation = propertyCycle.GetValue(firstNav);
                                    propertyCycle.SetValue(firstNav, null);

                                }
                            }
                            else
                            {
                                secondNavigation = propertyCycle.GetValue(firstNavigation);
                                propertyCycle.SetValue(firstNavigation, null);
                            }
                        }
                    }                   
                }
            }
            return list;
        }

        public List<Produit> test(List<Produit> list)
        {
            foreach (Produit p in list)
            {
                foreach (Variante v in p.VariantesProduitNavigation)
                {
                    v.ProduitVarianteNavigation = null;
                }
            }

            return list;
        }

        public T DeleteAllCyclesFunction<T>(T t)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.EndsWith("Navigation"))
                {
                    object value = property.GetValue(t);
                    if (value != null)
                    {
                        Type typeCycle = value.GetType();
                        PropertyInfo[] propertiesCycle = typeCycle.GetProperties();

                        foreach (PropertyInfo propertyCycle in propertiesCycle)
                        {
                            if (propertyCycle.Name.EndsWith("Navigation"))
                            {
                                Console.WriteLine("NAME : " + propertyCycle.Name);
                                propertyCycle.SetValue(value, null);
                            }
                        }
                    }
                }
            }
            return t;
        }

        //public async Task<List<T>> ChargeComposantsListe<T>(Func<int, Task<T>> functionToAddNaviguations, List<string> naviguations, int id)
        //{
        //    List<T> listFromManager = functionToAddNaviguations(id);

        //    Type type = typeof(T);
        //    PropertyInfo[] properties = type.GetProperties();

        //    foreach (T item in listFromManager)
        //    {
        //        foreach (PropertyInfo property in properties)
        //        {
        //            if (property.Name.EndsWith("Navigation") && naviguations.Contains(property.Name))
        //            {   
        //                foreach (string manager in naviguations)
        //                {
        //                    Type managerType = Type.GetType(manager + "Manager");
        //                    object managerInstance = Activator.CreateInstance(managerType);

        //                    Type DBContextType = Type.GetType(char.ToUpper(manager[0]) + manager.Substring(1) + "s");
        //                    object DBContextInstance = Activator.CreateInstance(managerType);

        //                    var dbSet = milibooDBContext.GetType().GetProperty(DBContextInstance.ToString()).GetValue(milibooDBContext) as DbSet<Variante>;
        //                    var result = dbSet.ToList();
        //                }
        //            }
        //        }
        //    }

        //    return listFromManager;
        //}

        public T ChargeComposants<T>(T item, List<string> naviguations)
        {
            //T fromManager = functionToAddNaviguations(id);

            foreach (string navToLoad in naviguations)
            {
                string manager = navToLoad;
                string typeName = "SAE_S4_MILIBOO.Models.DataManager." + manager + "Manager";
                string entityTypeString = "SAE_S4_MILIBOO.Models.EntityFramework." + manager;

                Type managerType = Type.GetType(typeName);

                object managerInstance = Activator.CreateInstance(managerType);

                var dbSetProperty = typeof(MilibooDBContext).GetProperty(manager + "s");
                var dbSetInstance = dbSetProperty.GetValue(milibooDBContext);
                var dbSetList = ((IEnumerable)dbSetInstance).Cast<object>().ToList();
            }

            return item;
        }
    }
}
