using System.Text.Json;
using EventCardCopilotApp.Models;

namespace EventCardCopilotApp.Services
{
    public class EventService
    {
        private readonly string filePath = "Data/events.json";
        private List<Event> events = new();

        public EventService()
        {
            LoadEvents();
        }

        public List<Event> GetEvents() => events;

        public Event? GetEventById(int id) => events.FirstOrDefault(e => e.Id == id);

        public void AddEvent(Event newEvent)
        {
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
        public void SignUp(int eventId, string userId)
        {
            var ev = events.FirstOrDefault(e => e.Id == eventId);
            if (ev != null && !ev.SignedUpUserIds.Contains(userId))
            {
                if (ev.SignedUpUserIds.Count < ev.MaxParticipants)
                {
                    ev.SignedUpUserIds.Add(userId);
                    SaveEvents();
                }
                // Altrimenti: evento pieno
            }
        }
        public void Unsign(int eventId, string userId)
        {
            var ev = events.FirstOrDefault(e => e.Id == eventId);
            if (ev != null && ev.SignedUpUserIds.Contains(userId))
            {
                ev.SignedUpUserIds.Remove(userId);
                SaveEvents();
            }
        }

        private void LoadEvents()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedEvents = JsonSerializer.Deserialize<List<Event>>(json);
                if (loadedEvents != null)
                    events = loadedEvents;
            }
        }

        private void SaveEvents()
        {
            var json = JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, json);
        }

        private int GenerateId()
        {
            return events.Any() ? events.Max(e => e.Id) + 1 : 1;
        }
    }
}
