namespace KeySwitchManager.UseCase.Commons
{
    public interface IPresenter<in TResponse>
    {
        public void Start()
        {}

        public void Progress( float progress )
        {}

        public void Present<T>( T param )
        {}

        public void Message( string message )
        {}

        public void Complete( TResponse response )
        {}
    }
}