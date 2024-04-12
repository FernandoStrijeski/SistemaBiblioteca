using Microsoft.VisualBasic;
using ORM.ValidacoesPersonalizadas;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ORM.Response
{
    public class Livro
    {
        [Key]
        public int idLivro { get; set; }

        [Required]
        [ForeignKey("Autor")]
        public int idAutor { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Título deve ter no máximo 50 caracteres.")]
        public string? titulo { get; set; }

        [Required(ErrorMessage = "O campo Ano de Publicação é obrigatório.")]
        [AnoRange(1500)]
        public short? anoPublicacao { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [EnumValue(typeof(LivroStatus))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LivroStatus status { get; set; }

        [JsonIgnore]
        public Autor? autor { get; set; }
    }

    public enum LivroStatus
    {
        Disponivel,
        Emprestado
    }
}
