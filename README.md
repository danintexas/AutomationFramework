# Kindergarten

## A simple to use and maintain automation framework for web testing

This automation framework is built from the ground up as an simple to use and maintain solution. 

All you need is Visual Studio 19 to write tests along with installing .NET Core 2.2!

If you want to just run a test just install .NET Core 2.2 and run through command line use the following in the solution folder: `dotnet test`

## From its initial design this Kindergarten was designed to do two things:
1. Help bring manual Q/A engineers up into the Automation world quickly while not needing full development skills and knowledge allowing them to ease into an automation role. 
2. Allow nearly anyone to quickly develop Web Automation tests without the Framework getting in the way.

Any comments, suggestions or complaints please reach out to me with them. I am looking to fine tune this Framework and I am always looking to make it better and with more features. 
This is a passion project and something I will be working on for the foreseeable future.

Daniel Gail<br />
Work Email: danielg@rumbleon.com<br />
Personal Email: daniel.gail@gmail.com

## Tech Used:
- .NET Core 2.2
- NUnit 3.12.0
- Extent Reports 1.0.2
- Selenium 3.141.0
- OpenPop 2.0.6.1120
- ADO 4.6.1

## Supports the following browsers:
- Chrome
- Firefox
- Edge

## Known Issues:
- SendKeys is kind of 'hacked' with .NET Core 2.2. A red X dialog comes up the first time it is called in a test. This can be closed
	and the method will work fine going forward. Believe this is because to get this to work I am calling a few .NET 4.6 Framework 
	files that technically is not supported. This will be resolved with .NET Core 3.0 due to be released mid Sept. 

## Planned To-Do List:
- Update Readme to full documentation for Framework use
- Upgrade to Core 3.0
- Upload Logs to Slack Channel
- Concurrent testing through ASYNC/Selenium Grid and allow concurrent browser testing
- Built in API endpoint testing. 
- Built in API stress testing.

## Folder Structure 
- `Framework Root`
  - `appsettings.json`: Contains all needed entries for the framework. Most of this is for Reports. Please ensure all this information is filled out or included in the appsettings.local.json which overrides the appsettings.json
  - `AutomationFramework.sln`: Visual Studio Solution file. Open this up to open, edit, or work with this framework.
  - `README.md`: This file you are reading
- `Core Folder`: As the name implies this is where the core of the framework resides.
  - `AdditionalFunctions.cs`: This file contains additional methods more to better manage an ever increasing core.cs file
  - `Core.cs`: As you can probably guess this is where the heart of the framework is. I have tried to make all code easy to read so if there is something here you think needs improving please let me know. 
- `Json Repo`: If the core folder is the heart of the framework this is the brains. Houses all needed input and locators for the framework to utilize. Please note ANY .JSON files dropped in this folder will be scanned on build time. 
  - `Json Backup Folder`: This folder is ignored during project build time and test execution. Simple folder to store backups of JSON files you do not want to use. This folder is not used or looked at by the framework.
  - `Json Override Folder`: Any JSON files put into this folder will override any other JSON files in the base repo file. Useful if you want to use an override for a specific JSON entry.
  - `VIN Store`: This file is special and used by the GenerateVIN method. If you call this method, you need to ensure this file is there. 
- `Support Folder`: This folder contains all side files needed for the framework. 
  - `Image Bank Folder`: Storage folder used for images used by the framework.
  - `Support DLLs Folder`: Currently houses .NET 4.6.1 DLLs to get SendKeys working with .NET Core 2.2. Once the framework is updated to 3.0 this folder will go away.
  - `chromedriver.exe`: `geckodriver.exe` : `MicrosoftWebDriver.exe`: Selenium browser drivers.
  - `Kill All Selenium Instances.bat`: Helpful batch file that will kill all Selenium Browser drivers running. This is helpful when writing tests and problems happen. 
- `Tests Folder`: This is where all the magic happens. The automation tests! Any .cs files in this folder will be included in the NUNIT Test Explorer.
   - `Assorted Tests Archive`: This is where I store my development tests. This folder is ignored during project build time. Tests in this folder do not appear in the NUNIT Test Explorer.
   - `RumbleOn`: All tests related to RumbleOn
- `c:\Automation Logs`: This is where the Extent reports, screenshots, and all ancillary files are stored for your test runs.

## Using the Framework
First off I will not go into great detail about the code of Kindergarten past just saying I have tried my hardest to name all variables and methods to where you should understand what is going on under the hood. 
Any where I feel needs a comment I have done so in the code. All methods that can be called from a test have filled out and up to date XML with it. You should be able to see what a method does and expects through
Visual Studio. If you see any where I have missed something please let me know. 

- Quickest way to get going with Kindergarten is to grab the `Test Template.cs` file from the `Tests\Assorted Tests Archive`, copy it to the base `Tests` folder and rename the commented fields. At that point when you 
build the project you should see that test appear in the NUNIT Test Explorer. 

### Currently supported methods you can use in your tests
- `ClickElement`: This method is used to have Selenium click on any element in a website like a form field or button. 
   - This method has two parameters that need to be passed. Locator type and the path to the locator. Example:
   ```
     ClickElement(XPath, "//button[@type='Login']");
   ```
    The above will use a XPath locator with the second parameter being the XPath locator. 

   - One tip I would suggest is instead of hardcoding your locators is to have a separate JSON file with your locators and then use the `JSONCALL` method to call that locator. This way you do not have to modify your tests.
