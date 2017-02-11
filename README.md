Simple Asp.Net Core app using external Overwatch API.

Provides example on how to do E2E, IntegrationTests and UnitTests.

Assumptions:
---------------------------
.NET Core available - https://www.microsoft.com/net/core#windowsvs2015

Chrome >= 49.0, Firefox >= 44

To support more browser, I would have to do transpilation magic for the let keyword.

Free ports:
* 2222 - used by faked api
* 22222 - use by main app
* 9515 - Chrome - WebDriver

Running Tests:
---------------------------

Tests should start in Visual Studio runner (I am using R# 10 and there is a bug with xproj projects).
E2E test will run and fail with VS runner without setup. 

To run E2E tests.
1. download Chrome Web Driver - https://chromedriver.storage.googleapis.com/2.27/chromedriver_win32.zip
2. Run Chrome Driver
3. Run the app (VS or 'dotnet run')
    * 'dotnet restore' - downloads the packages
4. Run the tests ('dotnet test' in the E2E project folder)

Preferable way of running tests is 'dotnet test' from command line as this will execute only tests assiociated with the xproj in the folder.