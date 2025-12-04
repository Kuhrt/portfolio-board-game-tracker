# Board Game Tracker

## Overview

My family and I love playing board games. We have so many, I wanted to have a collection of tools we could use while playing. It's starting as a tracker for what games we have, what we want, and how much time we spend playing them. I want to start having different features for our more complicated games like Gloomhaven, Catan, D&D, Magic, etc. I architected this stack to show off my knowledge with C#, Vue, and advanced db techinques with MSSQL.

## Live Demo

🔗 **TBD**

## Skills Demonstrated

This project showcases the following technical skills and concepts:

- **Full-stack Architecture**: Shows I can archetect a full-stack application. I'm using .NET, Typescript, and MSSQL.
- **Front-end Data Organization**: A landing page and dashboard using prebuilt components from PrimeVue.
- **API Structure and Ingestion**: I like API-driven architecture for its versatility and extensibility. This shows general preferences for structuring and ingesting an API
- **Database Schema Management**: An advanced database-first setup using MSSQL. Unit of work and repository pattern.
- **Authentication/Authorization**: Using JWT flows to gain access to data using .NET Identity

## Technology Stack

### Core Technologies

- **.NET API (C#)**: .NET Core 9.0 with a clean architecture in my favorite way to set up dotnet projects
- **Vite/Vue Web App (Typescript)**: I went for an extremely lightweight SPA. I wanted to show off building a web application from scratch with these technologies
- **MSSQL**: Database-first approach with scripts for reference data and seeding any other data. The data layer interacts with the db via a unit of work and the repository pattern.
- **.NET Identity (auth)**: Open-source, easy to use, and production ready. Custom auth models, claims and JWT flow
- **Vitest**: Newer testing framework with a more modern approach than Jest
- **Docker**: The `.tools` contains a compose file to run the project locally. I also have versions of this for the live demo.

### Additional Tools & Libraries

- **PrimeVue**: UI kit that works out of the box for amazing UIs
- **Axios**: My other portfolio project uses the Fetch API, so I built my API layer with Axios this time
- **Pinia**: The best state manager I've ever used
- **Sass**: I needed to show that I can write CSS/SCSS from scratch to match libraries like Tailwind

## Local Development Setup

### Prerequisites

Before running this project locally, ensure you have the following installed:

- **NPM**: [Download](https://docs.npmjs.com/downloading-and-installing-node-js-and-npm)
  - Package manager
  - Latest version recommended
- **uv**: [Install](https://docs.astral.sh/uv/getting-started/installation/)
- **Docker**: [Install](https://docs.docker.com/desktop/)

### Installation & Setup

1. **Clone the repository**

   ```bash
   git clone https://github.com/kuhrt/portfolio-board-game-tracker.git
   cd portfolio-board-game-tracker
   ```

2. **Create and run tool containers**

   ```bash
   cd .tools
   docker compose up
   ```

3. **Setup API**

   Ensure you have .NET Core 9.0 installed on your system. Open the solution in your favorite C# editor.

4. **Install Web App dependencies**

   ```bash
   cd web
   npm i
   ```

5. **Environment Configuration**

   ```bash
   # Copy environment template in the api/ and web/ directories
   cp .env .env.local

   # Edit .env file with your configuration
   # Ensure all values are populated
   ```

## Project Structure

```
portfolio-practice-hub/
├── .tools/
│   └── docker-compose.yml
├── api/
├── web/
└── README.md
```

## Repository Information

- **Status**: In Development
- **Type**: Full-stack portfolio project
- **Contributions**: This repository does not accept contributions as it's designed to showcase individual technical skills

---

_This project was created by Kuhrt as part of my software development portfolio._
