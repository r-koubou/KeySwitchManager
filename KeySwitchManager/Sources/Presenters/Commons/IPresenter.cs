namespace KeySwitchManager.Presenters.Commons
{
    public interface IPresenter<in TResponse>
    {
        public void Start()
        {}

        public void Progress( float progress )
        {}

        public void Present<T>( T param )
        {}

        public void Complete( TResponse response )
        {}
    }
}