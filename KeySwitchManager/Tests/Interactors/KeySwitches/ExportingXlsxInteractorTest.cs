using System;
using System.Collections.Generic;
using System.IO;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Entity;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.KeySwitches
{
    [TestFixture]
    public class ExportingXlsxInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Insert new keyswitch to db
            var memoryDb = new MemoryStream();
            var dbRepository = new LiteDbKeySwitchRepository( memoryDb );

            var entity = IKeySwitchFactory.Default.Create(
                Guid.NewGuid(),
                "Author",
                "Description",
                DateTime.Now,
                DateTime.Now,
                "Developer Name",
                "Product name",
                "Instrument name",
                new List<Articulation>
                {
                    IArticulationFactory.Default.Create(
                        "name",
                        new List<IMidiMessage>{ IMidiNoteOnFactory.Default.Create( 0, 100 )},
                        new List<IMidiMessage>{ IMidiControlChangeFactory.Default.Create( 1, 100 )},
                        new List<IMidiMessage>{ IMidiProgramChangeFactory.Default.Create( 2, 34 )},
                        new Dictionary<string, string>
                        {
                            { "extra1 key", "extra1 value" },
                            { "extra2 key", "extra2 value" },
                        }
                    ),
                },
                new Dictionary<string, string>
                {
                    { "extra1 key", "extra1 value" },
                    { "extra2 key", "extra2 value" },
                }
            );

            dbRepository.Save( entity );
            #endregion

            var xlsxRepository = new XlsxExportingRepository( new DirectoryPath( "." ) );
            var interactor = new ExportingXlsxInteractor( xlsxRepository );
            var response = interactor.Execute( new ExportingXlsxRequest( new[] { entity } ) );

            Assert.IsTrue( response.Result );
        }
    }
}