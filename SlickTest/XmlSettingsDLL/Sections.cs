using System;
using System.Collections;
using System.ComponentModel;

namespace XmlSettings
{
	/// <summary>Collection class of IniSections.</summary>
	/// <remarks>Provides PropertyGrid support.</remarks>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class IniSections :CollectionBase,ICustomTypeDescriptor
	{
		//Recently used index
		private int recentSectionIdx = 0;
		
		//Refers to the top level object.
		internal AppSettings TopObject;
		
		/// <summary>Constructor requiring a reference to the top object</summary>
		internal IniSections(AppSettings topObject)
		{
			this.TopObject = topObject;
		}

		/// <summary>
		/// Removes a single named item from the collection.
		/// </summary>
		/// <param name="itemName">The item name</param>
		public void Remove(string itemName)
		{
			RemoveAt(ByName(itemName));
			recentSectionIdx = 0;
		}

		/// <summary>
		/// Removes a single index item from the collection.
		/// </summary>
		/// <param name="itemNumber">The item number.</param>
		public void Remove(int itemNumber)
		{
			RemoveAt(itemNumber);
			recentSectionIdx = 0;
		}

		/// <summary>
		/// Adds a named section to the collection.
		/// </summary>
		/// <param name="sectionName">The section name.</param>
		/// <returns>The Section object reference.</returns>
		public IniSection Add(string sectionName)
		{
			//Add a new item if needed
			IniSection ret = this[sectionName];
			if(ret==null)
			{
				recentSectionIdx = this.List.Add(new IniSection(sectionName,this.TopObject));
				ret = (IniSection)this.List[recentSectionIdx];
				ret.parent = this;
				//ret.TopObject = this.TopObject;
			}
			return ret;
		}

		/// <summary>
		/// Indexer access to the Section by index.
		/// </summary>
		/// <value>The item number</value>
		public IniSection this[int itemNumber]
		{
			get
			{
				return (IniSection)this.List[itemNumber];
			}
			set
			{
				this.List[itemNumber] = value;
			}
		}

		/// <summary>
		/// Indexer access to the Section by name.
		/// </summary>
		/// <value>The item name</value>
		public IniSection this[string itemName]
		{
			get
			{
				int ret = ByName(itemName);
				if (ret ==-1)
					return null;
				return (IniSection)this.List[ret];
			}
			set
			{
				this.List[ByName(itemName)] = value;
			}
		}

		
		/// <summary>Gets the index of a section by name.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		internal int ByName(string sectionName)
		{
			int ct = Count;
			if(ct>0 && recentSectionIdx<ct)
			{	
				//Try to use the last accessed to avoid the loop
				if (((IniSection)this.List[recentSectionIdx]).Name == sectionName)
					return recentSectionIdx;
			}
			
			int r = 0;
			foreach (IniSection s in this)
			{
				if (s.Name == sectionName)
				{
					recentSectionIdx = r;
					return recentSectionIdx;
				}
				r++;
			}
			return -1;
		}
	
	#region  PropertyGrid Support

		///<summary>Required for PropertyGrid.</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			// Create a collection object to hold property descriptors
			PropertyDescriptorCollection secDesColl = new PropertyDescriptorCollection(null);
			
			// Iterate the list of settings and get the type of each one.
			for( int i=0; i<this.Count; i++ )
			{
				//Create a property descriptor for the section
				//and add to the property descriptor collection
				if (this[i].DisplayInPG)
				{
					SectionPd secDes = new SectionPd(this[i],attributes);
					secDesColl.Add(secDes);
				}
			}
			// return the property descriptor collection
			return secDesColl;
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}

		/// <summary>Required for PropertyGrid</summary>
		[EditorBrowsable(EditorBrowsableState.Never),BrowsableAttribute(false)]
		public PropertyDescriptorCollection GetProperties()
		{
			return TypeDescriptor.GetProperties(this, true);
		}
		#endregion  PropertyGrid Support
	}
}
