import os
import sys
import re

PROJECT_NAME_PREFIX = 'KeySwitchManager.'

THIS_DIR = os.path.dirname( sys.argv[ 0 ] )
SUFFIX = '.csproj'
PROJECTNAME_PATTERN = '$$PROJECT_NAME$$'

def generate( project_type, project_name ):

    template_file = os.path.join(THIS_DIR, "Template.{type}.csproj".format(type=project_type) )

    with open( template_file ) as f:
        template = f.read()

    new_text = template.replace( PROJECTNAME_PATTERN, PROJECT_NAME_PREFIX + project_name )

    if not os.path.exists( project_name ):
        os.mkdir( project_name )

    with open( os.path.join( project_name, project_name + SUFFIX ), 'w' ) as f:
        f.write( new_text )

    print( "Done : {name}".format( name=project_name ) )

if __name__ == '__main__':
    generate( sys.argv[ 1 ], sys.argv[ 2 ] )