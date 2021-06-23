# Record-Importer
## Work in Progress (Old Project from 2018)
This application was designed to move files on and off of mobile devices running Windows Ce and Android.
The main Ui of the application was developed using WPF (Windows Presentation Foundation)
It also makes use of the MVVM design
It makes use of WPD (Windows Portale Devices Framework).
It uses a custom DLL that was decompiled and put back together to fix a bug in the original Microsoft supplied DLL
It uses file streams to stream files to and from devices.
It can hande multiple devices as well.
Currently it requires NuGet packages to function on a custom nuget server.
I'm looking into removing this dependency

## Build Requirements
Build PortableDevices project first (Requires a manual rebuild for some reason. Does not build when you rebuild solution)
Then build solution

## Does not currently compile without NuGet packages on a custom server
## This will be fixed going forward
