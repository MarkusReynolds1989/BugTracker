# Bug Tracker
An open source project.

For Development:
1. Clone the repo.
2. CD into the repo on an elevated shell.
3. Run ./build. 
4. If there are any problems, wait for SQL to download and install and then run again.
5. The password should be admin and the username should be root for development purposes.
6. The default admin user is admin with a password of test.

To build:
1. Compile the project.
2. Take the files and put them into var/www on your server.
3. Create a daemon through System or Systemctl to keep the website service running on locat host.
4. Set up your database for local host and use the scripts to create the tables.
5. Write the logic in nginx to point to the bug tracker web app.
6. Rewrite the logic in the controllers to point to your database.



