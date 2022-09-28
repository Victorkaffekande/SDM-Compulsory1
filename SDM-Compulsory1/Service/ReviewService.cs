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
        if (reviewer <=0)
        {
            throw new ArgumentException("Id can not be negative or 0");
        }

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
        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        List<BeReview> allReviewByReviewer = allBeReviews.FindAll(beReview => beReview.Reviewer.Equals(reviewer));
        double totalGrade = 0;
        double counter = 0;

        if (allReviewByReviewer.Count <= 0)
        {
            throw new KeyNotFoundException("Reviewer does not exist");
        }
        
        foreach (var review in allReviewByReviewer)
        {
            if (review.Reviewer.Equals(reviewer))
            {
                totalGrade += review.Grade;
                counter++;
            }
        }
        return totalGrade / counter;
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