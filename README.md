# Windows Firewall Dashboard

[![Build status](https://img.shields.io/appveyor/ci/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://ci.appveyor.com/project/ErdnussFlipS/WindowsFirewallDashboard)
[![Coverage](https://img.shields.io/codecov/c/github/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](http://codecov.io/github/ErdnussFlipS/WindowsFirewallDashboard)
[![Issues](https://img.shields.io/github/issues/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/issues)
[![Release](https://img.shields.io/github/release/ErdnussFlipS/WindowsFirewallDashboard.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/releases/latest)
[![GPL License](https://img.shields.io/badge/license-GPL_v3-lightgrey.svg?style=flat-square)](https://github.com/ErdnussFlipS/WindowsFirewallDashboard/blob/nightly/LICENSE.md)

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