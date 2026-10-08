using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Repositories
{
    public class RegistroClinicoRepository : IRegistroClinicoRepository
    {
        private readonly AppDbContext _context;

        public RegistroClinicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroClinico?> ObterPorIdAsync(Guid id)
        {
            return await _context.RegistrosClinicos
                .Include(r => r.Prescricoes)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<RegistroClinico>> ObterTodosAsync()
        {
            return await _context.RegistrosClinicos
                .Include(r => r.Prescricoes)
                .ToListAsync();
        }

        public async Task CriarAsync(RegistroClinico registroClinico)
        {
            await _context.RegistrosClinicos.AddAsync(registroClinico);
            await _context.SaveChangesAsync();
        }
    }
}
