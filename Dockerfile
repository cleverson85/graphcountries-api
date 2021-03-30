FROM alpine

WORKDIR /app

COPY entrypoint.sh .
RUN chmod 700 ./entrypoint.sh

ENTRYPOINT [ "/bin/sh", "./entrypoint.sh" ]