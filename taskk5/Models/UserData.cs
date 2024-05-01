namespace taskk5.Models
{
    public class UserData
    {
        public string Identifier { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"ID: {Identifier}, Name: {FullName}, Address: {Address}, Phone: {Phone}";
        }
    }
}
