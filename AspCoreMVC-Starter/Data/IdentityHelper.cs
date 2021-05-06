using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMVC_Starter.Data
{
    public class IdentityHelper
    {
        internal static void ConfigureIdentitySettings(IdentityOptions options)
        {
            // TODO: Configure your password requirements
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 1;

            // TODO: Configure user options
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-,_@+";
            options.User.RequireUniqueEmail = false;

            // TODO: Configure signin options
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // TODO: Configure lockout options
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
        }
    }
}
