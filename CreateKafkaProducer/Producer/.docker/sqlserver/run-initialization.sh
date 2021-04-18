#!/bin/bash
# Wait to be sure that SQL Server came up

sleep 30s
# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P MyP4ssw0rd_ -d master -i create-database.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P MyP4ssw0rd_ -d master -i seed.sql