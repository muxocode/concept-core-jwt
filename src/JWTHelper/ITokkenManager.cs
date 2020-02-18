using JWTHelper.entities;
using Microsoft.AspNetCore.Http;

namespace JWTHelper
{
    public interface ITokkenManager
    {
        string Create(CustomTokken customTokken, double validMinutes = 30);
        CustomTokken GetTokken(HttpContext context);
    }
}