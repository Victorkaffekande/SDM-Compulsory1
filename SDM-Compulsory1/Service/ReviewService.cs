﻿using SDM_Compulsory1.Interface;
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

        if (count == 0) return 0;
        return total / count;
    }

    public int GetNumberOfRates(int movie, int rate)
    {
        if (movie <= 0) throw new ArgumentException("MovieId can not be negative or zero");


        if (rate <= 0 || rate > 5) throw new ArgumentException("Rate must be between 1 and 5");


        List<BeReview> allBeReviews = _repo.GetAllBeReviews();

        return allBeReviews.FindAll(beReview => beReview.Movie.Equals(movie) && beReview.Grade.Equals(rate)).Count;
    }

    public List<int> GetMoviesWithHighestNumberOfTopRates()
    {
        List<BeReview> allBeReviews = _repo.GetAllBeReviews();
        allBeReviews = allBeReviews.FindAll(bereview => bereview.Grade.Equals(5));
        List<int> onlyMovies = new List<int>();
        foreach (var beReview in allBeReviews)
        {
            onlyMovies.Add(beReview.Movie);
        }

        
        List<int> topRatedMovies = new List<int>();
        int mostRatedMovie = onlyMovies.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
            .Select(grp=>grp.Key).First();
        
        topRatedMovies.Add(mostRatedMovie);
        onlyMovies.Remove(mostRatedMovie);
        

        int movieId = 0;
        while (onlyMovies.Count !=0 )
        {
            movieId = onlyMovies.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
                .Select(grp=>grp.Key).First();

            if (movieId == mostRatedMovie)
            {
                topRatedMovies.Add(movieId);
                onlyMovies.Remove(movieId);
            }else
                break;
        }

        return topRatedMovies;
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