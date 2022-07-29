using System.Security.Claims;

namespace BookMarked.WebMVC.Controllers
{
    public static class UserUtility
    {
        public static Guid GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value; 
            //returns a string, First (a search, gets the first element that matches the expression in the parenthesis),
            //claim (variable) uses First to pick the first claim that matches the NammeIdentifier. 
            if (userIdClaim == null) return default; //make sure string is not null
            return Guid.Parse(userIdClaim); //parsing string into a guid
        }
    }
}
