using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class RepositoryController : Controller
    {
        public RepositoryController(IDispatcher parent, IArtistRepository repository)
            : base(parent)
        {
            AddChild(new ArtistRepositoryController(this, repository));
        }
    }
}
