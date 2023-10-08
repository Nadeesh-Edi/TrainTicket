namespace TrainTicketApi.Models
{
    public class Station
    {
        public string name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Station other = (Station)obj;
            return name == other.name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}
