using Pingvinas.Event.Domain.Models;

namespace Pingvinas.Event.Core.Features
{
    public class EmailService : INotificationService
    {
        public async Task NotifyParticipantAsync(Participant participant, string message)
        {
            try
            {
                await Task.Delay(100);
                // Implement email sending logic here
                return;
            }
            catch (Exception)
            {
                //if transient error, schedule retry later.
            }
        }
    }
}
