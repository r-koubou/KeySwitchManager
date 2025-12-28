import os
import sys
import re

AUTHOR                      = 'R-Koubou'
PROJECT_NAME_PREFIX         = 'KeySwitchManager.'
PROJECT_TEST_NAME_PREFIX    = 'KeySwitchManager.Testing.'
TARGET_LANGVERSION          = '9'
TARGET_FRAMEWORK            = 'netstandard2.1'
REPO_URL                    = 'https://github.com/r-koubou/KeySwitchManager'
PROJECT_TYPE_MODULE         = 'module'
PROJECT_TYPE_CLI            = 'cliapp'

THIS_DIR = os.path.dirname( sys.argv[ 0 ] )
SUFFIX = '.csproj'

def replace( template, options, is_test_project ):

    project_name = options[ 'project_name' ]
    project_type = options[ 'project_type' ]

    project_name_prefix = PROJECT_TEST_NAME_PREFIX if is_test_project else PROJECT_NAME_PREFIX

    if project_type == PROJECT_TYPE_CLI:
        new_text = template.replace( '$$PROJECT_NAME$$', project_name_prefix + 'Apps.'+ project_name )
    else:
        new_text = template.replace( '$$PROJECT_NAME$$', project_name_prefix + project_name )

    new_text = new_text.replace( '$$LANGVER$$', TARGET_LANGVERSION )
    new_text = new_text.replace( '$$FRAMEWORK$$', TARGET_FRAMEWORK )
    new_text = new_text.replace( '$$AUTHOR$$', AUTHOR )
    new_text = new_text.replace( '$$REPO_URL$$', REPO_URL )

    return new_text

def generate( project_type, project_name, is_test_project ):

    template_file = os.path.join(THIS_DIR, "Template.{type}.csproj".format(type=project_type) )

    if not os.path.exists( template_file ):
        return

    with open( template_file ) as f:
        template = f.read()

    new_text = replace( template,
        {
            'project_type': project_type,
            'project_name': project_name
        },
        is_test_project
    )

    dest_dir = os.path.join( 'out', project_type, project_name )

    if not os.path.exists( dest_dir ):
        os.makedirs( dest_dir )

    with open( os.path.join( dest_dir, project_name + SUFFIX ), 'w' ) as f:
        f.write( new_text )

    print( "Done : {name}".format( name=project_name ) )

if __name__ == '__main__':
    generate( sys.argv[ 1 ], sys.argv[ 2 ], False )
    generate( sys.argv[ 1 ] + ".Testing", sys.argv[ 2 ], True )