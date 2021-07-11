# Bug Tracker
An open source project.

To build:
1. Compile the project.
2. Take the files and put them into var/www on your server.
3. Create a daemon through System or Systemctl to keep the website service running on locat host.
4. Set up your database for local host and use the scripts to create the tables.
5. Write the logic in nginx to point to the bug tracker web app.
6. Rewrite the logic in the controllers to point to your database.



