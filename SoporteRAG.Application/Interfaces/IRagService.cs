using SoporteRAG.Application.DTOs;
using SoporteRAG.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoporteRAG.Application.Interfaces
{
    public interface IRagService
    {
        Task<RagResponse> AskAsync(string question);
    }
}
