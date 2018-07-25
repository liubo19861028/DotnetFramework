using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Ado.Cache
{
    /// <summary>
    /// 缓存容器
    /// </summary>
    public class Caches
    {

        private static object _lock = new object();


        private static EntityInfoCache entityInfoCache;

        /// <summary>
        /// 实体反射缓存
        /// </summary>
        public static EntityInfoCache EntityInfoCache
        {
            get
            {
                if (entityInfoCache == null)
                {
                    lock (_lock)
                    {
                        if (entityInfoCache == null)
                        {
                            entityInfoCache = new EntityInfoCache();
                        }
                    }
                }

                return entityInfoCache;
            }
        }

        private static PropertyAccessorCache propertyAccessorCache;

        /// <summary>
        /// 实体反射缓存
        /// </summary>
        public static PropertyAccessorCache PropertyAccessorCache
        {
            get
            {
                if (propertyAccessorCache == null)
                {
                    lock (_lock)
                    {
                        if (propertyAccessorCache == null)
                        {
                            propertyAccessorCache = new PropertyAccessorCache();
                        }
                    }
                }

                return propertyAccessorCache;
            }
        }

        private static ConstructorCache constructorCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        public static ConstructorCache ConstructorCache
        {
            get
            {
                if (constructorCache == null)
                {
                    lock (_lock)
                    {
                        if (constructorCache == null)
                        {
                            constructorCache = new ConstructorCache();
                        }
                    }
                }

                return constructorCache;
            }
        }

    }
}
