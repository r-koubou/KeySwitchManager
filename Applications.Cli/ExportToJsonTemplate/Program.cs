using System;

using KeySwitchManager.Interactors.KeySwitches.Exporting.Text;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;

namespace KeySwitchManager.App.ExportToJsonTemplate
{
    public class Program
    {
        public static void Main( string[] args )
        {
            var presenter =  new IExportingTextPresenter.Null();
            var interactor = new ExportingTemplateJsonInteractor(
                presenter,
                new KeySwitchListListToJsonModelList
                {
                    Formatted = true
                }
            );

            var response = interactor.Execute();
            Console.WriteLine( response.Text );
        }
    }
}