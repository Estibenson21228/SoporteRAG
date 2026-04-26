using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoporteRAG.Domain.Entities;
namespace SoporteRAG.Application.Interfaces
{
    public interface ITicketRepository
    {
        Task<int> GetCountAsync();
        Task<List<Ticket>> GetAllAsync();
        Task AddAsync(Ticket ticket);
    }
}
