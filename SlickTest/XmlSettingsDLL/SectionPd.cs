using System;
using System.ComponentModel;

namespace XmlSettings
{
	/// <summary>
	/// Defines a custom type descriptor to be displayed in the property grid.
	/// </summary>
	#region SectionPd : PropertyDescriptor
	internal class SectionPd : PropertyDescriptor 
	{ 
		private IniSection curSection;
		public SectionPd(IniSection s,Attribute[] atts) 
			: base(s.Name, atts ) 
		{ 
			this.curSection = s;
		} 

		public override AttributeCollection Attributes
		{
			get 
			{
				return new AttributeCollection(null);
			}
		}

		public override object GetValue(object component) 
		{ 
			return this.curSection.Settings;
		} 
 
		public override Type PropertyType 
		{ 
			get { return this.curSection.GetType(); }
		} 

		public override Type ComponentType 
		{ 
			get	{return this.curSection.Settings.GetType();}
		} 
		
		public override string Description
		{
			get
			{
				return "Section contains " + curSection.Settings.Count + " settings";
			}
		}

		public override string Category
		{
			get
			{
				return "Sections";
			}
		}

		public override string Name 
		{ 
			get
			{	
				return this.curSection.Name;
			} 
		} 
 
		public override string DisplayName 
		{ 
			get
			{	
				//Return the setting name in the left column
				return this.curSection.Name;
			} 
		} 
		 
		public override bool IsReadOnly 
		{ 
			get { return true;  }//Sections are read only
		} 
 
		public override bool CanResetValue(object component) 
		{ 
			return false; 
		} 

		public override void ResetValue(object component) 
		{ 
		} 
		
		public override void SetValue(object component, object Value) 
		{ 
		} 
 
		public override bool ShouldSerializeValue(object component) 
		{ 
			return false; 
		} 
	} 
	#endregion 

}
