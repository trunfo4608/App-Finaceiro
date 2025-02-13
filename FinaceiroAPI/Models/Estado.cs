using System.ComponentModel.DataAnnotations;

namespace FinaceiroAPI.Models
{
    public class Estado
    {
        [Key]
        [StringLength(2,MinimumLength =2,ErrorMessage ="Estado deve ter {2} caracteres.")]
        public string Sigla { get; set; }

        [Required(ErrorMessage ="Campo obrigatorio.")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="O campo deve ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }
    }
}
