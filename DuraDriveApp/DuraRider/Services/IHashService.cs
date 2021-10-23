using System;
namespace DuraRider.Services
{
    public interface IHashService
    {
        string GenerateHashkey();
        void StartSMSRetriverReceiver();
    }
}
