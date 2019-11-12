# .NET Automation Famework

## A simple to use and maintain automation framework for web testing

This automation framework is built from the ground up as an simple to use and maintain solution. 

All you need is Visual Studio 19 to write tests along with installing .NET Core 2.2!

If you want to just run a test just install .NET Core 2.2 and run through command line use the following in the solution folder:<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;`dotnet test`

## From its initial design this .NET Framework was designed to do two things:
1. Help bring manual Q/A engineers up into the Automation world quickly while not needing full development skills and knowledge allowing them to ease into an automation role. 
2. Allow nearly anyone to quickly develop Web Automation tests without the Framework getting in the way.

Any comments, suggestions or complaints please reach out to me with them. I am looking to fine tune this Framework and I am always looking to make it better and with more features. 
This is a passion project and something I will be working on for the foreseeable future.

Daniel Gail<br />
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

## Supports the following browsers in headless mode:
- Chrome
- Firefox

## Planned To-Do List:
- Upgrade to Core 3.0
- Upload Logs to Slack Channel & AWS S3 bucket as well as Slack notifications for pass/fail
- Xamarin integration to run mobile app testing on most devices
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
  - `Json Override Folder`: Any JSON files put into this folder will override any other JSON files in the base repo folder. Useful if you want to use an override for a specific JSON entry.

- `Support Folder`: This folder contains all side files needed for the framework. 
  - `Image Bank Folder`: Storage folder used for images used by the framework.
  - `Support DLLs Folder`: Currently houses .NET 4.6.1 DLLs to get SendKeys working with .NET Core 2.2. Once the framework is updated to 3.0 this folder will go away.
  - `chromedriver.exe`- `geckodriver.exe` - `MicrosoftWebDriver.exe`: Selenium browser drivers.
  - `Kill All Selenium Instances.bat`: Helpful batch file that will kill all Selenium Browser drivers running. This is helpful when writing tests and problems happen. 

- `Tests Folder`: This is where all the magic happens. The automation tests! Any .cs files in this folder will be included in the NUNIT Test Explorer.
   - `Assorted Tests Archive`: This is where I store my development tests. This folder is ignored during project build time. Tests in this folder do not appear in the NUNIT Test Explorer.

- `c:\Automation Logs`: This is where the Extent reports, screenshots, and all ancillary files are stored for your test runs. This folder will be created when you run a test from this framework the first time on a system. There are better places to put this and it will probably change down the road but for now this made sense. Right now it will archive all test runs so while you are developing any tests you will want to empty this folder out until I get to purging older test reports.

## Using the Framework
First off I will not go into great detail about the code of .NET Framework past just saying I have tried my hardest to name all variables and methods to where you should understand what is going on under the hood. 
Any where I feel needs a comment I have done so in the code. All methods that can be called from a test have filled out and up to date XML with it. You should be able to see what a method does and expects through Visual Studio. If you see any where I have missed something please let me know. 

- Quickest way to get going with .NET Framework is to grab the `Test Template.cs` file from the `\Tests\Assorted Tests Archive`, copy it to the base `Tests` folder and rename the commented fields. At that point when you build the project you should see that test appear in the NUNIT Test Explorer. 

### Currently supported methods you can use in your tests
- `ClickElement`: This method is used to have Selenium click on any element in a website like a form field or button. 
   - This method has one parameter that needs to be passed which is the locator.<br />Example:
   ```
     ClickElement("xpath=//button[@type='Login']");
   ```
    The above will use a XPath locator to find and click on the element. 

   - One tip I would suggest is instead of hard coding your locators is to have a separate JSON file with your locators and then use the `JSONCALL` method to call that locator. This way you do not have to modify your tests if a locator changes.

- `DatabaseCheck`: This will query the database for a single value using a SQL command and ensures it is a specific value. You can assign this call to a variable or tuck it into any other method that is expecting a string.
  - This method has a single parameter, the SQL query that needs to be passed.<br />Example: 
  ```
     DatabaseCheck("select UserId from ClsListing where ListingId = 111");
  ```
  - Again, a tip I would provide is combine the above with the JSONCALL method. Don't hard code your tests. This can also be used in conjunction with other functions. 

