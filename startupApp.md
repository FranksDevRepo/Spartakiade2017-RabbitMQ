# Startup-App

Im Workshop <i>Microservices mit .NET und RabbitMQ (Advanced)</i> wird Docker zur Bereitstellung der Umgebungen verwendet.
Um die Zeit effektiv nutzen zu können, testet bitte eure Dockerumgebung mit der Startup-App vorher.

1) Git-Repo <a href="https://github.com/fpommerening/Spartakiade2017-RabbitMQ.git">Spartakiade2017-RabbitMQ</a> klonen 

2) Docker-Console öffnen und

  a) Docker for Windows: Powershell starten

  b) Docker-Toolbox: Eingabeaufforderung cmd starten und Verbindung herstellen (Siehe <a href="https://docs.docker.com/toolbox/toolbox_install_windows/#step-3-verify-your-installation" target"_blank">Docs</a>)

3) Im geklonten Repo in den Ordner dockerfiles/StartupApp wechseln

4) Anwendung per Docker-Compose aktualisieren / starten 

	docker-compose pull

<b> WICHTIG: im Zuge des Starts werden alle notwendigen Dockerimages für den Workshop heruntergeladen. Dieses umfasst etwa 2 GB und kann abhängig von der Internetgeschwindigkeit etwas Zeit in Anspruch nehmen.</b>
	
	docker-compose up

5) Webbrowser starten und die Seite <a href ="http://localhost:18317">http://localhost:18317</a> öffnen. 
Wenn alles korrekt gestartet ist, siehst du folgende Seite.
<img src="images/startupApp.PNG" alt="Screenshot Startup-App"/>

6) Anwendung mit CTRL + C beenden

7) Container bereinigen 

	docker-compose rm
	
Hinweis: die Images bleiben auf dem System erhalten
