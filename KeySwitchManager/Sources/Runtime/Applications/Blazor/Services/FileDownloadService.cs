using Microsoft.JSInterop;

namespace KeySwitchManager.Applications.Blazor.Services;

public class FileDownloadService
{
    private readonly IJSRuntime jsRuntime;

    public FileDownloadService( IJSRuntime jsRuntime )
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task TriggerDownloadAsync( byte[] byteArray, string fileName )
    {
        var base64Data = Convert.ToBase64String( byteArray );
        await jsRuntime.InvokeVoidAsync( "downloadFromByteArray", base64Data, fileName );
    }
}
