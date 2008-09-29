using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zeta.Core;

namespace Zeta.Model
{
    public abstract class ModelBase : IModel
    {
        protected ModelBase()
        {
        }

        #region IModel Members
        public virtual Guid Id
        {
            get;
            private set;
        }
        #endregion
    }
}
