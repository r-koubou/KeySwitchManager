using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlFileSaveTemplateRepository : SaveOnlyFileRepository
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
            XlsxWorkBookWriter.Write( new[]{ keySwitch }, Target );

            return 1;

        }
        #endregion

    }
}
