using System.ComponentModel.DataAnnotations;

namespace FinaceiroAPI.Models
{
    public class UsuarioLogin
    {


        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Campo obrigatorio.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "O campo deve ter entre {2} e {1} caracteres.")]
        public string Senha { get; set; }

    }
}
