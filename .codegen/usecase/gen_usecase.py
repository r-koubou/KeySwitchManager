
import sys
import os

argv = sys.argv[1:]

with open( "UseCase.heredoc.cs", mode = "r", encoding = "utf-8") as f:
    TEMPLATE_USECASE = f.read().format(
        name = argv[ 0 ]
    )

with open( "Interactor.heredoc.cs", mode = "r", encoding = "utf-8") as f:
    TEMPLATE_INTERACTOR = f.read().format(
        name = argv[ 0 ]
    )

with open( "Request.heredoc.cs", mode = "r", encoding = "utf-8") as f:
    TEMPLATE_REQUEST = f.read().format(
        name = argv[ 0 ]
    )

with open( "Response.heredoc.cs", mode = "r", encoding = "utf-8") as f:
    TEMPLATE_RESPONSE = f.read().format(
        name = argv[ 0 ]
    )


os.makedirs( "out", exist_ok = True )

with open( "out/I{name}UseCase.cs".format( name = argv[ 0 ] ), mode = "w", encoding = "utf-8") as f:
    f.write( TEMPLATE_USECASE )

with open( "out/{name}Interactor.cs".format( name = argv[ 0 ] ), mode = "w", encoding = "utf-8") as f:
    f.write( TEMPLATE_INTERACTOR )

with open( "out/{name}Request.cs".format( name = argv[ 0 ] ), mode = "w", encoding = "utf-8") as f:
    f.write( TEMPLATE_REQUEST )

with open( "out/{name}Response.cs".format( name = argv[ 0 ] ), mode = "w", encoding = "utf-8") as f:
    f.write( TEMPLATE_RESPONSE )
