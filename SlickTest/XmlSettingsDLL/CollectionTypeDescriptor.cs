using System;
using System.Collections;
using System.ComponentModel;
namespace SettingsToXML
{
	/// <summary>
	/// Base class to implement ICustomTypeDescriptor.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	internal class CollectionTypeDescriptor :CollectionBase,ICustomTypeDescriptor
	{
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}

		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}

		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(this, true);
		}

	}
}
