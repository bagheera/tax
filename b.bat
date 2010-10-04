@echo off
if not exist .\logs md logs
@echo on

.\Utils\Nant\bin\nant -buildfile:.\tax.build %* -logfile:b.log


