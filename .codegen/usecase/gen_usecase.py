
import sys
import os

argv   = sys.argv[1:]
PREFIX = argv[ 0 ]

#region Load
def load_impl( soutce_path, prefix = PREFIX ):
        with open( soutce_path, mode = 'r' ) as f:
            return f.read().format(
                name = prefix
            )

# Load from template file
TEMPLATES = [
    {
        "code" : load_impl( "UseCase.heredoc.cs" ),
        "path" : "out/I{name}UseCase.cs".format( name = PREFIX )
    },
    {
        "code" : load_impl( "Interactor.heredoc.cs" ),
        "path" : "out/{name}Interactor.cs".format( name = PREFIX )
    },
    {
        "code" : load_impl( "Interactor.testing.heredoc.cs" ),
        "path" : "out/{name}InteractorTest.cs".format( name = PREFIX )
    },
    {
        "code" : load_impl( "Request.heredoc.cs" ),
        "path" : "out/{name}Request.cs".format( name = PREFIX )
    },
    {
        "code" : load_impl( "Response.heredoc.cs" ),
        "path" : "out/{name}Response.cs".format( name = PREFIX )
    },
    {
        "code" : load_impl( "Presenter.heredoc.cs" ),
        "path" : "out/I{name}Presenter.cs".format( name = PREFIX )
    },
]
#endregion

#region Output
print( '--------------------------------' )
print( PREFIX )
print( '--------------------------------' )

os.makedirs( "out", exist_ok = True )

for x in TEMPLATES:
    print( x[ 'path' ] )
    with open( x[ 'path' ], mode = "w", encoding = "utf-8" ) as f:
        f.write( x[ 'code' ] )
#endregion