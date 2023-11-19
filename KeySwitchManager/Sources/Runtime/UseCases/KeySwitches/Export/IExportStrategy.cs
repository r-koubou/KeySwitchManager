using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportStrategy
    {
        IObservable<KeySwitch> OnExported { get; }

        Task ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default );
    }
}
