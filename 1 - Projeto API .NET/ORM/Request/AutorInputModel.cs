using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORM.Request
{
    public class AutorInputModel
    {
        [Key]
        [JsonIgnore]
        public int idAutor { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(70, ErrorMessage = "O campo Nome deve ter no máximo 70 caracteres.")]
        public string? nome { get; set; }

    }
}
