# Zadanie

Projekt można uruchomić używając projektu Zadanie.Web i komendy dotnet run w cmd

Przetestować działanie rozwiązania można w uruchomionym projekcie pod adresem:
https://localhost:60843/swagger
Jako przykład można użyć 
userId: User1
cardNumber: Card11

wyjście będzie mniej więcej takie:
{
  "userId": "User1",
  "cardNumber": "Card11",
  "allowedActions": [
    2,
    3,
    6,
    7,
    8,
    9,
    11,
    12
  ],
  "actionsCount": 8,
  "requestedAt": "2025-09-01T15:45:04.2083441Z"
}

Dostępne są również testy.
