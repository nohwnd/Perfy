# Perfy

A vstest TestAdapter for measuring performance, that has almost no overhead. It does not do any actual discovery or execution. It creates results in place based on TEST_COUNT and returns them.

This shows how much overhead vstest has. 

## Usage

Have net7 SDK.
Be on Windows.

Run `.\Run.ps1` in PowerShell.

Test will run and you will see a summary: 

```
Step      Count Time    ExitCode
----      ----- ----    --------
Discovery     1 1383 ms        0
Discovery   100 1400 ms        0
Discovery  1000 1454 ms        0
Discovery 10000 1831 ms        0
Run           1 1403 ms        0
Run         100 1434 ms        0
Run        1000 1716 ms        0
Run       10000 2434 ms        0
```