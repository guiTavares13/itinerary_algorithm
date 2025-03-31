# itinerary_algorithm

This project is a proof of concept (POC) for a travel itinerary recommendation system. It uses clustering (K-Means) and route optimization (Traveling Salesman Problem - TSP) algorithms to generate optimized itineraries based on a user-provided country and number of days.

## Purpose
To create personalized travel itineraries by grouping tourist destinations by proximity and optimizing the daily visit order.

## Technologies Used
- **Frontend**: Simple interface for user input with WindowsForms Application (country and number of days).
- **Backend**: C# API that processes requests and runs the algorithms.
- **Database**: SQLite to store tourist destination data.
- **Algorithms**:
  - K-Means: Groups destinations by geographic proximity.
  - TSP (Traveling Salesman Problem): Optimizes the route within each cluster.

## Project Structure
### Architecture Diagram

![diagram_poc_roteiro](https://github.com/user-attachments/assets/3dbfaf65-4698-476d-91e6-74930de6359c)

### Frontend Application WindowsForms

![tela-1](https://github.com/user-attachments/assets/8922aa78-54e8-4c21-9de2-8d06b9121168)
![tela-2](https://github.com/user-attachments/assets/db45d188-c1e8-4781-929a-f1a5f71d4185)


- **Frontend**: User interface to input the country and number of days.
- **Backend**: Processes data, applying K-Means and TSP algorithms.
- **Database**: Stores destinations with details like latitude, longitude, number of visitors, and culture.
- **Recommendations**: Returns the optimized itinerary to the user.

### Execution Flow
1. The user inputs the country and number of travel days.
2. The system queries the country's tourist destinations from the SQLite database.
3. The K-Means algorithm clusters destinations by proximity.
4. For each cluster, the TSP algorithm optimizes the daily visit order.
5. The recommended itinerary is returned to the user.

## Prerequisites
- .NET Framework or .NET Core 8.0.
- SQLite installed or configured as a dependency.
- Visual Studio or another compatible IDE for development.

## How to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/guiTavares13/itinerary_algorithm.git

2. Open the poc_recommended_trip.sln file in Visual Studio.
3. Restore dependencies (if using NuGet packages).
4. Build and run the project.
5. Use the interface to input a country and number of days.

## Directory Structure
- Controllers/: Contains the API controllers.
- Models/: Data models (e.g., tourist destinations).
- Service/: Business logic and algorithms.
- Dao/: Database access layer.
- MachineLearning/: Implementation of K-Means and TSP.
- Form1.*: Frontend interface code.

## Usage Example
**Input:** Country = "Brazil", Days = 5.
**Output:** Itinerary with clusters of destinations (e.g., Rio de Janeiro, São Paulo) and optimized daily routes.

## Limitations
The database must be pre-populated with destination data.
Initial support limited to countries with available data.

## Contributions
Feel free to open issues or submit pull requests with improvements!

## License
This project is licensed under the MIT License (to be defined).

