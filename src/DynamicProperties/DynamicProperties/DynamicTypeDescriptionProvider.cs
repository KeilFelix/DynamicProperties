using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keil.DynamicProperties
{
    public class DynamicTypeDescriptionProvider : TypeDescriptionProvider
    {
        #region Fields

        private readonly List<PropertyDescriptor> properties = new List<PropertyDescriptor>();
        private readonly TypeDescriptionProvider provider;

        #endregion Fields

        #region Constructors

        public DynamicTypeDescriptionProvider(Type type)
        {
            provider = TypeDescriptor.GetProvider(type);
        }

        #endregion Constructors

        #region Properties

        public IList<PropertyDescriptor> Properties
        {
            get { return properties; }
        }

        #endregion Properties

        #region Methods

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new DynamicCustomTypeDescriptor(
               this, provider.GetTypeDescriptor(objectType, instance));
        }

        #endregion Methods

        #region Classes

        private class DynamicCustomTypeDescriptor : CustomTypeDescriptor
        {
            #region Fields

            private readonly DynamicTypeDescriptionProvider provider;

            #endregion Fields

            #region Constructors

            public DynamicCustomTypeDescriptor(DynamicTypeDescriptionProvider provider,
               ICustomTypeDescriptor descriptor)
                  : base(descriptor)
            {
                this.provider = provider;
            }

            #endregion Constructors

            #region Methods

            public override PropertyDescriptorCollection GetProperties()
            {
                return GetProperties(null);
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                var properties = new PropertyDescriptorCollection(null);

                foreach (PropertyDescriptor property in base.GetProperties(attributes))
                {
                    properties.Add(property);
                }

                foreach (PropertyDescriptor property in provider.Properties)
                {
                    properties.Add(property);
                }
                return properties;
            }

            #endregion Methods
        }

        #endregion Classes
    }
}