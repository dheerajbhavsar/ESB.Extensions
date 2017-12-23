using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ESB.Extensions.Resolutions
{
    public partial class GoList
    {
        public GoEnumerator GetGoEnumerator()
        {
            return new GoEnumerator(this);
        }
    }

    [Serializable]
    public class GoEnumerator
    {
        internal GoEnumerator(GoList goList)
        {
            _goList = goList;
            this.Reset();
        }

        public bool MoveNext()
        {
            if (this.GoList.Go.Length > (this.Index + 1))
            {
                this.Index++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            this.Index = -1;
        }

        public Go Current
        {
            get { return this.GoList.Go[this.Index]; }
        }

        private readonly GoList _goList;
        private GoList GoList
        {
            get { return _goList; }
        }

        private int _index;
        private int Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}
