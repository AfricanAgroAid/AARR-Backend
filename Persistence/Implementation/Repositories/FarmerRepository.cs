using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class FarmerRepository : GenericRepository<Farmer>, IFarmerRepository
{
          public FarmerRepository(ApplicationContext context)
          {
                    _context = context;
          }
}
