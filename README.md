# Kindergarten

## A simple to use and maintain automation framework for web testing

This automation framework is build from the ground up as an simple to use and maintain solution. 

All you need is Visual Studio 19 to write tests along with installing .NET Core 2.2!

If you want to just run an tests just install .NET Core 2.2 and run through command line use the following in the solution folder:

'dotnet test'

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

## Custom Items for RumbleOn to do

