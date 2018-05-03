using System;
using System.Collections;
using System.ComponentModel;

namespace XmlSettings
{
	/// <summary>
	/// Defines a custom type descriptor to be displayed in the property grid.
	/// </summary>
	#region SettingPd : PropertyDescriptor
	internal class SettingPd : PropertyDescriptor
	{ 
		private IniSetting curSetting;
		public SettingPd(IniSetting s,Attribute[] atts) 
			: base(s.Name, atts ) 
		{ 
			this.curSetting = s;
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
			if (this.curSetting.OriginalType == typeof(ArrayList))
			{
				return "Un-Editable";
			}
			else
			{
				return this.curSetting.Value;
			}
		} 
		
		public override object GetEditor(Type editorBaseType)
		{
			if (this.curSetting.OriginalType == typeof(ArrayList))
			{
				return TypeDescriptor.GetEditor(this, editorBaseType, true);
			}
			else
			{
				return TypeDescriptor.GetEditor(this, editorBaseType, true);
			}
			
		}
		
		public override Type PropertyType 
		{ 
			get
			{
				if (this.curSetting.OriginalType == typeof(ArrayList))
				{
					return typeof(string);
				}
				else
				{
					return this.curSetting.OriginalType;
				}
			}
		} 

		public override Type ComponentType 
		{ 
			get	{return this.curSetting.GetType();}
		} 
		
		public override string Description
		{
			get
			{
				if(curSetting.Description.Length==0)
				{
					return curSetting.TypeName;// + ", " +	curSetting.Value;
				}
				else
				{
					return curSetting.TypeName + "\n" + curSetting.Description;
				}
				
			}
		}

		public override string Name 
		{ 
			get
			{	
				return this.curSetting.Name;
			} 
		} 
 
		public override string DisplayName 
		{ 
			get
			{	
				//Return the setting name in the left column
				return this.curSetting.Name;
			} 
		} 
 
		public override bool IsReadOnly 
		{ 
			get
			{
				if (this.curSetting.OriginalType == typeof(ArrayList))
				{
					this.curSetting.ReadonlyInPG=true;
				}

				return this.curSetting.ReadonlyInPG;
			}
		} 
 
		public override bool CanResetValue(object component) 
		{ 
			return true; 
		} 

		public override void ResetValue(object component) 
		{ 
		} 
		public override void SetValue(object component, object Value) 
		{ 
			this.curSetting.Value = Value; 
		} 
 
		public override bool ShouldSerializeValue(object component) 
		{ 
			return false; 
		} 
 
	} 
	#endregion 

}
