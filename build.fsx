open System
open System.IO
open System.Diagnostics

let mutable mysqlInstalled = false
let sqlCountProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "(ps | findstr mysql).count",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        Verb = "runas")
    
let mysqlProcessCount = Process.Start(sqlCountProcessInfo)
mysqlProcessCount.WaitForExit()

let count = int (mysqlProcessCount.StandardOutput.ReadToEnd())
if count > 0 then mysqlInstalled <- true

// Download the mysql database on Windows, for test env or local use.
// Must run build from an elevated shell (admin).
let sqlDownloadProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "invoke-webrequest -URI https://dev.mysql.com/get/Downloads/MySQLInstaller/mysql-installer-community-8.0.30.0.msi -OutFile ./mysql.msi",
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
        RedirectStandardError = true)
    
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

printfn "Building solution..."
Process.Start("powershell.exe", "cd src; dotnet build")

printfn "Solution built."