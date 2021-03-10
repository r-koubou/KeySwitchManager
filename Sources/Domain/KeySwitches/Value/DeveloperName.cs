using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    [ValueObject(typeof(string))]
    [NotEmpty]
    public partial class DeveloperName {}
}
