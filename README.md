# Face Detection and Data Tracking System

This project is an end-to-end system designed to process camera feeds for face detection and automatically store the detected data (Camera ID, timestamp, confidence score, and bounding box information) into a **PostgreSQL** database.

## 🚀 System Architecture
The system is built using containerized services (Docker) for seamless portability:
* **Frontend/UI:** Python script for image processing and object detection.
* **Backend API:** C# .NET Core-based API to manage data intake.
* **Database:** PostgreSQL 15.



## 🛠 Tech Stack
* **Language:** C# (.NET Core), Python (OpenCV)
* **Database:** PostgreSQL 15
* **Orchestration:** Docker & Docker Compose
* **ORM/Connectivity:** Npgsql

## 📦 Installation & Usage

### Prerequisites
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) must be installed and running on your system.

### Steps
1. **Clone the Repository:**
   ```bash
   git clone <https://github.com/sytronee/detectFace-savetodb>
   cd <project-folder>
Start the Services:
Run the following command in the project's root directory:

Bash


docker-compose up --build
Connect to the Database:
Use DBeaver or pgAdmin to view the data:

Host: localhost | Port: 5432 | DB: CameraDb | User: postgres | Pass: 123

📊 Data Structure
The system stores data in the scanneddata table, capturing camera IDs, timestamps, confidence scores, and bounding box coordinates.



Click [here](https://github.com/sytronee/detectFace-savetodb/blob/main/README_tr.md) to access the Turkish documentation for the project. 
