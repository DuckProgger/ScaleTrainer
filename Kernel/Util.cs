using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scale_Trainer
{
    internal static class Util
    {
        public static Type FindTypeByNameAttribute(string name)
        {
            Type[] types = GetSubclasses(typeof(StringedInstrument));
            var typesWithSelectedName = from type in types
                                        let attr = (NameAttribute)type.GetCustomAttribute(typeof(NameAttribute))
                                        where attr.Name == name
                                        select type;
            foreach (Type item in typesWithSelectedName)
            {
                return item;
            }
            throw new NotImplementedException();
        }       

        public static string[] GetNameAttributes()
        {
            List<string> nameList = new List<string>(3);
            Type[] derivedTypes = GetSubclasses(typeof(StringedInstrument));
            foreach (Type type in derivedTypes)
            {
                NameAttribute attr = (NameAttribute)type.GetCustomAttribute(typeof(NameAttribute));
                if (attr != null)
                    nameList.Add(attr.Name);
            }
            return nameList.ToArray();
        }

        public static Type[] GetSubclasses(Type baseType)
        {
            List<Type> typeList = new List<Type>(3);
            Type[] types = Assembly.GetAssembly(baseType).GetTypes();
            var derivedTypes = from type in types
                               where type.IsSubclassOf(baseType)
                               select type;
            foreach (Type item in derivedTypes)
            {
                typeList.Add(item);
            }
            return typeList.ToArray();
        }
    }
}
