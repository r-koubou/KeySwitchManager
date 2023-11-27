using System;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Boundaries;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class CreateInteractor : ICreateUseCase
    {
        private IOutputPort<CreateResponse> OutputPort { get; }

        public CreateInteractor( IOutputPort<CreateResponse> outputPort )
        {
            OutputPort = outputPort;
        }

        public async Task HandleAsync( CreateRequest request, CancellationToken cancellationToken = default )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            CreateResponse response;

            try
            {
                await request.Request.ExportAsync( new[] { keyswitch }, cancellationToken );
                response = new CreateResponse( true, keyswitch, null );
            }
            catch( Exception e )
            {
                response = new CreateResponse( false, null!, e );
            }

            await OutputPort.HandleAsync( response, cancellationToken );
        }
    }
}
