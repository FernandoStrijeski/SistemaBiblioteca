    using AutoMapper;
    using Dados.Autores;
    using ORM.Request;
    using ORM.Response;

namespace API_Sistema_Biblioteca.Mapeamento
{
    public class AutorProfile : Profile
    {
        public AutorProfile()
        {
            CreateMap<AutorInputModel, Autor>();
        }
    }
}
