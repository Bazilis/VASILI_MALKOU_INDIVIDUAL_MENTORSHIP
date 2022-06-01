using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IFindCityWithMaxTemp
    {
        string FindCityWithMaxTemp(FindCityWithMaxTempInputDataDto inputData);
    }
}
