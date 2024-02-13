using EmployeeDiaryModel.Enums;
using JetBrains.Annotations;

namespace KsefCore.Model
{
    public class AuthorizationToken : BaseEntity
    {
        public Guid Id { get; set; }
        public string Nip { get; set; }
        public string Token { get; set; }
        public TokenStatus Status { get; set; }

        public AuthorizationToken()
        {
        }

        public AuthorizationToken([NotNull] string identifier, [NotNull] string token)
        {
            Id = Guid.NewGuid();
            Nip = identifier;
            Token = token;
            ActiveteTokenStatus();
        }

        public void ActiveteTokenStatus()
        {
            this.Status = TokenStatus.Active;
        }

        public void DeactivateTokenStatus()
        {
            this.Status = TokenStatus.NotActive;
        }
    }
}
