# Birthday Greetings Kata (C#)

## Objective

The Birthday Greetings is a kata born with the purpose 
to learn how to implement an hexagonal architecture, which 
is a good way to structure an application, to shield your 
domain model from external apis and systems.

## Description

The HR department of a company want to send birthday 
greetings to all the employees who are born today.
The employee list with their name, surname, birthday and 
email is kept in a text file. 

The program loads the employee's data and:
- reads the file line by line
- parses the csv in an `Employee` object
- filters the employees who have a birthday today
- sends an email to selected employees

The code is written with a structured programming style 
using the classical layered architecture.
The business logic is mostly written in the `BirthdayService` 
class and it comprises several layers of abstractions:
- opening and reading the file 
- selecting the employees who are born today
- sending an email to each of them 

## Hexagonal Architecture

We want to refactor the code toward an hexagonal architecture
so that the business logic is completely separated from the 
interaction with the low level classes accessing the filesystem 
and sending emails. 

The separation of the abstraction level allows, as a side effect,
to test the domain logic by using in memory super fast unit tests.

### Layer Separation

Every external systems is hidden behind a facade that:

Provides a simplified view of the external system, with 
only the operations that we need to do with it.
Is expressed in terms of the domain model.
The domain model does not depend on any other layer; 
all other layers depend on the domain model.


| gui | file system | database |
|-----|-------------|----------|
|     | domain      |          |

How can we make the domain independent, for instance, 
of the database? We should define a repository interface 
that returns domain objects. The interface is defined 
in the domain layer, and is implemented in the database 
layer.

![Hexagonal Architecture](docs/hexagonal-architecture.png "Hexagonal Architecture")  

## Building and Launching Tests

To build the project:
```bash
dotnet build
```

To launch the test:
```bash
dotnet test
```
There is a failing test to help you in understanding how 
the code is structured:
```bash
$ dotnet test                                                                                                                                            [21:36:08]
  Determining projects to restore...
  All projects are up-to-date for restore.
  BirthdayGreetings -> /Users/gpace/playground/birthday-kata-csharp/src/BirthdayGreetings/bin/Debug/net7.0/BirthdayGreetings.dll
  BirthdayGreetings.Tests -> /Users/gpace/playground/birthday-kata-csharp/test/BirthdayGreetings.Tests/bin/Debug/net7.0/BirthdayGreetings.Tests.dll
Test run for /Users/gpace/playground/birthday-kata-csharp/test/BirthdayGreetings.Tests/bin/Debug/net7.0/BirthdayGreetings.Tests.dll (.NETCoreApp,Version=v7.0)
Microsoft (R) Test Execution Command Line Tool Version 17.5.0 (x64)
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
  Failed WillSendGreetingsWhenItsSomebodysBirthday [121 ms]
  Error Message:
     Expected string length 26 but was 25. Strings differ at index 25.
  Expected: "Happy Birthday, dear John!"
  But was:  "Happy Birthday, dear John"
  ------------------------------------^

  Stack Trace:
     at BirthdayGreetings.Tests.AcceptanceTest.WillSendGreetingsWhenItsSomebodysBirthday() in /Users/gpace/playground/birthday-kata-csharp/test/BirthdayGreetings.Tests/AcceptanceTest.cs:line 32

1)    at BirthdayGreetings.Tests.AcceptanceTest.WillSendGreetingsWhenItsSomebodysBirthday() in /Users/gpace/playground/birthday-kata-csharp/test/BirthdayGreetings.Tests/AcceptanceTest.cs:line 32



Failed!  - Failed:     1, Passed:     7, Skipped:     0, Total:     8, Duration: 165 ms - BirthdayGreetings.Tests.dll (net7.0)
```
