using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
namespace XmlSettings
{
	/// <summary>
	/// Supported data type string constants.
	/// </summary>
	internal class Types
	{
		#region string constants
		internal const string EMPTY =		"Empty";
		internal const string STRING =	"String";
		internal const string BOOLEAN =	"Boolean";
		internal const string BYTE =		"Byte";
		internal const string CHAR =		"Char";
		internal const string DATETIME =	"DateTime";
		internal const string DECIMAL =	"Decimal";
		internal const string DOUBLE =	"Double";
		internal const string INT16 =		"Int16";
		internal const string INT32 =		"Int32";
		internal const string INT64 =		"Int64";
		internal const string SINGLE =	"Single";
		internal const string UINT16 =	"UInt16";
		internal const string UINT32 =	"UInt32";
		internal const string UINT64 =	"UInt64";
		internal const string ARRAYLIST =	"ArrayList";
		internal const string COLOR =		"Color";
		internal const string FONT =		"Font";
		internal const string SIZE =		"Size";
		internal const string POINT =		"Point";
		internal const string INI_SETTINGS="IniSettings";
		internal const string INI_SECTION="IniSection";
		internal const string KEYS = "Keys";
		#endregion
	}
	/// <summary>
	/// XML tag names.
	/// </summary>
	internal class Tags
	{
		#region string constants
		internal const string SECTIONS="Sections";
		internal const string SECTION="Section";
		internal const string SETTINGS="Settings";
		internal const string SETTING="Setting";
		internal const string VALUE="Value";
		internal const string ARRAYLIST="ArrayList";
		internal const string ITEM="Item";
		internal const string NAME="Name";
		internal const string TYPE="Type";
		internal const string TAG="Tag";
		internal const string DESCRIPTION="Desc";
		#endregion
	}
	/// <summary>
	/// Top-level class.
	/// Provides a way to save application settings to xml file format.
	/// </summary>
	/// <remarks>
	/// Used to replace the standard Windows INI file format.
	/// Sections are named categories to hold collections of settings.
	/// Settings are name-value pairs able to store many different data types.
	/// </remarks>
	/// <example>
	/// Create an instance of the XMLSettings class.
	/// <code>
	/// MySettings = new XMLSettings("MySettings.xml")
	/// MySettings = new XMLSettings("C:\\MySettings[.xml]")</code>
	/// Load the file.
	/// <code>
	/// bool loaded = MySettings.Load([File]);</code>
	/// Return a setting passing a default value.
	/// If the value exists then the existing setting is returned.
	/// <code>
	/// BackColor = MySettings.GetVal("FormSettings","BackColor",BackColor);</code>
	/// Set a value passing a section, name and value.
	/// Value and section will be created if needed.
	/// <code>
	/// MySettings.SetVal("FormSettings","Left",Left);</code>
	/// Return the same value with 2 different notations.
	/// <code>
	/// ret = MySettings.GetVal("FormSettings","Left");
	/// ret = MySettings.Sections["FormSettings"]["Left"];</code>
	/// Add a collection of Settings.
	/// <code>
	/// IniSettings SubSettings1 = new IniSettings(MySettings);			
	/// SubSettings1.Add("S1","SubSettings1 test");
	/// SubSettings1.Add("S2",1234);
	/// SubSettings1.Add("S3",true);
	/// MySettings.SetVal("Coll","SubSettings1",SubSettings1);</code>
	/// Access a sub-setting in that section.
	/// <code>
	/// bret = MySettings.Sections["Coll"]["SubSettings1"]["S3"];</code>
	/// Use as a Datasource.
	/// <code>
	/// list1.DisplayMember = "Name";
	///	list1.DataSource = MySettings.Sections["Coll"]["SubSettings1"].ToDataSource();</code>
	/// </example>
	public class AppSettings
	{
		KeysConverter mKeyConv = new KeysConverter();
		private IniSections topLevelSections;//Section names
		private string xmlFileName = "";
		private string DllPath = "";
		private string mXmlComments = "";
		internal bool mDirty;
		private DateTime mFileDate;
		private string mPathToXmlFile = "";
		private string mLastXmlError = "";

		/// <summary>
		/// Reurns a user description if there is one.
		/// </summary>
		public string LastXmlError
		{
			get
			{
				return mLastXmlError;
			}
		}
		/// <summary>
		/// Reurns the comments at the top of the XML
		/// after having read the file.
		/// </summary>
		public string XmlComments
		{
			get
			{
				return mXmlComments;
			}
		}

