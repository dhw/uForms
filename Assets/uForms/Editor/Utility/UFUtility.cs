using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;

namespace uForms
{
    class UFUtility
    {
        public static T LoadAssetFromGUID<T>(string guid) where T : UnityEngine.Object
        {
            if(string.IsNullOrEmpty(guid)) { return null; }
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if(string.IsNullOrEmpty(path)) { return null; }
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        /// <summary>Import the xml.</summary>
        /// <typeparam name="T">type of the import object</typeparam>
        /// <param name="xmlPath">import xml path</param>
        /// <param name="encoding">encoding of the xml</param>
        /// <returns>if any errors, return default(T)</returns>
        public static T ImportXml<T>(string xmlPath, Encoding encoding = null, XmlAttributeOverrides attrOverride = null)
        {
            if(encoding == null) { encoding = new UTF8Encoding(true); }

            try
            {
                using(StreamReader sr = new StreamReader(xmlPath, encoding))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T), attrOverride);
                    return (T)xs.Deserialize(sr);
                }
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>Export the specified object as xml file.</summary>
        /// <typeparam name="T">type of the export object</typeparam>
        /// <param name="xmlPath">export xml path</param>
        /// <param name="exportData">export object</param>
        /// <param name="encoding">encoding of the xml</param>
        public static void ExportXml<T>(string xmlPath, T exportData, Encoding encoding = null, XmlAttributeOverrides attrOverride = null)
        {
            if(encoding == null) { encoding = new UTF8Encoding(true); }

            try
            {
                CreateRequiredDirectory(xmlPath);

                using(StreamWriter sw = new StreamWriter(xmlPath, false, encoding))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T), attrOverride);

                    // remove default namespace
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add(string.Empty, string.Empty);

                    xs.Serialize(sw, exportData, ns);
                }
            }
            catch
            {
            }
        }

        /// <summary>Create the directory that is required by the specified file path.</summary>
        public static void CreateRequiredDirectory(string filePath)
        {
            if(!string.IsNullOrEmpty(filePath))
            {
                string directory = Path.GetDirectoryName(filePath);
                if(!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }
    }
}
