using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public abstract class Model : IModel
    {
        private object _id;
        private bool _isDeleted;

        public object Id
        {
            get { return _id; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        public bool IsNew
        {
            get { return _id == null; }
        }

        public void Delete()
        {
            if (_isDeleted)
                throw new InvalidOperationException("This model has already been marked to be deleted.\nYou cannot delete a model twice.");

            _isDeleted = true;
        }

        public void Initialize(object id)
        {
            if (_id != null)
                throw new InvalidOperationException("This model has already been initialized.\nYou cannot initialize a model twice.");

            _id = id;
        }
    }
}
