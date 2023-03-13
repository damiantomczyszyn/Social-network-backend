cd ../SocialNetwork.Migrations
dotnet publish -c Release
dotnet bin/release/net6.0/publish/SocialNetwork.Migrations.dll Server=eu-cdbr-west-02.cleardb.net;Port=3306;Database=heroku_6e749be67ba2a24;User=b834b7b78ada01;Password=c75de127;
cd ../scripts