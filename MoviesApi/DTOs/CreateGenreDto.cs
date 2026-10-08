using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "Название жанра обязательно")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Название от 1 до 50 символов")]
        public string Name { get; set; } = "";

        [StringLength(200, ErrorMessage = "Описание до 200 символов")]
        public string Description { get; set; } = "";
    }
}