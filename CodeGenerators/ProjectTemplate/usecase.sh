#!/bin/bash

python ./gen_project.py module UseCase.$1
python ./gen_project.py module Interactor.$1
