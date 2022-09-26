using SDM_Compulsory1.Interface;
using SDM_Compulsory1.Model;

namespace SDM_Compulsory1.Service;

public class ReviewService : IReviewService
{
    public IReviewRepository _repo;
    public ReviewService(IReviewRepository repository)
    {
        _repo = repository;
    }

    public int GetNumberOfReviewsFromReviewer(int reviewer)
    {
        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        int count = 0;
        foreach (var review in allBeReviews)
        {
            if (review.Reviewer.Equals(reviewer))
            {
                count++;
            }
            
        }

        return count;
    }

    public double GetAverageRateFromReviewer(int reviewer)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfRatesByReviewer(int reviewer, int rate)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfReviews(int movie)
    {
        throw new NotImplementedException();
    }

    public double GetAverageRateOfMovie(int movie)
    {
        throw new NotImplementedException();
    }

    public int GetNumberOfRates(int movie, int rate)
    {
        throw new NotImplementedException();
    }

    public List<int> GetMoviesWithHighestNumberOfTopRates()
    {
        throw new NotImplementedException();
    }

    public List<int> GetMostProductiveReviewers()
    {
        throw new NotImplementedException();
    }

    public List<int> GetTopRatedMovies(int amount)
    {
        throw new NotImplementedException();
    }

    public List<int> GetTopMoviesByReviewer(int reviewer)
    {
        throw new NotImplementedException();
    }

    public List<int> GetReviewersByMovie(int movie)
    {
        throw new NotImplementedException();
    }
}