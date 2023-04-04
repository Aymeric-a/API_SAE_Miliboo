using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_S4_MILIBOO.Models.EntityFramework;
using System;
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
            Console.WriteLine(type.Name + "Navigation");
            PropertyInfo[] properties = type.GetProperties();

            foreach (T item in list)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name.EndsWith("Navigation"))
                    {
                        var value = property.GetValue(item); 
                        if (value != null) 
                        {
                            Type typeCycle = null;
                            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
                            {
                                typeCycle = value.GetType().GetGenericArguments()[0];
                            }
                            else
                            {
                                typeCycle = value.GetType();
                            }
                            PropertyInfo[] propertiesCycle = typeCycle.GetProperties();

                            foreach (PropertyInfo propertyCycle in propertiesCycle)
                            {
                                Console.WriteLine("On veut : VariantesProduitNavigation");
                                Console.WriteLine("NAME : " + type.Name);
                                Console.WriteLine("NAME : " + typeCycle.Name);
                                if (propertyCycle.Name.EndsWith("Navigation") && propertyCycle.Name.StartsWith(type.Name))
                                {
                                    Console.WriteLine("00000000000000");
                                    object valueCycle = propertyCycle.GetValue(item);
                                    if (valueCycle != null)
                                    {
                                        Console.WriteLine("NAME : " + propertyCycle.Name);
                                        propertyCycle.SetValue(value, null);
                                    }
                                }
                            }
                        }
                    }
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

        public async Task<T> ChargeComposants<T>(Func<int, Task<ActionResult<T>>> functionToAddNaviguations, List<string> naviguations, int id)
        {
            int rank = 0;
            var TFromManager = await functionToAddNaviguations(id);
            T TFromManagerValue = TFromManager.Value;

            Type type = typeof(T);

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name.EndsWith("Navigation") && property.Name.StartsWith(naviguations[0]))
                {
                    foreach (string manager in naviguations)    
                    {
                        Console.WriteLine(typeof(VarianteManager));

                        string typeName = "SAE_S4_MILIBOO.Models.DataManager." + manager + "Manager";
                        Type managerType = Type.GetType(typeName);
                        object managerInstance = Activator.CreateInstance(managerType);

                        string DBContextInstance = "Variantes";

                        var dbSet = milibooDBContext.GetType().GetProperty(DBContextInstance.ToString()).GetValue(milibooDBContext) as DbSet<Variante>;
                        var navAdd = dbSet.ToList();
                        Console.WriteLine("rrrrrrrrrrrrr : " + navAdd[0].ProduitVarianteNavigation);
                        navAdd = DeleteAllCyclesFunction(navAdd);

                        TFromManagerValue = TFromManager.Value;
                        //Console.WriteLine(navAdd.Count());
                    }
                    rank++;
                    Console.WriteLine(rank);
                }
            }
            return DeleteAllCyclesFunction(TFromManagerValue);
        }
    }
}
