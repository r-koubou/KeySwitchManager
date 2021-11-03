#!/bin/bash

args=""

for x in *.yaml ;do
    args="${args} ${x}"
done

../simple_codegen.sh $args
