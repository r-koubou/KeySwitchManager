#!/bin/bash

pushd `dirname $0` > /dev/null
this_dir=`pwd`
popd > /dev/null

pipenv run python $this_dir/simple_codegen.py "${@}"