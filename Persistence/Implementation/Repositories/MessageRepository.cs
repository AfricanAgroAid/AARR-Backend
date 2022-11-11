using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repositories;

namespace Persistence.Implementation.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationContext context)
    {
        _context = context;
    }
}
