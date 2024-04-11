using System.ComponentModel.DataAnnotations;

namespace ORM.Response
{
    public class Usuario
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Nome deve ter no máximo 50 caracteres.")]
        public string? nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string? email { get; set; }

        [Required(ErrorMessage = "O campo DataCadastro é obrigatório.")]
        public DateTime dataCadastro { get; set; }
    }
}
