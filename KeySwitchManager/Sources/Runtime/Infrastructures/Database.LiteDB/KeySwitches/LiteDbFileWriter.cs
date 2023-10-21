using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches
{
    public class LiteDbFileWriter : IKeySwitchWriter
    {
        private IKeySwitchRepository Repository { get; }

        public bool LeaveOpen => false;

        public LiteDbFileWriter( FilePath dbFilePath )
        {
            Repository = new LiteDbRepository( dbFilePath );
        }

        public void Dispose()
        {
            Disposer.Dispose( Repository );
        }

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject )
        {
            foreach( var x in keySwitches )
            {
                loggingSubject?.OnNext( x.ToString() );
                await Repository.SaveAsync( x );
            }
        }
    }
}