- `GetAllEmailsFromAnEmailAccount`: Simple wrapper method to call the 'FetchAllMessages' method which pulls all emails from an email account using POP3.<br />To use this you need to ensure that your `appsettings.local.JSON` file has all the `EmaiInformation` filled out. 
  - This method has one optional parameter you can pass `true` to. If you do this the method will pull all the email down from that account and delete the emails as well. Useful to ensure the account is left clean after your testing. `Be careful with this setting as it is not reversible.`

- `GetFieldValue`: This is a Selenium command that will return the value of a field on a website. 
  - This method has one parameter that needs to be passed which is the locator.<br />Example:
  ```
     GetFieldValue("xpath=//button[@type='Login']");
  ```
    The above will use a XPath locator to return what is in that elements field. 

- `JsonCall`: Simple method that does a ton to ensure you do not have to rewrite your tests. Call this method and the value passed is a variable entry in one of your JSON files in the JSON Repo.
  - Example:
  ```
     JsonCall("GoogleHomePage:URL");
  ```
  - `A word on locators in your JSON files`: Please ensure all locators have their type before the actual locater.<br />Example:
  ```
    xpath=//button[@type='Login']
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
This will fail or pass the current test if the string is not found. It is recommended to run this after all other tests are completed to account for email delays getting into the target email account.
  - Argument passed is a string to parse the emails for. Again, recommend using JSONCALL for this. 
  - Example:
  ```
     ParseAllEmailFilesForAStringValue("This text should be in one of the email files.");
  ```

- `ScreenShot`: This is a simple method to take a screenshot of the currently active Selenium controlled browser. Single argument passed which is the name of the screenshot. 
  - Example:
  ```
     ScreenShot("Picture of Homepage");
  ```
  - This method automatically saves the screenshot into the `c:\Automation Logs\Screenshots` folder and adds them to the current Extent report for that test. 

- `SendKeys`: This sends a set of keyboard commands to a specific element within the Selenium controlled browser.
  - Two arguments are needed for this method. 
    - Locator (Again combine with JSONCALL)
	 - String to send to that element
  - Example:
  ```
     SendKeys("xpath=//button[@type='Login']", "Type this message in the box.");
  ```

- `ShouldBe`: Very simple method that compares two string values and ensures they are the same.
  - Two arguments are needed for this. 
    - Value #1 to compare to #2
	 - Value #2 to compare to #1

- `ShouldNotBe`: Method is the same as `ShouldBe` but ensures the values are not the same.

- `StripEndingUrl`: Method that strips all characters after the last `/` in a string. 

- `UseBrowser`: Method that tells Selenium what browser to run. Currently supports: `chrome` - `firefox` - `edge`
  - Your first test will `ALWAYS` start with this. 
  - To use the JSON setting in your `appsettings.json` file you will call it this way:
  ```
    UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
  ```

- `Wait`: Method that takes in a numeric value as an argument and tells Selenium to wait for that number of seconds. This is considered an implicit wait.

- `WaitForElement`: This is a Selenium command that will pause a test until Selenium sees a specific element on a webpage. This is considered an explicit wait.
  - Two arguments are needed for this method. The third is optional.
    - Locator (Again combine with JSONCALL)
	 - Numeric time to wait for the element. Default is this argument is not passed Selenium will wait 30 seconds before failing the test step.
  - Example:
  ```
     WaitForElement("//button[@type='Login']", "Type this message in the box.", 30);
  ```

### Methods you can use but are more used as support of the framework
- `CloseBrowser`: This method closes the current Selenium controlled browser.

- `CloseQuitBrowsers`: This closes all active Selenium controlled browsers in addition it also ends all Selenium controlled processes. Should know that this method is called at the end of a test run automatically by the framework.

- `LocatorCleaner`: Method is used to take a locator string and parse out the type of locator and the locator and return both using a tuple.

- `MessageSlack`: Method that requires a text string that will send that message to a hard coded Slack channel if the FrameworkConfiguration:SlackNotifications setting in the appsettings.json is set to `yes`

- `QuitBrowsers`: Quits all Selenium controlled browser processes. 
