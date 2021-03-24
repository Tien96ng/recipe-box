namespace Recipe.Models
{
  public class FoodUser
  {
    public int FoodUserId { get; set; }
    public int FoodId { get; set; }
    public int UserId { get; set; }
    public virtual Food Food { get; set; }
    public virtual User User { get; set; } 
  }
}