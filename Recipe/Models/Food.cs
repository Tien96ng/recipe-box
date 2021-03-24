using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Recipe.Models
{
  public class Food
  {
    public Food()
    {
      this.JoinEntities = new HashSet<FoodUser> ();
      // this.UserId = AppUser.Identity.GetUserId();

    }

    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public string Ingredients { get; set; }
    public string UserId { get; set; } 
    public virtual ApplicationUser AppUser { get; set; }
    public virtual ICollection<FoodUser> JoinEntities { get; set; }

  }
}