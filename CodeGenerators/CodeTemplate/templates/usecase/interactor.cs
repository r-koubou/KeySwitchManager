using KeySwitchManager.Domain.KeySwitches.Models;

namespace ${namespace}
{
    public class ${name}Interactor : I${name}UseCase
    {
        private IKeySwitchRepository Repository { get; }
        private I${name}Presenter Presenter { get; }

        public ${name}Interactor(
            IKeySwitchRepository repository ) :
            this( repository, new I${name}Presenter.Null() )
        {}

        public ${name}Interactor(
            IKeySwitchRepository repository,
            I${name}Presenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public ${name}Response Execute( ${name}Request request )
        {
        }
    }
}