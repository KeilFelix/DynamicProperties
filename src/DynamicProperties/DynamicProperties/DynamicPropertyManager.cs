using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keil.DynamicProperties
{
    public class DynamicPropertyManager<TTarget> : IDisposable
    {
        #region Fields

        private readonly DynamicTypeDescriptionProvider provider;
        private readonly TTarget target;

        #endregion Fields

        #region Constructors

        public DynamicPropertyManager()
        {
            Type type = typeof(TTarget);

            provider = new DynamicTypeDescriptionProvider(type);
            TypeDescriptor.AddProvider(provider, type);
        }

        public DynamicPropertyManager(TTarget target)
        {
            this.target = target;

            provider = new DynamicTypeDescriptionProvider(typeof(TTarget));
            TypeDescriptor.AddProvider(provider, target);
        }

        #endregion Constructors

        #region Properties

        public IList<PropertyDescriptor> Properties
        {
            get { return provider.Properties; }
        }

        #endregion Properties

        #region Methods

        public static DynamicPropertyDescriptor<TTargetType, TPropertyType>
           CreateProperty<TTargetType, TPropertyType>(
               string displayName,
               Func<TTargetType, TPropertyType> getter,
               Action<TTargetType, TPropertyType> setter,
               Attribute[] attributes)
        {
            return new DynamicPropertyDescriptor<TTargetType, TPropertyType>(
               displayName, getter, setter, attributes);
        }

        public static DynamicPropertyDescriptor<TTargetType, TPropertyType>
           CreateProperty<TTargetType, TPropertyType>(
              string displayName,
              Func<TTargetType, TPropertyType> getHandler,
              Attribute[] attributes)
        {
            return new DynamicPropertyDescriptor<TTargetType, TPropertyType>(
               displayName, getHandler, (t, p) => { }, attributes);
        }

        public void Dispose()
        {
            if (ReferenceEquals(target, null))
            {
                TypeDescriptor.RemoveProvider(provider, typeof(TTarget));
            }
            else
            {
                TypeDescriptor.RemoveProvider(provider, target);
            }
        }

        #endregion Methods
    }
}