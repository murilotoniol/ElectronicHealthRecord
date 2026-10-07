using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Repositories
{
    public class PrescricaoRepository : IPrescricaoRepository
    {
        private readonly AppDbContext _context;

        public PrescricaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Prescricao?> ObterPorIdAsync(Guid id)
        {
            return await _context.Prescricoes.FindAsync(id);
        }

        public async Task<IEnumerable<Prescricao>> ObterTodosAsync()
        {
            return await _context.Prescricoes.ToListAsync();
        }

        public async Task CriarAsync(Prescricao prescricao)
        {
            await _context.AddAsync(prescricao);
            await _context.SaveChangesAsync();
        }
    }
}
