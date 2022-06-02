namespace BLL.Dto
{
    internal class FindCityResponseInfoDto
    {
        public string CityName { get; set; }

        public double CityTemp { get; set; }

        public double ResponseTimeMs { get; set; }

        public ResponseState ResponseState { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum ResponseState
    {
        Successful,
        Failed,
        Canceled
    }
}
