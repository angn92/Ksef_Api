using JetBrains.Annotations;

namespace EmployeeDiaryModel.Model
{
    public class Certificate
    {
        public Guid Id { get; set; }
        public string CertificateName { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? MonthRelease { get; set; }
        public DateTime? YearRelease { get; set; }
        public DateTime? MonthValidTo { get; set; }
        public DateTime? YearValidTo { get; set; }

        public Certificate()
        {
        }

        public Certificate([NotNull] string certificateName, [NotNull] string releaseBy)
        {
            Id = Guid.NewGuid();
            CertificateName = certificateName;
            ReleasedBy = releaseBy;
        }
    }
}
