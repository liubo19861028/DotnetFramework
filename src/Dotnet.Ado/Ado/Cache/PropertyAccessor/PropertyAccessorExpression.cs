using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Ado.Cache
{
    public class PropertyAccessorExpression : IPropertyAccessor
    {
        private Func<object, object> m_getter;
        private MethodInvoker m_setMethodInvoker;

        public PropertyInfo PropertyInfo { get; private set; }

        public PropertyAccessorExpression(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.InitializeGet(propertyInfo);
            this.InitializeSet(propertyInfo);
        }

        private void InitializeGet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead) return;

            // 准备参数
            var instance = Expression.Parameter(typeof(object), "instance");
            var instanceCast = propertyInfo.GetGetMethod(true).IsStatic ? null :
                Expression.Convert(instance, propertyInfo.ReflectedType);
            var propertyAccess = Expression.Property(instanceCast, propertyInfo);
            var castPropertyValue = Expression.Convert(propertyAccess, typeof(object));

            // Lambda expression
            var lambda = Expression.Lambda<Func<object, object>>(castPropertyValue, instance);
            this.m_getter = lambda.Compile();

        }

        private void InitializeSet(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite) return;
            this.m_setMethodInvoker = new MethodInvoker(propertyInfo.GetSetMethod(true));
        }

        public object GetValue(object o)
        {
            if (this.m_getter == null)
            {
                throw new NotSupportedException("Get method is not defined for this property.");
            }

            return this.m_getter(o);
        }

        public void SetValue(object o, object value)
        {
            if (this.m_setMethodInvoker == null)
            {
                throw new NotSupportedException("Set method is not defined for this property.");
            }

            this.m_setMethodInvoker.Invoke(o, new object[] { value });
        }

        #region IPropertyAccessor Members

        object IPropertyAccessor.GetValue(object instance)
        {
            return this.GetValue(instance);
        }

        void IPropertyAccessor.SetValue(object instance, object value)
        {
            if (!this.PropertyInfo.PropertyType.IsGenericType)
            {
                this.SetValue(instance, Convert.ChangeType(value, this.PropertyInfo.PropertyType));
            }
            else
            {
                Type genericTypeDefinition = this.PropertyInfo.PropertyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    this.SetValue(instance, Convert.ChangeType(value, Nullable.GetUnderlyingType(this.PropertyInfo.PropertyType)));
                }
            }
            //this.SetValue(instance, value);
        }

        #endregion
    }
}
