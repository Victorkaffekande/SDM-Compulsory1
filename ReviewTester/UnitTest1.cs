using Moq;
using SDM_Compulsory1.Interface;
using SDM_Compulsory1.Service;

namespace ReviewTester;

public class UnitTest1
{

    [Fact]
    public void CreateReviewService()
    {
        //Arange
        Mock<IReviewRepository> mockRepo = new Mock<IReviewRepository>();
        //Act
        ReviewService service = new ReviewService(mockRepo.Object);
        //Assert
        Assert.NotNull(service);
        Assert.True(service is ReviewService);
    }
    
    
    /**
     * 
     
    [Fact]
    public void GetNumberOfReviewsFromValidReviewer(int reviewer)
    {
        //Arrange
        
        
        //Act
        
        
        //Assert
    }
    
    */
}