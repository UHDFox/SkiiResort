namespace Application.Entities.Skipass;

public interface ISkipassService
{
    List<SkipassDto> ShowAllSkipasses();
    Task<SkipassDto> GetSkipassById(int skipassId);
    Task<SkipassDto> AddNewSkipass(SkipassDto request);
    Task<SkipassDto> UpdateSkipassInfo(SkipassDto updatedSkipass);
    Task<SkipassDto> DeleteSkipass(int skipassId);
}