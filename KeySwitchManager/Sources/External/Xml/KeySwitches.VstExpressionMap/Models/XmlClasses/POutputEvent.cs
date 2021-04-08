namespace KeySwitchManager.Xml.KeySwitches.VstExpressionMap.Models.XmlClasses
{
    public static class POutputEvent
    {
        public static ObjectElement New( int midiStatus, int data1, int data2 )
        {
#if false
            <obj class="POutputEvent" ID="4196276652">
               <int name="status" value="176"/>
               <int name="data1" value="1"/>
               <int name="data2" value="23"/>
            </obj>
#endif
            var obj = new ObjectElement( "POutputEvent" );

            obj.Int.Add( new IntElement( "status", midiStatus ) );
            obj.Int.Add( new IntElement( "data1",  data1 ) );
            obj.Int.Add( new IntElement( "data2",  data2 ) );

            return obj;
        }

    }
}