using EmployeeDiaryModel.Enums;

namespace KsefCore.Model
{
    public class ClientSession
    {
        public Guid Id { get; set; }
        public string SessionId { get; set; }
        public SessionStatus SessionStatus { get; set; }

        public ClientSession()
        {
        }
    }
}
