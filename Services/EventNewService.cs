using System.Text.Json;
using EventCardCopilotApp.Models;

namespace EventCardCopilotApp.Services
{
    public class EventNewService
    {
        private readonly string filePath = "Data/events.json";
        private List<Event> events = new();
        public EventNewService()
        {
            LoadEvents();
        }

        private void LoadEvents()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedEvents = JsonSerializer.Deserialize<List<Event>>(json);
                if (loadedEvents != null && loadedEvents.Any())
                {
                    events = loadedEvents;
                    return;
                }
            }

            // Se il file non esiste o è vuoto, aggiungi eventi di default
            events = GetDefaultEvents();
            SaveEvents();
        }

        private List<Event> GetDefaultEvents()
        {
            return new List<Event>
            {       new Event { Id = 1, EventName = "Music Concert", Location = "New York", EventStart = DateTime.Now.AddMonths(1), BookingWindowEnd = DateTime.Now.AddDays(20), Download_url="https://picsum.photos/id/11/2500/1667", Price = 50.0, MaxMembership = 100,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 2, EventName = "Art Exhibition", Location = "Paris", EventStart = DateTime.Now.AddMonths(2), BookingWindowEnd = DateTime.Now.AddDays(30), Download_url="https://picsum.photos/id/11/2500/1667", Price = 30.0, MaxMembership = 80,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 3, EventName = "Tech Conference", Location = "San Francisco", EventStart = DateTime.Now.AddMonths(3), BookingWindowEnd = DateTime.Now.AddDays(40), Download_url="https://picsum.photos/id/1013/4256/2832", Price = 100.0, MaxMembership = 200,   Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 4, EventName = "Food Festival", Location = "Tokyo", EventStart = DateTime.Now.AddMonths(1), BookingWindowEnd = DateTime.Now.AddDays(25), Download_url="https://picsum.photos/id/11/2500/1667", Price = 20.0, MaxMembership = 150,  Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                    new Event { Id = 5, EventName = "Marathon", Location = "London", EventStart = DateTime.Now.AddMonths(4), BookingWindowEnd = DateTime.Now.AddDays(50), Download_url="https://picsum.photos/id/1016/3844/2563", Price = 40.0, MaxMembership = 300, Description="X. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." }
                };

        }
        private void SaveEvents()
        {
            var json = JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, json);
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
                    SaveEvents();
                }
                // Altrimenti: evento pieno
            }
            return assign;
        }
        public bool Unsign(int eventId, int userId)
        {
            var ev = events.FirstOrDefault(e => e.Id == eventId);
            if (ev != null && ev.SignedUpUserIds.Contains(userId))
            {
                ev.SignedUpUserIds.Remove(userId);
                SaveEvents();
                return true;
            }
            return false;
        }
        
        public async Task<List<Event>> GetEventsAsync()
        {
            await Task.Delay(1000);
            return events;        
        }

        

        public Event? GetEventById(int id) => events.FirstOrDefault(e => e.Id == id);

        
         public async Task AddEventAsync(Event newEvent)
        {
            Console.WriteLine($" {events.Count} Adding new event: " + newEvent.EventName);
            await Task.Delay(1000); // Simulate async operation
            newEvent.Id = GenerateId();
            events.Add(newEvent);
            SaveEvents();
        }

        public void UpdateEvent(Event updatedEvent)
        {
            var index = events.FindIndex(e => e.Id == updatedEvent.Id);
            if (index != -1)
            {
                events[index] = updatedEvent;
                SaveEvents();
            }
        }

        public void DeleteEvent(int id)
        {
            var ev = events.FirstOrDefault(e => e.Id == id);
            if (ev != null)
            {
                events.Remove(ev);
                SaveEvents();
            }
        }

        private int GenerateId()
        {
            return events.Any() ? events.Max(e => e.Id) + 1 : 1;
        }

        public bool IsSignUpToEvent(int userId, int eventId)
        {
            return events.Where(e => e.Id == eventId && e.SignedUpUserIds.Contains(userId)).ToList().Count() > 0;
        }
    }
}