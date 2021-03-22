namespace Recipe.Models
{
  public class RecipeUser
  {
    public int RecipeUserId { get; set; }
    public int UserId { get; set; }
    public int RecipeId { get; set; }
    public virtual User User { get; set; }
    public virtual Recipe Recipe { get; set; }
  }
}