using System.Collections.Generic;

namespace KeySwitchManager.Xml.KeySwitch.VstExpressionMap.Model.XmlClasses
{
    public static class PSoundSlot
    {
        #region xml
#if false
         <obj class="PSoundSlot" ID="1318271453">
            <obj class="PSlotThruTrigger" name="remote" ID="1357654234">
            :
            </obj>
            <obj class="PSlotMidiAction" name="action" ID="3857577004">
            :
            </obj>
            <member name="sv">
               <int name="ownership" value="2"/>
               <list name="obj" type="obj">
                  <obj class="USlotVisuals" ID="98496902">
                     <int name="displaytype" value="1"/>
                     <int name="articulationtype" value="1"/>
                     <int name="symbol" value="0"/>
                     <string name="text" value="##Articulation Name##" wide="true"/>
                     <string name="description" value="##Description##" wide="true"/>
                     <int name="group" value="0"/>
                  </obj>
                  <obj class="USlotVisuals" ID="49865566326">
                  :
                  </obj>
               </list>
            </member>
            <member name="name">
               <string name="s" value="##Slot Name##" wide="true"/>
            </member>
            <int name="color" value="0"/>
         </obj>
         <obj class="PSoundSlot" ID="4111551162">
         :
         </obj>
#endif
        #endregion

        public static ObjectElement New( string slotName = "", int color = -1 )
        {
           var obj = new ObjectElement( "PSoundSlot" );

           var m = new MemberElement( "name" );
           m.String.Add( new StringElement( "s", slotName ) );
           obj.Member.Add( m );

           if( color >= 0 )
           {
              obj.Int.Add( new IntElement( "color", color ) );
           }

           return obj;
        }

        public static MemberElement Sv( IReadOnlyCollection<ObjectElement> slotVisualList )
        {
           #region xml
#if false
            <member name="sv">
               <int name="ownership" value="1"/>
               <list name="obj" type="obj">
                  <obj class="USlotVisuals" ID="98496902">
                     <int name="displaytype" value="1"/>
                     <int name="articulationtype" value="1"/>
                     <int name="symbol" value="0"/>
                     <string name="text" value="##Articulation Name##" wide="true"/>
                     <string name="description" value="##Description##" wide="true"/>
                     <int name="group" value="0"/>
                  </obj>
                  <obj class="USlotVisuals" ID="0000000000">
                  :
                  </obj>
               </list>
            </member>
#endif
           #endregion

           var member = new MemberElement( "sv" );
           member.Int.Add( new IntElement( "ownership", 2 ) );

           var list = new ListElement
           {
              Name = "obj",
              Type = "obj"
           };

           foreach( var obj in slotVisualList )
           {
              list.Obj.Add( obj );
           }

           member.List.Add( list );

           return member;
        }

        public static MemberElement Name( string slotName )
        {
           #region xml
#if false
            <member name="name">
               <string name="s" value="SlotName" wide="true"/>
            </member>
#endif
           #endregion

           var member = new MemberElement( "name" );
           member.String.Add( new StringElement("s", slotName ) );

           return member;
        }
    }
}