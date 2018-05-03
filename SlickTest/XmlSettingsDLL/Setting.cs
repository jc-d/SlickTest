using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace XmlSettings
{
	/// <summary>Class represents a name-value pairs similar to an information file(INI).</summary>
	/// <remarks>Provides PropertyGrid support and
	/// support for sub-setings.</remarks>
	public class IniSetting:CollectionBase
	{
		private object settingValue = null;
		private string settingName = "";
		private string tag = "";
		private string description = "";
		private string settingType = "Empty";
		private Type originalType = null;
		internal object parent;
		private bool readonlyInPG = false;
		private bool displayInPG = true;
		//Refers to the top level object.
		internal AppSettings TopObject;


		/// <summary>
		/// Parent object is set with the Add method.
		/// </summary>
		/// <value>Parent object</value>
		/// <remarks>Could be a IniSection ,IniSetting or IniSettings</remarks>
		public object Parent
		{
			get { return parent; }
		}	

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <remarks>Every Setting object should have a name.</remarks>
		/// <param name="settingName">The name of the setting.</param>
		public IniSetting(string settingName) //Constructor
		{
			this.settingName = settingName;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <remarks>Default constructor is required for the propertyBrowser 
		/// collection editor to add an item.</remarks>
		public IniSetting()
		{
			this.settingName = "Empty";
			this.Value = "Empty";
		}
		
		/// <summary>
		/// Sets or returns the value of the setting
		/// </summary>	
		/// <remarks>The setting type is determined when the value is set. 
		/// This should be the only way we set a value.</remarks>
		public object Value
		{
			set
			{
				this.originalType = value.GetType();
				this.settingType = value.GetType().Name;
				this.settingValue = value;
				this.TopObject.mDirty = true;
			}
			get
			{
				//Returns an object that will need to be cast.
				return Convert.ChangeType(settingValue,originalType);
			}
		}
		
		/// <summary>
		/// Can we change a setting in the Property Grid or not.
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

		/// <summary>
		/// Can we change a setting in the Property Grid or not.
		/// </summary>
		public bool ReadonlyInPG
		{
			get
			{
				return this.readonlyInPG;
			}
			set
			{
				this.readonlyInPG = value;
			}

		}

		/// <summary>
		/// The Setting name.
		/// </summary>
		public string Name
		{
			get
			{
				return this.settingName;
			}
			set
			{
				this.settingName = value;
			}

		}
		/// <summary>
		/// Extra string information associated with the setting.
		/// </summary>
		/// <remarks>Used internally to save controls tag data.</remarks>
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}

		}

		/// <summary>
		/// An optional description of the setting.
		/// </summary>
		/// <remarks>Will be displayed in PropertyGrid.</remarks>
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}

		}

		/// <summary>Read-only. Returns a string type name.</summary>
		/// <remarks>Used internally to track the object type.</remarks>
		public string TypeName
		{
			get
			{
				return this.settingType;
			}

		}

		/// <summary>Read-only. Returns the value type.</summary>
		/// <remarks>Used internally</remarks>
		public Type OriginalType
		{
			get
			{
				return this.originalType;
			}
		}

		/// <summary>
		/// Conversion to IniSettings collection.
		/// A setting can act as a DataSource if it is a IniSettings collection.
		/// </summary>
		public IniSettings ToDataSource() 
		{
			return (IniSettings)this;
		}
	
		#region  Indexers

		/// <summary>
		/// Access to Setting in a sub-IniSettings by index.
		/// </summary>
		public IniSetting this[int itemNumber]
		{
			get
			{
				return ((IniSettings)settingValue)[itemNumber];
			}
		}
		/// <summary>
		/// Access to Setting in a sub-IniSettings by name.
		/// </summary>
		public IniSetting this[string itemName]
		{
			get
			{
				return ((IniSettings)settingValue)[itemName];
			}
		}
		#endregion  Indexers


		#region  Implicit conversions

		/// <summary>
		/// Implicit conversion to IniSettings collection.
		/// A setting can act as a section to contain a collection of more settings.
		/// </summary>
		/// <exception cref="System.ApplicationException">Thrown when something
		/// unexpected happens.</exception>
		public static implicit operator IniSettings(IniSetting v) 
		{
			if (v.settingType!=Types.INI_SETTINGS)
			{
				throw new Exception("No conversion from " + v.settingType + " to IniSettings." );
			}
			return (IniSettings)v.settingValue;
		}

		/// <summary>
		/// Implicit conversion to IniSection.
		/// A setting can be a sub-section to contain a collection of more settings.
		/// </summary>
		/// <exception cref="System.ApplicationException">Thrown when something
		/// unexpected happens.</exception>
		public static implicit operator IniSection(IniSetting v) 
		{
			if (v.settingType!=Types.INI_SECTION)
			{
				throw new Exception("No conversion from " + v.settingType + " to IniSection." );
			}
			return (IniSection)v.settingValue;
		}

		/// <summary>
		/// Implicit conversion to bool.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator bool(IniSetting v) 
		{
			if (v.settingType!=Types.BOOLEAN)
			{
				throw new Exception("No conversion from " + v.settingType + " to bool." );
			}
			return (bool)v.settingValue;
		}

		
		/// <summary>
		/// Implicit conversion to byte.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator byte(IniSetting v) 
		{
			if (v.settingType!=Types.BYTE)
			{
				throw new Exception("No conversion from " + v.settingType + " to byte." );
			}
			return (byte)v.settingValue;
		}

		
		/// <summary>
		/// Implicit conversion to char.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator char(IniSetting v) 
		{
			if (v.settingType!=Types.CHAR)
			{
				throw new Exception("No conversion from " + v.settingType + " to char." );
			}
			return (char)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to DateTime.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator DateTime(IniSetting v) 
		{
			if (v.settingType!=Types.DATETIME)
			{
				throw new Exception("No conversion from " + v.settingType + " to DateTime." );
			}
			return (DateTime)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to Decimal.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator decimal(IniSetting v) 
		{
			if (v.settingType!=Types.DECIMAL)
			{
				throw new Exception("No conversion from " + v.settingType + " to Decimal." );
			}
			return (decimal)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to double.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator double(IniSetting v) 
		{
			if (v.settingType!=Types.DOUBLE)
			{
				throw new Exception("No conversion from " + v.settingType + " to Double." );
			}
			return (double)v.settingValue;
		}
		

		/// <summary>
		/// Implicit conversion to Int16.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Int16(IniSetting v) 
		{
			if (v.settingType!=Types.INT16)
			{
				throw new Exception("No conversion from " + v.settingType + " to Int16." );
			}
			return (Int16)v.settingValue;
		}
		

		/// <summary>
		/// Implicit conversion to Int32.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Int32(IniSetting v) 
		{
			if (v.settingType!=Types.INT32)
			{
				throw new Exception("No conversion from " + v.settingType + " to Int32." );
			}
			return (Int32)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to Int64.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Int64(IniSetting v) 
		{
			if (v.settingType!=Types.INT64)
			{
				throw new Exception("No conversion from " + v.settingType + " to Int64." );
			}
			return (Int64)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to Single.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Single(IniSetting v) 
		{
			if (v.settingType!=Types.SINGLE)
			{
				throw new Exception("No conversion from " + v.settingType + " to Single." );
			}
			return (Single)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to UInt16.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator UInt16(IniSetting v) 
		{
			if (v.settingType!=Types.UINT16)
			{
				throw new Exception("No conversion from " + v.settingType + " to UInt16." );
			}
			return (UInt16)v.settingValue;
		}


		/// <summary>
		/// Implicit conversion to UInt32.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator UInt32(IniSetting v) 
		{
			if (v.settingType!=Types.UINT32)
			{
				throw new Exception("No conversion from " + v.settingType + " to UInt32." );
			}
			return (UInt32)v.settingValue;
		}
		

		/// <summary>
		/// Implicit conversion to UInt64.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator UInt64(IniSetting v) 
		{
			if (v.settingType!=Types.UINT64)
			{
				throw new Exception("No conversion from " + v.settingType + " to UInt64." );
			}
			return (UInt64)v.settingValue;
		}
		

		/// <summary>
		/// Implicit conversion to string.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator string(IniSetting v) 
		{
			return v.settingValue.ToString();
		}
		

		/// <summary>
		/// Implicit conversion to ArrayList.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator ArrayList(IniSetting v) 
		{
			if (v.settingType!=Types.ARRAYLIST)
			{
				throw new Exception("No conversion from " + v.settingType + " to ArrayList." );
			}
			return (ArrayList)v.settingValue;
		}
		
		/// <summary>
		/// Implicit conversion to Color.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Color(IniSetting v) 
		{
			if (v.settingType!=Types.COLOR)
			{
				throw new Exception("No conversion from " + v.settingType + " to Color." );
			}
			return (Color)v.settingValue;
		}
		
		/// <summary>
		/// Implicit conversion to Font.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Font(IniSetting v) 
		{
			if (v.settingType!=Types.FONT)
			{
				throw new Exception("No conversion from " + v.settingType + " to Font." );
			}
			return (Font)v.settingValue;
		}

		/// <summary>
		/// Implicit conversion to Size.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Size(IniSetting v) 
		{
			if (v.settingType!=Types.SIZE)
			{
				throw new Exception("No conversion from " + v.settingType + " to Size." );
			}
			return (Size)v.settingValue;
		}

		/// <summary>
		/// Implicit conversion to Point.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Point(IniSetting v) 
		{
			if (v.settingType!=Types.POINT)
			{
				throw new Exception("No conversion from " + v.settingType + " to Point." );
			}
			return (Point)v.settingValue;
		}

		/// <summary>
		/// Implicit conversion to Keys.
		/// Conversion to original type only.
		/// </summary>
		/// <remarks>To return an Object type use the Value property.</remarks>
		/// <exception cref="System.Exception">Thrown when no conversion is possible.</exception>
		public static implicit operator Keys(IniSetting v)
		{
			if (v.settingType != Types.KEYS)
			{
				throw new Exception("No conversion from " + v.settingType + " to Keys.");
			}
			return (Keys)v.settingValue;
		}
		#endregion  //implicit conversions

	}
}
