using System;
using System.IO;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Interactors.StudioOneKeySwitch.Exporting;
using KeySwitchManager.Presenters.StudioOneKeySwitch;
using KeySwitchManager.UseCases.KeySwitch.StudioOne.Exporting;
using KeySwitchManager.Xml.KeySwitch.StudioOne.Translation;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.StudioOneKeySwitch
{
    [TestFixture]
    public class ExportingStudioOneKeySwitchInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Adding To DB
            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var articulation = TestDataGenerator.CreateArticulation(
                new MidiNoteOn[]{ IMidiNoteOnFactory.Default.Create( 1, 23 ) },
                new MidiControlChange[]{},
                new MidiProgramChange[]{}
            );
            var entity = TestDataGenerator.CreateKeySwitch( articulation );
            dbRepository.Save( entity );
            #endregion

            var inputData = new ExportingStudioOneKeySwitchRequest( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingStudioOneKeySwitchInteractor(
                dbRepository,
                new KeySwitchToStudioOneKeySwitchModel(),
                new IExportingStudioOneKeySwitchPresenter.Null()
            );

            var response = interactor.Execute( inputData );

            foreach( var i in response.Elements )
            {
                Console.WriteLine( $"Expressionmap of {i.KeySwitch.InstrumentName}" );
                Console.WriteLine( i.XmlText );
            }
        }
    }
}