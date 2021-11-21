using System;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    [Obsolete]
    public interface IExportDawUseCase
    {
        public ExportDawResponse Execute( ExportDawRequest request );
    }
}