- `DatabaseCheck`: This will query the database for a single value using a SQL command and ensures it is a specific value.
  - This method has two parameters that need to be passed. The SQL query and the expected value. Example: 
  ```
     DatabaseCheck("select UserId from ClsListing where ListingId = 111", "61807");
  ```
  - Again, a tip I would provide is combine the above with JSONCALL - don't hard code your tests.
- `GenerateAVIN`: This is a custom-made module for RumbleOn. This will look at the VIN Store.json file in the Json Repo and randomly pull a VIN out of that file. 
  - This method has an option where you can specify `Motorcycle` - `Car` - `Truck` - `Offroad`. If no option is passed the method will randomly choose one of the four. 
  The information pulled will populate the following variables which you can combine with `DatabaseCheck`, `ShouldBe`, or `ShouldNotBe` methods. 
  - `vinUnderTest`
  - `yearUnderTest`
  - `makeUnderTest`
  - `modelUnderTest`
  - `trimUnderTest`
- `GetAllEmailsFromAnEmailAccount`: Simple wrapper method to call the 'FetchAllMessages' method which pulls all emails from an email account using POP3.<br />To use this you need to ensure that your appsettings.local.JSON file has all the EmaiInformation filled out. 
  - This method has one optional parameter you can pass `true` to. If you do this the method will pull all the email down from that account and delete the emails as well. Useful to ensure the account is left clean after your testing. 
- `GetFieldValue`: This is a Selenium command that will return the value of a field on a website. 
  - Method has two arguments. Locator type and the path to the locator. Example:
  ```
     GetFieldValue(XPath, "//button[@type='Login']");
  ```
    The above will use a XPath locator with the second parameter being the XPath locator. 
- `JsonCall`: Simple method that does a ton to ensure you do not have to rewrite your tests. Call this method and the value passed is a variable entry in one of your JSON files in the JSON Repo.
  - Example:
  ```
     JsonCall("GoogleHomePage:URL");
  ```
- `Logger`: Method that writes information into the after-test execution Extent logs. 
  - Two needed arguments supported - Type of report and the string value to write into the report. 
  - Example:
  ```
     Logger(Info, "This will show up in the report!");
  ```
  - The above will show up as an informational line in the logs for that test. Useful to put into logs what flow you are testing or what behavior you are expecting from your test to better track it. 
  - Supported Report types:
    - `Info`: Normal information line. 
	- `Pass`: This will mark the line in the report as a passed item. 
	- `Fail`: This will mark the line in the report as a failed item.
- `MaximizeBrowser`: This is a Selenium command that simply sets the current active Selenium controlled browser into a full screen state.
- `ParseAllEmailFilesForAStringValue`: Method will search in the `c:\Automation Logs\{Date}\Emails` folder for a specific string in all the emails downloaded with the `GetAllEmailsFromAnEmailAccount` method. 
This will fail or pass the current test if the string is not found. It is recommended to run this after all other tests are completed. 
  - Argument passed is a string to parse the emails for. Again, reccomend using JSONCALL for this. 
  - Example:
  ```
     ParseAllEmailFilesForAStringValue("This text should be in one of the email files.");
  ```
- `ScreenShot`: This is a simple method to take a screenshot of the currently active Selenium controlled browser. Single argument passed which is the name of the screenshot. 
  - Example:
  ```
     ScreenShot("Picture of Homepage");
  ```
  - This method automatically saves the screenshot into the Automation Logs folder and adds them to the current Extent report for that test. 
- `SendKeys`: This sends a set of keyboard commands to a specific element within the Selenium controlled browser.
  - Three arguments are needed for this method. 
    - Locator type
	- Locator (Again combine with JSONCALL)
	- String to send
  - Example:
  ```
     SendKeys(XPath, "//button[@type='Login']", "Type this message in the box.");
  ```
- `ShouldBe`: Very simple method that compares two string values and ensures they are the same.
  - Two arguments are needed for this. 
    - Value #1 to compare to #2
	- Value #2 to compare to #1
- `ShouldNotBe`: Method is the same as `ShouldBe` but ensures the values are not the same.
- `StripEndingUrl`: Custom method for RumbleOn that strips all characters after the last `/` in a string. This is was made to strip the ending of a URL for RumbleOn.
- `Wait`: Method that takes in a numeric value as an argument and tells Selenium to wait for that number of seconds. This is considered an implicit wait.
- `WaitForElement`: This is a Selenium command that will pause a test until Selenium sees a specific element on a webpage.
  - Two arguments are needed for this method. The third is optional.
    - Locator type
	- Locator (Again combine with JSONCALL)
	- Numeric time to wait for the element. Default is 30 seconds and if this argument is not passed Selenium will wait 30 seconds before failing the test step.
  - Example:
  ```
     WaitForElement(XPath, "//button[@type='Login']", "Type this message in the box.", 30);
  ```

### Methods you can use but are more used as support of the framework
- `CloseBrowser`: This method closes the current Selenium controlled browser.
- `CloseQuitBrowsers`: This closes all active Selenium controlled browsers in addition it also ends all Selenium controlled processes. Should know that this method is called at the end of a test run automatically by the framework.
- `QuitBrowsers`: Quits all Selenium controlled browser processes. 
- `UseBrowser`: Internal method that tells Selenium what browser to run. Currently supports: `chrome` - `firefox` - `edge`