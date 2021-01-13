namespace KeySwitchManager.CLI.Commands
{
    public interface ICommand
    {
        public int Execute( ICommandOption option );
    }
}