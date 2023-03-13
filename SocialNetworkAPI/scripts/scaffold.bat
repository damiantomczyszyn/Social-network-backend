@echo off
cd ../SocialNetwork.Data
dotnet restore
dotnet ef dbcontext scaffold "Server=localhost;Port=3306;Database=social-network;User=root;Password=root;" "Pomelo.EntityFrameworkCore.MySql" -o Model -f -v -c BaseContext
echo Zakonczono scaffolding bazy danych
pause