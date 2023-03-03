FROM mcr.microsoft.com/mssql/server:2019-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=12341234Minh@

USER root

# Create app directory
RUN mkdir -p /opt/mssql-scripts
WORKDIR /opt/mssql-scripts

# Grant permissions to the scripts directory
RUN chmod -R 777 /opt/mssql-scripts

# Copy SQL scripts
COPY schema-data.sql .

EXPOSE 1433/tcp
EXPOSE 1434/udp

# Start SQL Server and import data
CMD /opt/mssql/bin/sqlservr & sleep 10s && /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -i schema-data.sql && while true; do sleep 1; done;

