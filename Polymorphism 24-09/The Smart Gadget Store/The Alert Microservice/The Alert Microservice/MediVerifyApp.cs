using System;

namespace MediVerifyApp
{
    public class VerificationResponse<T>
    {
        public bool IsValid { get; set; }

        public T Payload { get; set; }

        public void ShowResult()
        {
            Console.WriteLine("Is Valid: " + IsValid + " | Payload Data: " + Payload);
        }
    }

}