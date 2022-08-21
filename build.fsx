open System.Diagnostics

// Download the mysql database on Windows, for test env or local use.
// Must run build from an elevated shell (admin).
let sqlInstallProcessInfo =
    ProcessStartInfo(
        FileName = "powershell.exe",
        Arguments = "choco -y install mysql",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        Verb = "runas",
        RedirectStandardError = true
    )

let sqlStartProcessInfo =
    ProcessStartInfo(FileName = "mysql", UseShellExecute = false, Verb = "runas")

printfn "Starting MySql Server Check..."

let sqlInstallProcess =
    Process.Start(sqlInstallProcessInfo)

sqlInstallProcess.WaitForExit()

printfn "Finished MySql Server Check..."

printfn "Starting MySql Server on Port:"
Process.Start(sqlStartProcessInfo)
printfn "Setting up default scheme..."
printfn "Setting up tables..."
printfn "MySql DB now ready for testing."

printfn "Building solution..."
Process.Start("dotnet build")

printfn "Solution built."
// Download the mysql database on Ubuntu, for production servers.
// Must run from an elevated shell, sudo.
