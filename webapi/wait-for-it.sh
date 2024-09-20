#!/usr/bin/env bash
host="$1"
shift
until nc -z "$host" 1433; do
  echo "Waiting for SQL Server..."
  sleep 2
done
exec "$@"