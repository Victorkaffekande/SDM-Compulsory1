using Moq;
using SDM_Compulsory1.Interface;
using SDM_Compulsory1.Model;
using SDM_Compulsory1.Service;

namespace ReviewTester;

public class UnitTest1
{

    [Fact]
    public void CreateReviewService() // test case 1.1
    {
        //Arange
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        //Act
        ReviewService service = new ReviewService(mockRepo.Object);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is ReviewService);
    }
    
    
     
    [Theory]
    [InlineData(1, 2)] // test case 2.1
    [InlineData(3, 1)] // test case 2.2
    [InlineData(2, 0)] // test case 2.3
    public void GetNumberOfReviewsFromValidReviewer(int reviewer, int expectedResult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview(){Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 1, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now});
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        //Act
        int result = service.GetNumberOfReviewsFromReviewer(reviewer);
        //Assert
        Assert.True(result.Equals(expectedResult));
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    
    [Theory]
    [InlineData(0)] // test case 2.4
    [InlineData(-1)] // test case 2.5
    [InlineData(unchecked(int.MaxValue + 1))] // test case 2.6
    public void GetNumberOfReviewsFromInvalidReviewer(int reviewer)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview(){Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 1, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now});
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        //Act
        Action action = () => service.GetNumberOfReviewsFromReviewer(reviewer);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("Id can not be negative or 0",ex.Message);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Never);
        
    }
    
    
    [Theory]
    [InlineData(3, 2)] // test case 3.1
    [InlineData(1, 4.5)] // test case 3.2
    [InlineData(10, 0)] // test case 3.3
    public void GetAverageRateFromValidReviewer(int reviewer, double expectedResult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview(){Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 1, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 10, Movie = 23232, Grade = 0, ReviewDate = DateTime.Now});
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        //Act
        double result = service.GetAverageRateFromReviewer(reviewer);
        //Assert
        Assert.Equal(expectedResult, result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    [Fact]
    public void GetAverageRateFromInvalidReviewer()
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview(){Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 1, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 10, Movie = 23232, Grade = 0, ReviewDate = DateTime.Now});
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        int reviewer = 5;
        //Act
        Action action = () => service.GetAverageRateFromReviewer(reviewer);
        //Assert
        var ex = Assert.Throws<KeyNotFoundException>(action);
        Assert.Equal("Reviewer does not exist",ex.Message);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
}