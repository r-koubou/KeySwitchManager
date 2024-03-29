﻿using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.KeySwitches.Find;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public sealed class FindPresenter : IFindPresenter
    {
        public static readonly IFindPresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public FindPresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleAsync( FindOutputData outputData, CancellationToken cancellationToken = default )
        {
            var sb = new StringBuilder( 128 );
            var keySwitches = outputData.Value.Result;

            foreach( var k in keySwitches.OrderBy( x => x.DeveloperName.Value )
                                         .ThenBy( x => x.ProductName.Value )
                                         .ThenBy( x => x.InstrumentName.Value ) )
            {
                sb.Clear();
                sb.Append( k );
                TextView.Append( sb.ToString() );
            }

            TextView.Append( "---------------------------------" );
            TextView.Append( $"{keySwitches.Count} record(s) found" );
            TextView.Append( "Complete" );

            TextView.ScrollToEnd();

            await Task.CompletedTask;
        }

        #region NullObject
        private class NullImpl : IFindPresenter
        {
            public async Task HandleAsync( FindOutputData outputData, CancellationToken cancellationToken = default )
                => await Task.CompletedTask;
        }
        #endregion
    }
}
