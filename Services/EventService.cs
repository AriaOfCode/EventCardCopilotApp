
using EventCardCopilotApp.Models;
namespace EventCardCopilotApp.Services
{
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        List<Event> events { get; set; } = new List<Event>{
                    new Event { Id = 1, EventName = "Music Concert", Location = "New York", EventStart = DateTime.Now.AddMonths(1), BookingWindowEnd = DateTime.Now.AddDays(20), Download_url="https://picsum.photos/id/11/2500/1667", Price = 50.0, MaxMembership = 100,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 2, EventName = "Art Exhibition", Location = "Paris", EventStart = DateTime.Now.AddMonths(2), BookingWindowEnd = DateTime.Now.AddDays(30), Download_url="https://picsum.photos/id/11/2500/1667", Price = 30.0, MaxMembership = 80,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 3, EventName = "Tech Conference", Location = "San Francisco", EventStart = DateTime.Now.AddMonths(3), BookingWindowEnd = DateTime.Now.AddDays(40), Download_url="https://picsum.photos/id/1013/4256/2832", Price = 100.0, MaxMembership = 200,   Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 4, EventName = "Food Festival", Location = "Tokyo", EventStart = DateTime.Now.AddMonths(1), BookingWindowEnd = DateTime.Now.AddDays(25), Download_url="https://picsum.photos/id/11/2500/1667", Price = 20.0, MaxMembership = 150,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 5, EventName = "Marathon", Location = "London", EventStart = DateTime.Now.AddMonths(4), BookingWindowEnd = DateTime.Now.AddDays(50), Download_url="https://picsum.photos/id/1016/3844/2563", Price = 40.0, MaxMembership = 300, Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." }
                };
        // Add methods to interact with Event data as needed
        public async Task<List<Event>> GetEventsAsync()
        {
            await Task.Delay(1000);
            return events;
            /* if(events.Count>0){
                 return events;
             }else{

                 return events;
             }*/
        }

        public async Task AddEventAsync(Event newEvent)
        {
            Console.WriteLine($" {events.Count} Adding new event: " + newEvent.EventName);
            await Task.Delay(1000); // Simulate async operation
            newEvent.Id = events.Count + 1; // Simple ID assignment
            events.Add(newEvent);
        }
 
        public bool SignUpToEvent(int eventId, int userId)
        {
            bool assign = false;
            var ev = events.FirstOrDefault(e => e.Id == eventId);
            if (ev != null && !ev.SignedUpUserIds.Contains(userId))
            {
                if (ev.SignedUpUserIds.Count < ev.MaxMembership)
                {
                    ev.SignedUpUserIds.Add(userId);
                    assign = true;
                }
                // Altrimenti: evento pieno
            }
            return assign;
        }
        public void Unsign(int eventId, int userId)
        {
            var ev = events.FirstOrDefault(e => e.Id == eventId);
            if (ev != null && ev.SignedUpUserIds.Contains(userId))
            {
                ev.SignedUpUserIds.Remove(userId);
            }
        }
    }
}