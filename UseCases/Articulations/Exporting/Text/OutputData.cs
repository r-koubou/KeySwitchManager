using System.Collections.Generic;

using ArticulationManager.Domain.Commons;

namespace ArticulationManager.UseCases.Articulations.Exporting.Text
{
    public class OutputData
    {
        public IEnumerable<IText> TextDataList { get; }

        public OutputData( IEnumerable<IText> textDataList )
        {
            TextDataList = new List<IText>( textDataList );
        }
    }
}