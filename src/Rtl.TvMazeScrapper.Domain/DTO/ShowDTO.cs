namespace Rtl.TvMazeScrapper.Domain.DTO;

public class ShowDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<CastDTO> Cast { get; set; } = null!;
}