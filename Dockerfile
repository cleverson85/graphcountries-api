FROM postgres:11-alpine
RUN mkdir -p /tmp/psql_data/

COPY script.sql /tmp/psql_data/
COPY entrypoint.sh /docker-entrypoint-initdb.d/

ENTRYPOINT entrypoint.sh
