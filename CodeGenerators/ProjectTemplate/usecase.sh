#!/bin/bash

python ./gen_project.py usecase UseCases.$1
python ./gen_project.py usecase Interactors.$1
python ./gen_project.py usecase Presenters.$1
