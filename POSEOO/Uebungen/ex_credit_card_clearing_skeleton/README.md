# Logging System

## Overview

The `Logging` namespace provides a flexible and extensible logging system for applications. It supports multiple log levels and allows logging to different outputs, such as the console or files. The system is designed to be easily extendable by implementing the `ILogger` interface.

## Components

### 1. `ILogger` Interface
Defines the contract for all loggers. It includes methods for logging error and informational messages:
- `LogError(string message)`
- `LogInfo(string message)`

### 2. `LogLevel` Enum
Defines the severity levels for log messages:
- `Error`
- `Warning`
- `Info`

### 3. `AbstractLogger` Class
An abstract base class that implements the `ILogger` interface. It provides default implementations for logging error and informational messages based on the log level.

### 4. `ConsoleLogger` Class
A concrete implementation of `AbstractLogger` that writes log messages to the console. Messages are color-coded based on their severity.

### 5. `FileLogger` Class
A concrete implementation of `AbstractLogger` that writes log messages to a file. Each log entry includes a timestamp and severity level.

### 6. `LoggerCollection` Class
An implementation of `ILogger` that aggregates multiple loggers. It forwards log messages to all the loggers in the collection.

## Usage

### Example: Logging to Console
```csharp
ILogger logger = new ConsoleLogger(LogLevel.Info);
logger.LogInfo("This is an informational message.");
logger.LogError("This is an error message.");
```

### Example: Logging to File
```csarp
ILogger logger = new FileLogger("log.txt", LogLevel.Error);
logger.LogError("This is an error message.");
```

### Example: Using Multiple Loggers

```csharp
ILogger consoleLogger = new ConsoleLogger(LogLevel.Info);
ILogger fileLogger = new FileLogger("log.txt", LogLevel.Info);
ILogger loggerCollection = new LoggerCollection(consoleLogger, fileLogger);

loggerCollection.LogInfo("This message is logged to both console and file.");
```