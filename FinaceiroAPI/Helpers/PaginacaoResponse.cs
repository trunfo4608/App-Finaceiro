namespace FinaceiroAPI.Helpers
{
    public class PaginacaoResponse<T> where T : class
    {

        public IEnumerable<T> Dados { get; set; }
        public long TotalRegistros { get; set; }
        public int Skip { get; set; }
        public int Take {  get; set; }

  

        public PaginacaoResponse(IEnumerable<T> dados, long totalRegistros, int skip, int take)
        {
            Dados = dados;
            TotalRegistros = totalRegistros;
            Skip = skip;
            Take = take;
        }
    }
}
