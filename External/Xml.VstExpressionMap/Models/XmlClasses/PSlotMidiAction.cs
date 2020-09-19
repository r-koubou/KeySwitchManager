namespace KeySwitchManager.Xml.VstExpressionMap.Models.XmlClasses
{
    public static class PSlotMidiAction
    {
       #region xml
#if false
            <obj class="PSlotMidiAction" name="action" ID="1015024336">
               <int name="version" value="600"/>
               <member name="noteChanger">
                  <int name="ownership" value="1"/>
                  <list name="obj" type="obj">
                     <obj class="PSlotNoteChanger" ID="1015887872">
                        <int name="channel" value="0"/>
                        <float name="velocityFact" value="0.25"/>
                        <float name="lengthFact" value="0.5"/>
                        <int name="minVelocity" value="0"/>
                        <int name="maxVelocity" value="127"/>
                        <int name="transpose" value="48"/>
                        <int name="minPitch" value="0"/>
                        <int name="maxPitch" value="127"/>
                     </obj>
                  </list>
               </member>
               <member name="midiMessages">
                  <int name="ownership" value="1"/>
                  <list name="obj" type="obj">
                     <obj class="POutputEvent" ID="1027366928">
                        <int name="status" value="144"/>
                        <int name="data1" value="0"/>
                        <int name="data2" value="100"/>
                     </obj>
                     <obj class="POutputEvent" ID="810426000">
                        <int name="status" value="176"/>
                        <int name="data1" value="1"/>
                        <int name="data2" value="23"/>
                     </obj>
                     <obj class="POutputEvent" ID="1027361616">
                        <int name="status" value="192"/>
                        <int name="data1" value="15"/>
                        <int name="data2" value="0"/>
                     </obj>
                  </list>
               </member>
               <int name="channel" value="0"/>
               <float name="velocityFact" value="0.25"/>
               <float name="lengthFact" value="0.5"/>
               <int name="minVelocity" value="0"/>
               <int name="maxVelocity" value="127"/>
               <int name="transpose" value="48"/>
               <int name="maxPitch" value="127"/>
               <int name="minPitch" value="0"/>
               <int name="key" value="0"/>
               <int name="controller1num" value="1"/>
               <int name="controller1value" value="23"/>
               <int name="program" value="16"/>
            </obj>
#endif
       #endregion

        public static ObjectElement New( ListElement listOfPOutputEvent )
        {
           var obj = new ObjectElement( "PSlotMidiAction" )
           {
              Name = "action"
           };

           obj.Int.Add( new IntElement( "version", 600 ) );

           obj.Member.Add( NoteChanger() );
           obj.Member.Add( MidiMessages( listOfPOutputEvent ) );

           obj.Int.Add( new IntElement( "channel", -1 ) );
           obj.Float.Add( new FloatElement( "velocityFact", 1f ) );
           obj.Float.Add( new FloatElement( "lengthFact",   1f ) );
           obj.Int.Add( new IntElement( "minVelocity", 0 ) );
           obj.Int.Add( new IntElement( "maxVelocity", 127 ) );
           obj.Int.Add( new IntElement( "transpose",   0 ) );
           obj.Int.Add( new IntElement( "minPitch",    0 ) );
           obj.Int.Add( new IntElement( "maxPitch",    127 ) );
           obj.Int.Add( new IntElement( "key",         0 ) );

            return obj;
        }

        private static MemberElement NoteChanger()
        {
            var member = new MemberElement( "noteChanger" );
            member.Int.Add( new IntElement( "ownership", 1 ) );

            var slotNoteChangers = new ListElement( "obj", "obj" );
            slotNoteChangers.Obj.Add( PSlotNoteChanger.New() );
            member.List.Add( slotNoteChangers );

            return member;
        }

        private static MemberElement MidiMessages( ListElement listOfPOutputEvent )
        {
           var member = new MemberElement( "midiMessages" );
           member.Int.Add( new IntElement( "ownership", 1 ) );

           if( listOfPOutputEvent.Obj.Count > 0 )
           {
              member.List.Add( listOfPOutputEvent );
           }

           return member;
        }
    }
}