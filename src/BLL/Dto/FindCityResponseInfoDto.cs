namespace BLL.Dto
{
    internal class FindCityResponseInfoDto
    {
        public string CityName { get; set; }

        public double CityTemp { get; set; }

        public double ResponseTimeMs { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}
