using System;
using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches
{
    public class LiteDbFileWriter : IKeySwitchWriter
    {
        private LiteDbRepository Repository { get; }

        public bool LeaveOpen => false;

        public LiteDbFileWriter( FilePath dbFilePath )
        {
            Repository = new LiteDbRepository( dbFilePath );
        }

        public void Dispose()
        {
            Disposer.Dispose( Repository );
        }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
        {
            foreach( var x in keySwitches )
            {
                loggingSubject?.OnNext( x.ToString() );
                Repository.Save( x );
            }
        }
    }
}
