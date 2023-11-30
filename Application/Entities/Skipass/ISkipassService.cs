namespace Application.Entities.Skipass;

public interface ISkipassService
{
    List<SkipassDto> ShowAllSkipasses();
    Task<SkipassDto> GetSkipassByID(int skipassId);
    Task<SkipassDto> AddNewSkipass(SkipassDto request);
    Task<SkipassDto> UpdateSkipassInfo(SkipassDto updatedSkipass);
    Task<SkipassDto> DeleteSkipass(int skipassId);
}