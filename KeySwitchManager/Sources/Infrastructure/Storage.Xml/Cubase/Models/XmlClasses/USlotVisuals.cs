namespace KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Models.XmlClasses
{
    public static class USlotVisuals
    {
        public static ObjectElement New( string slotName, string description, int symbol, int articulationType, int group )
        {
#if false
         <obj class="USlotVisuals" ID="3316603083">
            <int name="displaytype" value="1"/>
            <int name="articulationtype" value="****TYPE****"/>
            <int name="symbol" value="0"/>
            <string name="text" value="****NAME*****" wide="true"/>
            <string name="description" value="****DESC****" wide="true"/>
            <int name="group" value="****GROUP****"/>
         </obj>
#endif
            var obj = new ObjectElement( "USlotVisuals" );
            obj.Int.Add( new IntElement( "displaytype",      1 ) );
            obj.Int.Add( new IntElement( "articulationtype", articulationType ) );
            obj.Int.Add( new IntElement( "symbol",           symbol ) );
            obj.String.Add( new StringElement( "text",        slotName ) );
            obj.String.Add( new StringElement( "description", description ) );
            obj.Int.Add( new IntElement( "group", group ) );

            return obj;

        }
    }
}