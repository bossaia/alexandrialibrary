using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IUser
    {
        long Id { get; }
        string Name { get; }
        string PasswordHash { get; }
        void Rename(string name);
        void ChangePasswordHash(string password);
    }
}
