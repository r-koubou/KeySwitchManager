using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Xml.Cubase.Models
{
    [XmlRoot( ElementName = "InstrumentMap" )]
    public class RootElement
    {

        [XmlElement( ElementName = "string" )]
        public StringElement StringElement { get; set; }

        [XmlElement( ElementName = "member" )]
        public List<MemberElement> Member { get; set; } = new List<MemberElement>();

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public RootElement()
        {
            StringElement = new StringElement( "name", string.Empty );
        }

        public RootElement( string name )
        {
            StringElement = new StringElement( "name", name );
        }

        public RootElement( StringElement stringElement, IEnumerable<MemberElement> members )
        {
            StringElement = stringElement;
            Member.AddRange( members );
        }
    }
}