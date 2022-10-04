using System.Globalization;
using Moq;
using SDM_Compulsory1.Interface;
using SDM_Compulsory1.Model;
using SDM_Compulsory1.Service;
using Range = System.Range;

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
    
    [Fact] // 3.4
    public void GetAverageRateFromInvalidReviewer()
    {
        //Arange
        //FAKE DB simulation
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        int reviewer = -1;
        //Act
        Action action = () => service.GetAverageRateFromReviewer(reviewer);
        //Assert
        var ex = Assert.Throws<KeyNotFoundException>(action);
        Assert.Equal("Reviewer can not be negative",ex.Message);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Never);
    }
    
    [Theory]
    [InlineData(1,2, 0)] // test case 4.1
    [InlineData(1,4 ,2)] // test case 4.2
    [InlineData(3,2 ,1)] // test case 4.3
    public void GetNumberOfRatesByValidReviewer(int reviewer,int rate, int expectedResult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview(){Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 1, Movie = 4, Grade = 4, ReviewDate = DateTime.Now});
        data.Add(new BeReview(){Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now});
        
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        //Act
        int result = service.GetNumberOfRatesByReviewer(reviewer, rate);
        //Assert
        Assert.Equal(expectedResult, result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    [Theory]
    [InlineData(1,0, "Rate must between 1 & 5" )]  // to low rate
    [InlineData(1,6, "Rate must between 1 & 5")] // to high rate
    [InlineData(-1,2 ,"Reviewer can not be negative")] // reviewer doesnt exist
    public void GetNumberOfRatesByReviewerInvalidRequest(int reviewer,int rate, string expectedMessage)
    {
        //Arange
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);

        Action action = () => service.GetNumberOfRatesByReviewer(reviewer,rate);
        //Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage,ex.Message);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Never);
    }


    [Theory]
    [InlineData(1, 1)] 
    [InlineData(2, 2)] 
    [InlineData(5, 0)] 
    
    public void GetNumberOfReviewsFromValidMovie(int movie, int expectedResult)

    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview() { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now });

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);

        int result = service.GetNumberOfReviews(movie);
        //Assert
        
        Assert.Equal(expectedResult, result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    
    [Theory]
    [InlineData(1, 2)] 
    [InlineData(2, 4.5)] 
    [InlineData(5, 0)] 
    public void GetAverageRateOfMovieTest(int movie, double expectedResult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview() { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now });

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);

        double result = service.GetAverageRateOfMovie(movie);
        //Assert
        
        Assert.Equal(expectedResult, result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    [Theory]
    [InlineData(2,4,2)]
    [InlineData(1,3,0)]
    [InlineData(1,2,1)]
    public void GetNumberOfValidRates(int movie,int rate,int expected)
    {
        //arrange
        List<BeReview> data = new List<BeReview>();
        data.Add(new BeReview() { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 4, Movie = 2, Grade = 4, ReviewDate = DateTime.Now });
        data.Add(new BeReview() { Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now });

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        //act
        int result = service.GetNumberOfRates(movie,rate);
        
        //assert
        Assert.Equal(expected, result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    
    [Theory]
    [InlineData(0,4,"MovieId can not be negative or zero")]//invalid movie
    [InlineData(-1,4,"MovieId can not be negative or zero")]//invalid movie
    [InlineData(1,0,"Rate must be between 1 and 5")]//invalid rate low
    [InlineData(1,6,"Rate must be between 1 and 5")]//invalid rate high
    public void GetNumberOfInvalidRates(int movie,int rate,String expected)
    {
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);

        //act
        Action action = ()=> service.GetNumberOfRates(movie,rate);
        
        //assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expected, ex.Message);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Never);
    }

    [Theory]
    [MemberData(nameof(GetMoviesWithHighestNumberOfTopRatesData))]
    public void GetMoviesWithHighestNumberOfTopRates(List<BeReview> fakeRepo,List<int> expectedresult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = fakeRepo;

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        List<int> result = service.GetMoviesWithHighestNumberOfTopRates();
        //Assert
        result.Sort();
        expectedresult.Sort();
        Assert.Equal(expectedresult,result);
        Assert.True(result.Count == expectedresult.Count);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    public static IEnumerable<Object> GetMoviesWithHighestNumberOfTopRatesData()
    {
        
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 2, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 2, Grade = 2, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 3, Movie = 1, Grade = 4, ReviewDate = DateTime.Now }}),
            new List<int>()
        };
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 3, Movie = 1, Grade = 5, ReviewDate = DateTime.Now }}),
            new List<int>(new []{2})
        };
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 3, Movie = 5, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 4, Movie = 5, Grade = 5, ReviewDate = DateTime.Now }}),
                new List<int>(new[] { 2, 5 })        
                
        };
        
        
    }
    
    
    
    [Theory]
    [MemberData(nameof(GetMostProductiveReviewersData))]
    public void GetMostProductiveReviewers(List<BeReview> fakeRepo,List<int> expectedresult)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = fakeRepo;

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        List<int> result = service.GetMostProductiveReviewers();
        //Assert
        result.Sort();
        expectedresult.Sort();
        Assert.Equal(expectedresult,result);
        Assert.True(result.Count == expectedresult.Count);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
    }
    public static IEnumerable<Object> GetMostProductiveReviewersData()
    {
        
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 1, Grade = 5, ReviewDate = DateTime.Now }}),
            new List<int>(new []{2})
        };
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 5, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 5, Movie = 1, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 3, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 2, Movie = 5, Grade = 5, ReviewDate = DateTime.Now }}),
                new List<int>(new[] { 2, 5 })        
                
        };
        
        
    }
    
    
    
    
    
    
    
    
    #region TestRegion for MemberData
    [Theory]
    [MemberData(nameof(GetData))]
    public void TestOfMemberData(List<BeReview> fakeRepo,List<int> test)
    {
        //Arange
        //FAKE DB simulation
        List<BeReview> data = fakeRepo;

        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        ReviewService service = new ReviewService(mockRepo.Object);
        mockRepo.Setup(r => r.GetAllBeReviews()).Returns(() => data);
        
        double result = service.GetAverageRateOfMovie(test[0]);
        //Assert
        
        Assert.Equal(test[1], result);
        mockRepo.Verify(r => r.GetAllBeReviews(), Times.Once);
            
    }

    public static IEnumerable<Object> GetData()
    {
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 3, Movie = 1, Grade = 2, ReviewDate = DateTime.Now }}),
            new List<int>(new []{1,2})
        };
        yield return new object[]
        {
            new List<BeReview>(new []{  new BeReview { Reviewer = 1, Movie = 2, Grade = 5, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 1, Movie = 2, Grade = 4, ReviewDate = DateTime.Now },
                new BeReview { Reviewer = 3, Movie = 5, Grade = 4, ReviewDate = DateTime.Now }}),
            new List<int>(new []{5,4})
        };
        

    }
    #endregion
    }
