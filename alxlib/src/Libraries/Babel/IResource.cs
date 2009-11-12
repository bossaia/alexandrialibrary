using System;

namespace Babel
{
	public interface IResource
	{
		Uri Id { get; }
		IResponse Read(IRequest request);
		IResponse Write(IRequest request);
		IResponse Evaluate(IRequest request);
		IResponse Delete(IRequest request);
	}
}
