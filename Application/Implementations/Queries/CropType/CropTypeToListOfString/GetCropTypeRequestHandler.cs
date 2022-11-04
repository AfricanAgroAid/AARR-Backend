using Application.Abstractions.Messaging;
using Application.Wrapper;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Queries.CropType.CropTypeToListOfString
{
    public sealed record GetCropTypeRequestHandler : IQueryHandler<GetCropTypeRequest, IEnumerable<string>>
    {
        public async Task<Result<IEnumerable<string>>> Handle(GetCropTypeRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
          var enums= Enum.GetValues(typeof(Domain.Enums.CropType)).Cast< Domain.Enums.CropType>().Select(e=>e.ToString());

         
            return await Result<IEnumerable<string>>.SuccessAsync(enums, "Get Succesful");
        }
    }
}
