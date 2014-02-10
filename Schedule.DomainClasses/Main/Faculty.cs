namespace Schedule.DomainClasses.Main
{
    public class Faculty
    {
        public Faculty()
        {
            
        }

        public Faculty(string name, string letter, int sortingOrder)
        {
            Name = name;
            Letter = letter;
            SortingOrder = sortingOrder;
        }

        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Letter { get; set; }
        public int SortingOrder { get; set; }
    }
}
