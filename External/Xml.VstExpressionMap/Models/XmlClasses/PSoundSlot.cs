namespace KeySwitchManager.Xml.VstExpressionMap.Models.XmlClasses
{
    public static class PSoundSlot
    {
        #region xml
#if false
         <obj class="PSoundSlot" ID="1318271453">
            <obj class="PSlotThruTrigger" name="remote" ID="1357654234">
               <int name="status" value="144"/>
               <int name="data1" value="-1"/>
            </obj>
            <obj class="PSlotMidiAction" name="action" ID="3857577004">
               <int name="version" value="600"/>
               <member name="noteChanger">
                  <int name="ownership" value="1"/>
                  <list name="obj" type="obj">
                     <obj class="PSlotNoteChanger" ID="3893335653">
                        <int name="channel" value="-1"/>
                        <float name="velocityFact" value="1"/>
                        <float name="lengthFact" value="1"/>
                        <int name="minVelocity" value="0"/>
                        <int name="maxVelocity" value="127"/>
                        <int name="transpose" value="0"/>
                        <int name="minPitch" value="0"/>
                        <int name="maxPitch" value="127"/>
                     </obj>
                  </list>
               </member>
               <member name="midiMessages">
                  <int name="ownership" value="1"/>
               </member>
               <int name="channel" value="-1"/>
               <float name="velocityFact" value="1"/>
               <float name="lengthFact" value="1"/>
               <int name="minVelocity" value="0"/>
               <int name="maxVelocity" value="127"/>
               <int name="transpose" value="0"/>
               <int name="maxPitch" value="127"/>
               <int name="minPitch" value="0"/>

               <int name="key" value="-1"/>
            </obj>
            <member name="sv">
               <int name="ownership" value="2"/>
               <list name="obj" type="obj">
                  <obj class="USlotVisuals" ID="98496902">
                     <int name="displaytype" value="1"/>
                     <int name="articulationtype" value="1"/>
                     <int name="symbol" value="73"/>
                     <string name="text" value="IDLE" wide="true"/>
                     <string name="description" value="IDLE" wide="true"/>
                     <int name="group" value="0"/>
               </obj>
               </list>
            </member>
            <member name="name">
            <string name="s" value="IDLE" wide="true"/>
            </member>
            <int name="color" value="0"/>
         </obj>
         <obj class="PSoundSlot" ID="4111551162">
            <obj class="PSlotThruTrigger" name="remote" ID="2059916345">
               <int name="status" value="144"/>
               <int name="data1" value="-1"/>
            </obj>
            <obj class="PSlotMidiAction" name="action" ID="711831951">
               <int name="version" value="600"/>
               <member name="noteChanger">
                  <int name="ownership" value="1"/>
                  <list name="obj" type="obj">
                     <obj class="PSlotNoteChanger" ID="3328252152">
                        <int name="channel" value="-1"/>
                        <float name="velocityFact" value="1"/>
                        <float name="lengthFact" value="1"/>
                        <int name="minVelocity" value="0"/>
                        <int name="maxVelocity" value="127"/>
                        <int name="transpose" value="0"/>
                        <int name="minPitch" value="0"/>
                        <int name="maxPitch" value="127"/>
                     </obj>
                  </list>
               </member>
               <member name="midiMessages">
                  <int name="ownership" value="1"/>
                  <list name="obj" type="obj">
                     <obj class="POutputEvent" ID="1334620268">
                        <int name="status" value="144"/>
                        <int name="data1" value="0"/>
                        <int name="data2" value="100"/>
                     </obj>
                     <obj class="POutputEvent" ID="4196276652">
                        <int name="status" value="176"/>
                        <int name="data1" value="1"/>
                        <int name="data2" value="23"/>
                     </obj>

                  </list>
               </member>
               <int name="channel" value="-1"/>
               <float name="velocityFact" value="1"/>
               <float name="lengthFact" value="1"/>
               <int name="minVelocity" value="0"/>
               <int name="maxVelocity" value="127"/>
               <int name="transpose" value="0"/>
               <int name="maxPitch" value="127"/>
               <int name="minPitch" value="0"/>

               <int name="key" value="0"/>

            </obj>
            <member name="sv">
               <int name="ownership" value="2"/>
               <list name="obj" type="obj">
                  <obj class="USlotVisuals" ID="4092710359">
                     <int name="displaytype" value="1"/>
                     <int name="articulationtype" value="1"/>
                     <int name="symbol" value="73"/>
                     <string name="text" value="Power Chord" wide="true"/>
                     <string name="description" value="Power Chord" wide="true"/>
                     <int name="group" value="0"/>
               </obj>
               </list>
            </member>
            <member name="name">
            <string name="s" value="Power Chord" wide="true"/>
            </member>
            <int name="color" value="1"/>
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

        public static MemberElement Sv( ObjectElement slotVisual )
        {
           #region xml
#if false
            <member name="sv">
               <int name="ownership" value="2"/>
               <list name="obj" type="obj">
                  <obj class="USlotVisuals" ID="119171552">
                     <int name="displaytype" value="1"/>
                     <int name="articulationtype" value="1"/>
                     <int name="symbol" value="73"/>
                     <string name="text" value="ArticulationName" wide="true"/>
                     <string name="description" value="ArticulationName" wide="true"/>
                     <int name="group" value="0"/>
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

           list.Obj.Add( slotVisual );

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