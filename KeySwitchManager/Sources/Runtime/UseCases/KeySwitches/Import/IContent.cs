using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IContent
    {
        // メモ：データのソース元ストリームを実装する
        // 例：
        // ファイル：FileStream を返す
        // HTTP：レスポンスのストリームを返す

        Stream GetContentStream()
            => GetContentStreamAsync().GetAwaiter().GetResult();

        Task<Stream> GetContentStreamAsync( CancellationToken cancellationToken = default );
    }
}
