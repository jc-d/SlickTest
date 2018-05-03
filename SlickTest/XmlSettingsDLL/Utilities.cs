//using System;
//using System.IO;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;
//using System.Reflection;
//using System.Security.Cryptography;

//namespace XmlSettings
//{
//    /// <summary>
//    /// A utility class designed to work with the XmlSettings class.
//    /// </summary>
//    /// <remarks>
//    /// Contains methods to perform control-specific tasks.
//    /// </remarks>
//    public class XmlAppUtils
//    {

        ///// <summary>
        ///// Maintains the property value automatically.
        ///// </summary>
        ///// <param name="control">The control</param>
        ///// <param name="propertyName">Property name</param>
        ///// <param name="XmlAppSettings">The main AppSettings object.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool AddPropertyToBag(AppSettings XmlAppSettings,Control control,string propertyName)
        //{
        //    try
        //    {
        //        //Create a special section if needed
        //        IniSection parentSection = XmlAppSettings.Sections.Add("PropertyBag");
        //        //Get the full path to the top
        //        string ctrlBagName = BuildControlPathFromRoot(control); 
        //        IniSettings bagSec;
        //        //Add a setting if needed
        //        if(parentSection.Settings[ctrlBagName]==null)
        //        {
        //            bagSec = parentSection.Settings.Add(ctrlBagName,new IniSettings(XmlAppSettings));
        //        }
        //        else
        //        {
        //            bagSec = parentSection.Settings[ctrlBagName];
        //        }
        //        IniSetting setting = bagSec.Add(propertyName);
        //        //Only set the value if the value has not been set
        //        if(setting.Value==null)
        //        {
        //            setting.Value = control.GetType().GetProperty(propertyName).GetValue(control,null);
        //        }
        //        return true;
        //    }
        //    catch{return false;}
        //}

        ///// <summary>
        ///// Return a Control reference with the specified name
        ///// </summary>
        ///// <param name="path">The full path to the control</param>
        ///// <param name="context">The parent control</param>
        //private static Control FindControlInPath(Control context,string path){
        //    int r,ct;
        //    string[] sPath = path.Split(new char[]{'.'});
        //    ct = sPath.Length;
        //    Control curCtl = context;
        //    for(r=1;r<ct;r++)
        //    {
        //        foreach(Control ctl in curCtl.Controls)
        //        {
        //            if(ctl.Name==sPath[r])
        //            {
        //                curCtl = ctl;
        //                break;
        //            }
        //        }
        //    }
        //    return curCtl;
        //}

        ///// <summary>
        ///// Return a string containing the full path to the control
        ///// </summary>
        ///// <param name="control">The control</param>
        //private static string BuildControlPathFromRoot(Control control)
        //{
        //    StringBuilder sb = new StringBuilder(128);
        //    sb.Insert(0,control.Name);
        //    Control curCtl = control;
        //    while(curCtl.Parent!=null)
        //    {
        //        curCtl = curCtl.Parent;
        //        sb.Insert(0,".");
        //        sb.Insert(0,curCtl.Name);
        //    }
        //    return sb.ToString();
        //}
		
        ///// <summary>
        ///// Loads properties for ANY persisted control properties on this parent control.
        ///// </summary>
        ///// <param name="context">The parent control.Usually a form.</param>
        ///// <param name="XmlAppSettings">The main AppSettings object.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool SetAllPropertiesFromBag(AppSettings XmlAppSettings,Control context)
        //{
        //    try
        //    {
        //        //Get the PropertyBag section
        //        IniSection parentSection = XmlAppSettings.Sections["PropertyBag"];
        //        //The number of dots in the name indicates the level
        //        string ctrlBagName = BuildControlPathFromRoot(context);
        //        //Find the settings that are for this control and any of it't children.
        //        foreach(IniSetting setting in parentSection.Settings)
        //        {
        //            if(setting.TypeName!=Types.INI_SETTINGS){continue;}
        //            Control curCtl = FindControlInPath(context,setting.Name);
        //            Type tp = curCtl.GetType();
        //            IniSettings bagSettings = setting;
        //            foreach(IniSetting prop in bagSettings)
        //            {
        //                PropertyInfo p = tp.GetProperty(prop.Name);
        //                if(p.CanWrite)
        //                {
        //                    if(prop.Value!=null)
        //                        p.SetValue(curCtl,prop.Value,null);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch{return false;}
        //}


        ///// <summary>
        ///// Saves all persisted properties for this control.
        ///// </summary>
        ///// <param name="context">The parent control.Usually a form.</param>
        ///// <param name="XmlAppSettings">The main AppSettings object.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool SaveAllPropertiesInBag(AppSettings XmlAppSettings,Control context)
        //{
        //    try
        //    {
        //        //Get the PropertyBag section
        //        IniSection parentSection = XmlAppSettings.Sections["PropertyBag"];
        //        //The number of dots in the name indicates the level
        //        string ctrlBagName = BuildControlPathFromRoot(context);
        //        //Find the settings that are for this control and any of it't children.
        //        foreach(IniSetting setting in parentSection.Settings)
        //        {
        //            if(setting.TypeName!=Types.INI_SETTINGS){continue;}
        //            Control curCtl = FindControlInPath(context,setting.Name);
        //            Type tp = curCtl.GetType();
        //            IniSettings bagSettings = setting;
        //            foreach(IniSetting prop in bagSettings)
        //            {
        //                PropertyInfo p = tp.GetProperty(prop.Name);
        //                if(p.CanRead)
        //                {
        //                    prop.Value = p.GetValue(curCtl,null);
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch{return false;}		
        //}


        //#region  SaveItemsAndValues
        ///// <summary>
        ///// Saves the contents and selection status of a control.
        ///// </summary>
        ///// <param name="cbo">The ComboBox control source</param>
        ///// <param name="parentSection">The parent IniSection.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool SaveItemsAndValues(ComboBox cbo,IniSection parentSection)
        //{
        //    int r,ct;
        //    try
        //    {
        //        string settingName = cbo.Parent.Name + "." + cbo.Name;
        //        IniSettings settings = parentSection.Settings;
				
        //        //Add a new setting that will be a sub-section
        //        //One item in the section will be a setting and one will be a 
        //        //collection of settings.
        //        IniSettings cboData;//Two items. A list and selectedItem
        //        IniSettings cboItems;//All the items in the combo
        //        if(settings[settingName]==null)
        //        {
        //            cboData = new IniSettings(parentSection.TopObject);
        //            cboItems = new IniSettings(parentSection.TopObject);
        //        }
        //        else
        //        {
        //            cboData = settings[settingName];
        //            if(settings[settingName]["Items"]==null)
        //            {
        //                cboItems = new IniSettings(parentSection.TopObject);
        //            }
        //            else
        //            {
        //                cboItems = settings[settingName]["Items"];
        //            }
        //            cboData.Clear();
        //            cboItems.Clear();
        //        }
				
				
        //        ct = cbo.Items.Count;
        //        for(r=0;r<ct;r++)
        //        {
        //            cboItems.Add(r.ToString(),cbo.Items[r]);
        //        }
        //        cboData.Add("Items",cboItems);
        //        cboData.Add("SelectedIndex",cbo.SelectedIndex);
        //        settings.Add(settingName,cboData);

        //        return true;
        //    }
        //    catch{return false;}
        //}


        ///// <summary>
        ///// Saves the contents and selection status of a control.
        ///// </summary>
        ///// <param name="lb">The ListBox control source</param>
        ///// <param name="parentSection">The parent IniSection.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool SaveItemsAndValues(ListBox lb,IniSection parentSection)
        //{
        //    int r,ct;
        //    try
        //    {
        //        string settingName = lb.Parent.Name + "." + lb.Name;
        //        IniSettings settings = parentSection.Settings;
        //        //Add a new setting that will be a sub-section
        //        IniSettings lst;
        //        if(settings[settingName]==null)
        //        {
        //            lst = new IniSettings(settings.TopObject);
        //            settings.Add(settingName,lst);
        //        }
        //        else
        //        {
        //            lst = settings[settingName];
        //            lst.Clear();
        //        }

        //        ct = lb.Items.Count;
        //        for(r=0;r<ct;r++)
        //        {
        //            lst.Add(lb.Items[r].ToString(),lb.GetSelected(r));
        //        }
        //        return true;
        //    }
        //    catch{return false;}
        //}

        ///// <summary>
        ///// Saves the contents and checked status of a control
        ///// </summary>
        ///// <param name="tv">The TreeView control source</param>
        ///// <param name="parentSection">The parent IniSection.</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool SaveItemsAndValues(TreeView tv,IniSection parentSection)
        //{
        //    try
        //    {
        //        string settingName = tv.Parent.Name + "." + tv.Name;
        //        IniSettings settings = parentSection.Settings;
        //        //Add a new setting that will be a sub-section
        //        IniSettings lst;
        //        if(settings[settingName]==null)
        //        {
        //            lst = new IniSettings(parentSection.TopObject);
        //            settings.Add(settingName,lst);
        //        }
        //        else
        //        {
        //            lst = settings[settingName];
        //            lst.Clear();
        //        }

        //        AddSettingFromNode(tv.Nodes,ref lst);
        //        return true;
        //    }
        //    catch{return false;}
        //}
        //#endregion  SaveItemsAndValues

        //#region  LoadItemsAndValues
        ///// <summary>
        ///// Loads the contents and selection status of a control
        ///// </summary>
        ///// <param name="cbo">The ComboBox control destination</param>
        ///// <param name="parentSection">The IniSection source</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool LoadItemsAndValues(ComboBox cbo,IniSection parentSection)
        //{
        //    int r,ct,ret;
        //    try
        //    {
        //        string  settingName = cbo.Parent.Name + "." + cbo.Name;
        //        IniSettings cboData = parentSection.Settings[settingName];
        //        IniSettings cboItems = cboData["Items"];
        //        ct = cboItems.Count;
        //        cbo.BeginUpdate();
        //        cbo.Items.Clear();
        //        for(r=0;r<ct;r++)
        //        {
        //            ret = cbo.Items.Add(cboItems[r].Value);
        //        }
        //        int idx = cboData["SelectedIndex"];
        //        if(idx<ct && idx >-2) cbo.SelectedIndex = idx; 
        //        cbo.EndUpdate();
        //        return true;
        //    }
        //    catch{return false;}
        //}
		
        ///// <summary>
        ///// Loads the contents and selection status of a control
        ///// </summary>
        ///// <param name="lb">The ListBox control destination</param>
        ///// <param name="parentSection">The IniSection source</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool LoadItemsAndValues(ListBox lb,IniSection parentSection)
        //{
        //    int r,ct,ret;
        //    try
        //    {
        //        string  settingName = lb.Parent.Name + "." + lb.Name;
        //        IniSettings lst = parentSection.Settings[settingName];
        //        ct = lst.Count;
        //        lb.Items.Clear();
        //        lb.BeginUpdate();
        //        for(r=0;r<ct;r++)
        //        {
        //            ret = lb.Items.Add(lst[r].Name);
        //            lb.SetSelected(ret,lst[r]);
        //        }
        //        lb.EndUpdate();
        //        return true;
        //    }
        //    catch{return false;}
        //}

        ///// <summary>
        ///// Loads the contents and checked status of a control
        ///// </summary>
        ///// <param name="tv">The TreeView control destination</param>
        ///// <param name="parentSection">The IniSection source</param>
        ///// <returns>True on success, False on failure.</returns>
        //public static bool LoadItemsAndValues(TreeView tv,IniSection parentSection)
        //{
        //    try
        //    {
        //        string settingName = tv.Parent.Name + "." + tv.Name;
        //        IniSettings lst = parentSection.Settings[settingName];
        //        tv.Nodes.Clear();
        //        tv.BeginUpdate();
        //        GetNodeFromSetting(tv.Nodes,lst);
        //        tv.EndUpdate();
        //        return true;
        //    }
        //    catch{return false;}
        //}
        //#endregion  LodItemsAndValues

        //private static void AddSettingFromNode(TreeNodeCollection nds,ref IniSettings nodeSettings)
        //{
        //    string tagString;
        //    foreach(TreeNode nd in nds)
        //    {
        //        IniSettings nodeAttributes = new IniSettings(nodeSettings.TopObject);
        //        nodeAttributes.Add("Checked",nd.Checked);
        //        nodeAttributes.Add("IsVisible",nd.IsVisible);
        //        //if(nd.ImageIndex>-1)
        //        {
        //            nodeAttributes.Add("ImageIndex",nd.ImageIndex);
        //        }

        //        if(!nd.ForeColor.IsEmpty)//Color has been set
        //        {
        //            nodeAttributes.Add("ForeColor",nd.ForeColor);
        //        }
        //        else
        //        {
        //            nodeAttributes.Add("ForeColor",nd.TreeView.ForeColor);
        //        }
				
        //        if(nd.NodeFont!=null)
        //        {
        //            nodeAttributes.Add("NodeFont",nd.NodeFont);
        //        }
        //        else
        //        {
        //            nodeAttributes.Add("NodeFont",nd.TreeView.Font);
        //        }

        //        tagString="";
        //        if(nd.Tag!=null)//only if we have a tag
        //        {
        //            tagString = nd.Tag.ToString();
        //        }
				
        //        //If we have children
        //        if (nd.GetNodeCount(false)>0)
        //        {
        //            IniSettings children = nodeAttributes.Add("Children",new IniSettings(nodeSettings.TopObject));
        //            nodeSettings.Add(nd.Text,nodeAttributes,tagString);//this node
        //            AddSettingFromNode(nd.Nodes,ref children);
        //        }
        //        else
        //        {
        //            nodeSettings.Add(nd.Text,nodeAttributes,tagString);//this node
        //        }
        //    }
        //}
		


        //private static void GetNodeFromSetting(TreeNodeCollection nds,IniSettings lst)
        //{
        //    foreach(IniSetting item in lst)
        //    {
        //        //Add the node to the nodes collection.
        //        TreeNode thisNode = nds.Add(item.Name);

        //        //Get all the attributes for that node.
        //        IniSettings nodeAttributes = item;
        //        thisNode.Checked = nodeAttributes["Checked"];
        //        if(nodeAttributes["IsVisible"])
        //            thisNode.EnsureVisible();
        //        if(nodeAttributes["ForeColor"]!=null)
        //            thisNode.ForeColor = nodeAttributes["ForeColor"];
        //        if(nodeAttributes["NodeFont"]!=null)
        //            thisNode.NodeFont = nodeAttributes["NodeFont"];
        //        if(nodeAttributes["ImageIndex"]!=null)
        //            thisNode.ImageIndex = nodeAttributes["ImageIndex"];
				
        //        //If this node has a setting named children
        //        if(nodeAttributes["Children"]!=null)
        //        {
        //            //Make some child nodes
        //            GetNodeFromSetting(thisNode.Nodes,nodeAttributes["Children"]);
        //        }
        //    }
        //}
	
        //#region Encryption
        ///// <summary>
        ///// Performs a one-way encryption on the data and returns the result. Useful
        ///// for protected authentication credentials.
        ///// </summary>
        ///// <param name="str">Data to be encrypted.</param>
        //public static string CalcSHA512(string str)
        //{
        //    SHA512Managed	sha512			= new SHA512Managed();
        //    byte[]			sourceData		= new byte[Encoding.Default.GetByteCount(str)];
        //    return Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(str)));
        //}


        ///// <summary>
        ///// Encrypts a string using the KeyString.
        ///// </summary>
        ///// <remarks>
        /////	The encrypted data may safely be written and read from plain text ASCII files.
        ///// </remarks>
        ///// <param name="str">String to be encrypted.</param>
        ///// <param name="keyString">Key to use when encrypting the data.</param>
        //public static string Encrypt(string str, string keyString )
        //{
        //    byte[] arrBytes = Encoding.Default.GetBytes(str);
        //    SymmetricAlgorithm	encoder = SymmetricAlgorithm.Create("TripleDES");
        //    encoder.Padding	= PaddingMode.Zeros;
        //    encoder.Key =  Encoding.Default.GetBytes(GetLegalKey(keyString, encoder));
        //    encoder.GenerateIV();

        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cryptStream = new CryptoStream(ms, 
        //        encoder.CreateEncryptor(), CryptoStreamMode.Write);

        //    cryptStream.Write(arrBytes, 0, arrBytes.Length);
        //    cryptStream.FlushFinalBlock();
			
        //    string iv = Convert.ToBase64String(encoder.IV);
        //    return iv.Length.ToString("X2") + iv + Convert.ToBase64String(ms.ToArray());
        //}

        ///// <summary>
        ///// Decrypts a string previously encoded with <see cref="Encrypt"/> using 
        ///// the given KeyString.
        ///// </summary>
        ///// <param name="str">
        /////	Data that was previously encrypted with <see cref="Encrypt"/>.
        ///// </param>
        ///// <param name="keyString">
        /////		Key used when the data was encrypted.
        ///// </param>
        //public static string Decrypt(string str, string keyString )
        //{
        //    int					len			= int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        //    byte[]				iv			= Convert.FromBase64String(str.Substring(2, len));
        //    byte[]				arrBytes	= Convert.FromBase64String(str.Substring(2 + len));
        //    MemoryStream		ms			= new MemoryStream();
        //    SymmetricAlgorithm	encoder		= SymmetricAlgorithm.Create("TripleDES");

        //    encoder.Padding	= PaddingMode.Zeros;
        //    encoder.Key		=  Encoding.Default.GetBytes(GetLegalKey(keyString, encoder));
        //    encoder.IV		= iv;

        //    CryptoStream				cryptStream	= new CryptoStream(ms, encoder.CreateDecryptor(), CryptoStreamMode.Write);
        //    cryptStream.Write(arrBytes, 0, arrBytes.Length);
        //    cryptStream.FlushFinalBlock();
			
        //    encoder.Clear();
        //    cryptStream.Clear();

        //    return Encoding.Default.GetString(ms.ToArray());
        //}

		

		
        ///// <summary>
        ///// Generate a key that is known to be legal for the chosen algorithm.
        ///// </summary>
        //private static string GetLegalKey(string key, SymmetricAlgorithm alg)
        //{
        //    string	result	= key;
        //    int		size	= Encoding.Default.GetByteCount(key) * 8;

        //    if (alg.LegalKeySizes.Length > 0)
        //    {
        //        if( size > alg.LegalKeySizes[ 0 ].MaxSize )
        //        {
        //            result = result.Substring( 0, Encoding.Default.GetMaxCharCount( alg.LegalKeySizes[ 0 ].MaxSize / 8 ) );
        //        }
        //        else
        //        {
        //            if( size < alg.LegalKeySizes[ 0 ].MinSize )
        //            {
        //                result = result.PadRight( result.Length + Encoding.Default.GetMaxCharCount( ( alg.LegalKeySizes[ 0 ].MinSize - size ) / 8 ) );
        //            }
        //            else
        //            {
        //                if( alg.LegalKeySizes[ 0 ].SkipSize > 0 )
        //                {
        //                    if( ( size % alg.LegalKeySizes[ 0 ].SkipSize ) != 0 )
        //                    {
        //                        int pad = Encoding.Default.GetMaxCharCount( ( alg.LegalKeySizes[ 0 ].MaxSize - size ) / 8 );
        //                        result = result.PadRight( result.Length + pad, 'X' );
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}	
        //#endregion
	
//    }
//}
