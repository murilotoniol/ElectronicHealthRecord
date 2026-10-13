using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CriarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
