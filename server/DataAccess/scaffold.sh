dotnet ef dbcontext scaffold \
  "Server=localhost;Database=testdb;User Id=testuser;Password=testpass;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --output-dir ./Models \
  --context-dir . \
  --context WebShopContext  \
  --no-onconfiguring \
  --force