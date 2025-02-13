using FinaceiroAPI.Helpers;
using FinaceiroAPI.Models;

namespace FinaceiroAPI.Repository
{
    public interface IRepository
    {
        void Insert<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveChanges();


        Task<IEnumerable<Estado>> GetAllEstados();
        Estado GetEstadoBySigla(string sigla);
        PaginacaoResponse<Estado> GetEstadoPaginacao(string valor, int skip, int take, bool ordemDesc);



        Task<IEnumerable<Cidade>> GetAllCidades();
        Cidade GetCidadeById(int id);
        IEnumerable<Cidade> GetCidadePesquisa(string valor);
        PaginacaoResponse<Cidade> GetCidadePaginacao(string? valor, int skip, int take, bool ordemDesc);


        Task<IEnumerable<Pessoa>> GetAllPessoas();
        Pessoa GetPessoaById(int id);
        PaginacaoResponse<Pessoa> GetPessoaPaginacao(string? valor, int skip, int take, bool ordemDesc);


        Task<IEnumerable<Conta>> GetAllContas();
        Conta GetContaById(int id);
        PaginacaoResponse<Conta> GetContaPaginacao(string? valor, int skip, int take, bool ordemDesc);


        Task<IEnumerable<Usuario>> GetAllUsuario();
        Usuario GetUsuarioByLogin(string login);
        Usuario GetUsuarioLogin(UsuarioLogin usuarioLogin);

    }
}
