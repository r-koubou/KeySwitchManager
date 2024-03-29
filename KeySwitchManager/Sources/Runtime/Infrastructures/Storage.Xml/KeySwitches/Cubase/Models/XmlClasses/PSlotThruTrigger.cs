namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Models.XmlClasses
{
    public static class PSlotThruTrigger
    {
        public static ObjectElement New()
        {
#if false
            <obj class="PSlotThruTrigger" name="remote" ID="1205851952">
               <int name="status" value="144"/>
               <int name="data1" value="-1"/>
            </obj>
#endif
            var obj = new ObjectElement( "PSlotThruTrigger" )
            {
                Name = "remote"
            };

            obj.Int.Add( new IntElement( "status", 144 ) );
            obj.Int.Add( new IntElement( "data1",  -1 ) );

            return obj;
        }
    }
}