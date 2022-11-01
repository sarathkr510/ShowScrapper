using Moq;
using Rtl.TvMazeScrapper.Domain.DTO;
using Rtl.TvMazeScrapper.Domain.Entity;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository;

namespace Rtl.TvMazeScrapper.Tests.UnitTests.RepositoryTests;

public class ShowRepositoryTest
{
    private Mock<IShowRepository<Show>> _mockedShowRepository;

        [SetUp]
        public void Setup()
        {
            _mockedShowRepository = new Mock<IShowRepository<Show>>();
        }

        [Test]
        public async Task Show_Should_Be_AddedToTheDatabase()
        {
            
            var showEntity = new Show(Guid.NewGuid().ToString());
            _mockedShowRepository.Setup(s => s.AddAsync(showEntity)).ReturnsAsync(() => true);

            
            var show = await _mockedShowRepository.Object.AddAsync(showEntity);

            
            _mockedShowRepository.Verify(m => m.AddAsync(It.IsAny<Show>()), Times.Once());
        }

        [Test]
        public async Task TestToVerifyShowsInRepositoryWithAnyAsync()
        {
            
            _mockedShowRepository.Setup(s => s.AnyAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => true);

            
            var show = await _mockedShowRepository.Object.AnyAsync(a => a.Name == Guid.NewGuid().ToString());

            
            _mockedShowRepository.Verify(m => m.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Show, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Test_ToVerify_Cast_Returned_From_Repository_Async()
        {
            
            var showEntity = new Show(Guid.NewGuid().ToString());
            _mockedShowRepository.Setup(s => s.GetAsync(a => a.Name == Guid.NewGuid().ToString())).ReturnsAsync(() => showEntity);

            
            var show = await _mockedShowRepository.Object.GetAsync(a => a.Name == Guid.NewGuid().ToString());

            
            _mockedShowRepository.Verify(m => m.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Show, bool>>>()), Times.Once());
        }

        [Test]
        public async Task Test_ToVerify_Cast_Count_In_Repository_Async()
        {
            
            var count = 1;
            _mockedShowRepository.Setup(s => s.CountAsync()).ReturnsAsync(() => count);

            
            var show = await _mockedShowRepository.Object.CountAsync();

            
            _mockedShowRepository.Verify(m => m.CountAsync(), Times.Once());
        }

        [Test]
        public async Task Test_To_Check_GetShowsByPageNumber_IsWorking()
        {
            
            var queryDto = new GetShowsRequestDto(1, 1);
            var paginatedList = new List<Show>() { new Show(Guid.NewGuid().ToString()) };
            _mockedShowRepository.Setup(s 
                => s.GetShowsByPageNumber(queryDto)).ReturnsAsync(() => paginatedList);

            
            var show = await _mockedShowRepository.Object.GetShowsByPageNumber(queryDto);

            
            _mockedShowRepository.Verify(m 
                => m.GetShowsByPageNumber(It.IsAny<GetShowsRequestDto>()), Times.Once());
        }
    }