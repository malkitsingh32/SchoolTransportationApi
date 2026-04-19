namespace API.BackgroundServices.Dtos
{
    public class CardKnoxRecurringResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string RefNum { get; set; }
    }
}
