using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinaceiroAPI.Models
{
    public class Cidade
    {

        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage ="campo obrigatorio.")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="O campo deve ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }




        [Required(ErrorMessage ="Campo obrigatorio.")]
        [StringLength(2,MinimumLength =2,ErrorMessage ="O campo deve ter {2} caracteres.")]
        public string EstadoSigla { get;set; }

        [JsonIgnore]
        public Estado Estado { get; set; }  
    }
}
