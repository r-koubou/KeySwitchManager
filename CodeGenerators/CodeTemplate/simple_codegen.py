import os
import sys
import glob
import string

import yaml
import jsonschema
import munch

THIS_SCRIPT_DIR      = os.path.dirname( os.path.abspath( __file__ ) )
SCHEMA_DIR           = os.path.join( THIS_SCRIPT_DIR, 'schema' )
TEMPLATE_DIR         = os.path.join( THIS_SCRIPT_DIR, 'templates' )
DEFAULT_OUTPUT_DIR   = 'out'
CONFIG_SCHEMA_FILE   = os.path.join( SCHEMA_DIR, 'config.yaml' )
TEMPLATE_SCHEMA_FILE = os.path.join( SCHEMA_DIR, 'template_info.yaml' )

PRINT_INDENT         = '  '

def print_indent( text, indent_depth = 0 ):
    print( "{indent}{text}".format(
        indent = PRINT_INDENT * indent_depth,
        text   = text
    ))

def create_munch( yaml_body, none_altanative = '' ):
    if type( yaml_body ) is list:
        result = []
        for x in yaml_body:
            result.append( create_munch( x, none_altanative ) )
        return result

    result = munch.DefaultMunch( '', yaml_body )
    for k in yaml_body.keys():
        if result[ k ] is None:
            result[ k ] = none_altanative

    return result

def validate_yaml( source, schema ):
    try:
        jsonschema.validate( source, schema )
    except Exception:
        raise Exception( 'Yaml Validation Failed!' )

def load_text( filename, encoding = 'utf8' ):
    with open( filename, mode = 'r', encoding = encoding ) as fp:
        return fp.read()

def preprocess_yaml( filename ):
    text = load_text( filename )
    yaml_body = create_munch( yaml.safe_load( text ) )
    if len( yaml_body.variables ) == 0:
        return yaml_body

    vars_table = {}
    for x in yaml_body.variables:
        vars_table.update(x)

    template = string.Template( text )
    text = template.substitute( vars_table )
    result = create_munch( yaml.safe_load( text ) )
    return result

def load_yaml( filename ) :
    return preprocess_yaml( filename )

def load_template( filename ):
    return string.Template( load_text( filename ) )

def load_template_table():
    template_table  = create_munch( {} )
    template_schema = load_yaml( TEMPLATE_SCHEMA_FILE )

    for x in glob.glob( os.path.join( TEMPLATE_DIR, '**', '*.yaml' ), recursive = True ):
        template_infos = load_yaml( x )
        validate_yaml( template_infos, template_schema )

        for t in template_infos.templates:
            template_table[ t[ 'name' ] ] = munch.DefaultMunch( '', t )

    return template_table

def generate_code( template, parameter_dict = {} ):
    return template.safe_substitute( parameter_dict )

def generate_fully_classname( class_info, template_info ):
    return "{name_prefix}{name}{name_suffix}".format(
            name_prefix = template_info.prefix,
            name        = class_info.name,
            name_suffix = template_info.suffix
    )

def process_output( code_text, config_data, class_info, template_meta ):

    template_info = config_data.template_table[ template_meta.name ]

    file_name_prefix = ''
    file_name_suffix = ''

    if len( template_meta.file_name_prefix ) > 0:
        file_name_prefix = template_meta.file_name_prefix
    if len( template_meta.file_name_suffix ) > 0:
        file_name_suffix = template_meta.file_name_suffix

    file_name = "{prefix}{name}{suffix}".format(
        prefix = file_name_prefix,
        name = generate_fully_classname( class_info, template_info ),
        suffix = file_name_suffix
    )

    output_filename = "{name}{suffix}".format(
        name        = file_name,
        suffix      = config_data.suffix
    )

    output_dir = DEFAULT_OUTPUT_DIR
    if len( template_meta.output_dir ) > 0:
        output_dir = template_meta.output_dir
    elif len( config_data.output_dir ) > 0:
        output_dir = config_data.output_dir

    output_path = os.path.join( output_dir, output_filename )

    print_indent( output_path, 2 )

    os.makedirs( output_dir, exist_ok = True )

    with open( output_path, 'w', encoding = 'utf8' ) as fp:
        fp.write( code_text )

def generate_replace_variables( config_data, class_info, template_meta, template_info ):
    values = create_munch( {} )
    values.classname    = generate_fully_classname( class_info, template_info )
    values.prefix       = template_info.prefix
    values.suffix       = template_info.suffix
    values.name         = class_info.name
    values.description  = class_info.description

    if len( template_meta.namespace ) > 0 :
        values.namespace = template_meta.namespace
    elif len( class_info.namespace ) > 0 :
        values.namespace = class_info.namespace
    else:
        values.namespace = config_data.namespace

    if type( class_info.user_variables ) is dict:
        values.update( class_info.user_variables )

    return values

def process_template( config_data, class_info, template_meta ):

    if not template_meta.name in config_data.template_table:
        print_indent( "  template: `{name}` is not found.".format( name = template_meta.name ), 1 )
        return

    template_info = config_data.template_table[ template_meta.name ]
    template_text = load_template( os.path.join( TEMPLATE_DIR, template_info.path ) )

    values = generate_replace_variables( config_data, class_info, template_meta, template_info )
    # print_indent( values, 1 )

    code_text = generate_code( template_text, values )
    process_output( code_text, config_data, class_info, template_meta )

def process_class( config_data, class_info ):
    print_indent( class_info.name, 1 )
    for x in class_info.templates:
        process_template( config_data, class_info, create_munch( x ) )

def main( argv ):
    for input_file in argv:
        print_indent( input_file )

        schema_data = load_yaml( CONFIG_SCHEMA_FILE )
        config_data = load_yaml( input_file )
        validate_yaml( config_data, schema_data )

        config_data.template_table = load_template_table()

        for x in config_data.classes:
            process_class( config_data, create_munch( x ) )

def usage():
    print( "python {script} <config file>".format( script=os.path.basename( __file__ ) ) )
    print( "see config file schema : {dir}/schema/config.yaml".format( dir=THIS_SCRIPT_DIR ) )

if __name__ == '__main__':
    if len( sys.argv ) > 1:
        main( sys.argv[ 1: ] )
    else:
        usage()
