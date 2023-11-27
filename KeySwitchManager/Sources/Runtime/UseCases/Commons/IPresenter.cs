using System;

namespace KeySwitchManager.UseCase.Commons
{
    [Obsolete( "Use IOutputPort instead" )]
    public interface IPresenter<in TResponse>
    {
        public void Progress( float progress )
        {}

        public void Present<T>( T param )
        {}

        public void Complete( TResponse response )
        {}
    }
}
