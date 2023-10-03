using BlazorServerTest.Core.Data.Entities.Core;
using System.Linq.Expressions;

namespace BlazorServerTest.Core.Data.Repositories;

public class SortOrder<T, P> where T : IEntity
{
    public Expression<Func<T, P>>? Order { get; set; }

    public bool AscSort { get; set; }
}
