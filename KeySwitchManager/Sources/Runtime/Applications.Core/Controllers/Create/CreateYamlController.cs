using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Create.Text;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateYamlController : IController
    {
        private IKeySwitchWriter Writer { get; }
        private ICreateTextTemplatePresenter Presenter { get; }

        public CreateYamlController(
            IKeySwitchWriter writer,
            ICreateTextTemplatePresenter presenter )
        {
            Writer    = writer;
            Presenter = presenter;
        }

        public void Dispose()
        {
            if( !Writer.LeaveOpen )
            {
                Disposer.Dispose( Writer );
            }
        }

        public void Execute()
        {
            var source = new List<KeySwitch>
            {
                KeySwitchFactoryHelper.CreateTemplate()
            };

            Writer.Write( source );

            var response = new CreateTextTemplateResponse();
            Presenter.Complete( response );
        }
    }
}
