using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportContentReader
    {
        // メモ：content から得られるストリームを元にファイルタイプ毎の読み込みの具体を実装する
        Task<IReadOnlyCollection<KeySwitch>> ReadAsync( IContent content, CancellationToken cancellationToken = default );
    }
}
