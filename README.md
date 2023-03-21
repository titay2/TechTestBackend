# Tech Test Backend
In this test, you will be tasked to refactor an existing .NET Core Web API project containing numerous design flaws and bad practices. The goal is to find and address these design flaws while still maintaining the core functionality of the application.

## Scenario
The company you work for has just received a new client with an existing Web API application built by another agency. The client was dissatisfied with the service of the previous agency and is eager to continue the development of the Web API application with you instead. Upon first inspection of the source code, however, it seems that the quality of the solution is not up to par with the standards needed for further development.

You have been tasked to improve the quality of the solution before continuing on new features. It is essential that the functionality of the application remains the same since the application is already in use in a production environment.

## Introduction
The API application allows a user to search for tracks on Spotify and save them in a list of liked tracks. The user can also remove liked tracks and get a list of all liked tracks. It has an integration with Spotify API in order to search for tracks by name and store references to these tracks in an in-memory database using Entity Framework.

The agency previously working on this application also provided you with Spotify integration API-keys, and a link to the documentation:

Client ID: 996d0037680544c987287a9b0470fdbb

Client Secret: 5a3c92099a324b8f9e45d77e919fec13

Documentation: https://developer.spotify.com/documentation/web-api/

## Restrictions
Assume that the API is already running in a production environment, and has UI clients dependent on the current behavior of the application. Thus, some restrictions must be in place. 
* Do not introduce a breaking change, for example changing the API endpoints route, GET/POST methods, input parameters names, response type, or structure.
* Do not delete the IApiMarkerFile.
* Do not change the database storage method from In-memory. 
* Do not change the project namespace name TechTestBackend.

## Setup Instructions
1. Clone the repository to your local machine
2. Create a new public repository on your GitHub account
3. Push the cloned repository to your new public repository
4. Continue with the test

## Limitations & Scope
The current version of the application currently only concern one concurrent user. In order to reduce the scope of the test, this will not be counted as one of the design flaws you need to address.

Expect this test to take around 2-4 hours to finish.

## Delivery
Please provide a link to your repository when you are done with the test. When finishing up, please provide a short list of further improvements for continued development (if any).

If your repository includes changes to the application configuration, provide clear instructions at the end of this README.md file on how to run the application.