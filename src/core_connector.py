import json

import requests
from requests.exceptions import RequestException
from requests.models import Response

API_URL = "http://localhost:5277/x_launcher.core.service"


def connect_to_server() -> str:
    try:
        response: Response = requests.get(url=API_URL)

        response.raise_for_status()

        data: str = response.json()
        return json.dumps(obj=data, indent=4)

    except RequestException as err:
        return f"Error connecting to the server: {err}"
