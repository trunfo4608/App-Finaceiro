using FinaceiroAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinaceiroAPI.Models
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "Campo obrigatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataPagamento { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        public Situacao Situacao { get; set; }


        [Required(ErrorMessage ="Campo obrigatorio.")]
        public int PessoaId { get; set; }
        public Pessoa Pessoa{get;set;}


    }
}
