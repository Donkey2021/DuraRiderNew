using System;
namespace DuraRider.Helpers
{
    public static class CommonMethod
    {
        public static int GenerateOTP()
        {
            return new Random().Next(1000, 9999);
        }
    }
}
