using System;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public sealed class CreateInteractor : ICreateUseCase
    {
        private IOutputPort<CreateOutputData> OutputPort { get; }

        public CreateInteractor( IOutputPort<CreateOutputData> outputPort )
        {
            OutputPort = outputPort;
        }

        public async Task HandleAsync( CreateInputData inputData, CancellationToken cancellationToken = default )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            CreateOutputData outputData;

            try
            {
                await inputData.Value.ExportAsync( new[] { keyswitch }, cancellationToken );
                outputData = new CreateOutputData( true, keyswitch, null );
            }
            catch( Exception e )
            {
                outputData = new CreateOutputData( false, null!, e );
            }

            await OutputPort.HandleAsync( outputData, cancellationToken );
        }
    }
}
