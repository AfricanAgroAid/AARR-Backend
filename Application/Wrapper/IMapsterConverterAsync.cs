using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Wrapper
{
     public interface IMapsterConverterAsync<T, TD>
    {
        public Task<TD> ConvertAsync(T item);

        public Task<T> ConvertBackAsync(TD item);
    }
}