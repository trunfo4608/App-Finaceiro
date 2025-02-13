
using FinaceiroAPI.Data;
using FinaceiroAPI.Helpers;
using FinaceiroAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinaceiroAPI.Repository
{
    public class Repository : IRepository
    {
        private readonly FinanceiroAPIContext _context;

        public Repository(FinanceiroAPIContext context)
        {
            _context = context;
        }


        public void Insert<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);

        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }  


        public bool SaveChanges()
        {
            if(_context.SaveChanges() > 0)
                return true;

            return false;
        }

        public async Task<IEnumerable<Estado>> GetAllEstados()
        {
            return await _context.Estados.OrderBy(e => e.Sigla).ToListAsync();
        }


        public Estado GetEstadoBySigla(string sigla)
        {
            return _context.Estados
                  .AsNoTracking()
                  .FirstOrDefault(e => e.Sigla.ToUpper() == sigla.ToUpper());

        }

        public PaginacaoResponse<Estado> GetEstadoPaginacao(string? valor, int skip, int take, bool ordemDesc)
        {

            var lista = _context.Estados.ToList();

            if(!string.IsNullOrEmpty(valor))
            {

                lista = _context.Estados
                        .Where(
                                 e => e.Sigla.ToUpper().Contains(valor.ToUpper())
                                 ||
                                 e.Nome.ToUpper().Contains(valor.ToUpper())
                               ).ToList();

            }


            if (ordemDesc)
            {
                lista = lista.OrderByDescending(o => o.Nome).ToList();
            }
            else
            {
                lista = lista.OrderBy(o => o.Nome).ToList();
            }


            var qtde = lista.Count();
            

            lista = lista.Skip((skip - 1) * take).Take(take).ToList();

            var paginaResponse = new PaginacaoResponse<Estado>(lista, qtde, skip, take);

            return paginaResponse;
        }

        public async Task<IEnumerable<Cidade>> GetAllCidades()
        {
            //return _context.Cidades.Include(c => c.Estado).OrderBy(c => c.Nome).ToList();
            return await _context.Cidades.OrderBy(c => c.Nome).ToListAsync();
        }
  
        public Cidade GetCidadeById(int id)
        {
            return _context.Cidades.AsNoTracking().FirstOrDefault(c => c.Id == id);    
        }

        public IEnumerable<Cidade> GetCidadePesquisa(string valor)
        {

            var lista = _context.Cidades
                            .Where(c => c.EstadoSigla.ToUpper().Contains(valor)
                                        ||
                                        c.Nome.ToUpper().Contains(valor)
                                   ).ToList();

            return lista;
        }

        public PaginacaoResponse<Cidade> GetCidadePaginacao(string? valor, int skip, int take, bool ordemDesc)
        {

            var lista = _context.Cidades.ToList();


            if (!string.IsNullOrEmpty(valor))
            {
              lista = _context.Cidades
                      .Where(c => c.Nome.ToUpper().Contains(valor.ToUpper())
                            ||
                            c.EstadoSigla.ToUpper().Contains(valor.ToUpper())
                            ).ToList();
            }

            if (ordemDesc)
            {
                lista = lista.OrderByDescending(o => o.Nome).ToList();
            }
            else
            {
                lista = lista.OrderBy(o => o.Nome).ToList();
            }


            var qtde = lista.Count();

            lista = lista.Skip((skip - 1) * take).Take(take).ToList();

            var paginaResponse = new PaginacaoResponse<Cidade>(lista, qtde, skip, take);

            return paginaResponse;

        }

        public async Task<IEnumerable<Pessoa>> GetAllPessoas()
        {
         
            return await _context.Pessoas
                    .Include(p => p.Cidade)
                    .ThenInclude(c => c.Estado)
                    .OrderBy(p => p.Nome).ToListAsync();
        }

        public Pessoa GetPessoaById(int id)
        {
            return _context.Pessoas
                    .Include(p => p.Cidade)
                   .ThenInclude(c => c.Estado)
                   .AsNoTracking()
                   .FirstOrDefault(p => p.Id == id);
        }



        public PaginacaoResponse<Pessoa> GetPessoaPaginacao(string? valor, int skip, int take, bool ordemDesc)
        {

            var lista = _context.Pessoas.ToList();


            if (!string.IsNullOrEmpty(valor))
            {
                lista = _context.Pessoas
                       .Include(p => p.Cidade)
                       .ThenInclude(c => c.Estado)
                       .Where(p => p.Nome.ToUpper().Contains(valor.ToUpper())
                                    ||
                                   p.Telefone.ToUpper().Contains(valor.ToUpper())
                                    ||
                                   p.Email.ToUpper().Contains(valor.ToUpper())
                              ).ToList();
            }

            if (ordemDesc)
            {
                lista = lista.OrderByDescending(o => o.Nome).ToList();
            }
            else
            {
                lista = lista.OrderBy(o => o.Nome).ToList();
            }


            var qtde = lista.Count();

            lista = lista.Skip((skip - 1) * take).Take(take).ToList();

            var paginaResponse = new PaginacaoResponse<Pessoa>(lista, qtde, skip, take);

            return paginaResponse;

        }

        public async Task<IEnumerable<Conta>> GetAllContas()
        {
            return await _context.Contas
                   .Include(c => c.Pessoa)
                   .OrderBy(c => c.Id).ToListAsync();
        }

        public Conta GetContaById(int id)
        {
            return _context.Contas
                   .Include(c => c.Pessoa)
                   .AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public PaginacaoResponse<Conta> GetContaPaginacao(string? valor, int skip, int take, bool ordemDesc)
        {
            var lista = _context.Contas
                        .Include(c => c.Pessoa)
                        .ToList();


            if (!string.IsNullOrEmpty(valor))
            {
                lista = _context.Contas
                        .Include(c => c.Pessoa)
                        .Where(c => c.Descricao.ToUpper().Contains(valor.ToUpper())
                                 ||
                                c.Pessoa.Nome.ToUpper().Contains(valor.ToUpper())

                         ).ToList();
            }


            if (ordemDesc)
            {
                lista = lista.OrderByDescending(o => o.Descricao).ToList();
            }
            else
            {
                lista = lista.OrderBy(o => o.Descricao).ToList();
            }


            var qtde = lista.Count();

            lista = lista.Skip((skip - 1) * take).Take(take).ToList();

            var paginaResponse = new PaginacaoResponse<Conta>(lista, qtde, skip, take);

            return paginaResponse;

        }


        public async Task<IEnumerable<Usuario>> GetAllUsuario()
        {
            return await _context.Usuarios.OrderBy(u => u.Id).ToListAsync();
        }

        public Usuario GetUsuarioByLogin(string login)
        {
            return _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login == login);
        }

        public Usuario GetUsuarioLogin(UsuarioLogin usuarioLogin)
        {
            return  _context.Usuarios.Where(u => u.Login == usuarioLogin.Login).FirstOrDefault();

            
        }
    }
}
