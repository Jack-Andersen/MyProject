using BookStoreV2.Models;
using ScafBookStoreV2fold.DTO;

namespace BookStoreV2.DTO
{
    public class GenreDTO
    {
        public int? GenreId { get; set; }
        public string? Name { get; set; }

        //public int BookID { get; set; }
    }

    //public class GenresDTOForUpdate : GenresDTOForSave
    //{
    //    public int GenreID { get; set; }
    //}

    ////public class ProductDTO : ProductDTOForUpdate
    ////{

    ////}

    //public class GenresDTOPlusBooks : GenresDTOForUpdate
    //{
    //    public virtual BooksGenresDTO? Books { get; set; }
    //}

    //public class GenresBooksDTO : GenresDTOForUpdate
    //{
    //    //public int ProductID { get; set; }
    //    //public string ?ProductName { get; set; }
    //}

}
