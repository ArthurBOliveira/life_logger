namespace life_logger_models
{
    public class Activity : BaseModel
    {
        public Guid IdType { get; set; }
        public Guid IdLabel { get; set; }

        public string Details { get; set; }
    }
}
