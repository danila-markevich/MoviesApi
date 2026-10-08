using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DTOs;
using MoviesApi.Data;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetAll()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    Year = m.Year,
                    Rating = m.Rating,
                    GenreId = m.GenreId,
                    GenreName = m.Genre != null ? m.Genre.Name : null
                })
                .ToListAsync();

            return movies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetById(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Director = m.Director,
                    Year = m.Year,
                    Rating = m.Rating,
                    GenreId = m.GenreId,
                    GenreName = m.Genre != null ? m.Genre.Name : null
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> Create(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Director = dto.Director,
                Year = dto.Year,
                Rating = dto.Rating,
                GenreId = dto.GenreId
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var genre = await _context.Genres.FindAsync(dto.GenreId);

            var result = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Year = movie.Year,
                Rating = movie.Rating,
                GenreId = movie.GenreId,
                GenreName = genre != null ? genre.Name : null
            };

            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMovieDto dto)
        {
            var existing = await _context.Movies.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = dto.Title;
            existing.Director = dto.Director;
            existing.Year = dto.Year;
            existing.Rating = dto.Rating;
            existing.GenreId = dto.GenreId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
