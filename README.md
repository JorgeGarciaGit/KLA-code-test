# Currency to Words Converter

A client-server application that converts a dollar amount from numbers into words.

The conversion is performed server-side through an ASP.NET Core Web API. The desktop client provides a graphical user interface built with React and TypeScript.

The application currently supports English and German output. The user can select the desired language and submit a dollar amount for conversion.

## Features
Convert dollar amounts from numbers into words.
Support amounts from 0 to 999,999,999.99.
Support dollars and cents.
Support English and German output.
Language selection through the client UI.
Server-side conversion through a REST API.
Centralised exception handling.
Automated unit and integration tests.
Client-server architecture with a separate GUI and API.

## Architecture

The solution follows a client-server architecture:

┌──────────────────────────────┐
│     NumberToWord.Client      │
│                              │
│ React + TypeScript + Vite    │
│             │                │
└─────────────┼────────────────┘
              │ HTTP
              │ POST /api/currency/convert
              ▼
┌──────────────────────────────┐
│       NumberToWord.Api       │
│                              │
│ ASP.NET Core Web API         │
│ Controllers / Middleware     │
│             │                │
└─────────────┼────────────────┘
              ▼
┌──────────────────────────────┐
│      NumberToWord.Core       │
│                              │
│ Currency conversion service  │
│ Converter factory            │
│ Language-specific converters │
└──────────────────────────────┘

The frontend is responsible for collecting user input, calling the API, and displaying the result.

The conversion algorithm is implemented entirely in NumberToWord.Core and is therefore not duplicated in the client.

## Technologies
### Backend
.NET 10
ASP.NET Core Web API
C#
REST
Dependency Injection

### Frontend
React
TypeScript
Vite
CSS

### Testing
xUnit
ASP.NET Core integration testing

## Prerequisites

The following software is required to build and run the solution.

### .NET

Install the .NET 10 SDK.

Verify the installation:

dotnet --version

The returned version should be compatible with .NET 10.

### Node.js

Install Node.js and npm.

Verify the installation:

node --version
npm --version

Frontend dependencies are defined in:

src/NumberToWord.Client/package.json

## Getting Started
### 1. Clone the repository

Clone the repository and navigate to the project directory:

git clone <repository-url>
cd <repository-directory>
### 2. Configure the API URL

The React client uses the VITE_API_BASE_URL environment variable to determine the API base URL.

Create or update:

src/NumberToWord.Client/.env.development

For example:

VITE_API_BASE_URL=http://localhost:5233/api

The port must match the URL configured by the ASP.NET Core launch settings.

### 3. Start the API

From the solution root:

dotnet run --project src/NumberToWord.Api

The API will start using the configured ASP.NET Core launch settings.

For example:

http://localhost:5233

The currency conversion endpoint is:

POST /api/currency/convert

### 4. Start the React client

Open a second terminal:

cd src/NumberToWord.Client

Install the frontend dependencies:

npm install

Start the development server:

npm run dev

Vite will provide the frontend URL, typically:

http://localhost:5173

The browser client communicates with the API using the URL configured in .env.development.

### Running Tests

From the solution root:

dotnet test

This runs the automated test suite for the solution.

The tests include:

Unit tests for the currency conversion service.
Unit tests for the English converter.
Unit tests for the German converter.
Unit tests for the converter factory.
ASP.NET Core API integration tests.

The tests are located under:

tests/

## API
### Convert Currency

Converts a dollar amount into words using the requested language.

### Request
POST /api/currency/convert
Content-Type: application/json

Example:

{
  "dollars": 25,
  "cents": 10,
  "language": "en"
}

### Response
{
  "text": "twenty-five dollars and ten cents"
}

### Language codes

The API uses language codes rather than the human-readable labels displayed by the frontend:

Code	Language
en	English
de	German

The client may display these as English and Deutsch, while the API continues to use the stable language codes.

## Project Structure
.
├── NumberToWord.sln
├── src
│   ├── NumberToWord.Api
│   │   ├── Contracts
│   │   │   ├── ConvertCurrencyRequest.cs
│   │   │   └── ConvertCurrencyResponse.cs
│   │   ├── Controllers
│   │   │   └── CurrencyController.cs
│   │   ├── Middleware
│   │   │   └── ExceptionHandlingMiddleware.cs
│   │   ├── Program.cs
│   │   └── ...
│   │
│   ├── NumberToWord.Client
│   │   ├── src
│   │   │   ├── components
│   │   │   │   ├── ConversionResult.tsx
│   │   │   │   ├── CurrencyForm.tsx
│   │   │   │   └── LanguageSelector.tsx
│   │   │   ├── service
│   │   │   │   └── currencyApi.ts
│   │   │   ├── types
│   │   │   │   └── currency.ts
│   │   │   ├── App.tsx
│   │   │   └── ...
│   │   ├── package.json
│   │   └── ...
│   │
│   └── NumberToWord.Core
│       ├── Converters
│       │   ├── EnglishNumberToWordsConverter.cs
│       │   ├── GermanNumberToWordsConverter.cs
│       │   └── INumberToWordsConverter.cs
│       ├── Factories
│       │   ├── INumberToWordsConverterFactory.cs
│       │   └── NumberToWordsConverterFactory.cs
│       ├── Models
│       │   └── CurrencyAmount.cs
│       └── Services
│           ├── CurrencyConversionService.cs
│           └── ICurrencyConversionService.cs
│
└── tests
    ├── NumberToWord.Core.Tests
    │   ├── CurrencyConversionServiceTests.cs
    │   ├── EnglishNumberToWordsConverterTests.cs
    │   ├── GermanNumberToWordsConverterTests.cs
    │   └── Factories
    │       └── NumberToWordsConverterFactoryTests.cs
    │
    └── NumberToWord.Api.Tests
        └── CurrencyControllerIntegrationTests.cs

