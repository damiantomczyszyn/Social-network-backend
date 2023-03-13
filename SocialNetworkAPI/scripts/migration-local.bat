cd ../SocialNetwork.Migrations
dotnet publish -c Release
dotnet bin/release/net6.0/publish/SocialNetwork.Migrations.dll Server=localhost;Port=3306;Database=social-network;User=root;Password=root;
cd ../scripts