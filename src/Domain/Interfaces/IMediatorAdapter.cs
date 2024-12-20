using Cineplayers2Letterboxd.Infrastructure.Adapters;

namespace Cineplayers2Letterboxd.Domain.Interfaces;

public interface IMediatorAdapter
{
    Task Send(ICommand request);
}