		/// <summary>
		/// Indicates when the settings have changed and need to saved or re-read.
		/// </summary>
		public bool Dirty
		{
			get
			{
				return mDirty;
			}
		}

		/// <summary>
		/// Access to the collection of Sections.
		/// </summary>
		public IniSections Sections
		{
			get
			{
				return topLevelSections;
			}
			set
			{
				topLevelSections = value;
			}
		}

		/// <summary>Constructor with no file specified.</summary>
		public AppSettings()
		{
			try
			{
				topLevelSections = new IniSections(this);//Section names
			}
			catch
			{
				mLastXmlError += "AppSettings constructor failed\n";
				throw;
			}
		}


		/// <summary>
		/// Constructor with file name specified.
		/// </summary>
		/// <remarks> Passing only a file name without a path will result
		///  in the file being saved in the same folder as this DLL.
		/// </remarks>
		/// <param name="fileName">Full path and file name.</param>
		/// <exception cref="System.ApplicationException">Thrown when the specified
		/// folder does not exist.</exception>/// 
		public AppSettings(string fileName)
		{
			try
			{
				topLevelSections = new IniSections(this);//Section names
				DllPath = GetDllPath();
				xmlFileName = EnsurePathIsIncluded(fileName);
			}
			catch
			{
				mLastXmlError += "AppSettings(string fileName) constructor failed\n";
				throw;
			}
			
		}

		/// <summary>
		/// Removes a Setting with a specific name.
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <exception cref="System.NullReferenceException">Thrown when the specified
		/// section or setting does not exist.</exception>/// 
		public void Remove(string sectionName, string settingName)
		{
			try
			{
				//If the value exists then remove it.
				Sections[sectionName].Settings.Remove(settingName);
			}
			catch
			{
				mLastXmlError += "Remove(string sectionName, string settingName) failed\n";
				throw;
			}
		}
		
		/// <summary>
		/// Removes a Section with a specific name.
		/// </summary>
		/// <param name="sectionName">The name of the section</param>
		/// <exception cref="System.NullReferenceException">Thrown when the specified
		/// section does not exist.</exception>/// 
		public void Remove(string sectionName)
		{
			try
			{
				//If the section exists then remove it.
				Sections.Remove(sectionName);
			}
			catch
			{
				mLastXmlError += "Remove(string sectionName) failed\n";
				throw;
			}
		}
		
		/// <summary>
		/// Creates a section and setting as needed then assigns a value.
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="oValue">Oject supporting many data types</param>
		/// <returns>The setting object.</returns>
		public IniSetting SetVal(string sectionName, string settingName, object oValue)
		{
			//If the section exists then just update the value.
			IniSetting setting = Sections.Add(sectionName).Settings.Add(settingName);
			setting.Value = oValue;
			return setting;
		}

		/// <summary>
		/// Creates a section and setting as needed then assigns a value.
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="oValue">Oject supporting many data types</param>
		/// <param name="description">A description of the setting</param>
		/// <returns>The setting object.</returns>
		public IniSetting SetVal(string sectionName, string settingName, object oValue,string description)
		{
			//If the section exists then update the value.
			IniSetting setting = SetVal(sectionName,settingName,oValue);
			setting.Description = description;
			return setting;
		}

		/// <summary>
		/// Gets the IniSetting object by name.
		/// </summary>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <returns>The setting object.</returns>
		public IniSetting GetVal(string sectionName, string settingName)
		{
			try
			{
				return Sections[sectionName].Settings[settingName];
			}
			catch
			{
				throw;
			}
		}
		
