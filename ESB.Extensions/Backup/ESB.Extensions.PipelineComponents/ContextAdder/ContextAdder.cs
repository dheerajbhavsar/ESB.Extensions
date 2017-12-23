using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;

namespace ESB.Extensions.PipelineConponents.ContextAdder
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("4A2532B0-8AC0-47d0-94FD-598D0C415D66")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class ContextAdder : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("ESB.Extensions.PipelineConponents.ContextAdder.ContextAdder", Assembly.GetExecutingAssembly());

        #region IBaseComponent members
        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Version of the component
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        #endregion

        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classid)
        {
            classid = new System.Guid("4A2532B0-8AC0-47d0-94FD-598D0C415D66");
        }

        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            try
            {
                object val = ReadPropertyBag(pb, "ContextPropertyInfoCollection");
                if (val != null)
                {
                    string corrPropertiesList = (string)val;

                    XmlTextReader xml = new XmlTextReader(new StringReader(corrPropertiesList));
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreProcessingInstructions = true;
                    XmlReader reader = XmlReader.Create(xml, settings);

                    XmlSerializer ser = new XmlSerializer(typeof(ContextPropertyInfoCollection));
                    this.ContextProperties = (ContextPropertyInfoCollection)ser.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to load configuration properties", ex);
            }
        }

        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            try
            {
                XmlWriterSettings setting = new XmlWriterSettings();
                setting.OmitXmlDeclaration = true;

                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (XmlWriter writer = XmlWriter.Create(sw, setting))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(ContextPropertyInfoCollection));
                        ser.Serialize(writer, this.ContextProperties);
                    }
                }

                object val = sb.ToString();
                pb.Write("ContextPropertyInfoCollection", ref val);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save configuration properties", ex);
            }
        }

        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (System.ArgumentException)
            {
                return val;
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException("Can't read design time Properties", e);
            }
            return val;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException("Can't write design time properties", e);
            }
        }
        #endregion
        #endregion

        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return ((System.Drawing.Bitmap)(this.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture))).GetHicon();
            }
        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion

        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(IPipelineContext pc, IBaseMessage inmsg)
        {
            foreach (ContextPropertyInfo prop in this.ContextProperties)
            {
                if (prop.Promoted)
                {
                    inmsg.Context.Promote(prop.PropertyName, prop.PropertyNamespace, prop.PropertyValue);
                }
                else
                {
                    inmsg.Context.Write(prop.PropertyName, prop.PropertyNamespace, prop.PropertyValue);
                }
            }

            return inmsg;
        }
        #endregion

        #region Design Time Properties
        private ContextPropertyInfoCollection _propInfoCol = new ContextPropertyInfoCollection();
        public ContextPropertyInfoCollection ContextProperties
        {
            get { return _propInfoCol; }
            set { _propInfoCol = value; }
        }
        #endregion
    }
}
