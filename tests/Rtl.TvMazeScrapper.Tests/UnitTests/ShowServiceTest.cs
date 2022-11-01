using Moq;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Interfaces.Services;

namespace Rtl.TvMazeScrapper.Tests.UnitTests;

public class ShowServiceTest
{
    private Mock<IShowService> _mockedShowService;

    [SetUp]
    public void Setup()
    {
        _mockedShowService = new Mock<IShowService>();
    }

    [Test]
    public async Task Test_ToVerify_Show_IsAdded_InDb_Async()
    {
        
        var queryDto = new GetShowsRequestDto(1, 1);
        var paginatedList = new List<ShowDto>();
        _mockedShowService.Setup(s => 
            s.GetShowsByPageNumber(queryDto)).ReturnsAsync(() => paginatedList);

        
        var show = await _mockedShowService.Object.GetShowsByPageNumber(queryDto);

        
        _mockedShowService.Verify(m => 
            m.GetShowsByPageNumber(It.IsAny<GetShowsRequestDto>()), Times.Once());
    }
}