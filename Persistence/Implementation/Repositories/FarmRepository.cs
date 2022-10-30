using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class FarmRepository: GenericRepository<Farm>, IFarmRepository
{
          public FarmRepository(ApplicationContext context)
          {
                    _context = context;
          }
}