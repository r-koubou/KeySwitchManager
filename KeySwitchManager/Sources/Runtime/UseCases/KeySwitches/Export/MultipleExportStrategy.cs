using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class MultipleExportStrategy : IExportStrategy
    {
        private readonly Subject<KeySwitch> exported = new();
        IObservable<KeySwitch> IExportStrategy.OnExported => exported;

        private IExportContentWriterFactory ContentWriterFactory { get; }
        private IExportContentFactory ContentFactory { get; }

        public MultipleExportStrategy( IExportContentWriterFactory contentWriterFactory, IExportContentFactory contentFactory )
        {
            ContentWriterFactory = contentWriterFactory;
            ContentFactory       = contentFactory;
        }

        public async Task ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            foreach( var x in keySwitches )
            {
                if( cancellationToken.IsCancellationRequested )
                {
                    break;
                }

                var source = new[] { x };

                var content = await ContentFactory.CreateAsync( source , cancellationToken);
                if( cancellationToken.IsCancellationRequested )
                {
                    return;
                }

                var contentWriter = await ContentWriterFactory.CreateAsync( source, cancellationToken );
                if( cancellationToken.IsCancellationRequested )
                {
                    return;
                }

                await contentWriter.WriteAsync( content, cancellationToken );

                exported.OnNext( x );
            }
        }
    }
}
