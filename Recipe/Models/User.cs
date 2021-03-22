using System.Collections.Generic;

namespace Recipe.Models
{
  public class User
  {
    public User()
    {
      this.JoinEntities = new HashSet<RecipeUser> ();
    }

    public int UserId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser AppUser { get; set; }
    public virtual ICollection<RecipeUser> JoinEntities { get; set; }
  }
}