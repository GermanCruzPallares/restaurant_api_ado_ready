namespace RestauranteAPI.Models
{
   public class ComboCreateDto
   {
      public int PlatoPrincipalId { get; set; }
      public int BebidaId { get; set; }
      public int PostreId { get; set; }
      public double Descuento { get; set; }
   }
}
