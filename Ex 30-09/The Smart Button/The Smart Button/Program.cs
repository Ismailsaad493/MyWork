using The_Smart_Button;

internal class Program
{
    static void Main(string[] args)
    {
        SmartButton redButton = new SmartButton();
        redButton.OnClick += () =>
        {
            Console.WriteLine("Lightbulb: Turning ON!");
        };

        redButton.OnClick += () =>
        {
            Console.WriteLine("Speaker: Playing doorbell chime!");
        };
        redButton.Press();

        Console.ReadLine();
    }

}