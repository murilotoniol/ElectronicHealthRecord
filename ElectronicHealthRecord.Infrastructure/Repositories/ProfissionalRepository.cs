using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly AppDbContext _context;

        public ProfissionalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Profissional?> ObterPorIdAsync(Guid id)
        {
            return await _context.Profissionais.FindAsync(id);
        }

        public async Task<Profissional?> ObterPorCrmAsync(string registroCrm)
        {
            return await _context.Profissionais.FirstOrDefaultAsync(p => p.RegistroCrm == registroCrm);
        }

        public async Task<IEnumerable<Profissional>> ObterTodosAsync()
        {
            return await _context.Profissionais.ToListAsync();
        }

        public async Task CriarAsync(Profissional profissional)
        {
            await _context.Profissionais.AddAsync(profissional);
            await _context.SaveChangesAsync();
        }
    }
}
