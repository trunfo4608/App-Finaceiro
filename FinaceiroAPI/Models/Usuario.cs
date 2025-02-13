using System.ComponentModel.DataAnnotations;

namespace FinaceiroAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Funcao { get; set; }

    }
}
