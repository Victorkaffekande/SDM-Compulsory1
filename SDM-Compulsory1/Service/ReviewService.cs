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
        if (reviewer <= 0)
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
        if (reviewer <= 0)
        {
            throw new KeyNotFoundException("Reviewer can not be negative");
        }

        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        List<BeReview> allReviewByReviewer = allBeReviews.FindAll(beReview => beReview.Reviewer.Equals(reviewer));
        double totalGrade = 0;
        double counter = 0;


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
        if (reviewer <= 0) throw new ArgumentException("Reviewer can not be negative");


        if (rate <= 0 || rate >= 6) throw new ArgumentException("Rate must between 1 & 5");

        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        List<BeReview> allReviewByReviewer =
            allBeReviews.FindAll(beReview => beReview.Reviewer.Equals(reviewer) && beReview.Grade.Equals(rate));


        return allReviewByReviewer.Count;
    }

    public int GetNumberOfReviews(int movie)
    {
        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        return allBeReviews.FindAll(beReview => beReview.Movie.Equals(movie)).Count;
    }

    public double GetAverageRateOfMovie(int movie)
    {
        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        allBeReviews = allBeReviews.FindAll(beReview => beReview.Movie.Equals(movie));

        double total = 0;
        double count = 0;
        foreach (var beReviews in allBeReviews)
        {
            total += beReviews.Grade;
            count++;
        }

        return total / count;
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