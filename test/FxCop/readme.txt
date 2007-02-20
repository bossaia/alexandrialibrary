This is the FxCop project for all of the Alexandria assemblies:

Alexandria.dll
AlexandriaClient.exe
AlexandriaDb4o.dll
AlexandriaFmod.dll
AlexandriaMusicBrainz.dll
AlexandriaTagLib.dll

It uses relative paths so if you make changes to source and want to see them reflected
in the FxCop project make sure to build the Alexandria.Client solution since all of the
assemblies for the FxCop project are located in AlexandriaOrg\Alexandria.Client\bin\Debug\

Please copy CustomDictionary.xml into the directoy where FxCop is installed (probably
"C:\Program Files\Microsoft FxCop 1.35\") otherwise you will get spelling errors for
names like Ogg, Flac and MusicBrainz.  It is recommended that you always overwrite your
CustomDictionary.xml with the latest copy of this file before running any FxCop tests.
Likewise feel free to add to this dictionary but please overwrite this copy of the file
(AlexandriaOrg\FxCop\CustomDictionary.xml) with your local changes so that everyone else
has access to the latest dictionary as well. 

If you have any questions please email me:
dan.poage@gmail.com