## Key Design Decisions
### Separation of the Core Library

The conversion logic is isolated in NumberToWord.Core.

The API layer is responsible for HTTP concerns, while the core library contains the domain-specific conversion logic.

This separation keeps the conversion functionality independent from ASP.NET Core and makes it easier to test and potentially reuse independently of the API.

NumberToWord.Api
       │
       ▼
NumberToWord.Core

The controller therefore does not contain the number-to-words algorithm.

### Language-Specific Converter Interface

Language-specific converters implement:

INumberToWordsConverter

Current implementations are:

EnglishNumberToWordsConverter
GermanNumberToWordsConverter

The interface provides a common contract while allowing each language to implement its own number construction rules.

This is important because number formation is language-dependent. The conversion logic should therefore not attempt to force all languages into a single implementation.

Adding another language can be achieved by introducing another implementation of the converter interface.

### Converter Factory

The NumberToWordsConverterFactory is responsible for selecting the appropriate converter based on the requested language.

Conceptually:

Language
   │
   ▼
NumberToWordsConverterFactory
   │
   ├── en → EnglishNumberToWordsConverter
   │
   └── de → GermanNumberToWordsConverter

The factory keeps language-selection logic out of the currency conversion service and controller.

This also allows the application to maintain a single converter abstraction while supporting different language-specific implementations.

### Dependency Injection

ASP.NET Core's built-in dependency injection container is used to manage application services and the converter factory.

Dependencies are registered at application startup and injected where required.

This reduces direct coupling between components and makes the services easier to test and replace.

### Exception Handling

Unexpected exceptions are handled centrally through:

ExceptionHandlingMiddleware

Instead of implementing exception handling separately in each controller action, the middleware provides a single place for handling unexpected errors.

This keeps controllers focused on HTTP/API concerns and avoids duplicated error-handling code.

## Design Assumptions

The implementation is based on the following assumptions:

The currency is always US dollars.
The currency consists of dollars and cents.
The maximum supported value is 999,999,999.99.
The maximum cents value is 99.
Zero is a valid amount.
Negative amounts are not supported because they are outside the requirements.
The API receives dollars and cents as separate numeric values.
The API uses language codes (en, de) rather than display labels.
Language selection determines which server-side converter is used.
Singular and plural currency forms are handled according to the selected language.
No currency exchange or conversion between different currencies is performed. The term "conversion" refers to converting the numeric representation into words.

That last point is worth explicitly documenting because "currency conversion" can otherwise be interpreted as USD → EUR, etc.

## Input and Output

The application follows the input/output model specified by the assessment.

Examples:

0
→ zero dollars
1
→ one dollar
25,10
→ twenty-five dollars and ten cents
0,01
→ zero dollars and one cent
45,100
→ forty-five thousand one hundred dollars
999,999,999.99
→ nine hundred ninety-nine million
  nine hundred ninety-nine thousand
  nine hundred ninety-nine dollars
  and ninety-nine cents

The exact linguistic output is determined by the selected language.

## Testing Strategy

The solution contains both unit and integration tests.

### Unit tests

The core conversion logic is tested independently from the API.

Tests cover:

Basic numbers.
Tens and compound numbers.
Hundreds.
Thousands.
Millions.
Zero values.
Dollar singular/plural forms.
Cent singular/plural forms.
Language-specific conversion rules.
Converter selection.

### Integration tests

The API integration tests verify the behaviour of the HTTP layer and its interaction with the application services.

This provides coverage of the complete request path:

HTTP request
    ↓
Controller
    ↓
CurrencyConversionService
    ↓
Converter Factory
    ↓
Language-specific Converter
    ↓
HTTP response

## Limitations

The current implementation intentionally remains within the scope of the assessment.

Only English and German are supported.
Only dollars and cents are supported.
The maximum supported amount is 999,999,999.99.
Negative values are not supported.
There is no persistence layer because conversion history is not required.
The frontend and API are separate applications during development.
The API URL must be configured for the environment in which the client is running.
No authentication or authorisation is implemented because it is outside the requirements.

## Known Issues

There are currently no known functional issues.

During local development, ensure that:

The ASP.NET Core API is running.
The API port matches VITE_API_BASE_URL.
The frontend is running on its Vite development server.
The API allows requests from the frontend origin through its CORS configuration.

## AI Usage / Transparency

AI assistance was used during the development of this project.

AI was used as a development aid for:

Discussing architectural alternatives.
Reviewing implementation approaches.
Debugging development issues.
Reviewing code structure and separation of responsibilities.
Improving documentation.
Discussing testing and design considerations.

The AI was not used as a replacement for understanding or validating the implementation. The resulting code was reviewed and adapted as part of the development process.

The relevant prompt history is included in the repository as required by the assessment:

ai-prompt-history.md
Final Assessment Notes

The project was developed according to the requirements of the coding assessment, with particular focus on:

Separation of client and server responsibilities.
Server-side implementation of the conversion algorithm.
Maintainable language-specific conversion logic.
Dependency injection and abstraction.
Automated testing.
Clear API contracts.
Extensibility for additional languages.
Reproducible local setup.