using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService(IMovieRepository movieRepo) : IMovieService
{
    public Task<bool> CreateAsync(Movie movie) => movieRepo.CreateAsync(movie);

    public Task<Movie?> GetByIdAsync(Guid id) => movieRepo.GetByIdAsync(id);

    public Task<Movie?> GetBySlugAsync(string slug) => movieRepo.GetBySlugAsync(slug);

    public Task<IEnumerable<Movie>> GetAllAsync() => movieRepo.GetAllAsync();

    public async Task<Movie?> UpdateAsync(Movie movie)
    {
        var movieExists = await movieRepo.ExistsByIdAsync(movie.Id);
        if (!movieExists) return null;
        
        await movieRepo.UpdateAsync(movie);
        return movie;
    }

    public Task<bool> DeleteByIdAsync(Guid id) => movieRepo.DeleteByIdAsync(id);
}