using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            /*CreateMap<UpdateBookViewModel, Book>().ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                                                  .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
                                                  .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title)); */
            CreateMap<UpdateBookViewModel, Book>();

        }
    }
}