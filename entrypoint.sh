#!/bin/bash

# this script is run when the docker container is built
# it imports the base database structure and create the database for the tests

DATABASE_NAME="coutries"

echo "*** CREATING DATABASE ***"

# create default database
gosu postgres postgres --single <<EOSQL
  CREATE DATABASE "$DATABASE_NAME";
  GRANT ALL PRIVILEGES ON DATABASE "$DATABASE_NAME" TO postgres;
EOSQL


echo "*** DATABASE CREATED! ***"