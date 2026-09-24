using MediVerifyApp;

internal class Program
{
    static void Main(string[] args)
    {
        VerificationResponse<string> textResponse = new VerificationResponse<string>();
        textResponse.IsValid = true;
        textResponse.Payload = "Paracetamol - Batch 404 Authentic";
        VerificationResponse<int> idResponse = new VerificationResponse<int>();
        idResponse.IsValid = false;
        idResponse.Payload = 90210;

        Console.WriteLine("--- Pharmacist Verification Dashboard ---");

        textResponse.ShowResult();
        idResponse.ShowResult();

        Console.ReadLine();
    }

}