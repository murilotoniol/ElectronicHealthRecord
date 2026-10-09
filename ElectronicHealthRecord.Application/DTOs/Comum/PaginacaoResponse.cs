namespace ElectronicHealthRecord.Application.DTOs.Comum
{
    public record PaginacaoResponse<T>(
        IEnumerable<T> Itens,
        int TotalItens,
        int Pagina,
        int TotalPaginas
    );
}
