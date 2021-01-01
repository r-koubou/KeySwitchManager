import sys
import template

OUTDIR = "out"
PREFIX = sys.argv[ 1 ]

TEMPLATES = [
    template.create_template( OUTDIR, PREFIX, PREFIX + "UseCase", "templatefiles/usecase/UseCase.cs" ),
    template.create_template( OUTDIR, PREFIX, PREFIX + "Interactor", "templatefiles/usecase/Interactor.cs" ),
    template.create_template( OUTDIR, PREFIX, PREFIX + "Request", "templatefiles/usecase/Request.cs" ),
    template.create_template( OUTDIR, PREFIX, PREFIX + "Response", "templatefiles/usecase/Response.cs" ),
    template.create_template( OUTDIR, PREFIX, PREFIX + "Presenter", "templatefiles/usecase/Presenter.cs" ),
]

template.save_decorated( OUTDIR, TEMPLATES )
