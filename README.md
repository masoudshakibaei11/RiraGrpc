# Rira gRPC Project

A clean-architecture implementation of a gRPC service with Protocol Buffers, FluentValidation, and centralized error handling.

## Tech Stack

- .NET 9
- gRPC & Protocol Buffers
- FluentValidation
- Kestrel (HTTP/2)

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- [grpcui](https://github.com/fullstorydev/grpcui/releases) installed (for testing)

### Setup

Clone the repository and restore dependencies:

```bash
git clone https://github.com/masoudshakibaei11/RiraGrpc
cd RiraGrpc
dotnet restore

### Build & Run

Build the project:

bash
dotnet build

Run the server:

bash
dotnet run --project Rira.Presentation

### Testing with grpcui

Once the server is running, open a new terminal and run:

bash
# Ensure the server is active before running this
grpcui -plaintext localhost:5121

## Architectural Highlights

- **Layered Design** — Separated Presentation, Application, and Infrastructure layers
- **Validation** — Uses FluentValidation to keep business logic clean
- **Resilience** — Custom Interceptors for centralized error handling and request validation

-----------------------------------------------------------------------------------------------------

Created by [MasoudShakibaei](https://github.com/masoudshakibaei11)
```
