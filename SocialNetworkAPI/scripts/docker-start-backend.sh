docker rm $(docker stop $(docker ps -a -q --filter ancestor=socialnetworkapi --format="{{.ID}}"))
docker rmi -f socialnetworkapi_api:latest
docker-compose -f ../docker-compose.yaml up
#docker-compose -f ../docker-compose.yaml up