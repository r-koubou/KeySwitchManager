using System.Collections.Generic;

using ArticulationManager.Domain.Commons;

namespace ArticulationManager.UseCases.Articulations.Exporting.Text
{
    public class OutputData
    {
        public IText Text { get; }

        public OutputData( IText text )
        {
            Text = text;
        }
    }
}