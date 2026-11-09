# RaceDay System - Portfolio of Evidence (Part 1, 2, and 3)
**Student Name:** Kabuya Tambwe Josue  
**Student Number:** ST10468057  

## Overview
RaceDay is a full-stack web-based event management system designed specifically for the South African road running, walking, and cycling community. By Part 3, this system consists of a RESTful API connected to a SQL Server database, Azure Blob Storage for cloud image handling, and a fully containerised ASP.NET Core MVC front-end application.

## Roles Supported
- **Organiser**: Can create, edit, and delete events, manage event categories, capture participant results, upload event banners, and view all event enrolments.
- **Participant**: Can create an account, browse events, enter an event by selecting a category, view own enrolments, and track personal results.

## Setup and Run Instructions (Docker)
The MVC web application is containerised using Docker. To build and run the application locally, open your terminal inside the `RaceDayMvc` directory (where the `Dockerfile` is located) and execute the following commands:

1. **Build the Docker image:**
   ```bash
   docker build -t racedaymvc .

   ### Part 3: Video Demonstration
- **YouTube Video Link:** https://youtu.be/YYlesW84rAE