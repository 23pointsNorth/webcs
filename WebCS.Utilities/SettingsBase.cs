using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using Marker;
using System.IO;

namespace WebCS.Utilities
{
    public abstract class SettingsBase
    {
        public string SettingsPath { get; set; }
        public string EncryptionKey { get; set; }

        public SettingsBase()
        {
            // These properties must be set by derived class
            SettingsPath = null;
            EncryptionKey = null;
        }

        /// <summary>
        /// Loads user settings from the specified file. The file should
        /// have been created using this class' Save method.
        /// 
        /// You must implement ReadSettings for any data to be read.
        /// </summary>
        public void Load()
        {
            UserSettingsReader reader = new UserSettingsReader(EncryptionKey);
            reader.Load(SettingsPath);
            ReadSettings(reader);
        }

        /// <summary>
        /// Saves the current settings to the specified file.
        /// 
        /// You must implement WriteSettings for any data to be written.
        /// </summary>
        public void Save()
        {
            UserSettingsWriter writer = new UserSettingsWriter(EncryptionKey);
            WriteSettings(writer);
            writer.Save(SettingsPath);
        }

        // Abstract methods
        public abstract void ReadSettings(UserSettingsReader reader);
        public abstract void WriteSettings(UserSettingsWriter writer);
    }

    public class UserSettingsWriter
    {
        protected XmlDocument _doc = null;
        protected string _encryptionKey;

        public UserSettingsWriter(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
            _doc = new XmlDocument();

            // Initialize settings document
            _doc.AppendChild(_doc.CreateNode(XmlNodeType.XmlDeclaration, null, null));
            _doc.AppendChild(_doc.CreateElement("Settings"));
        }

        /// <summary>
        /// Saves the current data to the specified file
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            _doc.Save(filename);
        }

        /// <summary>
        /// Writes a string value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, string value)
        {
            WriteNodeValue(key, value != null ? value : String.Empty);
        }

        /// <summary>
        /// Writes an integer value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, int value)
        {
            WriteNodeValue(key, value);
        }

        /// <summary>
        /// Writes a floating-point value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, double value)
        {
            WriteNodeValue(key, value);
        }

        /// <summary>
        /// Writes a Boolean value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, bool value)
        {
            WriteNodeValue(key, value);
        }

        /// <summary>
        /// Writes a DateTime value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, DateTime value)
        {
            WriteNodeValue(key, value);
        }

        /// <summary>
        /// Writes a ColorMarker
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Write(string key, ColorMarker value)
        {
            WriteNodeValue(key, value);
        }

        /// <summary>
        /// Internal method to write a node to the XML document
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void WriteNodeValue(string key, object value)
        {
            XmlElement elem = _doc.CreateElement(key);
            elem.InnerText = value.ToString();
            _doc.DocumentElement.AppendChild(elem);
        }
    }

    public class UserSettingsReader
    {
        protected XmlDocument _doc = null;
        protected string _encryptionKey;

        public UserSettingsReader(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
            _doc = new XmlDocument();
        }

        /// <summary>
        /// Loads data from the specified file
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename)
        {
            try
            {
                _doc.Load(filename);
            }
            catch (FileNotFoundException)
            {
                _doc = new XmlDocument();
                // Initialize settings document
                _doc.AppendChild(_doc.CreateNode(XmlNodeType.XmlDeclaration, null, null));
                _doc.AppendChild(_doc.CreateElement("Settings"));
                _doc.Save(filename);
            }
        }

        /// <summary>
        /// Reads a string value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string Read(string key, string defaultValue)
        {
            string result = ReadNodeValue(key);
            if (result != null)
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Reads an integer value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int Read(string key, int defaultValue)
        {
            int result;
            string s = ReadNodeValue(key);
            if (int.TryParse(s, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Reads a floating-point value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public double Read(string key, double defaultValue)
        {
            double result;
            string s = ReadNodeValue(key);
            if (double.TryParse(s, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Reads a Boolean value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool Read(string key, bool defaultValue)
        {
            bool result;
            string s = ReadNodeValue(key);
            if (bool.TryParse(s, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Reads a DateTime value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public DateTime Read(string key, DateTime defaultValue)
        {
            DateTime result;
            string s = ReadNodeValue(key);
            if (DateTime.TryParse(s, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Reads a ColorMarker
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public ColorMarker Read(string key, ColorMarker defaultValue)
        {
            ColorMarker result;
            string s = ReadNodeValue(key);
            if (ColorMarker.TryParse(s, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Internal method to read a node from the XML document
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string ReadNodeValue(string key)
        {
            XmlNode node = _doc.DocumentElement.SelectSingleNode(key);
            if (node != null && !String.IsNullOrEmpty(node.InnerText))
                return node.InnerText;
            return null;
        }
    }
}