		/// <summary>
		/// Gets the IniSetting object by name passing a default.
		/// </summary>
		/// <remarks>Default value will only be assigned if the setting does not exist.</remarks>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="oDefault">Default value</param>
		/// <returns>The setting object.</returns>
		public IniSetting GetVal(string sectionName, string settingName,object oDefault)
		{
			try
			{
				IniSection sec = topLevelSections.Add(sectionName);
				IniSetting setting = sec.Settings[settingName];
				//Make a value if needed and assign the default value
				if(setting==null)
				{
					setting = sec.Settings.Add(settingName);
					setting.TopObject = this;
					setting.Value=oDefault;
				}
				return setting;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Gets the IniSetting object by name passing a default.
		/// </summary>
		/// <remarks>Default value will only be assigned if the setting does not exist.</remarks>
		/// <param name="sectionName">Section name</param>
		/// <param name="settingName">Setting name</param>
		/// <param name="oDefault">Default value</param>
		/// <param name="description">A description of the setting</param>
		/// <returns>The setting object.</returns>
		public IniSetting GetVal(string sectionName, string settingName,object oDefault,string description)
		{
			try
			{
				IniSection sec = topLevelSections.Add(sectionName);
				IniSetting setting = sec.Settings[settingName];
				//Make a value if needed and assign the default value
				if(setting==null)
				{
					setting = sec.Settings.Add(settingName);
					setting.TopObject = this;
					setting.Value=oDefault;
				}
				setting.Description = description;
				return setting;
			}
			catch
			{
				throw;
			}
		}
		
		/// <summary>
		/// Sets or returns the folder the file will be stored in.
		/// </summary>
		/// <remarks>Once set, you can just provide the file name without the path. 
		/// The value can be overridden by passing a fully dqualified path and file.</remarks>
		/// <exception cref="System.ApplicationException">Thrown when the path is not valid.</exception>
		public string PathToXmlFile
		{
			get
			{
				return mPathToXmlFile;
			}
			set
			{
				mPathToXmlFile = value;
				if(Directory.Exists(mPathToXmlFile)) 
				{
					if(!mPathToXmlFile.EndsWith("\\"))
					{
						mPathToXmlFile += "\\";
					}
				}
				else
				{
					throw new ApplicationException("Folder does not exist.");
				}

			}

		}
		
		/// <summary>
		/// Save the settings to the specified path and file.
		/// </summary>
		/// <param name="pathAndFileName">Full path and file.</param>
		/// <returns>True on success, False on failure.</returns>
		/// <exception cref="System.ApplicationException">Thrown when the path specified is nat valid.</exception>
		public void Save(string pathAndFileName)
		{
			try
			{
				xmlFileName = EnsurePathIsIncluded(pathAndFileName);
				Save();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Save the settings to the specified path and file.
		/// </summary>
		/// <remarks>Using the file name specified during construction.</remarks>
		/// <returns>void</returns>
		public void Save()
		{
			try
			{
				
				WriteXMLSettings(xmlFileName);
				mDirty=false;
				mFileDate = File.GetLastWriteTime(xmlFileName);
			}
			catch
			{
				throw;
			}
		}


		/// <summary>
		/// Loads the settings from the specified path and file.
		/// </summary>
		/// <param name="pathAndFileName">Full path and file.</param>
		/// <returns>void</returns>
		public void Load(string pathAndFileName)
		{
			try
			{
				xmlFileName = EnsurePathIsIncluded(pathAndFileName);
				Load();
			}
			catch(Exception ex)
			{
				mLastXmlError = ex.Message;
				throw;
			}
		}

		/// <summary>
		/// Loads the settings.
		/// </summary>
		/// <remarks>Using the file name specified during construction.</remarks>
		/// <returns>void</returns>
		public void Load()
		{
			try
			{
				ReadXMLSettings(xmlFileName);
				this.mDirty=false;
				mFileDate = File.GetLastWriteTime(xmlFileName);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Has the xml file changed since the last read or write?
		/// </summary>
		public bool FileChanged
		{
			get
			{
				try
				{
					return mFileDate!=File.GetLastWriteTime(xmlFileName);
				}
				catch{return true;}
			}
		}


		/// <summary>
		/// Writes all sections and settings to the xml file.
		/// </summary>
		private void WriteXMLSettings(string xmlFileName)
		{
			mLastXmlError="";
			XmlTextWriter xWriter = new XmlTextWriter(xmlFileName, null);
			try
			{
				xWriter.Formatting = Formatting.Indented;
				xWriter.WriteStartDocument(true);
				//Style sheet to make it look like an INI file			
				//string sXsl = "type='text/xsl' href='INI.xsl'";
				//xWriter.WriteProcessingInstruction("xml-stylesheet", sXsl);
				//Get started writing
                //xWriter.WriteComment("MacGen Programming www.CncEdit.com");
				xWriter.WriteComment("XML Version=2.0");
				//The sections
				xWriter.WriteStartElement(Tags.SECTIONS);
				foreach(IniSection TopLevelSection in Sections)
				{
					xWriter.WriteStartElement(Tags.SECTION);
					xWriter.WriteAttributeString(Tags.NAME,TopLevelSection.Name);
						WriteXMLSection(TopLevelSection.Settings,ref xWriter);//Pass the settings down
					xWriter.WriteEndElement();//End of the section
				}
				xWriter.WriteEndElement();
			}
			catch(Exception e)
			{
				mLastXmlError += "Cannot write settings. " + e.Message + "\n";
				throw;
			}
			finally
			{
				//Write the XML to file and close
				if(xWriter!=null)
				{
					xWriter.Close();
				}
			}
		}

		/// <summary>Writes each sections settings.</summary>
		private void WriteXMLSection(IniSettings currSettings,ref XmlTextWriter xWriter)
		{
			foreach(IniSetting iniSetting in currSettings)
			{
				if(iniSetting==null)continue;
				xWriter.WriteStartElement(Tags.SETTING);
				xWriter.WriteAttributeString(Tags.NAME,iniSetting.Name);
				xWriter.WriteAttributeString(Tags.TYPE,iniSetting.TypeName);
                //Make sure the tag and description are not null and have a value.
                if (iniSetting.Tag != null)
                {
                    if (iniSetting.Tag.Length > 0)
                        xWriter.WriteAttributeString(Tags.TAG, iniSetting.Tag);
                }

                if (iniSetting.Description != null)
                {
                    if (iniSetting.Description.Length > 0)
                        xWriter.WriteAttributeString(Tags.DESCRIPTION, iniSetting.Description);
                }

				switch(iniSetting.TypeName)
				{
						//If this value is a collection of settings then call this function again.
					case Types.INI_SETTINGS:
						WriteXMLSection(iniSetting,ref xWriter);
						break;

						//If this value is a section then call this function again.
					case Types.INI_SECTION:
						xWriter.WriteStartElement(Tags.SECTION);
						xWriter.WriteAttributeString(Tags.NAME,((IniSection)iniSetting).Name);
						WriteXMLSection(((IniSection)iniSetting).Settings,ref xWriter);//Pass the settings down
						xWriter.WriteEndElement();//End of the section
						break;
					case Types.ARRAYLIST:
						WriteArrayListItems(iniSetting,ref xWriter);
						break;
					case Types.COLOR:
						Color clr = (Color)iniSetting;
						xWriter.WriteElementString(Tags.VALUE,clr.ToArgb().ToString());
						break;
                    //case Types.FONT:
                    //    xWriter.WriteElementString(Tags.VALUE,FontToString(iniSetting));
                    //    break;
                    //case Types.SIZE:
                    //    xWriter.WriteElementString(Tags.VALUE,SizeToString(iniSetting));
                    //    break;
                    //case Types.POINT:
                    //    xWriter.WriteElementString(Tags.VALUE,PointToString(iniSetting));
                    //    break;
					case Types.DATETIME:
						xWriter.WriteElementString(Tags.VALUE,((DateTime)iniSetting).ToString(DateTimeFormatInfo.InvariantInfo));
						break;
					case Types.DECIMAL:
						xWriter.WriteElementString(Tags.VALUE,((decimal)iniSetting).ToString(NumberFormatInfo.InvariantInfo));
						break;
					case Types.DOUBLE:
						xWriter.WriteElementString(Tags.VALUE,((double)iniSetting).ToString(NumberFormatInfo.InvariantInfo));
						break;
					case Types.SINGLE:
						xWriter.WriteElementString(Tags.VALUE,((Single)iniSetting).ToString(NumberFormatInfo.InvariantInfo));
						break;
					case Types.KEYS:
						xWriter.WriteElementString(Tags.VALUE, ((Keys)iniSetting).ToString());
						break;
					default:
						if(iniSetting!=null)
							xWriter.WriteElementString(Tags.VALUE,iniSetting);
						break;
 				}
				xWriter.WriteEndElement();//End of the settings
			}
		}
		
		/// <summary>Writes array list to xml.</summary>
		private void WriteArrayListItems(ArrayList arrLst,ref XmlTextWriter xWriter)
		{
			ArrayList arr = arrLst;
			int ct = arr.Count;
			if(ct==0)return;
			xWriter.WriteStartElement(Tags.ARRAYLIST);
			xWriter.WriteAttributeString(Tags.TYPE,arr[0].GetType().Name);
			
			for (int r = 0;r<=ct-1;r++)
			{
				xWriter.WriteElementString(Tags.ITEM,arr[r].ToString());
			}
			xWriter.WriteEndElement();
		}

		
		/// <summary>Reads data into objects from xml.</summary>
		private bool ReadXMLSettings(string pathAndFileName)
		{
			bool ret=true;
			XmlTextReader xReader = new XmlTextReader(pathAndFileName);
			mLastXmlError="";
			mXmlComments="";
			try
			{
				//Load the the document.
				xReader.WhitespaceHandling = WhitespaceHandling.None;
				xReader.NameTable.Add(Tags.SECTION);
				xReader.NameTable.Add(Tags.SECTIONS);
				xReader.NameTable.Add(Tags.SETTING);
				xReader.NameTable.Add(Tags.SETTINGS);
				xReader.NameTable.Add(Tags.NAME);
				xReader.NameTable.Add(Tags.TYPE);
				xReader.NameTable.Add(Tags.VALUE);
				xReader.NameTable.Add(Tags.ARRAYLIST);
				xReader.NameTable.Add(Tags.ITEM);
				xReader.NameTable.Add(Tags.SETTINGS);
				while (xReader.Read()) 
				{
					if(xReader.NodeType==XmlNodeType.Comment)
					{
						//We might check the version here
						mXmlComments += xReader.Value + "\n";
					}

					if(xReader.NodeType==XmlNodeType.Element)
					{
						if(xReader.Name == Tags.SECTIONS)
						{
							//This is the top section. Sections will follow 
							ReadEachSectionInSectons(null,ref xReader);
						}
					}
				}
			}
			catch(Exception e)
			{
				mLastXmlError +="Cannot read settings\n. " + e.Message + "\n" + "Line " + xReader.LineNumber + "\n";
				ret=false;
			}
			finally
			{
				//Close
				if(xReader!=null)
					xReader.Close();
			}
			return ret;
		}
		
		/// <summary>
		/// Creates each section object from xml.
		///</summary>
		private void ReadEachSectionInSectons(IniSetting parentSetting,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			nodeDepth = xReader.Depth;
			//We must be at the top sections node
			while (xReader.Read())
			{
				if(nodeDepth == xReader.Depth) return;
				if (xReader.NodeType == XmlNodeType.Element && xReader.Name == Tags.SECTION)
				{
					string SecName = xReader.GetAttribute(Tags.NAME);//"Name"
					if(SecName.Length==0)return;//Get out. No name.
					IniSection thisSec;
					if(parentSetting==null)//No parent. Must be a top section
					{
						thisSec = Sections.Add(SecName);
					}
					else
					{
						//Create a section
						thisSec = Sections.Add(SecName);
						parentSetting.Value = thisSec;
					}
					//If this is an empty node we need to not read any more
					if(!xReader.IsEmptyElement)
					{
						ReadEachSettingInSection(thisSec,ref xReader);//Each section
					}
				}
			}
		}

		/// <summary>
		/// Creates each sub-setting object from xml.
		///</summary>
		private void ReadEachSettingInSubSetting(IniSetting parentSetting,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			//Create a collection of sub-settings
			IniSettings subSettings = new IniSettings(this);
			//Assign it to the parentSetting
			parentSetting.Value = subSettings;

			//We must be at the beginning of a section of settings
			nodeDepth = xReader.Depth;
			while (xReader.Read())
			{
				//If after reading the settings we have hit the end then get out.
				if(nodeDepth == xReader.Depth) return;
				if (xReader.NodeType==XmlNodeType.Element && xReader.Name == Tags.SETTING)
				{
					ReadSettingAndGetType(subSettings,ref xReader);
				}
			}
		}

		/// <summary>
		/// Reads all the arraylist items
		///</summary>
		private void ReadEachArrayListItem(IniSetting parentSetting,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			string subType;
			ArrayList alst = new ArrayList();
			//Move to ArrayList node
			while (xReader.Read())
			{
				if (xReader.NodeType==XmlNodeType.Element && xReader.Name == Tags.ARRAYLIST)
				{
					//We must be at the beginning of a section of settings
					nodeDepth = xReader.Depth;
					subType = xReader.GetAttribute(Tags.TYPE);
					xReader.Read();//Move into the items
					do
					{
						if (xReader.NodeType==XmlNodeType.Element)
						{
							if (xReader.Name == Tags.ITEM)
							{
								alst.Add(ConvertFromXmlToType( xReader.ReadElementString(),subType));
							}
						}
					}
					while (nodeDepth != xReader.Depth);
				}
				parentSetting.Value = alst;
				return;
			}
		}

		/// <summary>
		/// Creates each sub-setting object that has a setting parent.
		///</summary>
		private void ReadEachSubSettingInSettings(IniSettings parentSettings,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			//We must be at the beginning of a section of settings
			nodeDepth = xReader.Depth;
			while (xReader.Read())
			{
				if(nodeDepth == xReader.Depth) return;
				if (xReader.NodeType==XmlNodeType.Element && xReader.Name == Tags.SETTING)
				{
					ReadSettingAndGetType(parentSettings,ref xReader);//In this section
				}
			}
		}

		/// <summary>
		/// Creates each setting object from xml.
		///</summary>
		private void ReadEachSettingInSection(IniSection curSection,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			//We must be at the beginning of a section of settings
			nodeDepth = xReader.Depth;
			while (xReader.Read())
			{
				if(nodeDepth == xReader.Depth) return;
				if (xReader.NodeType==XmlNodeType.Element && xReader.Name == Tags.SETTING)
				{
					ReadSettingAndGetType(curSection.Settings,ref xReader);//In this section
				}
			}
		}

		/// <summary>
		/// Reads all the possible data in a setting.
		/// This is the smallest unit of data.
		///</summary>
		private void ReadAllSettingData(IniSetting curSetting,string sType,ref XmlTextReader xReader)
		{
			int nodeDepth=0;
			//We must be at the beginning of a section of settings
			nodeDepth = xReader.Depth;
			while (xReader.Read())
			{
				if (xReader.NodeType==XmlNodeType.Element)
				{
					if (xReader.Name == Tags.VALUE)
					{
						curSetting.Value = ConvertFromXmlToType( xReader.ReadElementString(),sType);
						if(xReader.NodeType!=XmlNodeType.Element)xReader.MoveToContent();
						if(nodeDepth == xReader.Depth) return;
					}
					if (xReader.Name == Tags.TAG)
					{
						curSetting.Tag = xReader.ReadElementString();
						if(xReader.NodeType!=XmlNodeType.Element)xReader.MoveToContent();
						if(nodeDepth == xReader.Depth) return;
					}
					if (xReader.Name == Tags.DESCRIPTION)
					{
						curSetting.Description = xReader.ReadElementString();
						if(xReader.NodeType!=XmlNodeType.Element)xReader.MoveToContent();
						if(nodeDepth == xReader.Depth) return;
					}
				}
			}
		}
		
		/// <summary>
		/// Create setting and value from xml.
		/// Cuold be a section or more settings.
		/// </summary>
		private void ReadSettingAndGetType(IniSettings collectionOfSettings,ref XmlTextReader xReader)
		{
			string settingName = "";
			string sType;
			IniSetting iniSetting = null;

			//Add a	new	item
			settingName = xReader.GetAttribute(Tags.NAME);
			sType = xReader.GetAttribute(Tags.TYPE);
			iniSetting = collectionOfSettings.Add(settingName);

			switch(sType)
			{
					//If this value is a collection of settings then call this function again
					//after moving to the node.
				case Types.INI_SETTINGS:
					ReadEachSettingInSubSetting(iniSetting,ref xReader);
					break;

					//If this value is a collection of settings then call this function again.
				case Types.INI_SECTION:
					//Create a section
					ReadEachSectionInSectons(iniSetting,ref xReader);
					break;

				case Types.ARRAYLIST:
					ReadEachArrayListItem(iniSetting,ref xReader);
					break;

				default:
					ReadAllSettingData(iniSetting,sType,ref xReader);
					break;
			}
			
//			if(iniSetting!=null)
//			{
//				collectionOfSettings.Remove(settingName);
//			}
		}


		/// <summary>
		/// Convert from an XML string to the proper type of object
		/// </summary>
		private object ConvertFromXmlToType(string valueFromXML,string typeFromXML)
		{
			switch(typeFromXML)
			{
				case Types.BOOLEAN:
					return Convert.ToBoolean(valueFromXML);
				case Types.BYTE:
					return Convert.ToByte(valueFromXML);
				case Types.CHAR:
					return Convert.ToChar(valueFromXML);
				
					//Localized settings
				case Types.DATETIME:
					return Convert.ToDateTime(valueFromXML,DateTimeFormatInfo.InvariantInfo);
				case Types.DECIMAL:
					return Convert.ToDecimal(valueFromXML, NumberFormatInfo.InvariantInfo);
				case Types.DOUBLE:
					return Convert.ToDouble(valueFromXML, NumberFormatInfo.InvariantInfo);
				case Types.SINGLE:
					return Convert.ToSingle(valueFromXML, NumberFormatInfo.InvariantInfo);
				case Types.KEYS:
					return (Keys)mKeyConv.ConvertFromString(valueFromXML);
				case Types.INT16:
					return Convert.ToInt16(valueFromXML);
				case Types.INT32:
					return Convert.ToInt32(valueFromXML);
				case Types.INT64:
					return Convert.ToInt64(valueFromXML);
				case Types.UINT16:
					return Convert.ToUInt16(valueFromXML);
				case Types.UINT32:
					return Convert.ToUInt32(valueFromXML);
				case Types.UINT64:
					return Convert.ToUInt64(valueFromXML);
                //case Types.FONT:
                //    return StringToFont(valueFromXML);
                //case Types.SIZE:
                //    return StringToSize(valueFromXML);
                //case Types.POINT:
                //    return StringToPoint(valueFromXML);
				case Types.COLOR:
					return Color.FromArgb(Convert.ToInt32(valueFromXML));
				default://Default to the string value
					return valueFromXML;
			}
		}

		#region Helpers

        //private string FontToString(Font f)
        //{
        //    StringBuilder sbl = new StringBuilder(128);
        //    sbl.Append(f.FontFamily.Name);
        //    sbl.Append("|");
        //    sbl.Append(f.Size);
        //    sbl.Append("|");
        //    sbl.Append(f.Style);
        //    sbl.Append("|");
        //    sbl.Append(f.Unit);
        //    sbl.Append("|");
        //    sbl.Append(f.GdiCharSet);
        //    sbl.Append("|");
        //    sbl.Append(f.GdiVerticalFont);
        //    return sbl.ToString();
        //}

        //private Font StringToFont(string s)
        //{
			
        //    try{
        //        char sep = '|';
        //        string[] sp = s.Split(sep);
        //        return new Font(sp[0],
        //            float.Parse(sp[1]),
        //            (FontStyle)FontStyle.Parse(typeof(FontStyle),sp[2],true),
        //            (GraphicsUnit)GraphicsUnit.Parse(typeof(GraphicsUnit),sp[3],true),
        //            Convert.ToByte(sp[4]),
        //            Convert.ToBoolean(sp[5]));
        //    }
        //    catch{
        //        return null;
        //    }

        //}
		
        //private string PointToString(Point sz)
        //{
        //    return sz.X + "," + sz.Y;
        //}
		
        //private Point StringToPoint(string st)
        //{
        //    char sep = ',';
        //    return new Point(Int32.Parse(st.Split(sep)[0]),Int32.Parse(st.Split(sep)[1]));
        //}
		
        //private string SizeToString(Size sz)
        //{
        //    return sz.Width + "," + sz.Height;
        //}
		
        //private Size StringToSize(string st)
        //{
        //    char sep = ',';
        //    return new Size(Int32.Parse(st.Split(sep)[0]),Int32.Parse(st.Split(sep)[1]));
        //}		

		private string GetDllPath()
		{
			string workingpath="";
			try
			{
				workingpath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
				if(workingpath.IndexOf("ile:\\")>=0)
				{
					return workingpath.Substring(6)+"\\";
				}
				return workingpath+"\\";
			}
			catch{throw;}

		}

		private string EnsurePathIsIncluded(string fileName)
		{
			string ret="";
			try
			{
				if(fileName.IndexOf("\\")>=0)//A path was specified
				{
					if(Directory.Exists(fileName.Substring(0,fileName.LastIndexOf("\\")))) 
					{
						//Make sure we have an exension.
						if(!ret.EndsWith(".xml"))
						{
							ret += ".xml";
						}
						ret = fileName;
					}
					else
					{
						throw new ApplicationException("Folder does not exist.");
					}
				}
				else//No path specified.
				{
					
					if(mPathToXmlFile != "")
					{
						ret = mPathToXmlFile+fileName;
					}
					else
					{
						ret = GetDllPath() + fileName;
					}
					//Make sure we have an exension.
					if(!ret.EndsWith(".xml"))
					{
						ret += ".xml";
					}

				}
				return ret;
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}