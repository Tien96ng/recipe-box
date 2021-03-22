using System.Collections.Generic;

namespace Recipe.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntities = new HashSet<RecipeUser> ();
    }

    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public List<string> Ingredients { get; set; } 
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<RecipeUser> JoinEntities { get; set; }
  }
}