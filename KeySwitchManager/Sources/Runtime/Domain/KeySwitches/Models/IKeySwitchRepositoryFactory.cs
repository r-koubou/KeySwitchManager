namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchRepositoryFactory
    {
        public IKeySwitchRepository Create();
    }
}
