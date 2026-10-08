namespace MovieApi.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Director { get; set; } = "";
        public int Year { get; set; }
        public decimal Rating { get; set; }
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
    }
}