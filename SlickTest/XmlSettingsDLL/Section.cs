using System;
using System.ComponentModel;

namespace XmlSettings
{
	/// <summary>Class represents a section similar to an information file(INI) section.
	/// </summary>
	/// <remarks>Provides PropertyGrid support.</remarks>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class IniSection
	{
		private IniSettings theSettings;
		private string sectionName;
		private bool displayInPG = true;
		internal object parent;
		//Refers to the top level object.
		internal AppSettings TopObject;

		/// <summary>Constructor requiring a name and a reference to the top object.</summary>
		[Category("Section")]
		public IniSection(string secName,AppSettings topObject)
		{
			this.Name = secName;
			this.TopObject = topObject;
			//New settings passing the reference to the top object.
			theSettings = new IniSettings(this.TopObject);
		}
		
		/// <summary>
		/// Settings collection property.
		/// </summary>
		[Description("Collection of settings")]
		public IniSettings Settings
		{
			get { return theSettings; }
			set{theSettings = value;}
		}
		
		/// <summary>
		/// Indexer access to the Setting by index.
		/// </summary>
		/// <value>The item number</value>
		public IniSetting this[int itemNumber]
		{
			get
			{
				return Settings[itemNumber];
			}
			set
			{
				Settings[itemNumber] = value;
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
				return Settings[itemName];
			}
			set
			{
				Settings[itemName] = value;
			}
		}

		/// <summary>
		/// Section name
		/// </summary>
		/// <value>Section name</value>
		[Description("Section name")]
		public string Name
		{
			get { return sectionName; }
			set{sectionName = value;}
		}	
		
		/// <summary>
		/// Parent object is set with the Add method.
		/// </summary>
		/// <value>Section name</value>
		/// <remarks>Could be a IniSection or IniSetting</remarks>
		public object Parent
		{
			get { return parent; }
		}	

		/// <summary>Meaningful text representation</summary>
		public override string ToString()
		{
			return this.Settings.Count + " Settings";;
		}
		
		/// <summary>
		/// Can we see a section in the Property Grid or not.
		/// </summary>
		public bool DisplayInPG
		{
			get
			{
				return this.displayInPG;
			}
			set
			{
				this.displayInPG = value;
			}

		}

	}

}
