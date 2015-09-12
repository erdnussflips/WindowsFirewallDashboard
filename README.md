# Windows Firewall Dashboard

[![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard) [![Release](https://img.shields.io/github/release/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/releases/latest) [![Issues](https://img.shields.io/github/issues/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/issues)

Branch	| Status
--------|--------
master 	| [![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard/master.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard)
dev		| [![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard/dev.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard)

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
