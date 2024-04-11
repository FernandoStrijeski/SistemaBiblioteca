using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ORM.Request
{
    public class LivroInputModel
    {
        [Key]
        [JsonIgnore]
        public int idLivro { get; set; }

        [Required]
        [ForeignKey("Autor")]
        public int idAutor { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Título deve ter no máximo 50 caracteres.")]
        public string? titulo { get; set; }

        [Required(ErrorMessage = "O campo Ano de Publicação é obrigatório.")]
        public short? anoPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório.")]

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LivroStatus? status { get; set; }
    }

    public enum LivroStatus
    {
        Disponivel,
        Emprestado
    }
}
