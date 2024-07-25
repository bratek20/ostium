using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core
{
    public abstract class Button
    {
        public void Click()
        {
            OnClick();
        }

        protected abstract void OnClick();
    }
}
