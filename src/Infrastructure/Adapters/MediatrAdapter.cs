using Cineplayers2Letterboxd.Domain.Interfaces;
using MediatR;

namespace Cineplayers2Letterboxd.Infrastructure.Adapters;

public class MediatrAdapter(IMediator mediator) : IMediatorAdapter
{
    private readonly IMediator Mediator = mediator;

    public async Task Send(ICommand request)
    {
        await Mediator.Send(request);
    }
}

public interface ICommand : IRequest;

public interface IHandler<in T> : IRequestHandler<T> where T : IRequest;
