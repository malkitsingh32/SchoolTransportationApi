namespace Application.Abstraction.Services
{
    public interface ICardknoxMonthlyTaskService
    {
        Task RunMonthlyTask(CancellationToken cancellationToken);
    }
}
