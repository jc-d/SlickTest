using System;
using System.Drawing.Design;
using System.Collections;
using System.ComponentModel;

namespace XmlSettings
{
	/// <summary>Collection class of IniSettings.</summary>
	/// <remarks>Provides PropertyGrid support.</remarks>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Editor(typeof(NullCollectionEditor),typeof(System.Drawing.Design.UITypeEditor))]
	public class IniSettings :CollectionBase,ICustomTypeDescriptor
	{
		//Recently used index
		private int recentSettingIndex;
		//Refers to the top level object.
		internal AppSettings TopObject;

		/// <summary>
		/// Constructor taking a reference to the top object.
		/// </summary>
		public IniSettings(AppSettings topObject)
		{
			this.TopObject = topObject;
		}

		/// <summary>
		/// A friendly description for the PropertyGrid.
		/// </summary>
		/// <returns>A friendly description.</returns>
		public override string ToString()
		{
			return this.Count + " Settings";
		}

		/// <summary>
		/// Removes a single named item from the collection.
		/// </summary>
		/// <param name="itemName">Item name</param>
		public void Remove(string itemName)
		{
			RemoveAt(ByName(itemName));
			recentSettingIndex = 0;
		}

		/// <summary>
		/// Removes a single indexed item from the collection.
		/// </summary>
		/// <param name="itemNumber">Item name</param>
		public void Remove(int itemNumber)
		{
			RemoveAt(itemNumber);
			recentSettingIndex = 0;
		}

		/// <summary>
		/// Add a new item and return a reference to it.
		/// </summary>
		/// <param name="settingName">Setting name</param>
		/// <returns>The IniSetting object</returns>
		public IniSetting Add(string settingName)
		{
			
			//Add a new item if needed
			IniSetting ret = this[settingName];
			if(ret==null)
			{
				recentSettingIndex = this.List.Add(new IniSetting(settingName));
				ret = (IniSetting)this.List[recentSettingIndex];
				//Any time we create a new setting we set the references
				ret.parent = this;
				ret.TopObject = this.TopObject;
			}
			return ret;
		}

		/// <summary>
		/// Add a new item, set the value and return a reference to it.
		/// </summary>
		/// <param name="settingName">Setting name</param>
		/// <param name="val">The setting value</param>
		/// <returns>The IniSetting object</returns>
		public IniSetting Add(string settingName,object val)
		{
			//Call the simpler add method.
			IniSetting ret = this.Add(settingName);
			ret.Value = val;
			return ret;
		}

		/// <summary>
		/// Add a new item, set the value and return a reference to it.
		/// </summary>
		/// <param name="settingName">Setting name</param>
		/// <param name="val">The setting value</param>
		/// <param name="tag">The extra tag string</param>
		/// <returns>The IniSetting object</returns>
		public IniSetting Add(string settingName,object val,string tag)
		{
			
			//Call the simpler add method.
			IniSetting ret = this.Add(settingName);
			ret.Value = val;
			ret.Tag = tag;
			return ret;
		}

		/// <summary>
		/// Indexer access to the Setting by index.
		/// </summary>
		/// <value>The item number</value>
		public IniSetting this[int itemNumber]
		{
			get
			{
				return (IniSetting)this.List[itemNumber];
			}
			set
			{
				this.List[itemNumber] = value;
			}
		}

		/// <summary>
		/// Indexer access to the Setting by name.
		/// </summary>
		/// <value>The item name</value>
		public IniSetting this[string itemName]
		{
			get
			{
				int ret = ByName(itemName);
				if (ret ==-1)return null;
				return (IniSetting)this.List[ret];
			}
			set
			{
				this.List[ByName(itemName)] = value;
			}
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		internal int ByName(string settingName)
		{
			int ct = Count;
			if(ct>0 && recentSettingIndex<ct)
			{
				//Try to use the last accessed to avoid the loop
				if (((IniSetting)this.List[recentSettingIndex]).Name == settingName)
					return recentSettingIndex;
			}

			//Search for it in the list
			int r = 0;
			foreach(IniSetting s in this)
			{
				if (s.Name == settingName)
				{
					recentSettingIndex = r;
					return recentSettingIndex;
				}
				r++;
			}
			
			return -1;
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			// Create a collection object to hold property descriptors
			PropertyDescriptorCollection propDesColl = new PropertyDescriptorCollection(null);
			

			// Iterate the list of settings and get the type of each one.
			for( int i=0; i<this.Count; i++ )
			{
				//Create a property descriptor for the setting
				//and add it to the property descriptor collection
				if(this[i].DisplayInPG)
				{
					SettingPd propDes = new SettingPd(this[i],attributes);
					propDesColl.Add(propDes);
				}
			}
			// return the property descriptor collection
			return propDesColl;
		}
#region  PropertyGrid Support
		///<summary>Required for PropertyGrid.</summary>
		///System.ComponentModel.ICustomTypeDescriptor implementation
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}
#endregion  PropertyGrid Support

	}
#region  PropertyGrid Support
	//Thwart the default collection editor for now
	///<summary>Required for PropertyGrid.</summary>
	[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
	public class NullCollectionEditor : UITypeEditor 
	{
		///<summary>Required for PropertyGrid.</summary>
		public NullCollectionEditor() {}
		///<summary>Required for PropertyGrid.</summary>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			// Tell that we don't want editing
			return UITypeEditorEditStyle.None;
		}
	}
#endregion  PropertyGrid Support
}
