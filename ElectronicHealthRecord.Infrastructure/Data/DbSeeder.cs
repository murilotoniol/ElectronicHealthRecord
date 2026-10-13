using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Domain.Entidades;
using ElectronicHealthRecord.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Infrastructure.Data
{
    /// <summary>
    /// Cria usuários de demonstração para facilitar testes locais.
    /// Deve ser chamado SOMENTE em ambiente de desenvolvimento.
    /// </summary>
    public static class DbSeeder
    {
        private const string SenhaDemo = "Demo@12345";

        public static async Task SeedUsuariosDemoAsync(AppDbContext context, ISenhaHasher senhaHasher)
        {
            if (await context.Usuarios.AnyAsync())
            {
                return;
            }

            var hash = senhaHasher.GerarHash(SenhaDemo);

            context.Usuarios.AddRange(
                new Usuario("Dr. Roberto Silva", "medico@hospital.com", hash, PerfilUsuario.Medico),
                new Usuario("Ana Souza", "recepcao@hospital.com", hash, PerfilUsuario.Recepcionista)
            );

            await context.SaveChangesAsync();
        }
    }
}
