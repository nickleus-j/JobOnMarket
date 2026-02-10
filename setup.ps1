# Ask user for DB name and SA password
$dbName = Read-Host "Enter the database name"
$saPassword = Read-Host "Enter the SA password" -AsSecureString
$plainPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
    [Runtime.InteropServices.Marshal]::SecureStringToBSTR($saPassword)
)


# Write values to .env file
@"
DB_NAME=$dbName
SA_PASSWORD=$plainPassword
"@ | Set-Content -Path ".env"

# Build and start containers
docker-compose up -d --build


# Apply EF Core migrations (which also seed data)
#docker exec jobsonmarket dotnet ef database update --project JobsOnMarket.csproj
