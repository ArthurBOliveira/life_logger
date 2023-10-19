namespace life_logger_models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }
    }
}