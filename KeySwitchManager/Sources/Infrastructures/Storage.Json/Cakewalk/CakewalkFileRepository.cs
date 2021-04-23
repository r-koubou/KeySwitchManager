using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Helpers;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk
{
    public class CakewalkFileRepository : SaveOnlyKeySwitchFileRepository
    {
        protected DirectoryPath TargetDirectory { get; }

        public CakewalkFileRepository( DirectoryPath targetDirectory ) :
            base( targetDirectory )
        {
            TargetDirectory = targetDirectory;
        }

        #region Write to file
        public override int Flush()
        {
            var saved = 0;

            // TODO 個別でファイルに書き出さず、すべての要素を束ねた 1 JSONファイルにしたい(Cakewalkは格納できる)
            // Cakewalkに他フォーマットからのインポーターが搭載されているので優先度は低
            foreach( var x in KeySwitches )
            {
                var outputFilePath = new FilePath(
                    CreatePathHelper.CreateFilePath( x, ".artmap", TargetDirectory ).Path
                );

                using var stream = File.Create( outputFilePath.Path );
                CakewalkFileWriter.Write( stream, x );
                saved++;
            }

            return saved;
        }
        #endregion

    }
}
