using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Component.Interop;
using System.Reflection;
using ESB.Extensions.Resolution;

namespace ESB.Extensions.PipelineComponents.ResolutionExecutor
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("18155027-B1C4-4403-BAA1-6ED9F694D6B2")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class ResolutionExecutor : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        #region IComponent Members

        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            ResolutionManager.Resolve(this.ResolutionConnectionString, pInMsg, pContext);
            return pInMsg;
        }

        #endregion

        #region IBaseComponent Members

        public string Description
        {
            get
            {
                return "Executes an ESB.Extensions resolution.";
            }
        }

        public string Name
        {
            get
            {
                return "ResolutionExecutor";
            }
        }

        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        #endregion

        #region IPersistPropertyBag Members

        public void GetClassID(out Guid classID)
        {
            classID = new System.Guid("18155027-B1C4-4403-BAA1-6ED9F694D6B2");
        }

        public void InitNew()
        {
        }

        public void Load(IPropertyBag propertyBag, int errorLog)
        {
            object obj2 = ReadPropertyBag(propertyBag, "ResolutionConnectionString");
            if (obj2 != null)
            {
                this.ResolutionConnectionString = (string)obj2;
            }
        }

        public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            this.WritePropertyBag(propertyBag, "ResolutionConnectionString", this.ResolutionConnectionString);
        }

        #endregion

        #region IComponentUI Members

        public IntPtr Icon
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerator Validate(object projectSystem)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Configuration Properties
        public string ResolutionConnectionString
        {
            get;
            set;
        }
        #endregion

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
    }
}
