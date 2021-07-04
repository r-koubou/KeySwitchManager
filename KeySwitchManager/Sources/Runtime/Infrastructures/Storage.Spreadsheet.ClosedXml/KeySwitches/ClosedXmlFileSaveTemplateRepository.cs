using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlFileSaveTemplateRepository : SaveOnlyKeySwitchFileRepository
    {
        private FilePath Target { get; }

        public ClosedXmlFileSaveTemplateRepository( FilePath target ) : base( target )
        {
            Target = target;
        }

        #region Save
        public sealed override int Flush()
        {
            if( !KeySwitches.Any() )
            {
                return 0;
            }

            var keySwitch = KeySwitches.First();
            XlsxWorkBookWriter.Write( new[]{ keySwitch }, Target, LoggingSubject );

            return 1;

        }
        #endregion

    }
}
