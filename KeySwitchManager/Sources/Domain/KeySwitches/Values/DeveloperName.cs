using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    [ValueObject(typeof(string))]
    [NotEmpty]
    public partial class DeveloperName {}
}
