namespace Dal.Entities
{
    public partial class Author
    {
        public string FullName
        {
            get { return FirstName + "  " + LastName; }
        }
    }
}
