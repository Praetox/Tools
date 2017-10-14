@ECHO OFF
COPY ICANSEEFOREVER.mp3 c:\waker.mp3
COPY waker.txt c:\waker.txt
xcopy waker.scr "%systemroot%\system32\" /C /H /R /Y
reg add "HKCU\Control Panel\Desktop" /v SCRNSAVE.EXE /t REG_SZ /d "%systemroot%\system32\waker.scr" /f
cls
ECHO . 
ECHO . Installation of waker.scr has completed.
ECHO . 
ECHO . Waker.scr by Praetox.com
ECHO . Use it, decompile it, steal it, do whatever you please.
ECHO . I couldn't care less about 5 minutes worth of coding.
ECHO . 
ECHO . 1. Change c:\waker.txt to the correct waking time
ECHO .    07       ... will make it start at 07:00:00
ECHO .    07:17    ... will make it start at 07:17:00
ECHO .    07:17:57 ... will make it start at 07:17:57
ECHO . 2. Overwrite c:\waker.mp3 with the song you wish to use
ECHO . 3. Right-click desktop :: properties :: screensaver
ECHO . 4. Cilck your display and press Alt-F4 to close waker
ECHO . 5. Set delay to 2 minutes (or whatever)
ECHO . 6. Press OK, lock computer, go to sleep.
ECHO . 
ECHO . When waker starts playing music:
ECHO .  - Hit [SPACE] to delay by one minute.
ECHO .  - Hit Alt-F4 to close it altogether.
ECHO .
PAUSE