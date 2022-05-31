using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IFindCityWithMaxTemp
    {
        public string FindCityWithMaxTemp(FindCityWithMaxTempInputDataDto inputData);
    }
}
