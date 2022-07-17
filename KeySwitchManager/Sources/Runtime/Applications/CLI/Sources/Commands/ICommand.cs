namespace KeySwitchManager.Applications.CLI.Commands
{
    public interface ICommand
    {
        public int Execute( ICommandOption option );
    }
}