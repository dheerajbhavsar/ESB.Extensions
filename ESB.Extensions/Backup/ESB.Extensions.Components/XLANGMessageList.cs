using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Microsoft.XLANGs.BaseTypes;

namespace ESB.Extensions.Components
{
    [Serializable]
    public class XLANGMessageList : List<XLANGMessage>
    {
        public XLANGMessageEnumerator GetXLANGMessageEnumerator()
        {
            return new XLANGMessageEnumerator(this);
        }

        /// <summary>
        /// Adds the message to the list using the provided key.
        /// Calling the base class Add<>() from within BizTalk XLANG doesn't compile,
        ///  so we 'override' it using the new keyword making sure BizTalk XLANG
        ///   can work with a non-generic method.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        public new void Add(XLANGMessage msg)
        {
            base.Add(msg);
        }

        public new void Sort(IComparer<XLANGMessage> xlangMessageComparer)
        {
            if (null != xlangMessageComparer)
            {
                base.Sort(xlangMessageComparer);
            }
        }
    }

    [Serializable]
    public sealed class XLANGMessageEnumerator
    {
        private readonly IEnumerator<XLANGMessage> _enum;
        internal XLANGMessageEnumerator(IList<XLANGMessage> xmsgs)
        {
            this._enum = xmsgs.GetEnumerator();
        }

        public bool MoveNext()
        {
            return this._enum.MoveNext();
        }

        public XLANGMessage Current
        {
            get { return this._enum.Current; }
        }
    }
}
