using System;
using System.Linq.Expressions;

namespace Application.Wrapper
{
    public class Filter<T>
    {
        public bool Condition { get; set; }
        public Expression<Func<T, bool>> Expression { get; set; }
    }
}