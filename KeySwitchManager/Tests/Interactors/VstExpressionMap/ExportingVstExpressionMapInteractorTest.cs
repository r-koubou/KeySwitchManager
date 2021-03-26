using System;
using System.IO;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Interactors.KeySwitch.VstExpressionMap.Exporting;
using KeySwitchManager.Presenters.VstExpressionMap;
using KeySwitchManager.UseCases.KeySwitches.VstExpressionMap.Exporting;
using KeySwitchManager.Xml.KeySwitch.VstExpressionMap.Translation;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.VstExpressionMap
{
    [TestFixture]
    public class ExportingVstExpressionMapInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            #region Adding To DB
            var dbRepository = new LiteDbKeySwitchRepository( new MemoryStream() );
            var entity = TestDataGenerator.CreateKeySwitch();
            dbRepository.Save( entity );
            #endregion

            var inputData = new ExportingVstExpressionMapRequest( entity.DeveloperName.Value, entity.ProductName.Value );
            var interactor = new ExportingVstExpressionMapInteractor(
                dbRepository,
                new KeySwitchToVstExpressionMapModel(),
                 new IExportingVstExpressionMapPresenter.Null()
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