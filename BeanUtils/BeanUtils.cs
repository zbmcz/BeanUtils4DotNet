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
        
        public BeanUtils()
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
        public static void CopyPorperties(Object dest,Object origin)
        {
            Type beanType = dest.GetType();
            PropertyInfo[] propInfos = beanType.GetProperties();
            foreach(var prop in propInfos)
            {
                prop.SetValue(dest,prop.GetValue(origin));
            }
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

    }
}
