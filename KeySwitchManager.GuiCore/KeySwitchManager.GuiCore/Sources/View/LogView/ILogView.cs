namespace KeySwitchManager.GuiCore.Sources.View.LogView
{
    public interface ILogView
    {
        void Append( LogViewModel model );
        void Clear();

        public class Null : ILogView
        {
            public void Append( LogViewModel model )
            {}

            public void Clear()
            {}
        }
    }
}
