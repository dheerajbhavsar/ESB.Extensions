using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Component.Interop;
using System.Reflection;
using ESB.Extensions.Resolution;
using System.Collections;
using Microsoft.BizTalk.Message.Interop;
using ESB.Extensions.Resolutions;
using System.Xml.Serialization;
using Microsoft.BizTalk.Component;
using System.ComponentModel;
using Microsoft.BizTalk.Component.Utilities;

namespace ESB.Extensions.PipelineComponents.ResolutionExecutor
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("A28395A4-FF01-45c4-BF61-9EF82CD273D9")]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    public class GoPublisherDisassembler : XmlDasmComp, Microsoft.BizTalk.Component.Interop.IDisassemblerComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        #region IDisassemblerComponent Members

        private readonly Queue<IBaseMessage> _msgQueue = new Queue<IBaseMessage>();

        public new void Disassemble(IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            base.Disassemble(pContext, pInMsg);

            if (this.ResolutionBeforeDisassembling)
            {
                this.PublishGoMessage(pContext, pInMsg);
            }
        }

        public new Microsoft.BizTalk.Message.Interop.IBaseMessage GetNext(IPipelineContext pContext)
        {
            IBaseMessage pMsg = base.GetNext(pContext);
            if (this.ResolutionBeforeDisassembling)
            {
                return pMsg;
            }

            if (null != pMsg)
            {
                this.PublishGoMessage(pContext, pMsg);
                return pMsg;
            }

            if (_msgQueue.Count > 0)
            {
                return _msgQueue.Dequeue();
            }

            return null;
        }

        private void PublishGoMessage(IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pMsg)
        {
            ResolutionDictionary rd = ResolutionManager.Resolve(this.ResolutionConnectionString, pMsg, pContext);
            if (rd.ContainsKey("ESB.Extensions.Resolutions.Go"))
            {
                Go go = rd.GetValue("ESB.Extensions.Resolutions.Go") as Go;
                if (null != go)
                {
                    IBaseMessageFactory mf = pContext.GetMessageFactory();
                    IBaseMessage goMsg = mf.CreateMessage();
                    goMsg.AddPart("Body", mf.CreateMessagePart(), true);
                    goMsg.BodyPart.Data = go.CreateStreamFromInstance();
                    goMsg.Context.Promote("BatchId", "https://ESB.Extensions.Schemas.Properties", go.BatchId);
                    goMsg.Context.Promote("SequenceId", "https://ESB.Extensions.Schemas.Properties", go.BatchId);
                    _msgQueue.Enqueue(goMsg);
                }
            }
        }

        #endregion

        #region IBaseComponent Members

        public new string Description
        {
            get
            {
                return "Publishes a Go message if the resolution provider indicates so.";
            }
        }

        public new string Name
        {
            get
            {
                return "GoPublisherDisassembler";
            }
        }

        public new string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        #endregion

        #region IPersistPropertyBag Members

        public new void GetClassID(out Guid classID)
        {
            classID = new System.Guid("A28395A4-FF01-45c4-BF61-9EF82CD273D9");
        }

        public new void InitNew()
        {
        }

        public new void Load(IPropertyBag propertyBag, int errorLog)
        {
            object obj2 = ReadPropertyBag(propertyBag, "ResolutionConnectionString");
            if (obj2 != null)
            {
                this.ResolutionConnectionString = (string)obj2;
            }
            base.Load(propertyBag, errorLog);
        }

        public new void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            this.WritePropertyBag(propertyBag, "ResolutionConnectionString", this.ResolutionConnectionString);
            base.Save(propertyBag, clearDirty, saveAllProperties);
        }

        #endregion

        #region IComponentUI Members

        public new IntPtr Icon
        {
            get { throw new NotImplementedException(); }
        }

        public new System.Collections.IEnumerator Validate(object projectSystem)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Configuration Properties
        public bool ResolutionBeforeDisassembling
        {
            get;
            set;
        }

        public string ResolutionConnectionString
        {
            get;
            set;
        }
        // Properties
        [Browsable(true), Description("")]
        public new bool AllowUnrecognizedMessage
        {
            get
            {
                return base.AllowUnrecognizedMessage;
            }
            set
            {
                base.AllowUnrecognizedMessage = value;
            }
        }

        [Browsable(true), Description("")]
        public new SchemaList DocumentSpecNames
        {
            get
            {
                return base.DocumentSpecNames;
            }
            set
            {
                base.DocumentSpecNames = value;
            }
        }

        [Browsable(true), Description("")]
        public new SchemaList EnvelopeSpecNames
        {
            get
            {
                return base.EnvelopeSpecNames;
            }
            set
            {
                base.EnvelopeSpecNames = value;
            }
        }

        [Browsable(true), Description("")]
        public new bool RecoverableInterchangeProcessing
        {
            get
            {
                return base.RecoverableInterchangeProcessing;
            }
            set
            {
                base.RecoverableInterchangeProcessing = value;
            }
        }

        [Description(""), Browsable(true)]
        public new bool ValidateDocument
        {
            get
            {
                return base.ValidateDocument;
            }
            set
            {
                base.ValidateDocument = value;
            }
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
