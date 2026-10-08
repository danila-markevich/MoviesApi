using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.DTOs;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAll()
        {
            var genres = await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    MovieCount = g.Movies.Count
                })
                .ToListAsync();

            return genres;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    MovieCount = g.Movies.Count
                })
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return NotFound();
            return genre;
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create(CreateGenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            var result = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
                MovieCount = 0   // ← только что созданный жанр — фильмов 0
            };

            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGenreDto dto)
        {
            var existing = await _context.Genres.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
             
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent() ;
        }
    }
}
