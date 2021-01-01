
import sys
import os

def parse_option( argv, options ):
    for arg in argv:
        kvs = arg.split( "=" )
        if len( kvs ) < 2:
            continue
        options[ kvs[0].strip() ] = kvs[1].strip()

    return options

#region Load
def load_template( soutce_path, prefix, options = {} ):
    with open( soutce_path, mode = 'r' ) as f:
        content = f.read()
        content = content.replace( "__name__", prefix )

        for (k, v) in options.items():
            content = content.replace( k, v )

        return content

def save_decorated( outdir, templates ):
    os.makedirs( "out", exist_ok = True )
    for x in templates:
        print( x[ 'path' ] )
        with open( x[ 'path' ], mode = "w", encoding = "utf-8" ) as f:
            f.write( x[ 'code' ] )

def create_template( outdir, name, outname, template_file, options = {} ):
    return {
        "code" : load_template( template_file, name, options ),
        "path" : "{outdir}/{outname}.cs".format(
            outdir=outdir,
            outname=outname
        )
    }
