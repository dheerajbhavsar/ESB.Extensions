using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.XLANGs.BaseTypes;
using System.Collections;

using ESB.Extensions.Schemas;

namespace ESB.Extensions.Components
{
    [Serializable]
    public class XLANGMessageSequenceIdComparer : XLANGMessagePropertyComparer<SequenceId, string>
    {
        public XLANGMessageSequenceIdComparer()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }

    [Serializable]
    public class XLANGMessagePropertyComparer<TProp, TValue> : IComparer, IComparer<XLANGMessage>
        where TProp : PropertyBase
    {
        public XLANGMessagePropertyComparer(IComparer<TValue> valueComparer)
        {
            _valueComparer = valueComparer;
        }

        private readonly Type _propertyType = typeof(TProp);
        public Type PropertyType
        {
            get { return _propertyType; }
        }

        private readonly IComparer<TValue> _valueComparer;
        public IComparer<TValue> ValueComparer
        {
            get { return _valueComparer; }
        }

        #region IComparer<XLANGMessage> Members

        public int Compare(XLANGMessage x, XLANGMessage y)
        {
            if (object.Equals(x, y))
            {
                return 0;
            }
            if ((null == x) && (null == y))
            {
                return 0;
            }
            if (null == x)
            {
                return -1;
            }
            if (null == y)
            {
                return 1;
            }

            TValue objX = (TValue)x.GetPropertyValue(this.PropertyType);
            TValue objY = (TValue)y.GetPropertyValue(this.PropertyType);
            return this.ValueComparer.Compare(objX, objY);
        }

        #endregion

        #region IComparer Members

        public int Compare(object x, object y)
        {
            XLANGMessage msgX = x as XLANGMessage;
            XLANGMessage msgY = y as XLANGMessage;
            return Compare(msgX, msgY);
        }

        #endregion
    }
}
