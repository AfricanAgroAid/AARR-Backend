using Application.Abstractions;

namespace Application.Implementations.Queries
{
    public sealed record GetCropTypeRequest:IQuery<IEnumerable<string>>;
   
}
