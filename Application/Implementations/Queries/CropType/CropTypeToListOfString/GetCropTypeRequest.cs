using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Queries.CropType.CropTypeToListOfString
{
    public sealed record GetCropTypeRequest:IQuery<IEnumerable<string>>;
   
}
