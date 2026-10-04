using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Repositories
{
    public class AtendimentoRepository : IAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public AtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Atendimento?> ObterPorIdAsync(Guid id)
        {
            return await _context.Atendimentos.FindAsync(id);
        }

        public async Task<IEnumerable<Atendimento>> ObterTodosAsync()
        {
            return await _context.Atendimentos.ToListAsync();
        }

        public async Task CriarAsync(Atendimento atendimento)
        {
            await _context.Atendimentos.AddAsync(atendimento);
            await _context.SaveChangesAsync();
        }
    }
}
