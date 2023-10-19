namespace life_logger_models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
    }
}
