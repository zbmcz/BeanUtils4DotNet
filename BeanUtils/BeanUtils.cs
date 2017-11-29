using System;
using System.Reflection;
using System.Collections.Generic;
namespace Abchina.Ebiz.Tools.BeanUtils
{
    /// <summary>
    /// Utility methods for populating Beans properties via reflection.
    /// </summary>
    public class BeanUtils
    {
        
        private BeanUtils()
        {
        }

        /// <summary>
        /// Clone a bean based on the available property via accessors.
        /// </summary>
        /// <returns>The bean.</returns>
        /// <param name="bean">Bean.</param>
        /// <param name="constructorArgs">Constructor arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T CloneBean<T>(Object bean,Object[] constructorArgs = null)
        {
            Type beanType = bean.GetType();
            Type[] constructorArgTypes = Type.EmptyTypes;
            // 上一句代码等同于 Type[] constructorArgTypes = new Type[0];

            if (constructorArgs != null){
                constructorArgTypes = new Type[constructorArgs.Length];
                for (int i = 0; i < constructorArgs.Length; i++){
                    constructorArgTypes[i] = constructorArgs[i].GetType();
                }
            }

            // get the specified constructor of bean
            ConstructorInfo constructor = beanType.GetConstructor(constructorArgTypes);

            // get the new instance of bean
            Object ret = constructor.Invoke(constructorArgs);

            PropertyInfo[] publicProps = beanType.GetProperties();
            foreach (PropertyInfo propInfo in publicProps)
            {
                propInfo.SetValue(ret,propInfo.GetValue(bean));
            }
            return (T)ret;
        }

        /// <summary>
        /// Copy property values from the origin bean to the destination bean for all cases where the property names are the same.
        /// </summary>
        /// <param name="dest">Destination.</param>
        /// <param name="origin">Origin.</param>
        public static void CopyPorperties(Object origin,Object dest)
        {
            Type originType = origin.GetType();
            PropertyInfo[] originProps = originType.GetProperties();
            foreach(var originProp in originProps)
            {
                PropertyInfo destProp = dest.GetType().GetProperty(originProp.Name);
                if(destProp != null)
                    originProp.SetValue(dest,originProp.GetValue(origin));
            }
        }

        public static T CopyPorperties<T>(Object origin,Object[] destConstructorArgs = null)
        {
            if (origin == null)
                return default(T);
            T ret = default(T);
            // 
            //T.GetType();
            Type[] destConstructorArgTypes = Type.EmptyTypes;
            if (destConstructorArgs != null)
            {
                destConstructorArgTypes = new Type[destConstructorArgs.Length];
                for (int i = 0; i < destConstructorArgs.Length; i++)
                {
                    destConstructorArgTypes[i] = destConstructorArgs[i].GetType();
                }
            }
            // get the T constructor by argument type
            ret.GetType();
            ConstructorInfo constructor = ret.GetType().GetConstructor(destConstructorArgTypes);
            // get the instance of T
            ret = (T)constructor.Invoke(destConstructorArgs);

            Type originType = origin.GetType();
            PropertyInfo[] originProps = originType.GetProperties();
            foreach (var originProp in originProps)
            {
                PropertyInfo destProp = ret.GetType().GetProperty(originProp.Name);
                if (destProp != null)
                    originProp.SetValue(ret, originProp.GetValue(origin));
            }
            return (T)ret;
        }

        /// <summary>
        /// Copy the specified property value to the specified destination bean, performing any type conversion that is required.
        /// </summary>
        /// <param name="bean">Bean.</param>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        public static void CopyProperty(Object bean,string name,Object value)
        {
            Type beanType = bean.GetType();
            PropertyInfo propInfo = beanType.GetProperty(name);
            propInfo.SetValue(bean,value);
        }

        /// <summary>
        /// Return the entire collection of properties for which the specified bean provides a read accessor.
        /// </summary>
        /// <returns>The describe.</returns>
        /// <param name="bean">Bean.</param>
        public static Dictionary<string,Object> Describe(Object bean)
        {
            PropertyInfo[] propInfos = bean.GetType().GetProperties();
            Dictionary<string, Object> ret = new Dictionary<string, object>();
            foreach (var propInfo in propInfos)
            {
                ret.Add(propInfo.Name,propInfo.GetValue(bean));
            }
            return ret;
        }

        /// <summary>
        /// Populate the C#Beans properties of the specified bean, based on the specified name/value pairs.
        /// </summary>
        /// <returns>The populate.</returns>
        /// <param name="bean">Bean.</param>
        /// <param name="properties">Properties.</param>
        public static void populate(Object bean, Dictionary<string, Object> properties)
        {
            if (properties == null)
                return;
            foreach (var item in properties)
            {
                PropertyInfo propInfo = bean.GetType().GetProperty(item.Key);
                if (propInfo != null)
                    propInfo.SetValue(bean, item.Value);
            }
        }

        // 以下方法都是参考 java中的commons-beanutils
        /*********想不明白这种方法到底有啥子用啊？？？*********/
        /// <summary>
        /// Return the value of the specified array property of the specified bean, as a string array.
        /// </summary>
        /// <returns>The array property.</returns>
        /// <param name="bean">Bean.</param>
        /// <param name="name">Name.</param>
        public static string[] GetArrayProperty(Object bean,string name)
        {
            PropertyInfo propInfo = bean.GetType().GetProperty(name);
            try{
                Object[] value = (object[])propInfo.GetValue(bean);
                string[] ret = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    ret[i] = value[i].ToString();
                }
                return ret;
            }catch(Exception e){
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public static string GetIndexedProperty(Object bean,string name){
            return null;
        }
        public static string GetIndexedProperty(Object bean, string name,int index)
        {
            return null;
        }
        public static string GetMappedProperty(Object bean, string name){
            return null;
        }
        public static string GetMappedProperty(Object bean, string name,string key)
        {
            return null;
        }
        public static string GetNestedProperty(Object bean, string name)
        {
            return null;
        }

    }
}
