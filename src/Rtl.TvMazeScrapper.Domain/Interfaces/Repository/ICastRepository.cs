using Rtl.TvMazeScrapper.Domain.Entity.Base;
using Rtl.TvMazeScrapper.Domain.Interfaces.Repository.Base;

namespace Rtl.TvMazeScrapper.Domain.Interfaces.Repository;

public interface ICastRepository<T> : IBaseRepository<T> where T : EntityBase { }