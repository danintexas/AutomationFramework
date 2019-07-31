# Kindergarten

## A simple to use and maintain automation framework for web testing

This automation framework is build from the ground up as an simple to use and maintain solution. Pages and all locators are kept in the appsettings.json file that can be changed per environment or application

All you need is Visual Studio 17 or 19 along with installing .NET Core 2.2!

To run an individual test through command line use the following in the solution folder:

'dotnet test -- filter FrameworkDemo'

This will run the test with the namespace of 'AutomationFramework.Tests.FrameworkDemo'

## Tech Used:
- .NET Core 2.2
- NUnit
- Extent Reports
- Selenium

## Supports the following browsers:
- Chrome
- Firefox
- Edge

## Planned ToDo List:
- Logs to AWS bucket
- Concurrent testing through ASYNC or Selenium Grid
- Redo logic for running single test with multiple browsers
- Shouldly implementation 
- Check POP3 email account for required email
- Direct checks on entered data in the database that is entered during testing. 
- Built in API endpoint testing. 
- Built in API stress testing.

## Custom Items for RumbleOn to do
- Smoke Test for RumbleOn -> Classifieds (In Progress)
- Mileage and Price randomizer
- Description text randomizer
- Upload photos from a bank of photos for testing
- Vin Generator
- Account Generator tied to personal or work webserver
- Upload logs/notification to Slack and or text message

Special shout out to David for mentoring me through this and providing great feedback!
