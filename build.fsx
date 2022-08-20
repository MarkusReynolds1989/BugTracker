open System
open System.Diagnostics

// Download the mysql database on Windows, for test env or local use.
let sqlInstallProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "choco install mysql",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardInput = true,
        Verb = "runas",
        RedirectStandardError = true
    )

let sqlInstallProcess =
    Process.Start(sqlInstallProcessInfo)
printf "Starting MySql Server Check..."
sqlInstallProcess.StandardInput.WriteLine("a")
sqlInstallProcess.WaitForExit()
printf "Finished MySql Server..."

// Download the mysql database on Ubuntu, for production servers.
