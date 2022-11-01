using Rtl.TvMazeScrapper.Domain.Entity.Base;

namespace Rtl.TvMazeScrapper.Domain.Entity
{
    public class Show : EntityBase
    {
        public Show(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public virtual List<Cast> Casts { get; set; }
    }
}