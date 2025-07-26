using Application.Abstraction.Service; 

namespace Infrastructure.Persistence.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
