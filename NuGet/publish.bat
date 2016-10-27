del *.nupkg
nuget pack ../Plaid.Net/Plaid.Net.csproj -Prop Configuration=Release
nuget push Plaid.Net.*.nupkg