# ShipStation4Net
A .NET Standard Library using System.Net.HttpClient and NewtonSoft.Json which provides access to the ShipStation API.

[![Build status](https://ci.appveyor.com/api/projects/status/u3qj2igpmxepnuoe/branch/master?svg=true)](https://ci.appveyor.com/project/nla-brandonjames/shipstation4net/branch/master)

## Testing
The ShipStation API Credentials are ignored by the `.gitignore` file. Its name is `configuration.json`. You should create a new configuration file in the json format with the following key value pairs as they are read by Json.Net into the Configuration object:


    {
        "UserName": "<required | your username>",
        "UserApiKey": "<required | your password>",
        "PartnerId": "<optional | your partner id if you have purchased a higher rate limit>"
    }


. Appveyor CI has a copy of my company's API credentials encrypted into `appveyor.yml`, so AppVeyor should run with our rates and rate limits on every build. The test files in the `ShipStation4Net.Tests` project have hardcoded test values into them from our database. If you want to test against your own data, you will need to replace the hardcoded values from the test files in the test project with your own.