#!/bin/bash
sleep 10

# Create the database
echo "Creating database + Importing data..."
/opt/mssql-tools/bin/sqlcmd -S SE140811\SQLEXPRESS -U sa -P $SA_PASSWORD -i schema-data.sql
echo "Create + Data imported."
docker run -e 'ACCEPT_EULA=Y' --name mssql -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -it -d microsoft/mssql-server-linux:latest /bin/bash