namespace LoggingObservabilityDemoAPI.Services
{
    public class TestService
    {
        public string GetData(int id)
        {
            if (id == 0)
                throw new Exception("Invalid ID");

            if (id == 99)
                throw new Exception("Database failure simulation");

            return $"Data for ID: {id}";
        }
    }
}