namespace KeySwitchManager.UseCase.KeySwitches.Dump
{
    public class DumpFileResponse
    {
        public int DumpDataCount { get; }

        public DumpFileResponse( int dumpDataCount )
        {
            DumpDataCount = dumpDataCount;
        }
    }
}