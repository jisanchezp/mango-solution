using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Frontend.MVC.Constants
{
    public static class RoleConstants
    {
        public const string ADMIN_ROLE = "ADMIN";
        public const string USER_ROLE = "USER";

        public static List<SelectListItem> rolesSelectList = new(){
            new SelectListItem { Text = ADMIN_ROLE, Value = ADMIN_ROLE },
            new SelectListItem { Text = USER_ROLE, Value = USER_ROLE }
        };
    }
}
