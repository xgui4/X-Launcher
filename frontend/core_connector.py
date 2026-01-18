import requests
import json

API_URL = "http://localhost:5277/x_launcher.core.service" 

def connect_to_server(): 
    try:
        response = requests.get(API_URL)

        # Check if the request was successful (status code 200-299)
        response.raise_for_status()

        # Parse the response data, typically JSON
        data = response.json()
        print("Data received from ASP.NET server:")
        print(json.dumps(data, indent=4))

    except requests.exceptions.RequestException as e:
        # Handle any errors during the request
        print(f"Error connecting to the server: {e}")

connect_to_server() 