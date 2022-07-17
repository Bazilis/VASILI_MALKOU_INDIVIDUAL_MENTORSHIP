namespace BLL.Interfaces
{
    public interface IRabbitmqProducer
    {
        (bool, string) Produce(string message);
    }
}
