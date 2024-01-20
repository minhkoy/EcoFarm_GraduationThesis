namespace EcoFarm.UseCases.Common.Hubs.Requests
{
    public class SendMessageRequest
    {
        public string Message { get; set; }
        public string ToUsername { get; set; }
    }
}
