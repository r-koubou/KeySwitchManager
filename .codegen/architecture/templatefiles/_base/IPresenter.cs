namespace path.to.your.ns
{
    public interface IPresenter<in TResponse> : System.IDisposable
    {
        public void Start()
        {}

        public void Progress( float progress )
        {}

        public void Present<T>( T param )
        {}

        public void Complete( TResponse response )
        {}

        public void Dispose()
        {}
    }
}