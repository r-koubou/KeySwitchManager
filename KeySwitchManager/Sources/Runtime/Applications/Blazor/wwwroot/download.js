function downloadFromByteArray( base64Data, fileName )
{
    // Create binary data from Base64 strings
    const byteCharacters  = atob( base64Data );
    const byteNumbers  = new Array( byteCharacters.length );

    for( let i = 0; i < byteCharacters.length; i++ )
    {
        byteNumbers[ i ] = byteCharacters.charCodeAt( i );
    }

    const byteArray  = new Uint8Array( byteNumbers );

    // Create a Blob
    const blob  = new Blob( [ byteArray ], {
        type: "application/octet-stream"
    });

    // Creating URLs from Blobs
    const blobUrl  = URL.createObjectURL( blob );

    // Create a temporary a-element and download
    const link  = document.createElement( 'a' );
    document.body.appendChild( link ); // for Firefox
    link.href     = blobUrl;
    link.download = fileName;
    link.click();

    // Clean up URL
    URL.revokeObjectURL( blobUrl );
    document.body.removeChild( link ); // for Firefox
}
