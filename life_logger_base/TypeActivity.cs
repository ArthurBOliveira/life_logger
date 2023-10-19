namespace life_logger_models
{
    public class TypeActivity : BaseModel
    {
        public Guid? IdParent { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
    }
}
