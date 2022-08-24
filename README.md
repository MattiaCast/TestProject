# TestProject

Appena scaricato il progetto, aprirlo tramite Visual Studio Code ed effettuare il Restore (oppure shift + ctrl + p > .NET: Generate Assets for Build and Debug).
Se si fa partire il progetto, si aprira' una pagina del browser con lo swagger del progetto.

istruzioni per far partire tramite docker:

Andare nella cartella Test, aprire un terminale ed eseguire i due comandi di seguito:
- docker build -t testproject .
- docker run -p 5000:80 testproject

in seguito, aprire http://localhost:5000 in una pagine del browser per interrogare il servizio tramite Swagger
