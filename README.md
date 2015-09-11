# Windows Firewall Dashboard

Project status: [![Build status](https://ci.appveyor.com/api/projects/status/o6ii86n4mn153vom?svg=true)](https://ci.appveyor.com/project/ErdnussFlipS/windowsfirewalldashboard)

Branch	| Status
--------|--------
master 	| [![Build status](https://ci.appveyor.com/api/projects/status/o6ii86n4mn153vom/branch/master?svg=true)](https://ci.appveyor.com/project/ErdnussFlipS/windowsfirewalldashboard)
dev		| [![Build status](https://ci.appveyor.com/api/projects/status/o6ii86n4mn153vom/branch/dev?svg=true)](https://ci.appveyor.com/project/ErdnussFlipS/windowsfirewalldashboard)

## Description
The Windows Firewall Dashboard is an alternative admanistration panel for the Windows Firewall with Notifications for both directions.

It should be easier to configure the Windows Firewall for normal users.

## Project Structure
This Application is separated into GUI-Client "Windows Firewall Dashboard" and .NET-API "Windows Advanced Firewall API" to access the windows advanced firewall. The API is as git-subtree into this repository integrated.

The .NET-API can be found at [ErdnussFlipS/WindowsAdvancedFirewallApi](https://github.com/ErdnussFlipS/WindowsAdvancedFirewallApi).

## Stuff
- Ereignisanzeige -> Anwendungs- und Dienstprotokolle -> Microsoft -> Windows -> Windows Firewall with Advanced Security -> Firewall
- Ereignisse:
-- Ereignis-ID: 2004 - Eine Regel wurde der Ausnahmeliste der Windows-Firewall hinzugefügt.
-- Ereignis-ID: 2005 - Eine Regel in der Ausnahmeliste der Windows-Firewall wurde geändert.
-- Ereignis-ID: 2006 - Eine Regel in der Ausnahmeliste der Windows-Firewall wurde gelöscht.
