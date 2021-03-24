using System.Collections.Generic;

namespace Recipe.Models
{
  public class User
  {
    public User()
    {
      this.JoinEntities = new HashSet<FoodUser> ();
    }

    public int UserId { get; set; }
    public string Name { get; set; }
    public virtual ApplicationUser AppUser { get; set; }
    public virtual ICollection<FoodUser> JoinEntities { get; set; }
  }
}