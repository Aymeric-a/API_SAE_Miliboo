﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SAE_S4_MILIBOO.Models.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Documents;

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
        public T DeleteAllCyclesFunction<T>(T item)
        {
            Type type = typeof(T);

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
            return item;
        }

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
                if (navToLoad == "Avis")
                {
                    dbSetProperty = typeof(MilibooDBContext).GetProperty(manager);
                }

                var dbSetInstance = dbSetProperty.GetValue(milibooDBContext);
                var dbSetList = ((IEnumerable)dbSetInstance).Cast<object>().ToList();
            }

            return item;
        }

        public List<T> ChargeComposants<T>(List<T> list, List<string> naviguations)
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
                if (navToLoad == "Avis")
                {
                    dbSetProperty = typeof(MilibooDBContext).GetProperty(manager);
                }

                var dbSetInstance = dbSetProperty.GetValue(milibooDBContext);
                var dbSetList = ((IEnumerable)dbSetInstance).Cast<object>().ToList();
            }

            return list;
        }
    }
}
