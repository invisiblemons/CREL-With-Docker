FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN apt-get update && \
    apt-get install -y gnupg2 && \
	apt-get update && \
	apt-get install -y dos2unix && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY . .

RUN dos2unix init-data.sh entrypoint.sh schema-data.sql && \
    chmod +x init-data.sh entrypoint.sh

CMD /bin/bash /app/entrypoint.sh
