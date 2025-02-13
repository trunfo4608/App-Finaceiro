using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinaceiroAPI.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obrigatorio.")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="O campo deve ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Campo obrigatorio.")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage ="Campo obrigatorio.")]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage ="Campo obrigatorio.")]
        [StringLength(20,ErrorMessage ="O campo deve ter {1} caracteres.")]
        public string Genero { get; set; }

        

        public int? CidadeId { get; set; }

       
        public Cidade? Cidade { get; set; }





    }
}
