using System;
using System.IO;

using Database.LiteDB.KeySwitches;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Interactors.KeySwitches.Cakewalk.Exporting;
using KeySwitchManager.Interactors.KeySwitches.StudioOne.Exporting;
using KeySwitchManager.Json.KeySwitches.Cakewalk.Translators;
using KeySwitchManager.Presenters.KeySwitch.Cakewalk;
using KeySwitchManager.Presenters.KeySwitches.StudioOneKeySwitch;
using KeySwitchManager.UseCases.KeySwitches.Cakewalk.Exporting;
using KeySwitchManager.UseCases.KeySwitches.StudioOne.Exporting;
using KeySwitchManager.Xml.KeySwitches.StudioOne.Translators;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.Cakewalk
{
    [TestFixture]
    public class ExportingCakewalkArticulationInteractorTest
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

            var inputData = new ExportingCakewalkArticulationRequest( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingCakewalkArticulationInteractor(
                dbRepository,
                new KeySwitchToCakewalkArticulationModel(),
                new IExportingCakewalkArticulationPresenter.Null()
            );

            var response = interactor.Execute( inputData );

            foreach( var i in response.Elements )
            {
                Console.WriteLine( $"Articulation of {i.KeySwitch.InstrumentName}" );
                Console.WriteLine( i.JsonText );
            }
        }
    }
}