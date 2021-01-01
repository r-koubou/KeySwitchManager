import sys
import template

argv        = sys.argv[1:]

OUTDIR      = "out"
TEMPLATE    = argv[ 0 ]
NAME        = argv[ 1 ]
OPTIONS     = {}

if len( argv ) > 2:
    OPTIONS = template.parse_option( argv[ 2: ], OPTIONS )

TEMPLATES = [
    template.create_template( OUTDIR, NAME, NAME, "templatefiles/{template}.cs".format(template=TEMPLATE), OPTIONS )
]

template.save_decorated( OUTDIR, TEMPLATES )
