dotnet build -c Debug
Remove-Item './ci/cob' -Recurse -ErrorAction SilentlyContinue
dotnet test -c Debug --no-build -r ./ci/cob --collect:"XPlat Code Coverage" --test-adapter-path:. --logger:"junit;LogFilePath=./../../ci/junit-test-result.xml;MethodFormat=Class;FailureBodyFormat=Verbose"
dotnet new tool-manifest --force
dotnet tool update dotnet-reportgenerator-globaltool --version 5.1.4
dotnet reportgenerator -reports:./ci/cob/*/coverage.cobertura.xml -targetdir:./ci/coverage-html -reporttypes:Html -historydir:./ci/coverage-history
