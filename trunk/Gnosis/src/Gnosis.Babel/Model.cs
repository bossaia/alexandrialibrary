using System;

namespace Gnosis.Babel
{
    public abstract class Model : IModel
    {
        public object Id { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsNew
        {
            get { return Id == null; }
        }

        public void Delete()
        {
            if (IsDeleted)
                throw new InvalidOperationException("This model has already been marked to be deleted. A model cannot be deleted twice.");

            IsDeleted = true;
        }

        public void Initialize(object id)
        {
            if (Id != null)
                throw new InvalidOperationException("This model has already been initialized. A model cannot be initialized twice.");

            Id = id;
        }
    }
}
