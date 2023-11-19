namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IKeySwitchRepositoryFactory
    {
        public IKeySwitchRepository Create();
    }
}
