namespace The_Beef.Domain.Entities;

public class Table
{
   public string Id { get; }
   public int Seats { get; }
   public bool IsAvailable { get; private set; }

   
   public Table(string id, int seats, bool isAvailable = true)
   {
      if (string.IsNullOrWhiteSpace(id))
         throw new ArgumentException("Id needed", nameof(id));
      if (seats <= 0)
         throw new ArgumentOutOfRangeException(nameof(seats), "Seats must be greater than zero.");

      Id = id.Trim();
      Seats = seats;
      IsAvailable = isAvailable;
   }
   
   public void MarkAsOccupied()
   {
      if (!IsAvailable)
         throw new InvalidOperationException("Table is already occupied.");
      
      IsAvailable = false;
   }
   public void MarkAsAvailable()
   {
      if (IsAvailable)
         throw new InvalidOperationException("Table is already available.");
      
      IsAvailable = true;
   }

}