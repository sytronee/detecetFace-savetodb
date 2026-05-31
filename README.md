Click [here](https://github.com/sytronee/detectFace-savetodb/blob/main/README_tr.md) for the Turkish document.
# Face Detection and Data Tracking System

An end-to-end computer vision system that captures camera feeds, detects faces, and logs data (Camera ID, timestamp, confidence score, bounding box) into a **PostgreSQL** database.

---

## 🏗 System Architecture
1.  **Frontend (Python):** Image processing module located in `detectFace-savetodb/detectFace/PythonScript`.
2.  **Backend (API):** C# .NET Core service for data management.
3.  **Database:** PostgreSQL 15.

---

## 🛠 Tech Stack
* **Languages:** C# (.NET 8.0), Python 3.x
* **Database:** PostgreSQL 15
* **Orchestration:** Docker & Docker Compose

---

## 📦 Installation Guide

### 1. Clone the Repository
```bash
git clone <[your-repo-link](https://github.com/sytronee/detectFace-savetodb)>
cd detectFace-savetodb
2. Prepare Python Environment
Bash


cd detectFace/PythonScript

# Create and activate virtual environment
python -m venv venv
# Windows:
venv\Scripts\activate
# Linux/Mac:
source venv/bin/activate

# Install dependencies
pip install -r requirements.txt
3. Launch Docker Services
Bash


cd ../..
docker-compose up --build
🚀 Execution
Run the Python script from the detectFace/PythonScript directory. It will stream detection events to the API at http://localhost:8080.

📊 Database Access
Host: localhost | Port: 5432

Database: CameraDb | Credentials: postgres / 123

⚠️ Troubleshooting
Port Conflicts: Use netstat -ano to find and terminate processes blocking ports 8080 or 5432.

Database Connection: Ensure connection strings point to db inside the Docker network.

Build Errors: If Docker fails to find project files, verify that Dockerfile paths match the current folder hierarchy.


