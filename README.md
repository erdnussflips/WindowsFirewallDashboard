# Windows Firewall Dashboard

[![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard)
[![Coverage](https://img.shields.io/codecov/c/github/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](http://codecov.io/github/ErdnussFlipS/WindowsFirewallDashboard)
[![Issues](https://img.shields.io/github/issues/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/issues)
[![Release](https://img.shields.io/github/release/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/releases/latest)
[![GNU GPL v3 license](https://img.shields.io/github/license/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/blob/nightly/LICENSE.md)
[![POEditor](https://img.shields.io/badge/POEditor-Help_Translate-blue.svg?style=flat-square)](https://poeditor.com/join/project/z0rLf12hEZ)

Branch  | Status
--------|--------
master  | [![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard/master.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard) [![Coverage](https://img.shields.io/codecov/c/github/ErdnussFlipS/WindowsFirewallDashboard/master.svg?style=flat-square)](http://codecov.io/github/ErdnussFlipS/WindowsFirewallDashboard?branch=master)
dev     | [![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard/dev.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard) [![Coverage](https://img.shields.io/codecov/c/github/ErdnussFlipS/WindowsFirewallDashboard/dev.svg?style=flat-square)](http://codecov.io/github/ErdnussFlipS/WindowsFirewallDashboard?branch=dev)
nightly | [![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard/nightly.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard) [![Coverage](https://img.shields.io/codecov/c/github/ErdnussFlipS/WindowsFirewallDashboard/nightly.svg?style=flat-square)](http://codecov.io/github/ErdnussFlipS/WindowsFirewallDashboard?branch=nightly)

## Description
The Windows Firewall Dashboard is an alternative administration panel for the Windows Firewall with Notifications for both directions.

It should be easier to configure the Windows Firewall for normal users.

Get first impressions [here](Documentation/First_impressions.md).

## Project Structure
This Application is separated into GUI-Client "Windows Firewall Dashboard" and .NET-API "Windows Advanced Firewall API" to access the windows advanced firewall. The API is as git-subtree into this repository integrated.

The .NET-API can be found at [ErdnussFlipS/WindowsAdvancedFirewallApi](https://github.com/ErdnussFlipS/WindowsAdvancedFirewallApi).

## Ideas and later features
- Automated updates through github releases
- Improvement of firewalls filter rules through (e.g. for games) second github repo

## Useful links
- [Understanding MVC, MVP and MVVM Design Patterns](http://www.dotnet-tricks.com/Tutorial/designpatterns/2FMM060314-Understanding-MVC,-MVP-and-MVVM-Design-Patterns.html)
- [Modern User Interfaces with WPF MVVM, XAML Templates and Entity Framework 6](http://www.codeproject.com/Articles/897441/Modern-User-Interfaces-with-WPF-MVVM-XAML-Template)
- [MVVM Tutorial](http://www.cocktailsandcode.de/?s=mvvm)
- [Wiring up View and Viewmodel in MVVM and Silverlight 4 – Blendability included](http://blog.roboblob.com/2010/01/17/wiring-up-view-and-viewmodel-in-mvvm-and-silverlight-4-blendability-included/)

## Stuff
- Ereignisanzeige -> Anwendungs- und Dienstprotokolle -> Microsoft -> Windows -> Windows Firewall with Advanced Security -> Firewall
- Ereignisse:
  - Ereignis Übersicht: https://technet.microsoft.com/en-us/library/dd364427(v=ws.10).aspx
  - Ereignis-ID: 2002 - Eine Windows Firewall Einstellung wurde geändert. (Allgemeine Änderung)
  - Ereignis-ID: 2003 - Eine Windows Firewall Einstellung im Profil %s wurde geändert. (Profiländerung)
  - Ereignis-ID: 2004 - Eine Regel wurde der Ausnahmeliste der Windows-Firewall hinzugefügt.
  - Ereignis-ID: 2005 - Eine Regel in der Ausnahmeliste der Windows-Firewall wurde geändert.
  - Ereignis-ID: 2006 - Eine Regel in der Ausnahmeliste der Windows-Firewall wurde gelöscht.
  - ...
  - Ereignis-ID: 2033 - Eine Regel in der Windows Firewall Konfiguration auf diesem Computer wurde gelöscht.