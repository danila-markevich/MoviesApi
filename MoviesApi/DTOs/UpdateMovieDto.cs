using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class UpdateMovieDto
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Режиссёр обязателен")]
        [StringLength(100, MinimumLength = 1)]
        public string Director { get; set; } = "";

        [Range(1900, 2100, ErrorMessage = "Год должен быть от 1900 до 2100")]
        public int Year { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Рейтинг должен быть от 0.0 до 10.0")]
        public decimal Rating { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Жанр обязателен")]
        public int GenreId { get; set; }
    }
}