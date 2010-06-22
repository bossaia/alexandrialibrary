using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IWork
        : IEntity, INamed, IRenamable, IMutable
    {
        WorkType Type { get; }
        IEnumerable<IPerformance> Performances();
        IEnumerable<IWorkAssociation> Associations();
        IEnumerable<IWorkLink> Links();
        IEnumerable<IWorkTag> Tags();

        void ChangeType(WorkType type);
        void AddPerformance(IPerformance performance);
        void RemovePerformance(IPerformance performance);
        void AddAssociation(IWorkAssociation association);
        void RemoveAssociation(IWorkAssociation association);
        void AddLink(IWorkLink link);
        void RemoveLink(IWorkLink link);
        void AddTag(ITag tag);
        void RemoveTag(ITag tag);
    }
}
