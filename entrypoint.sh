!/bin/sh

: ${HOST:=localhost}
: ${PORT:=5432}
: ${POSTGRES_DB:=graphcountries}
: ${POSTGRES_USER:=postgres}
: ${POSTGRES_PASSWORD:=docker}

until nc -z $HOST $PORT
do
    echo "Waiting for PSQL ($HOST:$PORT) to start..."
    sleep 0.5
done
  
echo "Start entrypoint - RUNTIME"

CONTAINER_ALREADY_STARTED="Container already started"

if [ ! -e $CONTAINER_ALREADY_STARTED ]; then
    touch $CONTAINER_ALREADY_STARTED
    echo "-- First container startup --"
    /bin/sh -c "dotnet ef database update"
else
    echo "-- Not first container startup --"
fi
