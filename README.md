# Kindergarten

## A simple to use and maintain automation framework for web testing

This automation framework is build from the ground up as an simple to use and maintain solution. 

All you need is Visual Studio 19 to write tests along with installing .NET Core 2.2!

If you want to just run an tests just install .NET Core 2.2 and run through command line use the following in the solution folder:

'dotnet test'

From it's initial design this Kindergarten was designed to do two things:
1. Help bring manual Q/A engineers up into the Automation world quickly while not needing full development skills and knowledge allowing them to ease into an automation role. 
2. Allow nearly anyone to quickly develop Web Automation tests without the Framework getting in the way.

Any comments, suggestions or complaints please reach out to me with them. I am looking to fine tune this Framework and I am always looking to make it better and with more features. 
This is a passion project and something I will be working on for the foreseable future.

Daniel Gail
Work Email: danielg@rumbleon.com
Personal Email: daniel.gail@gmail.com

## Tech Used:
- .NET Core 2.2
- NUnit
- Extent Reports
- Selenium
- OpenPop
- ADO

## Supports the following browsers:
- Chrome
- Firefox
- Edge

## Known Issues:
- SendKeys is kinda 'hacked' with .NET Core 2.2. A red X dialog comes up the first time it is called in a test. This can be closed
	and the method will work fine going forward. Believe this is because to get this to work I am calling a few .NET 4.6 Framework 
	files that technically is not supported. This will be resolved with .NET Core 3.0 due to be released mid Sept. 

## Planned ToDo List:
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
- `Core Folder`: As the name implys this is where the core of the framework resides.
  - `AdditionalFunctions.cs`: This file contains additional methods more to better manage an ever increasing core.cs file
  - `Core.cs`: As you can probably guess this is where the heart of the framework is. I have tried to make all code easy to read so if there is something here you think needs improving please let me know. 
- `Json Repo`: If the core folder is the heart of the framework this is the brains. Houses all needed input and locators for the framework to utilize. Please note ANY .JSON files dropped in this folder will be scanned on build time. 
  - `Json Backup Folder`: This folder is ignored during project build time and test execution. Simple folder to store back ups of JSON files you do not want to use. This folder is not used or looked at by the framework.
  - `Json Override Folder`: Any JSON files put into this folder will override any other JSON files in the base repo file. Usefull if you want to use an override for a specific JSON entry.
  - `VIN Store`: This file is special and used by the GenerateVIN method. If you call this method you need to ensure this file is there. 
- `Support Folder`: This folder contains all side files needed for the framework. 
  - `Image Bank`: Storage folder used for images used for multiple tests.
  ```
  {
  // Information needed for reports - This stays no matter the project
  "ReportInformation": {
    "TesterName": "TesterName",
    "Project": "Project Name",
    "Environment": "Environment Name",
    "Build": "Environment Build"
  },
  "FrameworkConfiguration": {
    // Browser Keywords: 'chrome' 'firefox' 'edge'
    "Browser": "chrome",
    "DatabaseTestSteps": "Yes" // 'Yes' or 'No'
  }
}
```