[CmdletBinding()]
param(
    [String]
    $nugetKey,

    [Switch]
    $runTests
)

begin {
    $ErrorActionPreference = 'Stop'

    #Download the latest stable `nuget.exe` (https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference)
    IF ($nugetKey -AND -NOT(Test-Path "nuget.exe"))
    {
        Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile 'nuget.exe'
    }

    #restore nuget packages for all projects
    $projectFiles = Get-ChildItem -Path '.' -Recurse -Filter "*.csproj" -Depth 7 -ErrorAction SilentlyContinue
    FOR($i = 0; $i -lt $projectFiles.Length; $i++){
        dotnet restore $projectFiles[$i].FullName;
    }

    #build package
    dotnet build .\ShipStation4Net\ShipStation4Net.csproj /p:Configuration=Release;

    #detect version from releaes notes file
    $releaseVersion = ([Regex]'(?<=#### )(.*?)(?= -)').Match((Get-Content 'release_notes.md' -First 1)).Value
    Write-Host "Release notes version: " $releaseVersion

    IF ($runTests){
        Set-Location "ShipStation4Net.Tests"
        
        #execute all tests
        dotnet test

        Set-Location ".."
    }

    #generate new package 
    dotnet pack ./ShipStation4Net/ShipStation4Net.csproj --output "../.nupkg" /p:Configuration=Release /p:PackageVersion=$releaseVersion

    #publish part
    IF($nugetKey){
        $currentNugetVersion = [version]((Invoke-RestMethod -uri 'https://api.nuget.org/v3/registration3/shipstation4net/index.json').items.upper)
        $version = [version]$releaseVersion;

        IF(($version -gt $currentNugetVersion)){
            .\nuget.exe push ".nupkg/ShipStation4Net.$releaseVersion.nupkg" $nugetKey -Source https://api.nuget.org/v3/index.json
            git tag -a $releaseVersion -m "Version $releaseVersion was pushed to nuget feed" > $null
            git push origin $releaseVersion --porcelain >$null
        }
    }
}