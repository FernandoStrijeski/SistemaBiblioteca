using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ORM.Response
{
    public class Autor
    {
        [Key]        
        public int idAutor { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(70, ErrorMessage = "O campo Nome deve ter no máximo 70 caracteres.")]
        public string? nome { get; set; }

        [JsonIgnore]
        public List<Livro>? livros { get; set; }

    }
}
