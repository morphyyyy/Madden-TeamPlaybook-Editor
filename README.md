# Madden-20-Team-Playbook-Editor

App is located in Madden20PlaybookEditor\Madden20PlaybookEditor\bin\Release

*For PlayART, add this folder https://drive.google.com/drive/folders/1VPKSmNjg-AUnSepsEn3u8q7soEvzTiWm?usp=sharing to the same folder as the exe.

**All files need to live in the same folder to work correctly.
*** Works with multiple instances running.

## v2.4.0 - Added option to load roster for player numbers File>Load Roster.

## v2.3.4 - Functions > PSAL Editor now converts line/route drawings to code 8 values.  The list is copied to the clipboard for now.

## v2.3.3 - Added context menu (right-click) for Formations, Sub-Formations, Plays in PlayArt as well as players and routes in FieldArt.  Added drag and drop in FieldArt, although it doesn't update/save to the database and sticks until reloading the playbook ¯\\_(ツ)_/¯

## v2.3.2 - Removed Animated Field View.  Added Defensive Playart.  Added a delete play exception for plays used in multiple sub-formations.

## v2.3.1 - Animated Field View.

## v2.3 - Playart now draws organically from ARTL.  Field View added.

## v2.2.4 - Fix for copy/paste different PSAL with same ID.

## v2.2.3 - Re-Tooled audible generation.  PBAI sorts by prct and flags now update.

## v2.2.2 - Added checks for existing Formations and Sub-Formations when pasting

## v2.2.1 - Added more PLYT checks to Generate Audibles Function

X = 101, 102, ,103, 159, 4
Y = 195, 151, 169, 196, 197, 205, 207
LB = 101, 102, ,103, 159, 4
RB = 4, 101, 102, ,103, 159

## v2.2 - Bug Fixes

ARTL was not serialized and was crashing on copy/paste sometimes

## v2.1.9 - Added Generate Audibles function

Quickly fill all Sub-Formations with proper audibles where they exist. Quick Pass, HB Dive, Deep Pass and PA

## v2.1.8 - Disabled PBAI when pasting from CPB

## v2.1.7 - Fix for some 21 team db with missing PSALs and ARTL

## v2.1.6 - Reoriented Plays vertically left to right

## v2.1.5 - Drag and Drop Reordering

Re-Order Formations, Sub-Formations and Plays with Drag and Drop

## v2.1.4 - Copy/Paste entire Formations, Sub-Formations and Plays from CPB to TPB

Pasting entire Formations from CPBE can be slow, like 30 minutes for all shotgun.  Let it go.

## v2.1.3 - Bug Fixes with Copy/Paste from CPB to TPB

## v2.1.2 - Bug Fixes Pasting Sub-Formations and Plays

## v2.0.1 - Fix for 7 on 7 (FCF)

Fixed a crash when pasting plays with empty or Null PSALs

## v2.0 - Compatibility with Custom Playbook Editor

Supports Copy/Paste from Custom Playbook Editor

https://github.com/morphyyyy/M20PlaybookEditor

## v1.4 - Delete, Copy, Paste is fully functional

## v1.3.1 - Bug fixes

Deleted tables save properly

Delete Sub-Formation works, except for Hail Mary and mixed Sub-Formations

Delete Formation disabled (buggy) - 2 tables

## v1.3 - Delete SubFormations and Formations

Select a SubFormation/Formation and press delete

Removes SubFormation/Formation and all relevant data

**also some support for other game playbooks (Street)

## v1.2 - Delete Play

Select a play and press delete

Removes the play and all it's psals, plpd, plrd and any other data not used in any other plays

NO UNDO!

## v1.1 - Copy/Paste Sub Formations and Formations

Copy Formation/Sub Formation: Select Formation/Sub Formation, CTRL+C or Options>Copy

Insert Formation: Select Formation, CTRL+V or Options>Paste to insert one before selected Formation if Formation doesn't exist, or combine with existing Formation.

Insert Sub Formation in Formation: Select Sub Formation in Formation, CTRL+V or Options>Paste to insert one before selected Sub Formation.

Append Sub Formation to Formation: Select Formation, CTRL+V or Options>Paste to append Sub Formation to end of list.

## v1.0 - Copy/Paste Plays

Copy Play: Select Play, CTRL+C or Options>Copy

Insert Play in Sub Formation: Select Play in Sub Formation, CTRL+V or Options>Paste to insert one before selected Play.

Append Play to Sub Formation: Select Sub Formation, CTRL+V or Options>Paste to append Play to end of list.

source (because push just will not work at all?) - https://drive.google.com/drive/folders/1gSP57cfnIl8Li6Cbiw1i14YFKnCs4x-G?usp=sharing
