using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keil.DynamicProperties
{
    public class DynamicPropertyDescriptor<TTarget, TProperty> : PropertyDescriptor
    {
        #region Fields

        private readonly Func<TTarget, TProperty> getter;
        private readonly string propertyName;
        private readonly Action<TTarget, TProperty> setter;

        #endregion Fields

        #region Constructors

        public DynamicPropertyDescriptor(
           string propertyName,
           Func<TTarget, TProperty> getter,
           Action<TTarget, TProperty> setter,
           Attribute[] attributes)
              : base(propertyName, attributes ?? new Attribute[] { })
        {
            this.setter = setter;
            this.getter = getter;
            this.propertyName = propertyName;
        }

        #endregion Constructors

        #region Properties

        public override Type ComponentType
        {
            get { return typeof(TTarget); }
        }

        public override bool IsReadOnly
        {
            get { return setter == null; }
        }

        public override Type PropertyType
        {
            get { return typeof(TProperty); }
        }

        #endregion Properties

        #region Methods

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            var o = obj as DynamicPropertyDescriptor<TTarget, TProperty>;
            return o != null && o.propertyName.Equals(propertyName);
        }

        public override int GetHashCode()
        {
            return propertyName.GetHashCode();
        }

        public override object GetValue(object component)
        {
            return getter((TTarget)component);
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            setter((TTarget)component, (TProperty)value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        #endregion Methods
    }
}