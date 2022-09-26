namespace SDM_Compulsory1.Interface;

public interface IReviewService
{
//1. On input N, what are the number of reviews from reviewer N?
int GetNumberOfReviewsFromReviewer(int reviewer);
//2. On input N, what is the average rate that reviewer N had given?
double GetAverageRateFromReviewer(int reviewer);
//3. On input N and R, how many times has reviewer N given rate R?
int GetNumberOfRatesByReviewer(int reviewer, int rate);
//4. On input N, how many have reviewed movie N?
int GetNumberOfReviews(int movie);
//5. On input N, what is the average rate the movie N had received?
double GetAverageRateOfMovie(int movie);
//6. On input N and R, how many times had movie N received rate R?
int GetNumberOfRates(int movie, int rate);
//7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
List<int> GetMoviesWithHighestNumberOfTopRates();
//8. What reviewer(s) had done most reviews?
List<int> GetMostProductiveReviewers();
//9. On input N, what is top N of movies? The score of a movie is its average rate.
List<int> GetTopRatedMovies(int amount);
//10. On input N, what are the movies that reviewer N has reviewed? The list should
//be sorted decreasing by rate first, and date secondly.
List<int> GetTopMoviesByReviewer(int reviewer);
//11. On input N, who are the reviewers that have reviewed movie N? The list
//should be sorted decreasing by rate first, and date secondly.
List<int> GetReviewersByMovie(int movie);

}