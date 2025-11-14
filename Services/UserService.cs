using EventCardCopilotApp.Models;
using System.Security.Claims; 

namespace EventCardCopilotApp.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _http;
 

        private readonly EventNewService _event;


        public UserService(EventNewService eventService, IHttpContextAccessor http)
        {
            _event = eventService;
            _http = http;
        }
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "jdoe", Fullname = "John Doe", ImageUrl = "/avatar/img_avatar.png" },
            new User { Id = 2, Username = "asmith", Fullname = "Alice Smith", ImageUrl = "/avatar/img_avatar2.png" },
            new User { Id = 3, Username = "bwilliams", Fullname = "Bob Williams", ImageUrl = "/avatar/avatar2.png" },
            new User { Id = 4, Username = "cjones", Fullname = "Carol Jones", ImageUrl = "/avatar/avatar5.png" },
            new User { Id = 5, Username = "dlee", Fullname = "David Lee", ImageUrl = "/avatar/avatar6.png" }

        };
        public List<User> GetAllUsers()
        {
            return _users;
        }
        public List<User> GetAllUsers(int eventId)
        {
            var evento = _event.GetEventById(eventId);
            if (evento != null)
            {

                var ids = evento.SignedUpUserIds;
                return _users.Where(u => !ids.Contains(u.Id)).ToList();

            }
            else
            {
                return _users;
            }

        }
        public List<User> GetUsersMember(int eventId)
        {
            var evento = _event.GetEventById(eventId);
            if (evento != null){

                var ids = evento.SignedUpUserIds;
                return _users.Where(u => ids.Contains(u.Id)).ToList();

            } else{
                return _users;
            }
        
        }

        public ClaimsPrincipal CreateClaims(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            return new ClaimsPrincipal(identity);
        }


        public string GetUsername()
        {
            return _http.HttpContext?.User?.Identity?.Name ?? "Anonimo";
        } 
        public int  GetIdUser()
        {
            string name = _http.HttpContext?.User?.Identity?.Name ?? "Anonimo";
            if (!name.Equals("Anonimo"))
            {
                var user = _users.FirstOrDefault(u => u.Username.Equals(name));
                    return user?.Id??0;
            }
            return 0;
        } 

    }
}