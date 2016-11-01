namespace Theseus
{
    public interface ISubcommand
    {
        string GetDescription();

        string GetUsage();

        void Execute(string[] args);
    }
}
