import sys
import template

def main( argv ):

    prefix = argv[ 0 ]
    outdir = "out"
    options = {
        "__minvalue__": "int.MinValue + 1",
        "__maxvalue__": "int.MaxValue - 1",
    }

    if len( argv ) > 1:
        options = template.parse_option( argv, options )

    TEMPLATES = [
        template.create_template( outdir, prefix, prefix, "templatefiles/model/integer.cs", options )
    ]

    template.save_decorated( outdir, TEMPLATES )

if __name__ == "__main__":
    if len( sys.argv ) == 1:
        pass
    else:
        main( sys.argv[1:] )
