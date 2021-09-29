using CatalogGames.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogGamesApi.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<CatalogGames.Entities.Jogo>> Obter(int pagina, int quantidade);
        Task<Jogo> Obter(Guid id);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
    }
}
