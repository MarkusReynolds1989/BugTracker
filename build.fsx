#r "nuget: MySql.Data, 8.0.30"
#r "nuget: Dapper, 2.0.123"

open System
open System.IO
open System.Diagnostics
open MySql.Data.MySqlClient
open Dapper

let ticketTableScript =
    @"
create table if not exists ticket
(
    TicketId        int auto_increment primary key,
    WorkerId        int                     not null,
    Title           varchar(45)             not null,
    Description     varchar(300)            not null,
    Resolution      varchar(300) default '' null,
    StatusIndicator int          default 0  not null,
    ActiveIndicator tinyint(1)   default 1  not null,
    LoggerId        int                     null
);"

let userTableScript =
    @"
create table if not exists user
(
    UserId              int auto_increment
        primary key,
    UserName            varchar(45)          not null,
    FirstName           varchar(45)          not null,
    LastName            varchar(45)          not null,
    Password            varchar(100)         not null,
    Email               varchar(45)          not null,
    ActiveIndicator     tinyint(1) default 1 not null,
    AuthenticationLevel int        default 0 null,
    constraint UserName
        unique (UserName)
);"

let mutable mysqlInstalled = false
let mutable buildDb = false

try
    let connectionString =
        "Server=127.0.0.1;Port=3306;Database=bugtracker;Uid=root;Pwd=admin"

    let connection =
        new MySqlConnection(connectionString)

    connection.Open()
with
| :? MySqlException -> buildDb <- true


let sqlCountProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "(ps | findstr mysql).count",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        Verb = "runas"
    )

let mysqlProcessCount =
    Process.Start(sqlCountProcessInfo)

mysqlProcessCount.WaitForExit()

let count =
    int (mysqlProcessCount.StandardOutput.ReadToEnd())

if count > 0 then mysqlInstalled <- true

// Download the mysql database on Windows, for test env or local use.
// Must run build from an elevated shell (admin).
let sqlDownloadProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments =
            "invoke-webrequest -URI https://dev.mysql.com/get/Downloads/MySQLInstaller/mysql-installer-community-8.0.30.0.msi -OutFile ./mysql.msi",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        Verb = "runas",
        RedirectStandardError = true
    )

let sqlInstallProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "msiexec.exe /I mysql.msi",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        Verb = "runas",
        RedirectStandardError = true
    )

printfn "Starting MySql Server Check..."


if not mysqlInstalled then
    printfn "MySql Not found, downloading as mysql.msi...\n\n\n\n"

    let sqlDownloadProcess =
        Process.Start(sqlDownloadProcessInfo)

    sqlDownloadProcess.WaitForExit()

    printfn "Mysql Downloaded."

    printfn "Installing mysql ..."

    let sqlInstallProcess =
        Process.Start(sqlInstallProcessInfo)

    sqlInstallProcess.WaitForExit()

    File.Delete("mysql.msi")

printfn "Finished MySql Server Check..."

// Build DB If doesn't exist.
// Check db first here.
if buildDb then
    printfn "Building database..."

    let connectionString =
        "Server=127.0.0.1;Port=3306;Uid=root;Pwd=admin"

    let connection =
        new MySqlConnection(connectionString)

    connection.Open()

    connection.Execute("create database bugtracker;")
    |> ignore

    connection.Execute("use bugtracker;") |> ignore
    connection.Execute(userTableScript) |> ignore
    connection.Execute(ticketTableScript) |> ignore
    
    let password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
    connection.Execute(
        $@"insert into user (UserName, FirstName, LastName, Password, Email, AuthenticationLevel) values ('admin', 'ad', 'min', '{password}', 'test@testing.com', 2)"
    )
    |> ignore

    printfn "Finished building database."

printfn "Cleaning solution..."

Process
    .Start("powershell.exe", "cd src; dotnet clean")
    .WaitForExit()

printfn "Solution clean."

printfn "Building solution..."

Process
    .Start("powershell.exe", "cd src; dotnet build")
    .WaitForExit()

printfn "Solution built."

printfn "Running tests..."

Process
    .Start("powershell.exe", "cd src; dotnet test")
    .WaitForExit()

printfn "Testing complete."
