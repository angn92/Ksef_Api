namespace KsefInfrastructure.Validation
{
    public static class Fail
    {
        public static void IfNull(string param)
        {
            if(String.IsNullOrWhiteSpace(param))
                throw new ArgumentNullException(nameof(param));
        }
    }
}
