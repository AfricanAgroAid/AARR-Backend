using Application.Wrapper;
using MediatR;

namespace Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where  TQuery : IQuery<TResponse>
    {
    }
}
